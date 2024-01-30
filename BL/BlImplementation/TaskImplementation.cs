
namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task item)
    {
        if (item.Id < 0 || item.Alias == "")
            throw new BO.BlValidationError("this task cant be created check the id and alias");
        try
        {
            DO.Task doTask = new DO.Task(0, item.Engineer?.Id, null, item.StartDate, item.DeadlineDate, item.CompleteDate, item.ScheduledDate, item.RequiredEffortTime, item.Deliverables, item.Remarks, (DO.EngineerExperience?)item.ComplexityLevel, item.Description, item.Alias);
            int idTsk = _dal.Task.Create(doTask);
            if (item.Dependencies != null)
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
            throw new BO.BlAlreadyExistsException($"Engineer with ID={item.Id} already exists", ex);
        }
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
            var currentTask = _dal.Task.Read(task => task.Id == dep?.DependenTask);
            int dependsOnTaskId = dep?.DependensOnTask ?? throw new BO.BlDoesNotExistException("No task dependents found");
            return new BO.TaskInList
            {
                Id = dependsOnTaskId,
                Alias = currentTask?.Alias ?? "",
                Description = currentTask?.Description ?? "",
                Status=(Status?)0
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
            throw new BO.BlDoesNotExistException($"cannot find the engineer",ex);
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
            Status = (BO.Status)stat,
            Dependencies = stat == 1 ? null : DepCreate(id),
            Milestone = stat == 0 ? null : createMileStone(id),
            RequiredEffortTime = dotsk.RequiredEffortTime,
            StartDate = dotsk.StartDate,
            ScheduledDate = dotsk.ScheduledDate,
            ForecastDate = null,
            DeadlineDate = dotsk.DeadlineDate,
            CompleteDate = dotsk.CompleteDate,
            Deliverables = dotsk.Deliverables,
            Remarks = dotsk.Remarks,
            Engineer = dotsk.EngineerId != null ? CreateEngineerInTask((int)dotsk.EngineerId) : null,
            ComplexityLevel = (BO.EngineerExperience?)dotsk.ComplexityLevel
        };
    }

    public IEnumerable<BO.Task?> ReadAll()
    {
        var list = _dal.Task.ReadAll();
        if (list != null)
            return list.Select(tsk => Read(tsk!.Id));
        throw new BO.BlValidationError("list is null");
    }

    public void Update(BO.Task item)
    {
        DO.Task doTask = new DO.Task(0, item.Engineer?.Id, null, item.StartDate, item.DeadlineDate, item.CompleteDate, item.ScheduledDate, item.RequiredEffortTime, item.Deliverables, item.Remarks, (DO.EngineerExperience?)item.ComplexityLevel, item.Description, item.Alias);
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={item.Id} does not exist exists", ex);
        }
    }
}

