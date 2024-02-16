
namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;
using System.Data.Common;
using BlImplementation;
using System;


internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private BlApi.IBl s_bl = BlApi.Factory.Get();


    /// <summary>
    /// Creates a new instance of BO.Engineer and adds it to the data store through the DAL layer.
    /// </summary>
    /// <param name="item">The BO.Engineer object to be created.</param>
    /// <returns>The unique identifier (ID) assigned to the newly created Engineer.</returns>
    /// <exception cref="BO.BlAlreadyExistsException">Thrown when attempting to create an Engineer with an ID that already exists.</exception>
    /// <exception cref="BO.BlValidationError">Thrown when the input data for creating an Engineer is invalid.</exception>
    public int Create(BO.Engineer item)
    {
        //if the Scheduled projecr started already-throw exception
        if (s_bl.isProjectStarted())
            throw new BO.BlStatusNotFit("you already started the project adding a new task is forbidden");
        // Validate input data for creating an Engineer
        if (item.Id > 0 && (item.Name == null || item.Name != " ") && (item.Cost > 0 || item.Cost == null) && (item.Email == null || item.Email.Contains('@')))
        {
            DO.Engineer doEngineer = new DO.Engineer(item.Id, (DO.EngineerExperience?)item.Level, item.Cost, item.Name, item.Email);

            try
            {
                // Create the Engineer through the DAL layer
                int idEng = _dal.Engineer.Create(doEngineer);
                return idEng;
            }
            catch (DO.DalAlreadyExistsException ex)
            {
                // Exception handling for DAL layer AlreadyExistsException
                throw new BO.BlAlreadyExistsException(ex.Message);
            }
        }
        else
            throw new BO.BlValidationError($"one of the details you insert is un valid");

    }
    /// <summary>
    /// method for delete exist Bo.engineer 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Delete(int id)
    {
        try
        {
            // Assuming _dal.Engineer.Delete handles the deletion in your Data Access Layer
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException( ex.Message);
        }
    }

    /// <summary>
    /// Reads an engineer from the data store based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the engineer to read.</param>
    /// <returns>The Business Object (BO) representation of the engineer, or null if not found.</returns>
    /// <exception cref="BO.BlDoesNotExistException">Thrown when the engineer with the specified ID does not exist.</exception>
    public BO.Engineer? Read(int id)
    {
        // Attempt to read the engineer from the data access layer (DAL).
        DO.Engineer? doeng = _dal.Engineer.Read(id);
        // If the engineer is not found, throw an exception.
        if (doeng == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        // Attempt to read a task associated with the engineer.
        DO.Task? tsk = _dal.Task.Read(tsk => tsk.EngineerId == id);
        // Create and return a new Business Object (BO) representation of the engineer.
        return new BO.Engineer
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

    /// <summary>
    /// Method for retrieving all engineers with an optional filter.
    /// </summary>
    /// <param name="filter">An optional filter function to apply to the engineers.</param>
    /// <returns>Returns an IEnumerable of Engineer objects based on the provided filter.</returns>
    /// <exception cref="BO.BlDoesNotExistException">Thrown if a Business Logic exception occurs.</exception>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        try
        {
            // Gets all engineers from the data access layer.
            var listFromDl = _dal.Engineer.ReadAll();

            try
            {
                // Maps the engineers to bl layer objects.
                var listFromBl = listFromDl.Select(eng => Read(eng!.Id));

                // Apply the optional filter if provided.
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
                // Propagate Bl exceptions.
                throw new BO.BlDoesNotExistException(ex.Message);
            }
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message);
        }
    }

    /// <summary>
    /// Method for updating an engineer.
    /// </summary>
    /// <param name="item">The Engineer object containing updated information.</param>
    /// <exception cref="BO.BlValidationError">Thrown when there is a validation error in the input.</exception>
    /// <exception cref="BO.BlDoesNotExistException">Thrown when the specified Engineer does not exist.</exception>
    public void Update(BO.Engineer item)
    {
        // Creating a Data Object (DO) representing the Engineer with updated information
        DO.Engineer doEngineer = new DO.Engineer(item.Id, (DO.EngineerExperience?)item.Level, item.Cost, item.Name, item.Email);

        // Validating input parameters before attempting to update
        if (item.Id > 0 && (item.Name == null || item.Name != "") && (item.Cost == null || item.Cost > 0) && (item.Email == null || item.Email!.Contains('@')))
        {
            // Checking if the Engineer has an associated Task
            if (item.Task != null)
            {
                // Creating an instance of TaskImplementation to interact with Task-related functionality
                TaskImplementation tskImp = new TaskImplementation();

                // Reading the dependent Task to ensure it exists
                BO.Task? dependTsk = tskImp.Read(item.Task.Id);
                if (dependTsk == null)
                    throw new BO.BlValidationError($"Task with ID:{item.Task.Id} does not exist");

                // Updating the Engineer information in the associated Task
                dependTsk.Engineer = new BO.EngineerInTask
                {
                    Id = item.Id,
                    Name = item.Name
                };

                // Calling Update method of TaskImplementation to save changes to the Task
                tskImp.Update(dependTsk);
            }

            try
            {
                // Updating the Engineer in the data access layer (DAL)
                _dal.Engineer.Update(doEngineer);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                // Handling the exception if the specified Engineer does not exist in the DAL
                throw new BO.BlDoesNotExistException(ex.Message);
            }
        }
        else
        {
            // Throwing a validation error if input details are invalid
            throw new BO.BlValidationError($"One or more details you inserted are invalid");
        }
    }
}


