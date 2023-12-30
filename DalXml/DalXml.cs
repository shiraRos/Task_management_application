using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

sealed public class DalXml : IDal
{
    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public ITask Task =>  new TaskImplementation();
    //Implementation of methods for updating  the start,end dates by the config
    public void ProjectEndDateUpdate(DateTime date)
    {
        Config.endDate = date;
    }

    public void ProjectStartDateUpdate(DateTime date)
    {
        Config.startDate = date;
    }
    //Implementation of methods for get the start ,end date from the config

    public DateTime? ReturnTheEndDate()
    {
        return Config.endDate;
    }

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
