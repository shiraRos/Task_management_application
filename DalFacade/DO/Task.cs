
namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">outhomatic id field</param>
/// <param name="EngineerId">id of the responsible engineer</param>
/// <param name="IsMileston">bool field for mile stone check</param>
/// <param name="StartDate">Task start date</param>
/// <param name="DeadlineDate">Date by which the task must be completed</param>
/// <param name="CompleteDate">Actual task completion date</param>
/// <param name="ScheduledDate">Estimated date when the task was supposed to be completed</param>
/// <param name="RequiredEffortTime">The length of time the task spanned</param>
/// <param name="Deliverables">delivery options</param>
/// <param name="Remarks">Additional notes on the task</param>
/// <param name="ComplexityLevel">The level of experience and difficulty of the engineer</param>
/// <param name="Description">General description of the task</param>
/// <param name="Alias">The nickname given to the task</param>
public record Task
(
   int Id,
   int? EngineerId=null,
   bool? IsMileston=false,
   DateTime? StartDate=null,
   DateTime? DeadlineDate = null,
   DateTime? CompleteDate = null,
   DateTime? ScheduledDate = null,
   TimeSpan? RequiredEffortTime = null,
   string? Deliverables = null,
   string? Remarks = null,
   EngineerExperience? ComplexityLevel = null,
   string? Description = null,
   string? Alias = null
)
{
    //empy builder
    public Task():this(0) { }
    public DateTime CreateAtDate => DateTime.Now;

    public override string ToString()
    {
        return $"id:{Id}, Engineer Id: {EngineerId}, Is Mileston: {IsMileston}, Start Date: {StartDate}, Deadline Date: {DeadlineDate}, Complete Date: {CompleteDate} , Scheduled Date: {ScheduledDate}, Required Effort Time: {RequiredEffortTime},Deliverables: {Deliverables}, Remarks: {Remarks} , ComplexityLevel: {ComplexityLevel}, Description: {Description}, Alias: {Alias} ";
    }                                                                                                      
}                                                                                                          
