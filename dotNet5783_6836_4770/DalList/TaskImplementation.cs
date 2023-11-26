namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

// implementation of the Engineer entity
public class TaskImplementation : ITask
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
        if (!(DataSource.Tasks.Exists(tsk => tsk.Id == id)))
            throw new Exception($"Task with ID={id} does Not exist");
        //if the id of task exists in the dependency-cannot be deleted
        if (DataSource.Dependencies.Exists(idTs=> idTs.DependensOnTask == id ))
            throw new Exception($"There is a task that depends on a task with ID={id},so you can not delete it");
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
        return DataSource.Tasks.Find(tsk => tsk.Id == id);
    }
    //function  for get all the list of Tasks items
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }
    //function for update details of Task
    public void Update(Task item)
    {
        //if id doesnt exist-no need for updating
        if (!(DataSource.Tasks.Exists(tsk => tsk.Id == item.Id)))
            throw new Exception($"Task with ID={item.Id} does Not exist");
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
        foreach (var item in DataSource.Tasks)
        {
            try
            {
                Delete(item.Id);
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}
