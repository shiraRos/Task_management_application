﻿
namespace BlImplementation;
using BlApi;
using BO;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task item)
    {
        if ((item?.Alias != "" || item?.Alias == null))
        {
            try
            {
                DO.Task doTask = new DO.Task(0, item?.Engineer?.Id, null, item?.StartDate, item?.DeadlineDate, item?.CompleteDate, item?.ScheduledDate, item?.RequiredEffortTime, item?.Deliverables, item?.Remarks, (DO.EngineerExperience?)item?.ComplexityLevel, item?.Description, item?.Alias);
                int idTsk = _dal.Task.Create(doTask);
                if (item?.Dependencies != null)
                {
                    foreach (var dep in item.Dependencies)
                    {
                        DO.Dependency newDep = new DO.Dependency(0, idTsk, dep.Id);
                        _dal.Dependency.Create(newDep);
                    }
                }
                return idTsk;
            }
            catch (DO.DalAlreadyExistsException ex)
            {
                throw new BO.BlAlreadyExistsException($"Engineer with ID={item?.Id} already exists", ex);
            }
        }
        else
            throw new BO.BlValidationError("this task cant be created check the id and alias");
    }

    public void Delete(int id)
    {
        try
        {
            // Assuming _dal.Engineer.Delete handles the deletion in your Data Access Layer
            _dal.Task.Delete(id);
        }
        catch (DO.DalDeletionImpossible ex)
        {
            throw new BO.BlDeletionImpossible($"Task with ID={id} cant be deleted", ex);
        }
    }
    internal int CreateStatuse()
    {
        MileStoneImplementation milestoneImpl = new MileStoneImplementation();
        return milestoneImpl.ReadAll() != null ? 1 : 0;
    }
    internal IEnumerable<BO.TaskInList> DepCreate(int id)
    {
        IEnumerable<BO.TaskInList> depList = _dal.Dependency.ReadAll(dep => dep.DependenTask == id).Select(dep =>
        {
            int dependsOnTaskId = dep?.DependensOnTask ?? throw new BO.BlDoesNotExistException("No task dependents found");
            DO.Task? depTaskInfo = _dal.Task.Read(dependsOnTaskId);
            return new BO.TaskInList
            {
                Id = dependsOnTaskId,
                Alias = depTaskInfo?.Alias ?? "",
                Description = depTaskInfo?.Description ?? "",
                Status = (Status?)0
            };
        }).ToList();
        return depList;

    }
    internal MilestoneInTask createMileStone(int id)
    {
        int milestoneDep = _dal.Dependency.Read(dep => dep.DependenTask == id)!.DependensOnTask;
        string? als = _dal.Task.Read(tsk => tsk.Id == milestoneDep)?.Alias;
        return new MilestoneInTask
        {
            Id = milestoneDep,
            Alias = als
        };
    }
    internal BO.EngineerInTask CreateEngineerInTask(int id)
    {
        try
        {
            DO.Engineer? doeng = _dal.Engineer.Read(id);
            return new BO.EngineerInTask
            {
                Id = doeng!.Id,
                Name = doeng.Name
            };
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"cannot find the engineer", ex);
        }


    }
    public BO.Task? Read(int id)
    {
        DO.Task? dotsk = _dal.Task.Read(id);
        if (dotsk == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        int stat = CreateStatuse();
        return new BO.Task()
        {
            Id = id,
            Alias = dotsk.Alias,
            Description = dotsk.Description,
            CreateAtDate = dotsk.CreateAtDate,
            //Status = (BO.Status)stat,
            Status = (Status?)0,
            Dependencies = DepCreate(id),
            Milestone = null,
            RequiredEffortTime = dotsk.RequiredEffortTime,
            StartDate = dotsk.StartDate,
            ScheduledDate = dotsk.ScheduledDate,
            ForecastDate = dotsk.ScheduledDate + dotsk.RequiredEffortTime,
            DeadlineDate = dotsk.DeadlineDate,
            CompleteDate = dotsk.CompleteDate,
            Deliverables = dotsk.Deliverables,
            Remarks = dotsk.Remarks,
            Engineer = dotsk.EngineerId != null ? CreateEngineerInTask((int)dotsk.EngineerId) : null,
            ComplexityLevel = (BO.EngineerExperience?)dotsk.ComplexityLevel
        };
    }


    public void Update(BO.Task item)
    {
        try
        {
            if (item.Engineer != null)
            {
                DO.Engineer? engToAdd = _dal.Engineer.Read(item.Engineer.Id);
                if (engToAdd == null)
                    throw new BO.BlDoesNotExistException($"engineer with id {item.Engineer.Id} is not exist");
                //יוצר משתנה מסוג המשימה התלויה
                BO.Task? dependTsk = Read(item.Id);
                if (dependTsk == null)
                    throw new BO.BlValidationError($"task with id:{item.Id} is not exist");
                //..בודק שהמשימה לא במצב לא מאותחל או הסתיימה כבר
                if (dependTsk.Status == (Status)0 || dependTsk.Status == (Status)3)
                    throw new BO.BlStatusNotFit($"task with id:{item.Id} cannt be taken");
                //בודק אם כל המשימות שהיא תלויה בהן הושלמו
                if (dependTsk.Dependencies != null)
                    foreach (var depOnTsk in dependTsk.Dependencies)
                    {
                        if (depOnTsk.Status != (Status)3)
                            throw new BO.BlStatusNotFit($"the dependent task: {depOnTsk.Alias} is not done yet");
                    }
                //בודק שאף מהנדס לא לקח את המשימה קודם
                if (dependTsk.Engineer != null && dependTsk.Engineer.Id != item.Engineer.Id)
                    throw new BO.BltaskHasEngineer($"the task: {item.Id} had already taken by other engineer");

                //בודק האם אין למהנדס משימה קודמת
                DO.Task? previousTsk = _dal.Task.Read(tsk => tsk.EngineerId == item.Id);
                if (previousTsk != null)
                    if (Read(previousTsk.Id)!.Status != (Status)4)
                        throw new BO.BlEngineerHasTask("this engineer has already took another task");

                //בדיקה רמת המהנדס לא ריקה וגבוהה מרמת המשימה
                if ((engToAdd.Level == null || item.ComplexityLevel == null) && (int)engToAdd.Level! < (int)item.ComplexityLevel!)
                {
                    throw new BO.BlValidationError("the engineer cant take this task becase his level is to low");
                }
            }
            //שולח לעדכון בשכבת הDL
            DO.Task doTask = new DO.Task(item.Id, item.Engineer?.Id, null, item.StartDate, item.DeadlineDate, item.CompleteDate, item.ScheduledDate, item.RequiredEffortTime, item.Deliverables, item.Remarks, (DO.EngineerExperience?)item.ComplexityLevel, item.Description, item.Alias);
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={item.Id} does not exist exists", ex);
        }
    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        try
        {
            var listFromDl = _dal.Task.ReadAll();
            try
            {
                var listFromBl = listFromDl.Select(tsk => Read(tsk!.Id));

                if (filter == null)
                {
                    return listFromBl!;
                }
                else
                {
                    return listFromBl.Where(filter!)!;
                }
            }
            catch (BO.BlDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException(ex.Message);
            }
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message);
        }
    }

    //public IEnumerable<Task> ReadAllDependentsTasks(int id)
    //{
    //    BO.Task myTask = Read(id)!;
    //    TaskInList myTaskInListTask = new BO.TaskInList
    //    {
    //        Id = id,
    //        Alias = myTask.Alias,
    //        Description = myTask.Description,
    //        Status = myTask.Status
    //    };
    //    //return ReadAll(tsk=>tsk.Dependencies!=null&&tsk.Dependencies.Contains(myTaskInListTask));
    //    return ReadAll(tsk => tsk.Dependencies != null && tsk.Dependencies.Contains(myTaskInListTask)) ?? Enumerable.Empty<Task>();
    //}
    public IEnumerable<BO.Task> ReadAllDependentsTasks(int id)
    {
        IEnumerable<BO.Task> dependTasks = _dal.Dependency.ReadAll(dep => dep.DependensOnTask == id).Select(dep => Read(dep.DependenTask))!;
        return dependTasks;
    }
}

