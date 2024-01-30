

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
    public IEnumerable<BO.Task?> ReadAll();


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

}
