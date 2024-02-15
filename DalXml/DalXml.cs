using DalApi;
using DO;
using System.Diagnostics;
using System.Xml.Linq;

namespace Dal;

sealed internal class DalXml : IDal
{

    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }

    //במקרה שרוצים לעשות את הרשות...
    //// משתנה סטטי של Lazy<IDal> המכיל את האובייקט היחיד של DalList
    //private static readonly Lazy<IDal> lazyInstance = new Lazy<IDal>(() => new DalXml());

    //// פרופרטי שמשתמש ב-Value של Lazy כדי לקבל את האינסטנס היחיד של DalList
    //public static IDal Instance => lazyInstance.Value;

    //// הגדרת קונסטרקטור פרטי כדי למנוע יצירה חיצונית של אובייקטים
    //private DalXml() { }



    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public ITask Task =>  new TaskImplementation();
    /// <summary>
    /// Implementation of methods for updating  the start,end dates by the config
    /// </summary>
    /// <param name="date">the date to update</param>
    public void ProjectEndDateUpdate(DateTime date)
    {
        Config.endDate = date;
    }

    public void ProjectStartDateUpdate(DateTime date)
    {
        XMLTools.SetStartDate("data-config", "startDate", date);
    }

    /// <summary>
    /// Implementation of methods for get the start ,end date from the config
    /// </summary>
    /// <returns>the end date</returns>
    public DateTime? ReturnTheEndDate()
    {
        return Config.endDate;
    }

    /// <summary>
    /// Implementation of methods for get the start ,end date from the config
    /// </summary>
    /// <returns>the start date</returns>
    public DateTime? ReturnTheStartDate()
    {
        return Config.startDate;
    }

    public void Reset()
    {
        XElement rootDependency = XMLTools.LoadListFromXMLElement("dependencys");
        rootDependency.Elements().Remove();
        XMLTools.SaveListToXMLElement(rootDependency, "dependencys");

        XElement rootTask = XMLTools.LoadListFromXMLElement("tasks");
        rootTask.Elements().Remove();
        XMLTools.SaveListToXMLElement(rootTask, "tasks");

        XElement rootEngineer = XMLTools.LoadListFromXMLElement("engineers");
        rootEngineer.Elements().Remove();
        XMLTools.SaveListToXMLElement(rootEngineer, "engineers");

    }

}
