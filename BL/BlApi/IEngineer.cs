
namespace BlApi;
/// <summary>
///  Interface for Engineer
/// </summary>
public interface IEngineer
{
    /// <summary>
    ///  Creates new engineer object 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Create(BO.Engineer item);


    /// <summary>
    ///  Reads engineer object by its ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Engineer? Read(int id);



    /// <summary>
    /// reads all the engineers objects 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);
   


    /// <summary>
    ///  Updates engineer object 
    /// </summary>
    /// <param name="item"></param>
    public void Update(BO.Engineer item);



    /// <summary>
    ///  Deletes engineer object 
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);


}
