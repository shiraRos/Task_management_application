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
        //general reset method
        void Reset();
        //Method for updating  the project start date
        public void ProjectStartDateUpdate(DateTime date);
        //Method for updating  the project end date
        public void ProjectEndDateUpdate(DateTime date);
        //Method for retrieving the project start date
        public  DateTime? ReturnTheStartDate();
       //Method for retrieving the project End date
        public DateTime? ReturnTheEndDate();
    }
}
