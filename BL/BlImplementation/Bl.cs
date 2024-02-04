
using BlApi;
using System.Data.Common;

namespace BlImplementation;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public IMileStone MileStone =>new MileStoneImplementation();

    public ITask Task => new TaskImplementation();

    public bool GetProjectStatuse()
    {
        return false;
    }

    public DateTime? GetStartDate()
    {
        return null;
    }


    /// <summary>
    /// function for delete every value from the data base
    /// </summary>
    public void Reset()
    {
       Engineer.Reset();
       Task.Reset();
       // MileStone.Reset();
    }
    //ממוש תאריך התחלה
}
