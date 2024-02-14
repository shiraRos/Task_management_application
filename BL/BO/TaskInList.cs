
namespace BO;

/// <summary>
/// Represents a Task in a list in the Business Object (BO) layer.
/// </summary>
public class TaskInList
{
    /// <summary>
    /// Gets or sets the unique identifier for the task.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the description of the task.
    /// </summary>
    public string? Description { get; set; } = null;

    /// <summary>
    /// Gets or sets the alias for the task.
    /// </summary>
    public string? Alias { get; set; } = null;

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public Status? Status { get; set; } = null;

    /// <summary>
    /// Overrides the default ToString method to provide a string representation of the TaskInList object.
    /// </summary>
    /// <returns>A string representation of the TaskInList object.</returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}

