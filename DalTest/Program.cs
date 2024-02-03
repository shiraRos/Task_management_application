using Dal;
using DalApi;
using DO;
namespace DalTest;

internal class Program
{
    ////Initialize items for using the interface
    //private static ITask t_dalTask = new TaskImplementation();                   //stage 1
    //private static IDependency d_dalDependecy = new DependencyImplementation();  //stage 1
    //private static IEngineer e_dalEngineer = new EngineerImplementation();       //stage 1

    //static readonly IDal s_dal = new DalList(); //stage 2
    //static readonly IDal s_dal = new DalXml(); //stage 3
    static readonly IDal s_dal = Factory.Get; //stage 4

    //converting the level
    private static int? GetComplexityLevel()
    {
        while (true)
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
            return  false; // Default value if parsing fails
        }

    }
    
    
    /// <summary>
    /// Creating a new task and adding it to the existing data
    /// </summary>
    private static void CreateTask()
    {
        //Receipt of data by the user
        int? _idEngineer;
        int?_complexityLevel;
        bool _isMileston;
        DateTime? _startDate, _deadlineDate, _completeDate, _scheduledDate;
        TimeSpan? _requiredEffortTime;
        string? _alias, _deliverables, _remarks, _description;
        Console.WriteLine("insert engineer id");
        if (int.TryParse(Console.ReadLine(), out int parsedId))
        {
            _idEngineer = parsedId;
        }
        else
            _idEngineer = null;

        Console.WriteLine("is it a miles tone?");
        _isMileston = ParseMilestone();
        // Now 'isMilestone' will be a bool value based on user input, or false if parsing failed.
        Console.WriteLine("Is Milestone: " + _isMileston);
        Console.WriteLine("insert start date");
        _startDate = ParseDate();
        Console.WriteLine("Start Date: " + (_startDate.HasValue ? _startDate.Value.ToString("yyyy-MM-dd") : null));
        Console.WriteLine("insert deadline date");
        _deadlineDate = ParseDate();
        Console.WriteLine("DeadLine Date: " + (_deadlineDate.HasValue ? _deadlineDate.Value.ToString("yyyy-MM-dd") : null));
        Console.WriteLine("insert complete date");
        _completeDate = ParseDate();
        Console.WriteLine("Complete Date: " + (_completeDate.HasValue ? _completeDate.Value.ToString("yyyy-MM-dd") : null));
        Console.WriteLine("insert scheduled date ");
        _scheduledDate = ParseDate();
        Console.WriteLine("Scheduled Date: " + (_scheduledDate.HasValue ? _scheduledDate.Value.ToString("yyyy-MM-dd") : null));
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
        // Now 'complexityLevel' will be an integer between 0-4 based on user input.
        Console.WriteLine("Complexity Level: " + _complexityLevel);
        Console.WriteLine("insert description");
        _description = Console.ReadLine() ?? " ";
        Console.WriteLine("insert alias");
        _alias = Console.ReadLine() ?? " ";
        //creating a new object
        DO.Task newTsk = new(0, _idEngineer, _isMileston, _startDate, _deadlineDate, _completeDate, _scheduledDate, _requiredEffortTime, _deliverables, _remarks, (_complexityLevel != null ? (EngineerExperience)_complexityLevel : null), _description, _alias);
        //Add to data by calling an external operation
        int idnt = s_dal.Task.Create(newTsk);
    }

    /// <summary>
    /// Creating a new dependency and adding it to the existing data
    /// </summary>
    private static void CreateDependency()
    {
        //Receipt of data by the user
        int _dependenTask, _dependensOnTask;
        Console.WriteLine("insert depeden task ");
        _dependenTask = int.Parse(Console.ReadLine()!);
        Console.WriteLine("depends on task ");
        _dependensOnTask = int.Parse(Console.ReadLine()!);
        //creating a new object
        Dependency newDpn = new(0, _dependenTask, _dependensOnTask);
        //Add to data by calling an external operation
        int temp = s_dal.Dependency.Create(newDpn);
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
        _name = Console.ReadLine() ?? " ";
        Console.WriteLine("insert complexity level between 0-4");
        _level = GetComplexityLevel();
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
        _email = Console.ReadLine() ?? " ";
        //creating a new object
        Engineer newEng = new(_id, (_level!=null? (EngineerExperience)_level:null), _cost, _name, _email);
        //Add to data by calling an external operation
        _id = s_dal.Engineer.Create(newEng);
    }

    /// <summary>
    /// Deleting a task from the existing data
    /// </summary>
    private static void DeleteTask()
    {
        Console.WriteLine("insert task code to remove");
        int taskId = int.Parse(Console.ReadLine()!);
        //delete from the data by calling an external operation
        s_dal.Task.Delete(taskId);
    }

    /// <summary>
    /// Deleting an engineer from the existing data
    /// </summary>
    private static void DeleteEngineer()
    {
        Console.WriteLine("insert engineer to remove");
        int engineerId = int.Parse(Console.ReadLine()!);
        //delete from the data by calling an external operation
        s_dal.Engineer.Delete(engineerId);
    }

    /// <summary>
    /// Deleting a dependency from the existing data
    /// </summary>
    private static void DeleteDependency()
    {
        Console.WriteLine("insert dependeny to remove");
        int dependencyId = int.Parse(Console.ReadLine() ?? " ");
        //delete from the data by calling an external operation
        s_dal.Dependency.Delete(dependencyId);
    }

    /// <summary>
    /// Reading a certain task from the existing data
    /// </summary>
    private static void ReadTask()
    {
        Console.WriteLine("insert task code to print");
        int taskId = int.Parse(Console.ReadLine()!);
        //print the data by calling an external operation
        Console.WriteLine(s_dal.Task.Read(taskId));
    }

    /// <summary>
    /// Reading a certain dependency from the existing data
    /// </summary>
    private static void ReadDependency()
    {
        Console.WriteLine("insert dependency code to print");
        int dependencyId = int.Parse(Console.ReadLine()!);
        //print the data by calling an external operation
        Console.WriteLine(s_dal.Dependency.Read(dependencyId));
    }

    /// <summary>
    /// Reading a certain engineer from the existing data
    /// </summary>
    private static void ReadEngineer()
    {
        Console.WriteLine("insert engineer code to print");
        int engineerId = int.Parse(Console.ReadLine()!);
        //print the data by calling an external operation
        Console.WriteLine(s_dal.Engineer.Read(engineerId));
    }

    /// <summary>
    /// Reading all of the engineers from the data
    /// </summary>
    private static void ReadAllEngineers()
    {
        Console.WriteLine("the all engineers:");
        //getting all the engineers to a new item
        //List<Engineer> engList = s_dal!.Engineer.ReadAll();
        List<Engineer> engList = s_dal!.Engineer.ReadAll()?.Where(e => e != null).Select(e => e!).ToList() ?? new List<Engineer>();
        //print evey item
        foreach (var item in engList)
        {
            Console.WriteLine(item);
        }
    }

    /// <summary>
    /// Reading all of the dependencies from the data
    /// </summary>
    private static void ReadAllDependencies()
    {
        Console.WriteLine("the all Dependencies:");
        //getting all the dependencies to a new item
        List<Dependency> depList = s_dal!.Dependency.ReadAll()?.Where(d=>d !=null).Select(d=>d!).ToList()?? new List<Dependency>();
        //print evey item
        foreach (var item in depList)
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
        List<DO.Task> tskList = s_dal!.Task.ReadAll()?.Where(t => t != null).Select(t => t!).ToList() ?? new List<DO.Task>();
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
        //Receipt of data by the user by id
        Console.WriteLine("insert id");
        int id = int.Parse(Console.ReadLine()!);
        s_dal.Task.Read(id);
        int? _idEngineer;
        int? _complexityLevel;
        bool? _isMileston;
        DateTime? _deadlineDate, _completeDate, _scheduledDate, _startDate;
        TimeSpan? _requiredEffortTime;
        string? _alias, _deliverables, _remarks, _description;
        Console.WriteLine("insert engineer id");
        if (int.TryParse(Console.ReadLine(), out int parsedId))
        {
            _idEngineer = parsedId;
        }
        else
            _idEngineer = null;
        Console.WriteLine("is it a miles tone?");
        _isMileston = ParseMilestone();
        // Now 'isMilestone' will be a bool value based on user input, or false if parsing failed.
        Console.WriteLine("Is Milestone: " + _isMileston);
        Console.WriteLine("insert start date");
        _startDate = ParseDate();
        Console.WriteLine("Start Date: " + (_startDate.HasValue ? _startDate.Value.ToString("yyyy-MM-dd") : null));
        Console.WriteLine("insert deadline date");
        _deadlineDate = ParseDate();
        Console.WriteLine("DeadLine Date: " + (_deadlineDate.HasValue ? _deadlineDate.Value.ToString("yyyy-MM-dd") : null));
        Console.WriteLine("insert complete date");
        _completeDate = ParseDate();
        Console.WriteLine("Complete Date: " + (_completeDate.HasValue ? _completeDate.Value.ToString("yyyy-MM-dd") : null));
        Console.WriteLine("insert scheduled date ");
        _scheduledDate =ParseDate();
        Console.WriteLine("Scheduled Date: Date: " + (_scheduledDate.HasValue ? _scheduledDate.Value.ToString("yyyy-MM-dd") : null));
        Console.WriteLine("insert required Effort Time");
        if (TimeSpan.TryParse(Console.ReadLine(), out TimeSpan result))
        {
            _requiredEffortTime = result;
        }
        else
        {
            _requiredEffortTime = null;
        }
        Console.WriteLine("Required Effort Time: " + (_requiredEffortTime.HasValue ? _requiredEffortTime.ToString() : null));
        Console.WriteLine("insert deliverables");
        _deliverables = Console.ReadLine() ?? " ";
        Console.WriteLine("insert remarks");
        _remarks = Console.ReadLine() ?? " ";
        Console.WriteLine("insert complexity level between 0-4");
        _complexityLevel = GetComplexityLevel();
        // Now 'complexityLevel' will be an integer between 0-4 based on user input.
        Console.WriteLine("Complexity Level: " + _complexityLevel);
        Console.WriteLine("insert description");
        _description = Console.ReadLine() ?? " ";
        Console.WriteLine("insert alias");
        _alias = Console.ReadLine() ?? " ";
        //creating a new object
        DO.Task newTsk = new(id, _idEngineer, _isMileston, _startDate, _deadlineDate, _completeDate, _scheduledDate, _requiredEffortTime, _deliverables, _remarks, (_complexityLevel != null ? (EngineerExperience)_complexityLevel : null), _description, _alias);
        //Update the data by calling an external operation
        s_dal.Task.Update(newTsk);
    }

    /// <summary>
    /// Updating information about a dependency that already exists in the system
    /// </summary>
    private static void UpdateDependency()
    {
        //Receipt of data by the user by id
        Console.WriteLine("insert id");
        int id = int.Parse(Console.ReadLine()!);
        s_dal.Dependency.Read(id);
        int _dependenTask, _dependensOnTask;
        Console.WriteLine("insert depeden task ");
        _dependenTask = int.Parse(Console.ReadLine()!);
        Console.WriteLine("depends on task ");
        _dependensOnTask = int.Parse(Console.ReadLine()!);
        //creating a new object
        Dependency newDpn = new(id, _dependenTask, _dependensOnTask);
        //Update the data by calling an external operation
        s_dal.Dependency.Update(newDpn);
    }

    /// <summary>
    /// Updating information about an engineer that already exists in the system
    /// </summary>
    private static void UpdateEngineer()
    {
        //Receipt of data by the user by id
        Console.WriteLine("insert id");
        int id = int.Parse(Console.ReadLine()!);
        s_dal.Engineer.Read(id);
        int? _level;
        double? _cost;
        string _name, _email;
        Console.WriteLine(" insert name");
        _name = Console.ReadLine() ?? " ";
        Console.WriteLine("insert complexity kevel between 0-4");
        _level = GetComplexityLevel();
        // Now 'complexityLevel' will be an integer between 0-4 based on user input.
        Console.WriteLine("Complexity Level: " + _level);
        Console.WriteLine("insert cost");
        if (double.TryParse(Console.ReadLine(), out double parsedCost))
            _cost = parsedCost;
        else
            _cost = null;
        // Now 'cost' will be a double value if parsing was successful, or null if it failed or the input was blank.
        Console.WriteLine("Cost: " + (_cost.HasValue ? _cost.ToString() : "null"));
        Console.WriteLine("insert email");
        _email = Console.ReadLine() ?? " ";
        //creating a new object
        Engineer newEng = new(id, (_level != null ? (EngineerExperience)_level : null), _cost, _name, _email);
        //Update the data by calling an external operation
        s_dal.Engineer.Update(newEng);
    }

    /// <summary>
    /// delete all the dependencies
    /// </summary>
    private static void ResetDependency()
    {
        //Reste the data by calling an external operation
        s_dal.Dependency.Reset();
    }

    /// <summary>
    /// delete all the engineers
    /// </summary>
    private static void ResetEngineer()
    {
        //Reste the data by calling an external operation
        s_dal.Engineer.Reset();
    }

    /// <summary>
    /// delete all the tasks
    /// </summary>
    private static void ResetTask()
    {
        //Reste the data by calling an external operation
        s_dal.Task.Reset();
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
           
            if (int.TryParse(Console.ReadLine(),out choice) && choice >= min && choice <= max)
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
    /// A menu for the user selection for the dependencies
    /// </summary>
    private static void OptionsDependencykManu()
    {
        try
        {
            int choice;
            Console.WriteLine("press 0 to exit\n press 1 create a new Dependency\n press 2 to read Dependency\npress 3 to read all Dependencies\npress 4 to update \npress 5 to delete\npress 6 to reset\n");
            choice = int.Parse(Console.ReadLine() ?? " ");
            while (choice < 0 || choice > 6)
            {
                Console.WriteLine("insert number between 0-6");
                choice = int.Parse(Console.ReadLine() ?? " ");
            }
            while (choice != 0)
            {

                switch (choice)
                {
                    case 1:
                        CreateDependency();
                        break;
                    case 2:
                        ReadDependency();
                        break;
                    case 3:
                        ReadAllDependencies();
                        break;
                    case 4:
                        UpdateDependency();
                        break;
                    case 5:
                        DeleteDependency();
                        break;
                    case 6:
                        ResetDependency();
                        break;
                }
                Console.WriteLine("press 0 to exit\n press 1 create a new Dependency\n press 2 to read Dependency\npress 3 to read all Dependencies\npress 4 to update \npress 5 to delete\npress 6 to reset\n");
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
            s_dal.Reset();
            //Initialization.DO(s_dal);//stage 2
            Initialization.Do(); //stage 4
        }
    }

    /// <summary>
    /// The main menu where the user can choose which entity to enter
    /// </summary>
    private static void MainManu()
    {
        Console.WriteLine("hi here is an options namu \npress 0 to exit\npress 1 to task\n press 2 to dependendy\npress 3 to engineer\n press 4 to reset all Data\n");
        int choice = GetValidChoice(0, 4);
        while (choice > 0)
        {

            switch (choice)
            {
                case 1:
                    OptionsTaskManu();
                    break;
                case 2:
                    OptionsDependencykManu();
                    break;
                case 3:
                    OptionsEngineerManu();
                    break;
                case 4:
                    InitializationData();
                    break;
            }
            Console.WriteLine("hi here is an options namu \npress 0 to exit\npress 1 to task\n press 2 to dependendy\npress 3 to engineer\n press 4 to reset all Data\n");
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