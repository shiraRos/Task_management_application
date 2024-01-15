
namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using System.Net.NetworkInformation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task item)
    {
        throw new NotImplementedException();
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

    public Task? Read(int id)
    {
        DO.Task? dotsk = _dal.Task.Read(id);
        if (dotsk == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        int stat=_dal.Dependency.ReadAll()!=null?1 : 0;
        return new BO.Task()
        {
            Id = id,
            Alias =dotsk.Alias,
            Description=dotsk.Description,
            CreateAtDate=dotsk.CreateAtDate,
            status = (BO.Status?)stat,
            Dependencies=dotsk.????
            Milestone=dotsk????,
            RequiredEffortTime=dotsk.RequiredEffortTime,
            StartDate=dotsk.StartDate,
            ScheduledDate=dotsk.ScheduledDate,
            ForecastDate=dotsk.???,
            DeadlineDate = dotsk.DeadlineDate,
            CompleteDate=dotsk.CompleteDate,
            Deliverables=dotsk.Deliverables,
            Remarks=dotsk.Remarks,
            Engineer = dotsk.EngineerId!=null?(EngineerInTask)(_dal.Engineer.Read(dotsk.EngineerId)):null,
            ComplexityLevel= (BO.EngineerExperience?)dotsk.ComplexityLevel
        };
    }

    public Task? ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
