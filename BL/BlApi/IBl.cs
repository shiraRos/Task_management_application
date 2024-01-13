using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
/// <summary>
/// A main logical interface named IBl that will bring together all the logical layer interfaces.
/// </summary>
public interface IBl
{
    public IEngineer Engineer { get; }  
    public IEngineerInTask EngineerInTask { get; }  
    public IMileStone MileStone { get; }
    public IMilestoneInList MilestoneInList { get; }
    public IMilestoneInTask MilestoneInTask { get; }    
    public  ITask Task { get; }
    public ITaskInEngineer TaskInEngineer { get; }
    public ITaskInList TaskInList { get; }

}
