
namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;
using System.Data.Common;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer item)
    {
        DO.Engineer doEngineer = new DO.Engineer(item.Id, (DO.EngineerExperience?)item.Level, item.Cost, item.Name, item.Email);
        if (item.Id > 0 && (item.Name != "" || item.Name == null) && (item.Cost > 0 || item.Cost == null) && (item.Email == null || item.Email.Contains('@')))
        {

            try
            {
                int idEng = _dal.Engineer.Create(doEngineer);
                return idEng;
            }
            catch (DO.DalAlreadyExistsException ex)
            {
                throw new BO.BlAlreadyExistsException($"Engineer with ID={item.Id} already exists", ex);
            }
        }
        else
            throw new BO.BlValidationError($"one of the details you insert is un valid");

    }

    public void Delete(int id)
    {
        try
        {
            // Assuming _dal.Engineer.Delete handles the deletion in your Data Access Layer
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} not found", ex);
        }
    }


    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doeng = _dal.Engineer.Read(id);
        if (doeng == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        DO.Task? tsk = _dal.Task.Read(tsk => tsk.EngineerId == id);
        return new BO.Engineer()
        {
            Id = id,
            Name = doeng.Name,
            Email = doeng.Email,
            Level = (BO.EngineerExperience?)doeng.Level,
            Cost = doeng.Cost,
            Task = tsk == null ? null : new BO.TaskInEngineer
            {
                Id = tsk!.Id,
                Alias = tsk!.Alias
            }
        };

    }

    public IEnumerable<BO.Engineer?> ReadAll()
    {
        var list = _dal.Engineer.ReadAll();
        if (list != null)
            return list.Select(eng => Read(eng!.Id));
        throw new BO.BlValidationError("list is null");
    }

    public void Update(BO.Engineer item)
    {
        DO.Engineer doEngineer = new DO.Engineer(item.Id, (DO.EngineerExperience?)item.Level, item.Cost, item.Name, item.Email);
        if (item.Id > 0 && (item.Name==null|| item.Name != "" )&&(item.Cost==null|| item.Cost > 0) && (item.Email==null|| item.Email!.Contains('@')))
        {

            try
            {
                _dal.Engineer.Update(doEngineer);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Engineer with ID={item.Id} does not exists", ex);
            }
        }
        else
            throw new BO.BlValidationError($"one of the details you insert is un valid");
    }
}
