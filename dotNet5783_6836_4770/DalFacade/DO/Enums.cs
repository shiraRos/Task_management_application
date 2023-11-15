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
    double Cost,
    string? Name= null,
    string? Email= null
);

