namespace DalTest;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml.Linq;


public static class Initialization
{
    private static IEngineer? e_dalEngineer;
    private static IDependency? d_dalDependency;
    private static ITask? t_dalTask;
    private static readonly Random s_rand = new();
    //create objects Engineer type
    private static void createEngineer()
    {
        string[] EngineerNames =
        {
            "Kewin Klein", "Dan Segal", "Dave Jos","noya go","kodi hh",
            "Barbara Sol", "Alice Weiss", "Sara Finlan","jonatan von",
        };
        foreach (var _name in EngineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(200000000, 400000000);
            while (e_dalEngineer!.Read(_id) != null);
            EngineerExperience _level = (EngineerExperience)s_rand.Next(0, 4);
            double? _cost = s_rand.Next(1000, 400000);
            string? _email = _name + "@gmail.com";
            Engineer newEng = new(_id, _level, _cost, _name, _email);
            e_dalEngineer!.Create(newEng);
        }
    }
    private static void createDependency()
    {
        int otomatId = 0;
        int _dependenTask = GetRanTask().Id;
        int nextRanTask = GetRanTask().Id;
        while(nextRanTask == _dependenTask)
            nextRanTask = GetRanTask().Id;
        int _dependensOnTask = nextRanTask;

        Dependency newDpn = new(otomatId,_dependenTask,_dependensOnTask);
        otomatId=d_dalDependency!.Create(newDpn);

    }
    //פונקציית עזר חיצונית להגרלת משימת תלות רנדומלית
    private static Task GetRanTask()
    {
        Task[] tempArr = new Task[20];
        tempArr = t_dalTask!.ReadAll().ToArray();
        return tempArr[s_rand.Next(0, 19)];
    }
    private static void createTask()
    {

        for (int i = 0; i < 20; i++)
        {
            int otamtId = 0;
            int _idEngineer= GetRanEng().Id;
            bool _isMileston = false;
            // DateTime date = new DateTime(1995, 1, 1);
            //הגרלת מספר ימים להוספה
            int daysToAdd = s_rand.Next(0, 10);
            DateTime _startDate = (DateTime.Today);
            //הגדרת דדליין ע"פ מס הימים שהוגרלו
            DateTime? _deadlineDate = _startDate.AddDays(daysToAdd+1);
            //הגדרה שהפרוייקט הסתיים יום לפני הדדליין עבור כל פרוייקט
            DateTime? _completeDate = null;
            //הגדרת יום תחילת העבודה למעשה כעכשיו
            DateTime? _scheduledDate = DateTime.Today;
            //DateTime? ForecastDate = null;
            TimeSpan? _requiredEffortTime = null;
            string? _alias = "tsk" + s_rand.Next(32, 122);
            string? _deliverables = null;
            string? _remarks = null;
            EngineerExperience? _complexityLevel = (EngineerExperience)s_rand.Next(0, 4);
            string? _description = null;
            Task newTsk = new(otamtId, _idEngineer, _isMileston, _startDate, _deadlineDate, _completeDate, _scheduledDate, _requiredEffortTime, _deliverables, _remarks, _complexityLevel, _description,_alias);
            otamtId=t_dalTask!.Create(newTsk);
        }
    }
    //פונקציית עזר חיצונית להגרלת מתכנת אחראי רנדומלי
    private static Engineer GetRanEng()
    {
        Engineer[] tempArr = new Engineer[9];
        tempArr=e_dalEngineer!.ReadAll().ToArray();
        return tempArr[s_rand.Next(0, 8)];
    }
    //מתודה ציבורית לזימון כל המתודות הפרטיות
    public static void DO(IEngineer dalEngineer, IDependency? dalDependency, ITask? dalTask)
    {
        e_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        d_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        t_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        createEngineer();
        createDependency();
        createTask();
    }
}
