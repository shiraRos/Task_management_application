
namespace BO;
/// <summary>
/// class of MilestoneInList
/// </summary>
public class MilestoneInList
{
    public int Id { get; init; }
    public string? Description { get; set; } = null;
    public string? Alias { get; set; } = null;
    public Status? status { get; set; } = null;
    public double? completionPercentage {  get; set; } = null;
}
