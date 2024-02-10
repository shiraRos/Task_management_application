
using BlApi;
using DO;
using System.Data.Common;

namespace BlImplementation;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public IMileStone MileStone => new MileStoneImplementation();

    public ITask Task => new TaskImplementation();

    public void createSchedule()
    {
        DateTime? statDate = null;
        DateTime result;
        bool isDefaultTimeUsed=false;
        TimeSpan defaultTime,defResult,maximumLevelTimeSpan;
        Console.WriteLine("insert project start date:");
        while (statDate == null)
            if (DateTime.TryParse(Console.ReadLine(), out result))
            {
                statDate = result;
            }
            else
            {
                statDate = null;
                Console.WriteLine("unvalid value insert start date again");
            }
        Console.WriteLine("do you want to use default time span for every task insert Y/N");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); 
        if (ans == "Y")
        {
            isDefaultTimeUsed = true;
            Console.WriteLine("insert default time span for the tasks");
            if (TimeSpan.TryParse(Console.ReadLine(), out defResult))
            {
                defaultTime = defResult;
            }
            else
            {
                Console.WriteLine("unvalid value 1 day inserted as default time span");
                defaultTime= TimeSpan.FromDays(1);
            }

        }
        IEnumerable<BO.Task> getLevelTasks = Task.ReadAll(tsk => tsk.Dependencies == null);
        foreach (var item in getLevelTasks)
        {
            
        }



    }

    public bool isProjectStarted()
    {
        return GetStartDate() != null;
    }

    public DateTime? GetStartDate()
    {
        return DalApi.Factory.Get.ReturnTheStartDate();
    }


    /// <summary>
    /// function for delete every value from the data base
    /// </summary>
    public void Reset()
    {
        DalApi.Factory.Get.Reset();
    }
    //ממוש תאריך התחלה
}
