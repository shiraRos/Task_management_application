

using System.Reflection.Emit;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">outhomatic id field</param>
/// <param name="DependenTask">id of the depends task</param>
/// <param name="DependensOnTask">id of the task that the first task -depends on</param>
public record Dependency
(
    int Id,
    int DependenTask,
    int DependensOnTask
)
{
    //empy builder
    public Dependency() : this(0, 0, 0) { }
    public override string ToString()
    {
        return $"id: {Id}, Dependen Task: {DependenTask}, Dependens On Task: {DependensOnTask}";
    }
}