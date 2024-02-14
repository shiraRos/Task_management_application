namespace BO;

/// <summary>
/// Represents a Task in the Business Object (BO) layer.
/// </summary>
public class Task
{
    /// <summary>
    /// Gets or sets the unique identifier for the task.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the alias for the task.
    /// </summary>
    public string? Alias { get; set; } = null;

    /// <summary>
    /// Gets or sets the description of the task.
    /// </summary>
    public string? Description { get; set; } = null;

    /// <summary>
    /// Gets or sets the creation date of the task.
    /// </summary>
    public DateTime? CreateAtDate { get; init; }

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public Status? Status { get; set; } = null;

    /// <summary>
    /// Gets or sets the dependencies associated with the task.
    /// </summary>
    public IEnumerable<TaskInList>? Dependencies { get; set; } = null;

    /// <summary>
    /// Gets or sets the milestone associated with the task.
    /// </summary>
    public MilestoneInTask? Milestone { get; set; } = null;

    /// <summary>
    /// Gets or sets the required effort time for the task.
    /// </summary>
    public TimeSpan? RequiredEffortTime { get; set; } = null;

    /// <summary>
    /// Gets or sets the start date of the task.
    /// </summary>
    public DateTime? StartDate { get; set; } = null;

    /// <summary>
    /// Gets or sets the scheduled date of the task.
    /// </summary>
    public DateTime? ScheduledDate { get; set; } = null;

    /// <summary>
    /// Gets or sets the forecasted date of the task.
    /// </summary>
    public DateTime? ForecastDate { get; set; } = null;

    /// <summary>
    /// Gets or sets the deadline date of the task.
    /// </summary>
    public DateTime? DeadlineDate { get; set; } = null;

    /// <summary>
    /// Gets or sets the completion date of the task.
    /// </summary>
    public DateTime? CompleteDate { get; set; } = null;

    /// <summary>
    /// Gets or sets the deliverables associated with the task.
    /// </summary>
    public string? Deliverables { get; set; } = null;

    /// <summary>
    /// Gets or sets the remarks for the task.
    /// </summary>
    public string? Remarks { get; set; } = null;

    /// <summary>
    /// Gets or sets the engineer associated with the task.
    /// </summary>
    public EngineerInTask? Engineer { get; set; } = null;

    /// <summary>
    /// Gets or sets the complexity level of the task.
    /// </summary>
    public EngineerExperience? ComplexityLevel { get; set; } = null;

    /// <summary>
    /// Overrides the default ToString method to provide a string representation of the Task object.
    /// </summary>
    /// <returns>A string representation of the Task object.</returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
