
namespace BlApi;
/// <summary>
/// A main logical interface named IBl that will bring together all the logical layer interfaces.
/// </summary>
public interface IBl
{
    /// <summary>
    /// public interface of the Engineer
    /// </summary>
    public IEngineer Engineer { get; }
    public IMileStone MileStone { get; }
    /// <summary>
    /// public interface of the Task
    /// </summary>
    public ITask Task { get; }
    /// <summary>
    /// method for get the start date of the task
    /// </summary>
    /// <returns>the start date</returns>
    public DateTime? GetStartDate();

    /// <summary>
    /// method for checking if the project has started
    /// </summary>
    /// <returns>true or false acording to the result</returns>
    public bool isProjectStarted();

    /// <summary>
    /// method for create The Schedule of  the tasks
    /// </summary>
    public void createSchedule();

    /// <summary>
    /// general reset method
    /// </summary>
    void Reset();


    void InitializeDB();
}
