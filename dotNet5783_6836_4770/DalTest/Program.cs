using Dal;
using DalApi;
using DO;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalTest
{
    internal class Program
    {
        //Initialize items for using the interface
        private static ITask t_dalTask = new TaskImplementation();
        private static IDependency d_dalDependecy = new DependencyImplementation();
        private static IEngineer e_dalEngineer = new EngineerImplementation();


        //Creating a new task and adding it to the existing data
        private static void CreateTask()
        {
            //Receipt of data by the user
            int _idEngineer, _complexityLevel;
            bool _isMileston;
            DateTime? _deadlineDate, _completeDate, _scheduledDate, _startDate;
            TimeSpan? _requiredEffortTime;
            string? _alias, _deliverables, _remarks, _description;
            Console.WriteLine("insert engineer id");
            _idEngineer = int.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("is it a miles tone?");
            _isMileston = bool.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert start date");
            _startDate = DateTime.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert deadline date");
            _deadlineDate = DateTime.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert complete date");
            _completeDate = DateTime.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert scheduled date ");
            _scheduledDate = DateTime.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert required Effort Time");
            _requiredEffortTime = TimeSpan.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert deliverables");
            _deliverables = Console.ReadLine() ?? " ";
            Console.WriteLine("insert remarks");
            _remarks = Console.ReadLine() ?? " ";
            Console.WriteLine("insert complexity level between 0-4");
            _complexityLevel = int.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert description");
            _description = Console.ReadLine() ?? " ";
            Console.WriteLine("insert alias");
            _alias = Console.ReadLine() ?? " ";
            //creating a new object
            DO.Task newTsk = new(0, _idEngineer, _isMileston, _startDate, _deadlineDate, _completeDate, _scheduledDate, _requiredEffortTime, _deliverables, _remarks, (EngineerExperience)_complexityLevel, _description, _alias);
            //Add to data by calling an external operation
            int idnt = t_dalTask.Create(newTsk);
        }

        //Creating a new dependency and adding it to the existing data
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
            int temp = d_dalDependecy.Create(newDpn);
        }

        //Creating a new engineer and adding it to the existing data
        private static void CreateEngineer()
        {
            //Receipt of data by the user
            int _id, _level;
            double _cost;
            string _name, _email;
            Console.WriteLine("insert id");
            _id = int.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine(" insert name");
            _name = Console.ReadLine() ?? " ";
            Console.WriteLine("insert complexity kevel between 0-4");
            _level = int.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert cost");
            _cost = double.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert email");
            _email = Console.ReadLine() ?? " ";
            //creating a new object
            Engineer newEng = new(_id, (EngineerExperience)_level, _cost, _name, _email);
            //Add to data by calling an external operation
            _id = e_dalEngineer.Create(newEng);
        }

        //Deleting a task from the existing data
        private static void DeleteTask()
        {
            Console.WriteLine("insert task code to remove");
            int taskId = int.Parse(Console.ReadLine() ?? " ");
            //delete from the data by calling an external operation
            t_dalTask.Delete(taskId);
        }

        //Deleting an engineer from the existing data
        private static void DeleteEngineer()
        {
            Console.WriteLine("insert engineer to remove");
            int engineerId = int.Parse(Console.ReadLine() ?? " ");
            //delete from the data by calling an external operation
            e_dalEngineer.Delete(engineerId);
        }

        //Deleting a dependency from the existing data
        private static void DeleteDependency()
        {
            Console.WriteLine("insert dependeny to remove");
            int dependencyId = int.Parse(Console.ReadLine() ?? " ");
            //delete from the data by calling an external operation
            d_dalDependecy.Delete(dependencyId);
        }

        //Reading a certain task from the existing data
        private static void ReadTask()
        {
            Console.WriteLine("insert task code to print");
            int taskId = int.Parse(Console.ReadLine() ?? " ");
            //print the data by calling an external operation
            Console.WriteLine(t_dalTask.Read(taskId));
        }

        //Reading a certain dependency from the existing data
        private static void ReadDependency()
        {
            Console.WriteLine("insert dependency code to print");
            int dependencyId = int.Parse(Console.ReadLine() ?? " ");
            //print the data by calling an external operation
            Console.WriteLine(d_dalDependecy.Read(dependencyId));
        }

        //Reading a certain engineer from the existing data
        private static void ReadEngineer()
        {
            Console.WriteLine("insert engineer code to print");
            int engineerId = int.Parse(Console.ReadLine() ?? " ");
            //print the data by calling an external operation
            Console.WriteLine(e_dalEngineer.Read(engineerId));
        }

        //Reading all of the engineers from the data
        private static void ReadAllEngineers()
        {
            Console.WriteLine("the all engineers:");
            //getting all the engineers to a new item
            List<Engineer> engList = e_dalEngineer!.ReadAll();
            //print evey item
            foreach (var item in engList)
            {
                Console.WriteLine(item);
            }
        }

        //Reading all of the dependencies from the data
        private static void ReadAllDependencies()
        {
            Console.WriteLine("the all Dependencies:");
            //getting all the dependencies to a new item
            List<Dependency> depList = d_dalDependecy!.ReadAll();
            //print evey item
            foreach (var item in depList)
            {
                Console.WriteLine(item);
            }
        }

        //Reading all of the tasks from the data
        private static void ReadAllTasks()
        {
            Console.WriteLine("the all Tasks:");
            //getting all the tasks to a new item
            List<DO.Task> tskList = t_dalTask!.ReadAll();
            //print evey item
            foreach (var item in tskList)
            {
                Console.WriteLine(item);
            }
        }

        //Updating information about a task that already exists in the system
        private static void UpdateTask()
        {
            //Receipt of data by the user by id
            Console.WriteLine("insert id");
            int id = int.Parse(Console.ReadLine()!);
            t_dalTask.Read(id);
            int _idEngineer;
            int? _complexityLevel;
            bool? _isMileston;
            DateTime? _deadlineDate, _completeDate, _scheduledDate, _startDate;
            TimeSpan? _requiredEffortTime;
            string? _alias, _deliverables, _remarks, _description;
            Console.WriteLine("insert engineer id");
            _idEngineer = int.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("is it a miles tone?");
            _isMileston = bool.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert start date");
            _startDate = DateTime.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert deadline date");
            _deadlineDate = DateTime.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert complete date");
            _completeDate = DateTime.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert scheduled date ");
            _scheduledDate = DateTime.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert required Effort Time");
            _requiredEffortTime = TimeSpan.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert deliverables");
            _deliverables = Console.ReadLine() ?? " ";
            Console.WriteLine("insert remarks");
            _remarks = Console.ReadLine() ?? " ";
            Console.WriteLine("insert complexity level between 0-4");
            _complexityLevel = int.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert description");
            _description = Console.ReadLine() ?? " ";
            Console.WriteLine("insert alias");
            _alias = Console.ReadLine() ?? " ";
            //creating a new object
            DO.Task newTsk = new(id, _idEngineer, _isMileston, _startDate, _deadlineDate, _completeDate, _scheduledDate, _requiredEffortTime, _deliverables, _remarks, (EngineerExperience)_complexityLevel, _description, _alias);
            //Update the data by calling an external operation
            t_dalTask.Update(newTsk);
        }

        //Updating information about a dependency that already exists in the system
        private static void UpdateDependency()
        {
            //Receipt of data by the user by id
            Console.WriteLine("insert id");
            int id = int.Parse(Console.ReadLine()!);
            d_dalDependecy.Read(id);
            int _dependenTask, _dependensOnTask;
            Console.WriteLine("insert depeden task ");
            _dependenTask = int.Parse(Console.ReadLine()!);
            Console.WriteLine("depends on task ");
            _dependensOnTask = int.Parse(Console.ReadLine()!);
            //creating a new object
            Dependency newDpn = new(id, _dependenTask, _dependensOnTask);
            //Update the data by calling an external operation
            d_dalDependecy.Update(newDpn);
        }

        //Updating information about an engineer that already exists in the system
        private static void UpdateEngineer()
        {
            //Receipt of data by the user by id
            Console.WriteLine("insert id");
            int id = int.Parse(Console.ReadLine()!);
            e_dalEngineer.Read(id);
            int? _level;
            double _cost;
            string _name, _email;
            Console.WriteLine(" insert name");
            _name = Console.ReadLine() ?? " ";
            Console.WriteLine("insert complexity kevel between 0-4");
            _level = int.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert cost");
            _cost = double.Parse(Console.ReadLine() ?? " ");
            Console.WriteLine("insert email");
            _email = Console.ReadLine() ?? " ";
            //creating a new object
            Engineer newEng = new(id, (EngineerExperience)_level, _cost, _name, _email);
            //Update the data by calling an external operation
            e_dalEngineer.Update(newEng);
        }

        //delete all the dependencies
        private static void ResetDependency()
        {
            //Reste the data by calling an external operation
            d_dalDependecy.Reset();
        }

        //delete all the engineers
        private static void ResetEngineer()
        {
            //Reste the data by calling an external operation
            e_dalEngineer.Reset();
        }

        //delete all the tasks
        private static void ResetTask()
        {
            //Reste the data by calling an external operation
            t_dalTask.Reset();
        }

        //A menu for the user selection for the tasks
        private static void OptionsTaskManu()
        {
            try
            {
                int choice;
                Console.WriteLine("press 0 to exit\n press 1 create a new task\n press 2 to read task\npress 3 to read all tasks\npress 4 to update \npress 5 to delete\npress 6 to reset\n");
                choice = int.Parse(Console.ReadLine() ?? " ");
                while (choice < 0 || choice >6)
                {
                    Console.WriteLine("insert number between 0-6");
                    choice = int.Parse(Console.ReadLine() ?? " ");
                }
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
                    Console.WriteLine("insert number between 0-6");
                    choice = int.Parse(Console.ReadLine() ?? " ");
                    while (choice < 0 || choice > 6)
                    {
                        Console.WriteLine("insert number between 0-6");
                        choice = int.Parse(Console.ReadLine() ?? " ");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //A menu for the user selection for the dependencies
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
                    Console.WriteLine("insert number between 0-6");
                    choice = int.Parse(Console.ReadLine() ?? " ");
                    while (choice < 0 || choice > 6)
                    {
                        Console.WriteLine("insert number between 0-6");
                        choice = int.Parse(Console.ReadLine() ?? " ");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //A menu for the user selection for the engineers
        private static void OptionsEngineerManu()
        {
            try
            {
                int choice;
                Console.WriteLine("press 0 to exit\n press 1 create a new Engineer\n press 2 to read Engineer\npress 3 to read all Engineers\npress 4 to update \npress 5 to delete\npress 6 to reset\n");
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
                    Console.WriteLine("insert number between 0-6");
                    choice = int.Parse(Console.ReadLine() ?? "0");
                    while (choice < 0 || choice > 6)
                    {
                        Console.WriteLine("insert number between 0-6");
                        choice = int.Parse(Console.ReadLine() ??"0");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //The main menu where the user can choose which entity to enter
        private static void MainManu()
        {
            Console.WriteLine("hi here is an options namu \npress 0 to exit\npress 1 to task\n press 2 to dependendy\npress 3 to engineer\n");
            int choice = int.Parse(Console.ReadLine() ?? " ");
            while (choice < 0 || choice > 3)
            {
                Console.WriteLine("insert number between 0-3");
                choice = int.Parse(Console.ReadLine() ?? " ");
            }
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
                }
                Console.WriteLine("hi here is an options namu \npress 0 to exit\npress 1 to task\n press 2 to dependendy\npress 3 to engineer\n");
                choice = int.Parse(Console.ReadLine() ?? " ");
                while (choice < 0 || choice > 3)
                {
                    Console.WriteLine("insert number between 0-3");
                    choice = int.Parse(Console.ReadLine() ?? " ");
                }
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Initialization.DO(e_dalEngineer,d_dalDependecy,t_dalTask);
                //int mainChoice = MainManu();
                //while(mainChoice<0)
                //{ }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}