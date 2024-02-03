

namespace BO;
/// <summary>
/// class of TaskInEngineer
/// </summary>
public class TaskInEngineer
{
    public int Id { get; init; }
    public string? Alias { get; set; } = null;

    public override string ToString()
    { return this.ToStringProperty(); }
}
