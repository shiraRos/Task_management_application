
namespace BO;
/// <summary>
/// class Engineer 
/// </summary>
public class Engineer
{
   public int Id {  get; init; }
   public string? Name { get; set; } = null;
    public string? Email { get; set; } = null;
    public EngineerExperience? Level { get; set; } = null;
    public double? Cost { get; set; } = null;
    public TaskInEngineer? Task { get; set; } = null;

    public override string ToString()
    { return this.ToStringProperty(); }
}

