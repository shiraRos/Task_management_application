namespace Dal;
using DalApi;
sealed public class DalList : IDal
{
    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public ITask Task =>  new TaskImplementation();
    public DateTime? StartDate {  get; set; }
    //complete date
    public DateTime? CompleteDate {  get; set; }
    public void Reset()
    {
        DataSource.Dependencies.Clear();
        DataSource.Engineers.Clear();
        DataSource.Tasks.Clear();
    }
}
