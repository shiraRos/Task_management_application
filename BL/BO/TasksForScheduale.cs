
namespace BO;

/// <summary>
/// Represents a Task for scheduale in the Business Object (BO) layer.
/// </summary>
public class TasksForScheduale
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
    /// Gets or sets the engineer id 
    /// </summary>
    public int? EngineerId {  get; set; }

    /// <summary>
    /// Gets or sets the name of the engineer.
    /// </summary>
    public string? EngineerName {  get; set; }

    /// <summary>
    /// Gets or sets the dependencies associated with the task.
    /// </summary>
    public IEnumerable<TaskInList>? Dependencies { get; set; } = null;

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public Status TaskStaus { get; set; }
    /// <summary>
    /// Gets or sets the startDate of the task.
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Gets or sets the endDate of the task.
    /// </summary>
    public DateTime EndDate { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }

}
