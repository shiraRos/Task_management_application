namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Level"></param>
/// <param name="Cost"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
public record Engineer
(
    int Id,
    int Level,
    double? Cost = null,
    string? Name = null,
    string? Email = null
);
public record Dependency
(
    int Id,
    int DependenTask,
    int DependensOnTask
);
public record Task
(
   int Id ,
   bool IsMileston,
   DateTime CreateAtDate,
   DateTime StartDate,
   DateTime ScheduledDate,
   DateTime DeadlineDate,
   DateTime CompleteDate,
   TimeSpan RequiredEffortTime,
   int EngineerId,
   string? Deliverables=null,
   string? Remarks=null,
   int? CopmlexityLevel=null,
   string? Description=null,
   string? Alias=null
);
