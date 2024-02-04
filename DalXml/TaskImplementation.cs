

using DalApi;
using DO;
using System.Data.Common;

namespace Dal;

internal class TaskImplementation : ITask
{
    const string s_task = @"tasks";
    const string s_dependency = @"dependendys";

    /// <summary>
    /// the function gets a task and save it it the list in XMLSerializer way
    /// </summary>
    /// <param name="item">the task to add</param>
    /// <returns>the id of the new task</returns>
    public int Create(DO.Task item)
    {
        List<DO.Task> tsk = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task);
        int newId = Config.StartTaskId;
        DO.Task ts = new DO.Task(newId, item.EngineerId, item.IsMileston, item.StartDate, item.DeadlineDate, item.CompleteDate, item.ScheduledDate, item.RequiredEffortTime, item.Deliverables, item.Remarks, item.ComplexityLevel, item.Description, item.Alias);
        tsk.Add(ts);
        XMLTools.SaveListToXMLSerializer(tsk, s_task);
        return newId;
    }

    /// <summary>
    /// function for delete an item of task object
    /// </summary>
    /// <param name="id">code of the item to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    /// <exception cref="DalDeletionImpossible"></exception>
    public void Delete(int id)
    {
        List<Dependency> dep = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency);
        List<DO.Task> tsk = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task);
        //if the id of task  not exists -no need for delete
        if (!(tsk.Any(tsk => tsk.Id == id)))
            throw new DalDoesNotExistException($"Task with ID={id} does Not exist");
        //if the id of task exists in the dependency-cannot be deleted
        if (dep.Exists(idTs => idTs.DependensOnTask == id))
            throw new DalDeletionImpossible($"There is a task that depends on a task with ID={id},so you can not delete it");
        foreach (var x in tsk)
        {
            if (id == x.Id)
            {
                tsk.Remove(x);
                break;

            }
        }
        XMLTools.SaveListToXMLSerializer(tsk, s_task);
    }

    /// <summary>
    /// function for get an item of task by checking the id
    /// </summary>
    /// <param name="id">the id ot the task to read</param>
    /// <returns>the task with the accepted id</returns>
    public DO.Task? Read(int id)
    {
        List<DO.Task> tsk = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task);
        return tsk.FirstOrDefault(tsk => tsk.Id == id);
    }

    /// <summary>
    /// function to read a single task by a condition
    /// </summary> 
    /// <param name="filter">filter received from the user</param>
    /// <returns>The task we found after filtering</returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> tsk = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task);
        return tsk.FirstOrDefault(filter);
    }

    /// <summary>
    /// function for reading all of the objects in the list
    /// </summary>
    /// Returning all tasks according to a received filter 
    /// If no filter was received, the function will return all tasks
    /// </summary>
    /// <param name="filter">A condition received from the user is set to null by default</param>
    /// <returns>all the  tasks according to the filter</returns>
    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null) 
    {
        List<DO.Task> tsk = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task);
        if (filter == null)
            return tsk.Select(item => item);
        else if (tsk == null)
            throw new DalDoesNotExistException("this list is not exist");
        return tsk.Where(filter);
    }

    /// <summary>
    /// function for reset all the list of tasks
    /// </summary>
    public void Reset()
    {
        List<DO.Task> tsk = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task);
        DO.Task[] arrtsk = tsk.ToArray();
        for (int i = 0; i < arrtsk.Length; i++)
        {
            try
            {
                Delete(arrtsk[i].Id);
            }
            catch (DalDeletionImpossible e) { Console.WriteLine(e); }
        }
    }

    /// <summary>
    /// function for update details of task
    /// </summary>
    /// <param name="item">the updated task to replase</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(DO.Task item)
    {
        List<DO.Task> tsk = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task);
        //if id doesnt exist-no need for updating
        if (!(tsk.Any(tsk1 => tsk1.Id == item.Id)))
            throw new DalDoesNotExistException($"Task with ID={item.Id} does Not exist");
        tsk.RemoveAll(x => x.Id == item.Id);
        tsk.Add(item);
        XMLTools.SaveListToXMLSerializer(tsk, s_task);
    }
}
