using Dal;
using DalApi;
using DO;
using System;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace DalTest
{
    internal class Program
    {
        private static ITask t_dalTask = new TaskImplementation();
        private static IDependency d_dalDependecy = new DependencyImplementation();
        private static IEngineer e_dalEngineer = new EngineerImplementation();


       
        private static void CreateTask()
        {
            int _idEngineer, _complexityLevel;
            bool _isMileston;
            DateTime? _deadlineDate, _completeDate, _scheduledDate, _startDate;
            TimeSpan? _requiredEffortTime;
            string? _alias, _deliverables, _remarks, _description;
            Console.WriteLine("insert engineer id");
            _idEngineer = int.Parse(Console.ReadLine());
            Console.WriteLine("is it a miles tone?");
            _isMileston = bool.Parse(Console.ReadLine());
            Console.WriteLine("insert start date");
            _startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("insert deadline date");
            _deadlineDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("insert complete date");
            _completeDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("insert scheduled date ");
            _scheduledDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("insert required Effort Time");
            _requiredEffortTime = TimeSpan.Parse(Console.ReadLine());
            Console.WriteLine("insert deliverables");
            _deliverables = Console.ReadLine();
            Console.WriteLine("insert remarks");
            _remarks = Console.ReadLine();
            Console.WriteLine("insert complexity level between 0-4");
            _complexityLevel = int.Parse(Console.ReadLine());
            Console.WriteLine("insert description");
            _description = Console.ReadLine();
            Console.WriteLine("insert alias");
            _alias = Console.ReadLine();
            DO.Task newTsk = new(0, _idEngineer, _isMileston, _startDate, _deadlineDate, _completeDate, _scheduledDate, _requiredEffortTime, _deliverables, _remarks, (EngineerExperience)_complexityLevel, _description, _alias);
            int idnt = t_dalTask.Create(newTsk);
        }
        private static void CreateDependency()
        {
            int _dependenTask, _dependensOnTask;
            Console.WriteLine("insert depeden task ");
            _dependenTask = int.Parse(Console.ReadLine());
            Console.WriteLine("depends on task ");
            _dependensOnTask = int.Parse(Console.ReadLine());
            Dependency newDpn = new(0, _dependenTask, _dependensOnTask);
            int temp = d_dalDependecy.Create(newDpn);
        }
        private static void CreateEngineer()
        {
            int _id, _level;
            double _cost;
            string _name, _email;
            Console.WriteLine("insert id");
            _id = int.Parse(Console.ReadLine());
            Console.WriteLine(" insert name");
            _name = Console.ReadLine();
            Console.WriteLine("insert complexity kevel between 0-4");
            _level = int.Parse(Console.ReadLine());
            Console.WriteLine("insert cost");
            _cost = double.Parse(Console.ReadLine());
            Console.WriteLine("insert email");
            _email = Console.ReadLine();
            Engineer newEng = new(_id, (EngineerExperience)_level, _cost, _name, _email);
            _id = e_dalEngineer.Create(newEng);
        }
        private static void DeleteTask()
        {
            Console.WriteLine("insert task code to remove");
            int taskId = int.Parse(Console.ReadLine());
            t_dalTask.Delete(taskId);
        }

        private static void DeleteEngineer()
        {
            Console.WriteLine("insert engineer to remove");
            int engineerId = int.Parse(Console.ReadLine());
            e_dalEngineer.Delete(engineerId);
        }

        private static void DeleteDependency()
        {
            Console.WriteLine("insert dependeny to remove");
            int dependencyId = int.Parse(Console.ReadLine());
            d_dalDependecy.Delete(dependencyId);
        }


        private static void ReadTask()
        {
            Console.WriteLine("insert task code to print");
            int taskId = int.Parse(Console.ReadLine());
            Console.WriteLine(t_dalTask.Read(taskId));
        }

        private static void ReadDependency()
        {
            Console.WriteLine("insert dependency code to print");
            int dependencyId = int.Parse(Console.ReadLine());
            Console.WriteLine(d_dalDependecy.Read(dependencyId));
        }

        private static void ReadEngineer()
        {
            Console.WriteLine("insert engineer code to print");
            int engineerId = int.Parse(Console.ReadLine());
            Console.WriteLine(e_dalEngineer.Read(engineerId));
        }

        private static void ReadAllEngineers()
        {
            Console.WriteLine("the all engineers:");
            List<Engineer> engList = e_dalEngineer!.ReadAll();
            foreach (var item in engList)
            {
                Console.WriteLine(item);
            }
        }

        private static void ReadAllDependencies()
        {
            Console.WriteLine("the all Dependencies:");
            List<Dependency> depList = d_dalDependecy!.ReadAll();
            foreach (var item in depList)
            {
                Console.WriteLine(item);
            }
        }

        private static void ReadAllTasks()
        {
            Console.WriteLine("the all Tasks:");
            List<DO.Task> tskList = t_dalTask!.ReadAll();
            foreach (var item in tskList)
            {
                Console.WriteLine(item);
            }
        }

        private static void UpdateTask()
        {

        }

        private static void UpdateDependency()
        {

        }

        private static void UpdateEngineer()
        {

        }

        private static void OptionsTaskManu()
        {
            try
            {
                int choice;
                Console.WriteLine("press 0 to exit\n press 1 create a new task\n press 2 to read task\npress 3 to read all tasks\npress 4 to update \npress 5 to delete\n");
                choice = int.Parse(Console.ReadLine());
                while (choice < 0 || choice > 5)
                {
                    Console.WriteLine("insert number between 0-5");
                    choice = int.Parse(Console.ReadLine());
                }
                while (choice > 0)
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
                    }
                    Console.WriteLine("insert number between 0-5");
                    choice = int.Parse(Console.ReadLine());
                    while (choice < 0||choice>5) {
                        Console.WriteLine("insert number between 0-5");
                        choice = int.Parse(Console.ReadLine());
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void OptionsDependencykManu()
        {
            try
            {
                int choice;
                Console.WriteLine("press 0 to exit\n press 1 create a new Dependency\n press 2 to read Dependency\npress 3 to read all Dependencies\npress 4 to update \npress 5 to delete\n");
                choice = int.Parse(Console.ReadLine());
                while (choice < 0 || choice > 5)
                {
                    Console.WriteLine("insert number between 0-5");
                    choice = int.Parse(Console.ReadLine());
                }
                while (choice > 0)
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
                    }
                    Console.WriteLine("insert number between 0-5");
                    choice = int.Parse(Console.ReadLine());
                    while (choice < 0 || choice > 5)
                    {
                        Console.WriteLine("insert number between 0-5");
                        choice = int.Parse(Console.ReadLine());
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void OptionsEngineerManu()
        {
            try
            {
                int choice;
                Console.WriteLine("press 0 to exit\n press 1 create a new Engineer\n press 2 to read Engineer\npress 3 to read all Engineers\npress 4 to update \npress 5 to delete\n"); 
                choice = int.Parse(Console.ReadLine());
                while (choice < 0 || choice > 5)
                {
                    Console.WriteLine("insert number between 0-5");
                    choice = int.Parse(Console.ReadLine());
                }
                while (choice > 0)
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
                    }
                    Console.WriteLine("insert number between 0-5");
                    choice = int.Parse(Console.ReadLine());
                    while (choice < 0 || choice > 5)
                    {
                        Console.WriteLine("insert number between 0-5");
                        choice = int.Parse(Console.ReadLine());
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        private static void MainManu()
        {
            Console.WriteLine("hi here is an options namu \npress 0 to exit\npress 1 to task\n press 2 to dependendy\npress 3 to engineer\n");
            int choice = int.Parse(Console.ReadLine());
            while (choice < 0 || choice > 3)
            {
                Console.WriteLine("insert number between 0-3");
                choice = int.Parse(Console.ReadLine());
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
                Console.WriteLine("הקש מספר בין 0-5");
                choice = int.Parse(Console.ReadLine());
                while (choice < 0 || choice > 5)
                {
                    Console.WriteLine("הקש מספר בין 0-5");
                    choice = int.Parse(Console.ReadLine());
                }
            }
        }
      
        static void Main(string[] args)
        {
            try
            {
                Initialization.DO(e_dalEngineer, d_dalDependecy, t_dalTask);
                MainManu();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}