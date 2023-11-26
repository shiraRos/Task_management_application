
namespace DO;
public record Task
(
   int Id,
   int EngineerId,
   bool IsMileston=false,
  // DateTime CreateAtDate,
   DateTime? StartDate=null,
   DateTime? DeadlineDate = null,
   DateTime? CompleteDate = null,
   DateTime? ScheduledDate = null,
  // DateTime? ForecastDate = null,
   TimeSpan? RequiredEffortTime = null,
   string? Deliverables = null,
   string? Remarks = null,
   EngineerExperience? ComplexityLevel = null,
   string? Description = null,
   string? Alias = null
)
{
    public Task():this(0,0) { }
    public DateTime CreateAtDate => DateTime.Now;

    public override string ToString()
    {
        return $"id:{Id}, Engineer Id: {EngineerId}, Is Mileston: {IsMileston}, Start Date: {StartDate}, Deadline Date: {DeadlineDate}, Complete Date: {CompleteDate} , Scheduled Date: {ScheduledDate}, Required Effort Time: {RequiredEffortTime},Deliverables: {Deliverables}, Remarks: {Remarks} , ComplexityLevel: {ComplexityLevel}, Description: {Description}, Alias: {Alias} ";
    }                                                                                                      
}                                                                                                          
