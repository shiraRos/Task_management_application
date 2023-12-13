namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

// implementation of the Engineer entity
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
    // function for delete an item of Task object
    public void Delete(int id)
    {
        //if the id of task  not exists -no need for delete
        if (!(DataSource.Tasks.Any(tsk => tsk.Id == id)))
            throw new DalDoesNotExistException($"Task with ID={id} does Not exist");
        //if the id of task exists in the dependency-cannot be deleted
        if (DataSource.Dependencies.Exists(idTs=> idTs.DependensOnTask == id ))
            throw new DalDeletionImpossible($"There is a task that depends on a task with ID={id},so you can not delete it");
        foreach (var x in DataSource.Tasks)
        {
            if (id == x.Id)
            {
                DataSource.Tasks.Remove(x);
             
            }
        }
    }
    // function for get an item of task by checking the id
    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(tsk => tsk.Id == id);
    }


    //function to read a single object by a condition
    public Task? Read(Func<Task, bool> filter) // stage 2
    {
        return DataSource.Tasks.FirstOrDefault(filter) ?? throw new DalDoesNotExistException("No task found matching the specified condition.");
    }

    /// function for reading all of the objects in the list
    /// <param name="filter"> the condition what to read</param>
    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else if (DataSource.Tasks == null)
            throw new DalDoesNotExistException("this list is not exist");
        return DataSource.Tasks.Where(filter);
    }
    //function for update details of Task
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
    //function for reset all the list of Tasks
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
