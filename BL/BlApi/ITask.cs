

using BO;

namespace BlApi;
/// <summary>
///  Interface for Task
/// </summary>
public interface ITask
{
    /// <summary>
    /// Creates new task object 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Create(BO.Task item);


    /// <summary>
    ///Reads task object by its ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Task? Read(int id);


    /// <summary>
    ///  Reads all tasks objects  
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null);
    /// <summary>
    /// Mehod reads all the dependencies of the specific task
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IEnumerable<BO.Task> ReadAllDependentsTasks(int id);


    /// <summary>
    ///  Updates  task object 
    /// </summary>
    /// <param name="item"></param>
    public void Update(BO.Task item);


    /// <summary>
    /// Deletes  task object 
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);

    
    /// <summary>
    /// function for filtering and getting all of the not taken tasks
    /// </summary>
    /// <returns>all tasks in TaskInEngineer format</returns>
    public IEnumerable<TaskInEngineer> GetAvailableTasks(BO.EngineerExperience exp);

    /// <summary>
    /// Retrieves all available engineers based on the provided task ID and experience level.
    /// </summary>
    /// <param name="tskId">The ID of the task.</param>
    /// <param name="taskLenel">The experience level required for the task.</param>
    /// <returns>An IEnumerable of EngineerInTask objects representing available engineers.</returns>
    public IEnumerable<EngineerInTask> GetAllAvialbleEngineers(int tskId, EngineerExperience? taskLenel);


    /// <summary>
    /// Retrieves all task dependency options.
    /// </summary>
    /// <returns>An IEnumerable of TaskInList objects representing all task dependency options.</returns>
    public IEnumerable<TaskInList> GetAllDependenciesOptions();

    /// <summary>
    /// Retrieves all tasks for Gantt chart.
    /// </summary>
    /// <returns>An IEnumerable of TasksForScheduale objects representing all tasks for the Gantt chart.</returns>
    public IEnumerable<TasksForScheduale> GetAllTaskForGantt();

    /// <summary>
    /// Retrieves available tasks for a given engineer ID.
    /// </summary>
    /// <param name="Id">The ID of the engineer.</param>
    /// <returns>An IEnumerable of TaskInEngineer objects representing available tasks for the specified engineer.</returns>
    public IEnumerable<TaskInEngineer> GetAvailableTasksForEngineer(int Id);



    /// <summary>
    /// Retrieves all tasks for list with optional filtering.
    /// </summary>
    /// <param name="filter">Optional filter function to apply to the tasks.</param>
    /// <returns>An IEnumerable of TaskForlList objects representing tasks that match the filter criteria, if provided.</returns>
    public IEnumerable<TaskForlList> GetAllTasksForList(Func<BO.TaskForlList, bool>? filter = null);


}
