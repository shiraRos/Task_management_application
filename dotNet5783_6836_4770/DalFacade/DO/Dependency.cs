

using System.Reflection.Emit;
using System.Xml.Linq;

namespace DO;
public record Dependency
(
    int Id,
    int DependenTask,
    int DependensOnTask
)
{
    public Dependency() : this(0, 0, 0) { }
    public override string ToString()
    {
        return $"id: {Id}, Dependen Task: {DependenTask}, Dependens On Task: {DependensOnTask}";
    }
}