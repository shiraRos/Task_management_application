
namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">the ingneer id</param>
/// <param name="Level">the level of prffesional of the engineer</param>
/// <param name="Cost">the salary of the engineer</param>
/// <param name="Name">the engineer name</param>
/// <param name="Email">the engineer Email</param>
public record Engineer
    (
        int Id,
        EngineerExperience? Level=null,
        double? Cost = null,
        string? Name = null,
        string? Email = null
    )
    {
    //empty builder
     public Engineer() : this(0) { }
    public override string ToString()
    {
        return $"id: {Id}, level: {Level}, cost: {Cost}, name: {Name}, email: {Email}";
    }
    }
