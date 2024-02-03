
namespace BlApi;
/// <summary>
/// A main logical interface named IBl that will bring together all the logical layer interfaces.
/// </summary>
public interface IBl
{
    public IEngineer Engineer { get; }
    public IMileStone MileStone { get; }
    public ITask Task { get; }

    public DateTime? GetStartDate();

    public bool GetProjectStatuse();

    /// <summary>
    /// general reset method
    /// </summary>
    void Reset();
}
