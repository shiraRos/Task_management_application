

namespace BO;

/// <summary>
/// Represents a Task associated with an Engineer in the (BO) layer.
/// </summary>
public class TaskInEngineer
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
    /// Overrides the default ToString method to provide a string representation of the TaskInEngineer object.
    /// </summary>
    /// <returns>A string representation of the TaskInEngineer object.</returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
