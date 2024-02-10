namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// implementation of the Engineer entity
/// </summary>
internal class TaskImplementation : ITask
{
    // function for create an item of Task object
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task ts = new Task(newId,item.EngineerId,item.IsMileston,item.StartDate,item.DeadlineDate,item.CompleteDate,item.ScheduledDate,item.RequiredEffortTime,item.Deliverables,item.Remarks,item.ComplexityLevel,item.Description,item.Alias);
        DataSource.Tasks.Add(ts);
        return newId;
    }
    /// <summary>
    /// function for delete an item of Task object
    /// </summary>
    /// <param name="id">the id of the deleted item</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    /// <exception cref="DalDeletionImpossible"></exception>
    public void Delete(int id)
    {
        //if the id of task  not exists -no need for delete
        if (!(DataSource.Tasks.Any(tsk => tsk.Id == id)))
            throw new DalDoesNotExistException($"Task with ID={id} does Not exist");
        //if the id of task exists in the dependency-cannot be deleted
        //if (DataSource.Dependencies.Exists(idTs=> idTs.DependensOnTask == id ))
        //    throw new DalDeletionImpossible($"There is a task that depends on a task with ID={id},so you can not delete it");
        foreach (var x in DataSource.Tasks)
        {
            if (id == x.Id)
            {
                DataSource.Tasks.Remove(x);
             
            }
        }
    }
    
    /// <summary>
    /// function for get an item of task by checking the id
    /// </summary>
    /// <param name="id">id of the returns item</param>
    /// <returns>the item of thw accepted id</returns>
    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(tsk => tsk.Id == id);
    }


    /// <summary>
    /// function to read a single object by a condition
    /// </summary>
    /// <param name="filter">a condition for getting an item</param>
    /// <returns>the item of the accepted id</returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public Task? Read(Func<Task, bool> filter) // stage 2
    {
        return DataSource.Tasks.FirstOrDefault(filter);
    }

    /// function for reading all of the objects in the list
    /// <param name="filter"> the condition what to read</param>
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else if (DataSource.Tasks == null)
            throw new DalDoesNotExistException("this list is not exist");
        return DataSource.Tasks.Where(filter);
    }
    
    /// <summary>
    /// function for update details of Task
    /// </summary>
    /// <param name="item">the fix item to repalce and update</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Task item)
    {
        //if id doesnt exist-no need for updating
        if (!(DataSource.Tasks.Any(tsk => tsk.Id == item.Id)))
            throw new DalDoesNotExistException($"Task with ID={item.Id} does Not exist");
        foreach (var x in DataSource.Tasks)
        {
            if (item.Id == x.Id)
            {
                //first- remove the existing item
                DataSource.Tasks.Remove(x);
                //second- adding the new item
                DataSource.Tasks.Add(item);
            }
        }
    }
    
    
    /// <summary>
    /// function for reset all the list of Tasks
    /// </summary>
    public void Reset()
    {
        DO.Task[] arrtsk = DataSource.Tasks.ToArray();
        try
        {
            Delete(arrtsk[0].Id);
        }
        catch (DalDeletionImpossible e) { Console.WriteLine(e); }
        for (int i = 1; i < arrtsk.Length; i++)
        {
            try
            {
                Delete(arrtsk[i].Id);
            }
            catch (DalDeletionImpossible e) { Console.WriteLine(e); }
        }
    }
}
