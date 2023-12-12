using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    /// <summary>
    /// Dal Interface for wrapping all the entities
    /// </summary>
    public interface IDal
    {
        IEngineer Engineer { get; }
        IDependency Dependency { get; }
        ITask Task { get; }
    }
}
