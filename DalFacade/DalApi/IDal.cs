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


        /// <summary>
        /// general reset method
        /// </summary>
        void Reset();


        /// <summary>
        /// Method for updating  the project start date
        /// </summary>
        /// <param name="date">getting the date to update</param>
        public void ProjectStartDateUpdate(DateTime? date);


        /// <summary>
        /// Method for updating  the project end date
        /// </summary>
        /// <param name="date">getting the end date to update</param>
        public void ProjectEndDateUpdate(DateTime date);



        /// <summary>
        /// Method for retrieving the project start date
        /// </summary>
        /// <returns>the current start date</returns>
        public  DateTime? ReturnTheStartDate();


        /// <summary>
        /// Method for retrieving the project End date
        /// </summary>
        /// <returns>the current end date</returns>
        public DateTime? ReturnTheEndDate();
    }
}
