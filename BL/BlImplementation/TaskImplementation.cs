﻿
namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    private readonly Bl _bl;
    internal TaskImplementation(Bl bl) => _bl = bl;

    /// <summary>
    /// function for creating a new task in the DL layer 
    /// </summary>
    /// <param name="item">the BL layer task objet from the user</param>
    /// <returns></returns>
    /// <exception cref="BO.BlStatusNotFit">if project started the user cant add a new task</exception>
    /// <exception cref="BO.BlAlreadyExistsException">catch the exception from the Dal </exception>
    /// <exception cref="BO.BlValidationError">exception in case the data from the engineer wasnt correct</exception>
    public int Create(BO.Task item)
    {
        if (s_bl.isProjectStarted())
            throw new BO.BlStatusNotFit("you already started the project adding a new task is forbidden");
        //creating a new task only if the data is correct
        if ((item?.Alias != "" || item?.Alias == null))
        {
            try
            {
                //creating a new DO task
                DO.Task doTask = new DO.Task(0, item?.Engineer?.Id, null, item?.StartDate, item?.DeadlineDate, item?.CompleteDate, item?.ScheduledDate, item?.RequiredEffortTime, item?.Deliverables, item?.Remarks, (DO.EngineerExperience?)item?.ComplexityLevel, item?.Description, item?.Alias, s_bl.Clock);

                //create the task and saving in the Dal
                int idTsk = _dal.Task.Create(doTask);
                //in casr the task depends on other tasks, create the suit dependency
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
                throw new BO.BlAlreadyExistsException(ex.Message);
            }
        }
        else
            throw new BO.BlValidationError("this task cant be created check the id and alias");
    }
    /// <summary>
    /// finction foe deleting a task from the DAL layer 
    /// </summary>
    /// <param name="id">id of the deleted task</param>
    /// <exception cref="BO.BlStatusNotFit"></exception>
    /// <exception cref="BO.BlDeletionImpossible"></exception>
    public void Delete(int id)
    {
        if (s_bl.isProjectStarted())
            throw new BO.BlStatusNotFit("you already started the project adding a new task is forbidden");
        try
        {
            // handles the deletion in the DAL
            _dal.Task.Delete(id);
        }
        catch (DO.DalDeletionImpossible ex)
        {
            throw new BO.BlDeletionImpossible(ex.Message);
        }
    }
    /// <summary>
    /// internal function foe ceating a status according to the situation
    /// </summary>
    /// <param name="isEngExist">parameter for checking if there is an engineer in this task</param>
    /// <returns>the index of the suit status</returns>
    internal int CreateStatus(bool isEngExist, DateTime? compDate)
    {
        //if the project didnt start yet the status is 0-unschduled
        if (!s_bl.isProjectStarted())
            return 0;
        //if the project started and there is no engineer return 1-schduled
        if (!isEngExist)
            return 1;
        if (compDate != null)
            return 3;
        //if the project started and there is a responsible engineer return 2-OnTrack
        return 2;
    }
    /// <summary>
    /// internal function for receiving the dependencies by reading the from the Dal Dependency class
    /// </summary>
    /// <param name="id">the id of the task i need to read</param>
    /// <returns></returns>
    /// <exception cref="BO.BlDoesNotExistException">throws if the dal couldent find dependencies for the task</exception>
    internal IEnumerable<BO.TaskInList> DepCreate(int id)
    {
        IEnumerable<BO.TaskInList> depList = _dal.Dependency.ReadAll(dep => dep.DependenTask == id).Select(dep =>
        {
            int dependsOnTaskId = dep?.DependensOnTask ?? throw new BO.BlDoesNotExistException("No task dependents found");
            DO.Task? depTaskInfo = _dal.Task.Read(dependsOnTaskId);
            //creating a new task in list for the ienumerable item 
            return new BO.TaskInList
            {
                Id = dependsOnTaskId,
                Alias = depTaskInfo?.Alias ?? "",
                Description = depTaskInfo?.Description ?? "",
                Status = (Status?)CreateStatus(depTaskInfo!.EngineerId != null, depTaskInfo.CompleteDate)
            };
        }).ToList();
        return depList;

    }
    /// <summary>
    /// the milestone functions are not in use according to the instructions 
    /// </summary>
    /// <param name="id">task id</param>
    /// <returns>a  new mileston in task item</returns>
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
    /// <summary>
    /// internal helping function foe creacing an engineer in task item for reading the BO task
    /// </summary>
    /// <param name="id">the current task id</param>
    /// <returns>the suit engineer who working on the task</returns>
    /// <exception cref="BO.BlDoesNotExistException">throw the Dal exception if it couldent find the engineer</exception>
    internal BO.EngineerInTask CreateEngineerInTask(int id)
    {
        try
        {
            //reading the engineer from the Dal layer
            DO.Engineer? doeng = _dal.Engineer.Read(id);
            return new BO.EngineerInTask
            {
                Id = doeng!.Id,
                Name = doeng.Name
            };
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message);
        }


    }
    /// <summary>
    /// function to check for circular dependency
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    private bool ImpossibleDependency(BO.Task task)
    {
        Queue<BO.TaskInList> taskQueue = new Queue<BO.TaskInList>(task.Dependencies!);
        while (taskQueue.Count != 0)
        {
            var currentTask = taskQueue.Dequeue();
            if (currentTask.Id == task.Id)
                return false;

            // Check if Dependencies is not null before iterating over it
            if (Read(currentTask.Id)?.Dependencies != null)
            {
                foreach (var tsk in Read(currentTask.Id).Dependencies)
                {
                    taskQueue.Enqueue(tsk);
                }
            }
        }
        return true;
    }
    /// <summary>
    ///Read fuction gets id of the task and create the BO task item by collecing data from the Dal layer
    /// </summary>
    /// <param name="id">id of the task to read</param>
    /// <returns>a new BO task item</returns>
    /// <exception cref="BO.BlDoesNotExistException">throws the messege if the dal couldent find the task </exception>
    public BO.Task? Read(int id)
    {
        //getting the task from the dal
        DO.Task? dotsk = _dal.Task.Read(id);
        if (dotsk == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        //creating a new BO task item
        return new BO.Task()
        {
            Id = id,
            Alias = dotsk.Alias,
            Description = dotsk.Description,
            CreateAtDate = dotsk.CreateAtDate,
            //calling the internal functions for getting the data
            Status = (Status?)CreateStatus(dotsk.EngineerId != null, dotsk.CompleteDate),
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

    /// <summary>
    /// update function for an existing task 
    /// </summary>
    /// <param name="item">the new task item to update</param>
    /// <exception cref="BO.BlDoesNotExistException">in case the received engineer is not exist</exception>
    /// <exception cref="BO.BlValidationError">in case it couldent find the dependent task</exception>
    /// <exception cref="BO.BlStatusNotFit">in case the engineer status is too low for taking the task</exception>
    /// <exception cref="BO.BltaskHasEngineer">in case the task had already taken by other engineer</exception>
    /// <exception cref="BO.BlEngineerHasTask">in case the engineer took a different task befor and didnt finsh it yet</exception>
    public void Update(BO.Task item)
    {
        try
        {
            if (!ImpossibleDependency(item))
                throw new BO.BlValidationError("there is a circular dependency the dependencies are impossible");
            if (item.Engineer != null)
            {
                DO.Engineer? engToAdd = _dal.Engineer.Read(item.Engineer.Id);
                if (engToAdd == null)
                    throw new BO.BlDoesNotExistException($"engineer with id {item.Engineer.Id} is not exist");
                //creat a new BO task of the dependent task
                BO.Task? dependTsk = Read(item.Id);
                if (dependTsk == null)
                    throw new BO.BlValidationError($"task with id:{item.Id} is not exist");
                //Checks that the task is not in an uninitialized state or has already ended
                //if (dependTsk.Status == (Status)0|| (dependTsk.Status == (Status)2&& dependTsk.Engineer != item.Engineer)|| (dependTsk.Status==(Status)3&&dependTsk.Engineer!=item.Engineer))
                //    throw new BO.BlStatusNotFit($"task with id:{item.Id} cannt be taken because the status is wrong");
                //Checks if all the tasks it depends on have been completed
                if (dependTsk.Dependencies != null)
                    foreach (var depOnTsk in dependTsk.Dependencies)
                    {
                        if (depOnTsk.Status != (Status)3 && depOnTsk.Status != (Status)0)
                            throw new BO.BlStatusNotFit($"the dependent task: {depOnTsk.Alias} is not done yet");
                    }
               
                //Checks that no engineer has taken the task before
                if (dependTsk.Engineer != null && dependTsk.Engineer.Id != item.Engineer.Id)
                    throw new BO.BltaskHasEngineer($"the task: {item.Id} had already taken by other engineer");

                //Checks whether the engineer has no previous assignment
                DO.Task? previousTsk = _dal.Task.Read(tsk => tsk.EngineerId == item.Id);
                if (previousTsk != null)
                    if (Read(previousTsk.Id)!.Status != (Status)3)
                        throw new BO.BlEngineerHasTask("this engineer has already took another task");

                //Checking the engineer level is not empty and higher than the task level
                if ((engToAdd.Level == null || item.ComplexityLevel == null) || (int)engToAdd.Level! < (int)item.ComplexityLevel!)
                {
                    throw new BO.BlValidationError("the engineer cant take this task becase his level is to low");
                }
            }
            //sending to update in the Dal layer
            DO.Task doTask = new DO.Task(item.Id, item.Engineer?.Id, null, item.StartDate, item.DeadlineDate, item.CompleteDate, item.ScheduledDate, item.RequiredEffortTime, item.Deliverables, item.Remarks, (DO.EngineerExperience?)item.ComplexityLevel, item.Description, item.Alias, s_bl.Clock);
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message);
        }
    }
    /// <summary>
    /// function for reading all of the task by the receiving filter condition
    /// </summary>
    /// <param name="filter">a condition for filtering the tasks</param>
    /// <returns></returns>
    /// <exception cref="BO.BlDoesNotExistException">if the dal couldnt find the data</exception>
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        try
        {
            //receiving DO IEnumerable of all the Dal tasks
            var listFromDl = _dal.Task.ReadAll();
            try
            {
                //converting the dl IEnumerable to be a BO items by calling the read BO function 
                var listFromBl = listFromDl.Select(tsk => Read(tsk!.Id));

                if (filter == null)
                {
                    return listFromBl!;
                }
                else
                {
                    //filterg by the receivd filter conditin
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
    /// <summary>
    /// function for reading all of the tasks that depends on the accepted task id
    /// </summary>
    /// <param name="id">id of the task to read</param>
    /// <returns></returns>
    public IEnumerable<BO.Task> ReadAllDependentsTasks(int id)
    {
        IEnumerable<BO.Task> dependTasks = _dal.Dependency.ReadAll(dep => dep.DependensOnTask == id).Select(dep => Read(dep.DependenTask))!;
        return dependTasks;
    }

    /// <summary>
    /// function for filtering and getting all of the not taken tasks
    /// </summary>
    /// <returns>all tasks in TaskInEngineer format</returns>
    public IEnumerable<TaskInEngineer> GetAvailableTasks(BO.EngineerExperience exp)
    {
        return ReadAll(tsk => tsk.Engineer == null&&tsk.ComplexityLevel<=exp).Select(tsk => new TaskInEngineer { Id = tsk.Id, Alias = tsk.Alias }).ToList();
    }


    /// <summary>
    /// Retrieves all available engineers based on the provided task ID and experience level.
    /// </summary>
    /// <param name="tskId">The ID of the task.</param>
    /// <param name="taskLenel">The experience level required for the task.</param>
    /// <returns>An IEnumerable of EngineerInTask objects representing available engineers.</returns>
    public IEnumerable<EngineerInTask> GetAllAvialbleEngineers(int tskId, BO.EngineerExperience? taskLenel)
    {
        // Create an object of EngineerImplementation class
        EngineerImplementation engImp = new EngineerImplementation();
        // Retrieve all available engineers based on task ID and experience level
        return engImp.ReadAll(eng => (int)eng.Level! >= (int)taskLenel! && (eng.Task == null || Read(eng.Task.Id)!.Status == (Status)3)).Select(eng =>
         {
             // Map engineer data to EngineerInTask model
             return new BO.EngineerInTask
             {
                 Id = eng.Id,
                 Name = eng.Name
             };
         }).ToList();
    }

    /// <summary>
    /// Retrieves all task dependency options.
    /// </summary>
    /// <returns>An IEnumerable of TaskInList objects representing all task dependency options.</returns>
    public IEnumerable<TaskInList> GetAllDependenciesOptions()
    {
        // Retrieve all task dependencies options
        IEnumerable<BO.TaskInList> depList = _dal.Task.ReadAll().Select(tsk =>
        {
            //creating a new task in list for the ienumerable item 
            return new BO.TaskInList
            {
                Id = tsk.Id,
                Alias = tsk?.Alias ?? "",
                Description = tsk?.Description ?? "",
                Status = (Status?)CreateStatus(tsk!.EngineerId != null, tsk.CompleteDate)
            };
        }).ToList();
        return depList;
    }


    /// <summary>
    /// Retrieves all tasks for Gantt chart.
    /// </summary>
    /// <returns>An IEnumerable of TasksForScheduale objects representing all tasks for the Gantt chart.</returns>
    public IEnumerable<TasksForScheduale> GetAllTaskForGantt()
    // Retrieve all tasks for Gantt chart
    {
        IEnumerable<BO.TasksForScheduale> allTsk = _dal.Task.ReadAll().Select(tsk =>
        {
            //creating a new task in list for the ienumerable item 
            return new BO.TasksForScheduale
            {
                Id = tsk.Id,
                Alias = tsk?.Alias ?? "",
                EngineerId = tsk?.EngineerId ?? 0,
                EngineerName = tsk?.EngineerId == null ? "no engineer" : _dal.Engineer.Read((int)tsk.EngineerId)?.Name ?? "",
                Dependencies = DepCreate(tsk.Id),
                TaskStaus = (Status)CreateStatus(tsk!.EngineerId != null, tsk.CompleteDate),
                StartDate = (DateTime)tsk.ScheduledDate!,
                EndDate = (DateTime)tsk.DeadlineDate!
            };
        }).ToList();
        return allTsk;
    }


    /// <summary>
    /// Retrieves available tasks for a given engineer ID.
    /// </summary>
    /// <param name="Id">The ID of the engineer.</param>
    /// <returns>An IEnumerable of TaskInEngineer objects representing available tasks for the specified engineer.</returns>
    public IEnumerable<TaskInEngineer> GetAvailableTasksForEngineer(int Id)
    {
        // Retrieve available tasks for a given engineer ID
        BO.EngineerExperience? engEx = s_bl.Engineer.Read(Id)?.Level!;
        return ReadAll(tsk => tsk.Engineer == null && tsk.ComplexityLevel! <= engEx).Select(tsk => new TaskInEngineer { Id = tsk.Id, Alias = tsk.Alias }).ToList();
    }

    /// <summary>
    /// Retrieves all tasks for list with optional filtering.
    /// </summary>
    /// <param name="filter">Optional filter function to apply to the tasks.</param>
    /// <returns>An IEnumerable of TaskForlList objects representing tasks that match the filter criteria, if provided.</returns>
    public IEnumerable<TaskForlList> GetAllTasksForList(Func<BO.TaskForlList, bool>? filter = null)
    {

        IEnumerable<BO.TaskForlList> allTsk = _dal.Task.ReadAll().Select(tsk =>
        {
            
            //creating a new task for list for the ienumerable item 
            return new BO.TaskForlList
            {
                Id = tsk.Id,
                Alias = tsk?.Alias ?? "",
                Description=tsk.Description ??" ",
                Status = (Status)CreateStatus(tsk!.EngineerId != null, tsk.CompleteDate),
                ComplexityLevel = (BO.EngineerExperience?)tsk.ComplexityLevel,
                Dependencies=DepCreate(tsk.Id)
            };
        }).ToList();
        // Apply filter if provided
        if (filter==null)
               return allTsk;
        return allTsk.Where(filter!)!;
    }
}

