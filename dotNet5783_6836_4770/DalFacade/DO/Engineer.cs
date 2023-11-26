
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
        EngineerExperience Level,
        double? Cost = null,
        string? Name = null,
        string? Email = null
    )
    {
     public Engineer() : this(0, 0) { }
    public override string ToString()
    {
        return $"id: {Id}, level: {Level}, cost: {Cost}, name: {Name}, email: {Email}";
    }
    }
