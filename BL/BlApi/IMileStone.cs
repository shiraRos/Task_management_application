
namespace BlApi;
/// <summary>
///  Interface for IMileStone
/// </summary>
public interface IMileStone
{
    /// <summary>
    ///  Creates new Schedule in IMileStone
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Create(BO.MileStone item);


    /// <summary>
    /// resds MileStone by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.MileStone? Read(int id);


    /// <summary>
    /// Updates new nilestone
    /// </summary>
    /// <param name="item"></param>
    public void Update(BO.Engineer item);

}
