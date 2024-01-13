
namespace BO;
/// <summary>
/// class ofTaskilist
/// </summary>
public class TaskInList
{
    public int Id { get; init; }
    public string? Description { get; set; } = null;
    public string? Alias { get; set; } = null;
    public Status? status { get; set; } = null;
}
