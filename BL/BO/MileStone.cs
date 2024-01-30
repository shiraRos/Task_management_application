
namespace BO;
/// <summary>
/// class of MileStone
/// </summary>
public class MileStone
{
    public int Id { get; init; }
    public string? Alias { get; set; } = null;
    public string? Description { get; set; } = null;
    public DateTime CreateAtDate { get; init; }
    public Status? Status { get; set; } = null;
    public DateTime? ForecastDate { get; set; } = null;
    public DateTime? DeadlineDate { get; set; } = null;
    public DateTime? CompleteDate { get; set; } = null;
    public double? completionPercentage { get; set; } = null;
    public string? Remarks { get; set; } = null;
    public IEnumerable<TaskInList>? Dependencies { get; set; } = null;
}
