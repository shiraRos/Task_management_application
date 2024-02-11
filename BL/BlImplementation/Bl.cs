
using BlApi;
using BO;
using DO;
using System.Data.Common;

namespace BlImplementation;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public IMileStone MileStone => new MileStoneImplementation();

    public ITask Task => new TaskImplementation();

    //internal DateTime UpdateLevelTasks(IEnumerable<BO.Task> getLevelTasks, DateTime currentStart, TimeSpan? defaultTime)
    //{
    //    TimeSpan defResult;
    //    foreach (var item in getLevelTasks)
    //    {
    //        item.ScheduledDate = currentStart;
    //        if (item.RequiredEffortTime == null)
    //        {
    //            if (defaultTime == null)
    //            {
    //                Console.WriteLine($"insert required effort time for task {item.Id}: {item.Alias}");
    //                if (TimeSpan.TryParse(Console.ReadLine(), out defResult))
    //                    defaultTime = defResult;
    //                else
    //                {
    //                    defaultTime = TimeSpan.FromDays(1);
    //                    Console.WriteLine("invalid value 1 day inserted as default time span");
    //                }

    //            }
    //            item.RequiredEffortTime = defaultTime;
    //        }
    //        item.ForecastDate = item.ScheduledDate + item.RequiredEffortTime;
    //        Task.Update(item);
    //    }
    //    DateTime maxTimeTask = (DateTime)getLevelTasks.Max(tsk => tsk.ForecastDate)!;
    //    foreach (var item in getLevelTasks)
    //    {
    //        item.DeadlineDate = maxTimeTask;
    //        Task.Update(item);
    //    }
    //    return maxTimeTask;
    //}

    public void createSchedule()
    {
        DateTime? statDate = null;
        DateTime result, maxTimeTask;
        TimeSpan? defaultTime = null;
        TimeSpan defResult;
        Queue<BO.Task>? tasksToCheck = new Queue<BO.Task>();
        BO.Task currentTask;
        Console.WriteLine("insert project start date:");
        while (statDate == null)
            if (DateTime.TryParse(Console.ReadLine(), out result))
            {
                statDate = result;
            }
            else
            {
                statDate = null;
                Console.WriteLine("unvalid value insert start date again");
            }
        Console.WriteLine("do you want to use default time span for every task insert Y/N");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
        {
            Console.WriteLine("insert default time span for the tasks");
            if (TimeSpan.TryParse(Console.ReadLine(), out defResult))
            {
                defaultTime = defResult;
            }
            else
            {
                defaultTime = TimeSpan.FromDays(1);
                Console.WriteLine("invalid value 1 day inserted as default time span");
            }

        }
        IEnumerable<BO.Task> getLevelTasks = Task.ReadAll(tsk => tsk.Dependencies == null);
        foreach (BO.Task task in getLevelTasks)
            tasksToCheck.Enqueue(task);
        foreach (var item in getLevelTasks)
        {
            item.ScheduledDate = statDate;
            item.Status = (Status)1;
            if (item.RequiredEffortTime == null)
            {
                if (defaultTime == null)
                {
                    Console.WriteLine($"insert required effort time for task {item.Id}: {item.Alias}");
                    if (TimeSpan.TryParse(Console.ReadLine(), out defResult))
                        defaultTime = defResult;
                    else
                    {
                        defaultTime = TimeSpan.FromDays(1);
                        Console.WriteLine("invalid value 1 day inserted as default time span");
                    }

                }
                item.RequiredEffortTime = defaultTime;
            }
            item.ForecastDate = item.ScheduledDate + item.RequiredEffortTime;
            Task.Update(item);
        }
        maxTimeTask = (DateTime)getLevelTasks.Max(tsk => tsk.ForecastDate)!;
        foreach (var item in getLevelTasks)
        {
            item.DeadlineDate = maxTimeTask;
            Task.Update(item);
        }
        while(tasksToCheck.Count > 0)
        {
            currentTask = tasksToCheck.Dequeue();
            //לכתוב פונקציה שתתין לי עבור תז של משימה את כל המשימות התלויות בה
            getLevelTasks = Task.ReadAllDependentsTasks(currentTask.Id);
            foreach (var item in getLevelTasks)
            {
                item.Status = (Status)1;
                item.ScheduledDate = currentTask.DeadlineDate;
                if (item.RequiredEffortTime == null)
                {
                    if (defaultTime == null)
                    {
                        Console.WriteLine($"insert required effort time for task {item.Id}: {item.Alias}");
                        if (TimeSpan.TryParse(Console.ReadLine(), out defResult))
                            defaultTime = defResult;
                        else
                        {
                            defaultTime = TimeSpan.FromDays(1);
                            Console.WriteLine("invalid value 1 day inserted as default time span");
                        }

                    }
                    item.RequiredEffortTime = defaultTime;
                }
                item.ForecastDate = item.ScheduledDate + item.RequiredEffortTime;
                Task.Update(item);
            }
            maxTimeTask = (DateTime)getLevelTasks.Max(tsk => tsk.ForecastDate)!;
            foreach (var item in getLevelTasks)
            {
                item.DeadlineDate = maxTimeTask;
                Task.Update(item);
                tasksToCheck.Enqueue(item);
            }
        }

    }

    public bool isProjectStarted()
    {
        return GetStartDate() != null;
    }

    public DateTime? GetStartDate()
    {
        return DalApi.Factory.Get.ReturnTheStartDate();
    }


    /// <summary>
    /// function for delete every value from the data base
    /// </summary>
    public void Reset()
    {
        DalApi.Factory.Get.Reset();
    }
    //ממוש תאריך התחלה
}
