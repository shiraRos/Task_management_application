
using BlApi;
using BlImplementation;
using BO;
using System.Data.Common;

namespace BlImplementation;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public IMileStone MileStone => new MileStoneImplementation();

    public ITask Task => new TaskImplementation(this);

    /// <summary>
    /// function for automatic schedule creating
    /// </summary>
    /// <exception cref="FormatException">in case the user typed a wrong data type</exception>
    public void createSchedule(DateTime? statDate)
    {
        DateTime?  maxTimeTask;
        TimeSpan defaultTime = new TimeSpan(24, 0, 0);
        Queue<BO.Task> tasksToCheck = new Queue<BO.Task>();
        BO.Task currentTask;
        //checking if the accepted start date is valid 
        if (statDate < Clock)
            throw new BO.BlValidationError("the start date must be later than the current time");
        //Console.WriteLine("insert project start date:");
        //while (statDate == null)
        //    if (DateTime.TryParse(Console.ReadLine(), out result))
        //    {
        //        statDate = result;
        //    }
        //    else
        //    {
        //        statDate = null;
        //        //Console.WriteLine("unvalid value insert start date again");
        //    }
        //Asks the user if they want to set a default amount of time elapsed for an uninitialized task
        //If not, the user will be prompted to enter a duration for each uninitialized task
        ////////Console.WriteLine("do you want to use default time span for every task insert Y/N");
        ////////string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        ////////if (ans == "Y")
        ////////{
        ////////    Console.WriteLine("insert default time span for the tasks");
        ////////    if (TimeSpan.TryParse(Console.ReadLine(), out defResult))
        ////////    {
        ////////        defaultTime = defResult;
        ////////    }
        ////////    else
        ////////    {
        ////////        defaultTime = TimeSpan.FromDays(1);
        ////////        Console.WriteLine("invalid value 1 day inserted as default time span");
        ////////    }

            ////////}
            //Initializing the tasks that do not depend on the nose task and entering a queue
        IEnumerable<BO.Task> getLevelTasks = Task.ReadAll(tsk => tsk.Dependencies == null || tsk.Dependencies.Count() == 0);
        //foreach (BO.Task task in getLevelTasks)
        //For each task sets the start time as the start time of the project, the duration by definition
        foreach (var item in getLevelTasks)
        {

            item.ScheduledDate = statDate;
            item.Status = (Status)1;
            if (item.RequiredEffortTime == null)
            {
                //if (defaultTime == null)
                //{
                //    Console.WriteLine($"insert required effort time for task {item.Id}: {item.Alias}");
                //    if (TimeSpan.TryParse(Console.ReadLine(), out defResult))
                //        defaultTime = defResult;
                //    else
                //    {
                        defaultTime = TimeSpan.FromDays(1);
                        //Console.WriteLine("invalid value 1 day inserted as default time span");
                    //}

               //}
                item.RequiredEffortTime = defaultTime;
            }
            item.ForecastDate = item.ScheduledDate + item.RequiredEffortTime;
            Task.Update(item);
        }
        maxTimeTask = getLevelTasks.Max(tsk => tsk.ForecastDate);
        //Determines the latest finish time of each for each task based on the task that will finish at the latest time
        foreach (var item in getLevelTasks)
        {
            {
                item.DeadlineDate = maxTimeTask;
                Task.Update(item);
                tasksToCheck.Enqueue(item);
            }

        }
        //Go through all the tasks until there are no tasks left that have tasks that depend on them
        while (tasksToCheck.Count > 0)
        {
            //Removing the current task from the queue
            currentTask = tasksToCheck.Dequeue();
            //Acceptance of the tasks that depend on the current task
            getLevelTasks = Task.ReadAllDependentsTasks(currentTask.Id);
            // //For each task sets the start time as the start time of the project, the duration by definition
            foreach (var item in getLevelTasks)
            {
                item.Status = (Status)1;
                item.ScheduledDate = currentTask.DeadlineDate;
                if (item.RequiredEffortTime == null)
                {
                    //if (defaultTime == null)
                    //{
                    //    Console.WriteLine($"insert required effort time for task {item.Id}: {item.Alias}");
                    //    if (TimeSpan.TryParse(Console.ReadLine(), out defResult))
                    //        defaultTime = defResult;
                    //    else
                        //{
                            defaultTime = TimeSpan.FromDays(1);
                            //Console.WriteLine("invalid value 1 day inserted as default time span");
                    //    }

                    //}
                    item.RequiredEffortTime = defaultTime;
                }
                item.ForecastDate = currentTask.DeadlineDate + defaultTime;
                Task.Update(item);
            }
            maxTimeTask = getLevelTasks.Max(tsk => tsk.ForecastDate);
            //Determines the latest finish time of each for each task based on the task that will finish at the latest time
            foreach (var item in getLevelTasks)
            {
                item.DeadlineDate = maxTimeTask;
                Task.Update(item);
                tasksToCheck.Enqueue(item);
            }
        }
        //saving the start date in the config 
        DalApi.Factory.Get.ProjectStartDateUpdate(statDate);
    }
    /// <summary>
    /// A function that returns true if the project has an initialized schedule
    /// </summary>
    public bool isProjectStarted()
    {
        return GetStartDate() != null;
    }
    /// <summary>
    /// A function that returns the start date of the project stored in the config
    /// </summary>
    /// <returns></returns>
    public DateTime? GetStartDate()
    {
        return DalApi.Factory.Get.ReturnTheStartDate();
    }


    /// <summary>
    /// function for delete every value from the data base
    /// </summary>
    public void Reset()
    {
        DalTest.Initialization.Reset();
    }

    public void InitializeDB()
    {
        DalTest.Initialization.Do();
    }


    #region Time Management

    /// <summary>
    /// Gets or sets the current time.
    /// </summary>
    /// <remarks>
    /// This property has a private setter to ensure that the time can only be updated through the provided methods.
    /// </remarks>
    private static DateTime s_Clock = DateTime.Now.Date;

    public DateTime Clock
    {
        get { return s_Clock; }
        private set { s_Clock = value; }
    }

    /// <summary>
    /// Advances the time by one hour.
    /// </summary>
    public void AdvanceTimeByHour()
    {
        Clock = Clock.AddHours(1);
    }

    /// <summary>
    /// Advances the time by one day.
    /// </summary>
    public void AdvanceTimeByDay()
    {
        Clock = Clock.AddDays(1);
    }

    /// <summary>
    /// Advances the time by one week.
    /// </summary>
    public void AdvanceTimeByYear()
    {
        Clock = Clock.AddYears(1);
    }

    /// <summary>
    /// Initializes the time to the current date and time.
    /// </summary>
    public void InitializeTime()
    {
        Clock = DateTime.Now;
    }

    #endregion
}
