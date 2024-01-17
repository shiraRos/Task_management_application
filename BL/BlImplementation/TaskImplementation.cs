
namespace BlImplementation;
using BlApi;
using BO;
using DO;
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
    internal IEnumerable<BO.TaskInList>? DepCreate(int id)
    {
        IEnumerable<DO.Dependency>? depLIst = _dal.Dependency.ReadAll(dep => dep.DependenTask == id);
        IEnumerable<BO.TaskInList>? depsToTaskInList = null;
        foreach (var dep in depLIst)
        {
            // לוקח את המזהה של המשימה שהתקבלה ויוצר עצם משימה חדש על פי המזהה פקודת ריד...
            //על פי העצם משימה שהגיע יוצרים עצם של משימה ברשימה
            //מוסיפים את המשימה ברשימה לרשימת התלויות
        }
        return depsToTaskInList;
    }
    internal MilestoneInTask? createMileStone(int id)
    { 
        //צריך ליצור פונקציה לקריאת אבן דרך על פי מזהה
    }
    internal BO.EngineerInTask CreateEngineerInTask(int? id)
    {
       DO.Engineer? doeng= _dal.Engineer.Read(id);
        return new BO.EngineerInTask(doeng!.Id, doeng.Name);

    }
    public Task? Read(int id)
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
            status = (BO.Status)stat,
            Dependencies = stat == 1 ? null : DepCreate(id),
            Milestone = stat == 0 ? null : createMileStone(id),
            RequiredEffortTime = dotsk.RequiredEffortTime,
            StartDate = dotsk.StartDate,
            ScheduledDate = dotsk.ScheduledDate,
            ForecastDate = dotsk.???,
            DeadlineDate = dotsk.DeadlineDate,
            CompleteDate = dotsk.CompleteDate,
            Deliverables = dotsk.Deliverables,
            Remarks = dotsk.Remarks,
            Engineer = dotsk.EngineerId != null ? CreateEngineerInTask(dotsk.EngineerId) : null,
            ComplexityLevel = (BO.EngineerExperience?)dotsk.ComplexityLevel
        };
    }

    public BO.Task? ReadAll()
    {
        return _dal.Task.ReadAll().Select(item => Read(item.Id));
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
