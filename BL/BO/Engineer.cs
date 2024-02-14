
namespace BO;

/// <summary>
/// Represents an Engineer in the Business Object (BO) layer.
/// </summary>
public class Engineer
{
    /// <summary>
    /// Gets or sets the unique identifier for the engineer.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the engineer.
    /// </summary>
    public string? Name { get; set; } = null;

    /// <summary>
    /// Gets or sets the email address of the engineer.
    /// </summary>
    public string? Email { get; set; } = null;

    /// <summary>
    /// Gets or sets the experience level of the engineer.
    /// </summary>
    public EngineerExperience? Level { get; set; } = null;

    /// <summary>
    /// Gets or sets the cost associated with the engineer.
    /// </summary>
    public double? Cost { get; set; } = null;

    /// <summary>
    /// Gets or sets the task associated with the engineer.
    /// </summary>
    public TaskInEngineer? Task { get; set; } = null;

    /// <summary>
    /// Overrides the default ToString method to provide a string representation of the Engineer object.
    /// </summary>
    /// <returns>A string representation of the Engineer object.</returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}


