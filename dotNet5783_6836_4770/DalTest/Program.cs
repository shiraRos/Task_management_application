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


        //private static int MainManu()
        //{
        //    Console.WriteLine("לפנייך תפריט האפשרויות \nהקש 0 ליציאה\nהקש 1 למשימה\n הקש 2 לתלות\nהקש 3 למתכנת");
        //    int choice=int.Parse(Console.ReadLine());
        //    return choice;
        //}
        ////יצרת תפריט עבור משימה
        //private static void TaskManu()
        //{
            
        //}
        private static void CreateTask()
        {
            int _idEngineer,_complexityLevel;
            bool _isMileston;
            DateTime? _deadlineDate, _completeDate, _scheduledDate, _startDate;
            TimeSpan? _requiredEffortTime;
            string? _alias,_deliverables,_remarks,_description;
            Console.WriteLine("הכנס ת.ז מתכנת");
            _idEngineer = int.Parse(Console.ReadLine());
            Console.WriteLine("האם זוהי משימת אבן דרך?");
            _isMileston =bool.Parse(Console.ReadLine());
            Console.WriteLine("הקש תאריך התחלה");
            _startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("הקש תאריך אחרון לסיום");
            _deadlineDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("הקש תאריך סיום בפועל");
            _completeDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("התאריך בו המשימה הייתה אמורה להסתיים");
            _scheduledDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("משך הזמן שארכה המשימה");
            _requiredEffortTime =TimeSpan.Parse(Console.ReadLine());
            Console.WriteLine("האם בר שליחה?");
            _deliverables = Console.ReadLine();
            Console.WriteLine("הערות");
            _remarks = Console.ReadLine();
            Console.WriteLine("הקש רמת מורכבות בין 0-4");
            _complexityLevel =int.Parse(Console.ReadLine());
            Console.WriteLine("תיאור כללי");
            _description = Console.ReadLine();
            Console.WriteLine("כינוי");
            _alias =Console.ReadLine();
            DO.Task newTsk = new(0, _idEngineer, _isMileston, _startDate, _deadlineDate, _completeDate, _scheduledDate, _requiredEffortTime, _deliverables, _remarks, (EngineerExperience)_complexityLevel, _description, _alias);
            int idnt = t_dalTask.Create(newTsk);
        }
        private static void CreatDependency()
        {
            int _dependenTask, _dependensOnTask;
            Console.WriteLine("הקש קוד משימה נוכחית ");
            _dependenTask=int.Parse(Console.ReadLine());
            Console.WriteLine("הקש קוד משימה שבה תלויה ");
            _dependensOnTask=int.Parse(Console.ReadLine());
            Dependency newDpn = new(0, _dependenTask, _dependensOnTask);
            int temp = d_dalDependecy.Create(newDpn);
        }
        private static void CreateEngineer()
        {
            int _id, _level;
            double _cost;
            string _name, _email;
            Console.WriteLine("הקש ת.ז.");
            _id=int.Parse(Console.ReadLine());
            Console.WriteLine("הקש שם");
            _name = Console.ReadLine();
            Console.WriteLine("הקש רמה בין 0-4");
            _level=int.Parse(Console.ReadLine());
            Console.WriteLine("הקש עלות");
            _cost=double.Parse(Console.ReadLine());
            Console.WriteLine("הקש אמייל");
            _email=Console.ReadLine();
            Engineer newEng = new(_id, (EngineerExperience)_level, _cost, _name, _email);
            _id = e_dalEngineer.Create(newEng);
        }
        private static void DeleteTask()
        {
            Console.WriteLine("הקש קוד משימה להסרה");
            int taskId = int.Parse(Console.ReadLine());
            t_dalTask.Delete(taskId);
        }

        private static void DeleteEngineer()
        {
            Console.WriteLine("הקש קוד מתכנת להסרה");
            int engineerId = int.Parse(Console.ReadLine());
            e_dalEngineer.Delete(engineerId);
        }

        private static void DeleteDependency()
        {
            Console.WriteLine("הקש קוד תלות למחיקה");
            int dependencyId = int.Parse(Console.ReadLine());
            d_dalDependecy.Delete(dependencyId);
        }


        private static void ReadTask()
        {
            Console.WriteLine("הקש קוד משימה להדפסה");
            int taskId = int.Parse(Console.ReadLine());
            Console.WriteLine(t_dalTask.Read(taskId));
        }

        private static void ReadDependency()
        {
            Console.WriteLine("הקש קוד תלות להדפסה");
            int dependencyId = int.Parse(Console.ReadLine());
            Console.WriteLine(d_dalDependecy.Read(dependencyId));
        }

        private static void ReadEngineer()
        {
            Console.WriteLine("הקש קוד מתכנת להדפסה");
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
        static void Main(string[] args)
        {
            try 
            {
                Initialization.DO(s_engineer,s_dalDependecy,s_dalTask);
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