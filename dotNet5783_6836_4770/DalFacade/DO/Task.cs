
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
   TimeSpan? RequiredEffortTime = null,
   string? Deliverables = null,
   string? Remarks = null,
   int? ComplexityLevel = null,
   string? Description = null,
   string? Alias = null
)
{
    public Task():this(0,0) { }
    public DateTime CreateAtDate => DateTime.Now;
}
