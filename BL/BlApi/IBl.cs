
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


    #region Time Management

    /// <summary>
    /// Read-only property of type DateTime representing the current date and time.
    /// </summary>
    public DateTime Clock { get; }

    /// <summary>
    /// Advances time by one hour.
    /// </summary>
    public void AdvanceTimeByHour();

    /// <summary>
    /// Advances time by one day.
    /// </summary>
    public void AdvanceTimeByDay();


    /// <summary>
    /// Advances time by one year.
    /// </summary>
    public void AdvanceTimeByYear();
    /// <summary>
    /// Initializes time to the current date and time.
    /// </summary>
    public void InitializeTime();

    #endregion

}
