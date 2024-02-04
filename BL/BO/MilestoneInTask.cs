

namespace BO;
/// <summary>
/// class of MilestoneInTask
/// </summary>
public class MilestoneInTask
{
    public int Id { get; init; }
    public string? Alias { get; set; } = null;

    public override string ToString()
    { return this.ToStringProperty(); }
}
