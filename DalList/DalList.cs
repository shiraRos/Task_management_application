namespace Dal;
using DalApi;
using static Dal.DataSource;
using System.Xml.Linq;

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }


    //למקרה שרוצים לנסות את הרשות זו הדרך וצריך להבין אותה...
    //// משתנה סטטי של Lazy<IDal> המכיל את האובייקט היחיד של DalList
    //private static readonly Lazy<IDal> lazyInstance = new Lazy<IDal>(() => new DalList());

    //// פרופרטי שמשתמש ב-Value של Lazy כדי לקבל את האינסטנס היחיד של DalList
    //public static IDal Instance => lazyInstance.Value;

    //// הגדרת קונסטרקטור פרטי כדי למנוע יצירה חיצונית של אובייקטים
    //private DalList() { }




    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public ITask Task =>  new TaskImplementation();
    public void ProjectEndDateUpdate(DateTime date)
    {
        Config.endDate = date;
    }

    public void ProjectStartDateUpdate(DateTime date)
    {
        Config.startDate = date;
    }


    /// <summary>
    /// Implementation of methods for get the start ,end date from the config
    /// </summary>
    /// <returns></returns>

    public DateTime? ReturnTheEndDate()
    {
        return Config.endDate;
    }

    public DateTime? ReturnTheStartDate()
    {
        return Config.startDate;
    }

    /// <summary>
    /// function for delete every value from the data base
    /// </summary>
    public void Reset()
    {
        DataSource.Dependencies.Clear();
        DataSource.Engineers.Clear();
        DataSource.Tasks.Clear();
    }
}
