namespace Dal;
using DalApi;
using static Dal.DataSource;
using System.Xml.Linq;

sealed public class DalList : IDal
{
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
    //Implementation of methods for get the start ,end date from the config

    public DateTime? ReturnTheEndDate()
    {
        return Config.endDate;
    }

    public DateTime? ReturnTheStartDate()
    {
        return Config.startDate;
    }

  
    //public DateTime? ReturnTheStartDate {  get; set; }
    ////complete date
    //public DateTime? ReturnTheEndDate {  get; set; }
    public void Reset()
    {
        DataSource.Dependencies.Clear();
        DataSource.Engineers.Clear();
        DataSource.Tasks.Clear();
    }
}
