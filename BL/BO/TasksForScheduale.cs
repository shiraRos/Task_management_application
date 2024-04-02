
namespace BO;

public class TasksForScheduale
{
    public int Id { get; set; }
    public string? Alias { get; set; }
    public int? EngineerId {  get; set; }
    public string? EngineerName {  get; set; }
    public Status TaskStaus { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
