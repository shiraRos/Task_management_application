

namespace BO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">outhomatic id field</param>
/// <param name="EngineerId">id of the responsible engineer</param>
/// <param name="IsMileston">bool field for mile stone check</param>
/// <param name="StartDate">Task start date</param>
/// <param name="DeadlineDate">Date by which the task must be completed</param>
/// <param name="CompleteDate">Actual task completion date</param>
/// <param name="ScheduledDate">Estimated date when the task was supposed to be completed</param>
/// <param name="RequiredEffortTime">The length of time the task spanned</param>
/// <param name="Deliverables">delivery options</param>
/// <param name="Remarks">Additional notes on the task</param>
/// <param name="ComplexityLevel">The level of experience and difficulty of the engineer</param>
/// <param name="Description">General description of the task</param>
/// <param name="Alias">The nickname given to the task</param>
public class Task
{
    public int Id { get; init; }
    public int? EngineerId { get; set; } = null;
    public bool? IsMileston { get; set; } = false;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? DeadlineDate { get; set; } = null;
    public DateTime? CompleteDate { get; set; } = null;
    public DateTime? ScheduledDate { get; set; } = null;
    public TimeSpan? RequiredEffortTime { get; set; } = null;
    public string? Deliverables { get; set; } = null;
    public string? Remarks { get; set; } = null;
    public EngineerExperience? ComplexityLevel { get; set; } = null;
    public string? Description { get; set; } = null;
    public string? Alias { get; set; } = null;
    public DateTime CreateAtDate { get; init; }
}
