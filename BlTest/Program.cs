using BO;
using BlApi;
using DalTest;
using BlImplementation;

namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


    // static readonly IDal s_dal = Factory.Get; //stage 4

    //converting the level
    private static int? GetComplexityLevel()
    {
        if (int.TryParse(Console.ReadLine(), out int parsedComplexityLevel))
        {
            return parsedComplexityLevel; // Exit the loop and return the value if parsing is successful and within the range
        }
        else
        {
            return null;
        }
    }
    private static int? GetOptionalInt()
    {
        if (int.TryParse(Console.ReadLine(), out int parsedInt))
        {
            return parsedInt; // Exit the loop and return the value if parsing is successful and within the range
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// Convertig Dates
    /// </summary>
    /// <returns>the converted date</returns>
    private static DateTime? ParseDate()
    {
        DateTime result;

        if (DateTime.TryParse(Console.ReadLine(), out result))
        {
            return result;
        }
        else
        {
            Console.WriteLine("unvalid value accepted null as defult");
            return null; // Return null if parsing fails
        }
    }
    private static bool ParseMilestone()
    {
        if (bool.TryParse(Console.ReadLine(), out bool result))
        {
            return result;
        }
        else
        {
            return false; // Default value if parsing fails
        }

    }


    /// <summary>
    /// Creating a new task and adding it to the existing data
    /// </summary>
    private static void CreateTask()
    {
        if (s_bl.isProjectStarted())
            throw new BO.BlStatusNotFit("you already started the project adding a new task is forbidden");
        //Receipt of data by the user
        int? _complexityLevel, _dependOnTaskId;
        TimeSpan? _requiredEffortTime;
        string? _alias, _deliverables, _remarks, _description;
        List<TaskInList>? _dependencies = new List<TaskInList>();
        BO.Task? dependTsk = null;
        Console.WriteLine("insert requiredEffortTime");
        if (TimeSpan.TryParse(Console.ReadLine(), out TimeSpan result))
        {
            _requiredEffortTime = result;
        }
        else
        {
            _requiredEffortTime = null;
        }
        // Now 'requiredEffortTime' will be a TimeSpan value if parsing was successful, or null if it failed.
        Console.WriteLine("Required Effort Time: " + (_requiredEffortTime.HasValue ? _requiredEffortTime.ToString() : null));
        Console.WriteLine("insert deliverables");
        _deliverables = Console.ReadLine() ?? " ";
        Console.WriteLine("insert remarks");
        _remarks = Console.ReadLine() ?? " ";
        Console.WriteLine("insert complexity level between 0-4");
        _complexityLevel = GetComplexityLevel();
        while (_complexityLevel < 0 || _complexityLevel > 4)
        {
            Console.WriteLine("the number must be between 0-4 insert again!");
            _complexityLevel = GetComplexityLevel();
        }
        // Now 'complexityLevel' will be an integer between 0-4 based on user input.
        Console.WriteLine("Complexity Level: " + _complexityLevel);
        Console.WriteLine("insert description");
        _description = Console.ReadLine() ?? " ";
        Console.WriteLine("insert alias");
        _alias = Console.ReadLine();
        if (_alias == "")
            _alias = null;
        Console.WriteLine("insert the ids of the tasks you denends on:");
        _dependOnTaskId = GetOptionalInt();
        while (_dependOnTaskId != null)
        {
            dependTsk = s_bl.Task.Read((int)_dependOnTaskId);
            if (dependTsk != null)
            {
                _dependencies.Add(new TaskInList
                {
                    Id = (int)_dependOnTaskId,
                    Description = dependTsk.Description,
                    Alias = dependTsk.Alias,
                    Status = dependTsk.Status
                });
                Console.WriteLine("insert the ids of the tasks you denends on:");

                _dependOnTaskId = GetOptionalInt();
            }
            else
            {
                Console.WriteLine("the task you typed is not exist try again");
                Console.WriteLine("insert the ids of the tasks you denends on:");
                _dependOnTaskId = GetOptionalInt();
            }
        }
        //creating a new object
        BO.Task newTsk = new BO.Task { Id = 0, Alias = _alias, Description = _description, CreateAtDate = null, Status = (Status)0, Dependencies = _dependencies, Milestone = null, RequiredEffortTime = _requiredEffortTime, StartDate = null, ScheduledDate = null, ForecastDate = null, DeadlineDate = null, CompleteDate = null, Deliverables = _deliverables, Remarks = _remarks, Engineer = null, ComplexityLevel = (EngineerExperience?)_complexityLevel };
        //Add to data by calling an external operation
        try
        {

            int idnt = s_bl.Task.Create(newTsk);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Creating a new engineer and adding it to the existing data
    /// </summary>
    private static void CreateEngineer()
    {
        //Receipt of data by the user
        int _id;
        int? _level;
        double? _cost;
        string? _name, _email;
        Console.WriteLine("insert id");
        _id = int.Parse(Console.ReadLine()!);
        Console.WriteLine(" insert name");
        _name = Console.ReadLine();
        if (_name == "")
            _name = null;
        Console.WriteLine("insert complexity level between 0-4");
        _level = GetComplexityLevel();
        while (_level < 0 || _level > 4)
        {
            Console.WriteLine("the number must be between 0-4 insert again!");
            _level = GetComplexityLevel();
        }
        // Now 'complexityLevel' will be an integer between 0-4 based on user input.
        Console.WriteLine("Complexity Level: " + _level);
        Console.WriteLine("insert cost");
        if (double.TryParse(Console.ReadLine(), out double parsedCost))
        {
            _cost = parsedCost;
        }
        else
            _cost = null;
        // Now 'cost' will be a double value if parsing was successful, or null if it failed or the input was blank.
        Console.WriteLine("Cost: " + (_cost.HasValue ? _cost.ToString() : "null"));
        Console.WriteLine("insert email");
        _email = Console.ReadLine();
        if (_email == "")
            _email = null;
        //creating a new object
        BO.Engineer newEng = new BO.Engineer { Id = _id, Name = _name, Email = _email, Level = (_level != null ? (EngineerExperience)_level : null), Cost = _cost, Task = null };
        //Add to data by calling an external operation
        try
        {
            _id = s_bl.Engineer.Create(newEng);
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    /// <summary>
    /// Deleting a task from the existing data
    /// </summary>
    private static void DeleteTask()
    {
        Console.WriteLine("insert task code to remove");
        int taskId = int.Parse(Console.ReadLine()!);
        //delete from the data by calling an external operation
        s_bl.Task.Delete(taskId);
    }

    /// <summary>
    /// Deleting an engineer from the existing data
    /// </summary>
    private static void DeleteEngineer()
    {
        Console.WriteLine("insert engineer to remove");
        int engineerId = int.Parse(Console.ReadLine()!);
        //delete from the data by calling an external operation
        s_bl.Engineer.Delete(engineerId);
    }


    /// <summary>
    /// Reading a certain task from the existing data
    /// </summary>
    private static void ReadTask()
    {
        Console.WriteLine("insert task code to print");
        int taskId = int.Parse(Console.ReadLine()!);
        //print the data by calling an external operation
        Console.WriteLine(s_bl.Task.Read(taskId));
    }

    /// <summary>
    /// Reading a certain engineer from the existing data
    /// </summary>
    private static void ReadEngineer()
    {
        Console.WriteLine("insert engineer code to print");
        int engineerId = int.Parse(Console.ReadLine()!);
        //print the data by calling an external operation
        Console.WriteLine(s_bl.Engineer.Read(engineerId));
    }

    /// <summary>
    /// Reading all of the engineers from the data
    /// </summary>
    private static void ReadAllEngineers()
    {
        Console.WriteLine("the all engineers:");
        //getting all the engineers to a new item
        //List<Engineer> engList = s_dal!.Engineer.ReadAll();
        List<Engineer> engList = s_bl.Engineer.ReadAll()?.Where(e => e != null).Select(e => e!).ToList() ?? new List<Engineer>();
        //print evey item
        foreach (var item in engList)
        {
            Console.WriteLine(item);
        }
    }


    /// <summary>
    /// Reading all of the tasks from the data
    /// </summary>
    private static void ReadAllTasks()
    {
        Console.WriteLine("the all Tasks:");
        //getting all the tasks to a new item
        List<BO.Task> tskList = s_bl.Task.ReadAll().Where(t => t != null).Select(t => t!).ToList() ?? new List<BO.Task>();
        //print evey item
        foreach (var item in tskList)
        {
            Console.WriteLine(item);
        }
    }

    /// <summary>
    /// Updating information about a task that already exists in the system
    /// </summary>
    private static void UpdateTask()
    {
        //Receipt of data by the user
        int? _complexityLevel = null, _dependOnTaskId;
        int _taskId = 0;
        string? _alias, _deliverables, _remarks, _description;
        List<TaskInList>? _dependencies = new List<TaskInList>();
        BO.Task? dependTsk;
        Console.WriteLine("insert task id");
        _taskId = int.Parse(Console.ReadLine()!);
        BO.Task? previousTsk = s_bl.Task.Read(_taskId);
        if (previousTsk == null)
            throw new Exception("the task is not found");
        BO.EngineerInTask? _engInTask = previousTsk.Engineer;
        DateTime? _startDate=previousTsk.StartDate, _deadlineDate=previousTsk.DeadlineDate, _completeDate=previousTsk.CompleteDate, _scheduledDate=previousTsk.ScheduledDate;
        TimeSpan? _requiredEffortTime=previousTsk.RequiredEffortTime;
        //the user will be able to insert data only if the project started
        if (s_bl.isProjectStarted())
        {
            Console.WriteLine("insert engineer id");
            if (int.TryParse(Console.ReadLine(), out int parsedId))
            {
                BO.Engineer? fullEngData = s_bl.Engineer.Read(parsedId);
                if (fullEngData != null)
                {
                    _engInTask = new BO.EngineerInTask { Id = parsedId, Name = fullEngData.Name };
                }
                else
                {
                    Console.WriteLine("the id tou typed is not exist the engineer is null by defult");
                }
            }
            Console.WriteLine("insert start date");
            _startDate = ParseDate();
            if (_startDate == null||_startDate<previousTsk.ScheduledDate)
                _startDate = previousTsk.StartDate;
            Console.WriteLine("Start Date: " + (_startDate != null ? _startDate.Value.ToString("yyyy-MM-dd") : null));
            Console.WriteLine("insert deadline date");
            _deadlineDate = ParseDate();
            if (_deadlineDate == null||_deadlineDate>previousTsk.DeadlineDate)
                _deadlineDate = previousTsk.DeadlineDate;
            Console.WriteLine("DeadLine Date: " + (_deadlineDate != null ? _deadlineDate.Value.ToString("yyyy-MM-dd") : null));
            Console.WriteLine("insert complete date");
            _completeDate = ParseDate();
            if (_completeDate == null||_completeDate>previousTsk.ScheduledDate)
                _completeDate = previousTsk.CompleteDate;
            Console.WriteLine("Complete Date: " + (_completeDate != null ? _completeDate.Value.ToString("yyyy-MM-dd") : null));
            Console.WriteLine("insert scheduled date ");
            _scheduledDate = ParseDate();
            if (_scheduledDate == null||_scheduledDate<previousTsk.ScheduledDate)
                _scheduledDate = previousTsk.ScheduledDate;
            Console.WriteLine("Scheduled Date: " + (_scheduledDate != null ? _scheduledDate.Value.ToString("yyyy-MM-dd") : null));
        }
        //the user will be able to insert requored effort time only if the project didnt start yet 
        else
        {
            Console.WriteLine("insert requiredEffortTime");
            if (TimeSpan.TryParse(Console.ReadLine(), out TimeSpan result))
            {
                _requiredEffortTime = result;
            }
            else
            {
                _requiredEffortTime = previousTsk.RequiredEffortTime;
            }

            // Now 'requiredEffortTime' will be a TimeSpan value if parsing was successful, or null if it failed.
            Console.WriteLine("Required Effort Time: " + (_requiredEffortTime != null ? _requiredEffortTime.ToString() : null));
        }
        Console.WriteLine("insert deliverables");
        _deliverables = Console.ReadLine();
        if (_deliverables == "")
            _deliverables = previousTsk.Deliverables;
        Console.WriteLine("insert remarks");
        _remarks = Console.ReadLine();
        if (_remarks == "")
            _remarks = previousTsk.Remarks;
        Console.WriteLine("insert complexity level between 0-4");
        if (int.TryParse(Console.ReadLine(), out int parsedComplexityLevel))
        {
            if (parsedComplexityLevel >= 0 && parsedComplexityLevel <= 4)
                _complexityLevel = parsedComplexityLevel;
        }
        else
        {
            _complexityLevel = (int?)previousTsk.ComplexityLevel;
        }
        // Now 'complexityLevel' will be an integer between 0-4 based on user input.
        Console.WriteLine("Complexity Level: " + _complexityLevel);
        Console.WriteLine("insert description");
        _description = Console.ReadLine();
        if (_description == "")
            _description = previousTsk.Description;
        Console.WriteLine("insert alias");
        _alias = Console.ReadLine();
        if (_alias == "")
            _alias = previousTsk.Alias;
        Console.WriteLine("insert the ids of the tasks you denends on:");
        _dependOnTaskId = GetOptionalInt();
        while (_dependOnTaskId != null)
        {
            dependTsk = s_bl.Task.Read((int)_dependOnTaskId);
            if (dependTsk != null)
            {
                _dependencies.Add(new TaskInList
                {
                    Id = (int)_dependOnTaskId,
                    Description = dependTsk.Description,
                    Alias = dependTsk.Alias,
                    Status = dependTsk.Status
                });
                Console.WriteLine("insert the ids of the tasks you denends on:");
                _dependOnTaskId = GetOptionalInt();
            }
            else
            {
                Console.WriteLine("the task you typed is not exist try again");
                Console.WriteLine("insert the ids of the tasks you denends on:");
                _dependOnTaskId = GetOptionalInt();
            }
        }
        //creating a new object
        BO.Task newTsk = new BO.Task { Id = _taskId, Alias = _alias, Description = _description, CreateAtDate = null, Status = (Status)0, Dependencies = _dependencies, Milestone = null, RequiredEffortTime = _requiredEffortTime, StartDate = _startDate, ScheduledDate = _scheduledDate, ForecastDate = null, DeadlineDate = _deadlineDate, CompleteDate = _completeDate, Deliverables = _deliverables, Remarks = _remarks, Engineer = _engInTask, ComplexityLevel = (EngineerExperience?)_complexityLevel };
        //Update the data by calling an external operation
        try
        {
            s_bl.Task.Update(newTsk);
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    /// <summary>
    /// Updating information about an engineer that already exists in the system
    /// </summary>
    private static void UpdateEngineer()
    {
        //Receipt of data by the user
        int _id;
        int? _level = null;
        double? _cost;
        string? _name, _email;
        BO.Task? tskById = null;
        Console.WriteLine("insert id");
        _id = int.Parse(Console.ReadLine()!);
        BO.Engineer? previousEng = s_bl.Engineer.Read(_id);
        if (previousEng == null)
            throw new BO.BlDoesNotExistException($"engineer with id {_id} does not exist");
        TaskInEngineer? tskInEng = previousEng.Task;
        Console.WriteLine(" insert name");
        _name = Console.ReadLine();
        if (_name == "")
            _name = previousEng.Name;
        Console.WriteLine("insert complexity level between 0-4");
        if (int.TryParse(Console.ReadLine(), out int parsedComplexityLevel))
        {
            if (parsedComplexityLevel >= 0 && parsedComplexityLevel <= 4)
                _level = parsedComplexityLevel;
        }
        else
        {
            _level = (int?)previousEng.Level;
        }
        Console.WriteLine("insert cost");
        if (double.TryParse(Console.ReadLine(), out double parsedCost))
        {
            _cost = parsedCost;
        }
        else
            _cost = previousEng.Cost;
        // Now 'cost' will be a double value if parsing was successful, or null if it failed or the input was blank.
        Console.WriteLine("Cost: " + (_cost.HasValue ? _cost.ToString() : "null"));
        Console.WriteLine("insert email");
        _email = Console.ReadLine();
        if (_email == "")
            _email = previousEng.Email;
        if (s_bl.isProjectStarted())
        {
            Console.WriteLine("insert id of belong task:");
            int? tskId = GetOptionalInt();
            if (tskId != null)
            {
                tskById = s_bl.Task.Read((int)tskId);
                if (tskById != null)
                    tskInEng = new BO.TaskInEngineer { Id = (int)tskId, Alias = tskById.Alias };
                else
                    Console.WriteLine("this task is not exist the task is null by deflut");
            }
        }
        //creating a new object
        BO.Engineer newEng = new BO.Engineer { Id = _id, Name = _name, Email = _email, Level = (_level != null ? (EngineerExperience)_level : null), Cost = _cost, Task = tskInEng };
        //Update the data by calling an external operation
        try
        {
            s_bl.Engineer.Update(newEng);
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }


    /// <summary>
    /// delete all the engineers
    /// </summary>
    private static void ResetEngineer()
    {
        //Reste the data by calling an external operation
        DalApi.Factory.Get.Engineer.Reset();
    }

    /// <summary>
    /// delete all the tasks
    /// </summary>
    private static void ResetTask()
    {
        //Reste the data by calling an external operation
        DalApi.Factory.Get.Task.Reset();
    }
    /// <summary>
    /// A function for checking validiation of choice
    /// </summary>
    /// <param name="min">nimimum number to press</param>
    /// <param name="max">maximum number to press</param>
    /// <returns></returns>
    static int GetValidChoice(int min, int max)
    {
        int choice;
        do
        {

            if (int.TryParse(Console.ReadLine(), out choice) && choice >= min && choice <= max)
            {
                break; // Exit the loop if parsing is successful and within the range
            }

            Console.WriteLine($"Invalid input. Please enter a valid number between {min}-{max}");
        } while (true);

        return choice;
    }


    /// <summary>
    /// A menu for the user selection for the tasks
    /// </summary>
    private static void OptionsTaskManu()
    {
        try
        {
            Console.WriteLine("press 0 to exit\n press 1 create a new task\n press 2 to read task\npress 3 to read all tasks\npress 4 to update \npress 5 to delete\npress 6 to reset\n");
            int choice = GetValidChoice(0, 6);

            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        CreateTask();
                        break;
                    case 2:
                        ReadTask();
                        break;
                    case 3:
                        ReadAllTasks();
                        break;
                    case 4:
                        UpdateTask();
                        break;
                    case 5:
                        DeleteTask();
                        break;
                    case 6:
                        ResetTask();
                        break;
                }
                Console.WriteLine("press 0 to exit\n press 1 create a new task\n press 2 to read task\npress 3 to read all tasks\npress 4 to update \npress 5 to delete\npress 6 to reset\n");
                choice = GetValidChoice(0, 6);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }



    /// <summary>
    /// A menu for the user selection for the engineers
    /// </summary>
    private static void OptionsEngineerManu()
    {
        try
        {
            int choice;
            Console.WriteLine("press 0 to exit\n press 1 create a new Engineer\n press 2 to read Engineer\npress 3 to read all Engineers\npress 4 to update \npress 5 to delete\npress 6 to reset\n");
            choice = GetValidChoice(0, 6);
            while (choice != 0)
            {

                switch (choice)
                {
                    case 1:
                        CreateEngineer();
                        break;
                    case 2:
                        ReadEngineer();
                        break;
                    case 3:
                        ReadAllEngineers();
                        break;
                    case 4:
                        UpdateEngineer();
                        break;
                    case 5:
                        DeleteEngineer();
                        break;
                    case 6:
                        ResetEngineer();
                        break;
                }
                Console.WriteLine("press 0 to exit\n press 1 create a new Engineer\n press 2 to read Engineer\npress 3 to read all Engineers\npress 4 to update \npress 5 to delete\npress 6 to reset\n");
                choice = GetValidChoice(0, 6);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// resetting all of the data base 
    /// </summary>
    /// <exception cref="FormatException"></exception>
    private static void InitializationData()
    {
        Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
        if (ans == "Y")
        {
            s_bl.Reset();
            //Initialization.DO(s_dal);//stage 2
            //     DalTest.Initialization.Do(); //stage 4
        }
    }
    private static void CreateSche()
    {
        s_bl.createSchedule();
    }

    /// <summary>
    /// The main menu where the user can choose which entity to enter
    /// </summary>
    private static void MainManu()
    {
        Console.WriteLine("hi here is an options namu \npress 0 to exit\npress 1 to task\n press 2 to engineer\n press 3 to reset all Data\n press 4 to create schedule");
        int choice = GetValidChoice(0, 4);
        while (choice > 0)
        {

            switch (choice)
            {
                case 1:
                    OptionsTaskManu();
                    break;
                case 2:
                    OptionsEngineerManu();
                    break;
                case 3:
                    InitializationData();
                    break;
                case 4:
                    CreateSche();
                    break;
            }
            Console.WriteLine("hi here is an options namu \npress 0 to exit\npress 1 to task\n press 2 to engineer\n press 3 to reset all Data\n press 4 to create schedule");
            choice = GetValidChoice(0, 4);
        }
    }

    static void Main(string[] args)
    {
        try
        {
            MainManu();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

}