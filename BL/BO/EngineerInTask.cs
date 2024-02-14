namespace BO;

/// <summary>
/// Represents an Engineer associated with a Task in the (BO) layer.
/// </summary>
public class EngineerInTask
{
    /// <summary>
    /// Gets or sets the unique identifier for the engineer.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the engineer.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Overrides the default ToString method to provide a string representation of the EngineerInTask object.
    /// </summary>
    /// <returns>A string representation of the EngineerInTask object.</returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
