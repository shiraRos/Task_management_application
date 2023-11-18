
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task ts = new Task(newId,item.EngineerId,item.IsMileston,item.StartDate,item.DeadlineDate,item.CompleteDate,item.ScheduledDate,item.RequiredEffortTime,item.Deliverables,item.Remarks,item.ComplexityLevel,item.Description,item.Alias);
        DataSource.Tasks.Add(ts);
        return newId;
    }

    public void Delete(int id)
    {
        if (!(DataSource.Tasks.Exists(tsk => tsk.Id == id)))
            throw new Exception($"Task with ID={id} does Not exist");
        if(DataSource.Dependencies.Exists(idTs=> idTs.DependensOnTask == id ))
            throw new Exception($"There is a task that depends on a task with ID={id},so you can delete it");
        foreach (var x in DataSource.Tasks)
        {
            if (id == x.Id)
            {
                DataSource.Tasks.Remove(x);
             
            }
        }
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(tsk => tsk.Id == id);
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        if (!(DataSource.Tasks.Exists(tsk => tsk.Id == item.Id)))
            throw new Exception($"Task with ID={item.Id} does Not exist");
        foreach (var x in DataSource.Tasks)
        {
            if (item.Id == x.Id)
            {
                DataSource.Tasks.Remove(x);
                DataSource.Tasks.Add(item);
            }
        }
    }
}
