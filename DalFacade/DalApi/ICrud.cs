using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    /// <summary>
    /// Gneral interface for the entities
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrud<T> where T : class
    {
        /// <summary>
        /// Creates new entity object in DAL
        /// </summary>
        /// <param name="item">the accepted data</param>
        /// <returns></returns>
        /// 

        int Create(T item); 
        /// <summary>
        /// Reads entity object by its ID
        /// </summary>
        /// <param name="id">the id of the item we want to read</param>
        /// <returns></returns>
        T? Read(int id);


        /// <summary>
        /// Reads entity object by filter
        /// </summary>
        /// <param name="filter">a condition from the user</param>
        /// <returns></returns>
        T? Read(Func<T, bool> filter); // stage 2

        /// <summary>
        /// read all the objects in the entity
        /// </summary>
        /// <param name="filter">a condotion for getting all of the items </param>
        /// <returns></returns>
        IEnumerable<T?> ReadAll(Func<T, bool>? filter = null);//stage 2 


        /// <summary>
        /// Updates entity object
        /// </summary>
        /// <param name="item">getting the item we want to update</param>
        void Update(T item); 
        
        
        
        /// <summary>
        /// Deletes an object by its Id
        /// </summary>
        /// <param name="id">the id of the item to delete</param>
        void Delete(int id); 

        
        
        /// <summary>
        /// Delete all the existing items 
        /// </summary>
        void Reset();
       
    }

}
