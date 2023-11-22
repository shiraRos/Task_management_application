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
        int _dependenTask = DataSource.Config.NextTaskId;
        int _dependensOnTask = DataSource.Config.NextTaskId;

        Dependency newDpn = new(otomatId,_dependenTask,_dependensOnTask);
        d_dalDependency!.Create(newDpn);

    }
    private static void createTask()
    {

        for (int i = 0; i < 20; i++)
        {
            int otamtId = 0;
            int _idEngineer;
            Engineer randomEngineer = e_dalEngineer!.ReadAll().OrderBy(x => s_rand.Next()).First();
            _idEngineer = randomEngineer.Id;
            // _idEngineer =s_rand.Next(2000,4000);
            bool _isMileston = false;
            DateTime date = new DateTime(1995, 1, 1);
            DateTime _startDate = (DateTime.Today);
            int range = (DateTime.Today - date).Days;
            DateTime _deadlineDate = _startDate.AddDays(s_rand.Next(range));
            DateTime _completeDate = _deadlineDate.AddDays(s_rand.Next(range));
            DateTime _scheduledDate = _startDate.AddDays(s_rand.Next(range));
            DateTime? ForecastDate = null;
            TimeSpan? _requiredEffortTime = null;
            string? _alias = "tsk" + (char)s_rand.Next(32, 122);
            string? _deliverables = null;
            string? _remarks = null;
            int? _complexityLevel = s_rand.Next(1, 10);
            string? _description = null;
            Task newTsk = new(otamtId, _idEngineer, _isMileston, _startDate, _deadlineDate, _completeDate, _scheduledDate, ForecastDate, _requiredEffortTime, _alias, _deliverables, _remarks, _complexityLevel, _description);
            t_dalTask!.Create(newTsk);
        }
    }
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
