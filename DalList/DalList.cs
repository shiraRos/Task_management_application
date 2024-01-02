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
