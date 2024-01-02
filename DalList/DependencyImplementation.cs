namespace Dal;
using DalApi;
using DO;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

/// <summary>
/// implementation off the dependency entity
/// </summary>
internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// function for create an item of Dependency object
    /// </summary>
    /// <param name="item">the new item values to create</param>
    /// <returns>the id of the new dependency</returns>
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDepenId;
        Dependency dep = new Dependency(newId, item.DependenTask, item.DependensOnTask);
        DataSource.Dependencies.Add(dep);
        return newId;
    }
    /// <summary>
    /// function for delete an item of dependency object
    /// </summary>
    /// <param name="id">the id of the deleted item</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        //if the id of dependency not exists -no need for delete
        if (!(DataSource.Dependencies.Any(dpn => dpn.Id == id)))
            throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        foreach (var x in DataSource.Dependencies)
        {
            if (id == x.Id)
            {
                DataSource.Dependencies.Remove(x);
                break;
            }
        }
    }

    /// <summary>
    /// getting a single item by id
    /// </summary>
    /// <param name="id">getting the id of the present item</param>
    /// <returns>the item of the accepted id</returns>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(dpn => dpn.Id == id);
    }

    /// <summary>
    /// function to read a single object by a condition
    /// </summary>
    /// <param name="filter">a condition for filtering the items</param>
    /// <returns></returns>
    /// <exception cref="DalDoesNotExistException"></exception>
   public Dependency? Read(Func<Dependency, bool> filter) // stage 2
    {
        return DataSource.Dependencies.FirstOrDefault(filter) ?? throw new DalDoesNotExistException("No Dependency found matching the specified condition.");
    }

    /// function for reading all of the objects in the list
    /// <param name="filter"> the condition what to read</param>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        return filter == null ? DataSource.Dependencies.Select(item => item) : DataSource.Dependencies.Where(filter!) ?? throw new DalDoesNotExistException("no dependencies exist");
    }




    public void Update(Dependency item)
    {
        if (!(DataSource.Dependencies.Any(dpn => dpn.Id == item.Id)))
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} does Not exist");
        foreach (var x in DataSource.Dependencies)
        {
            if (item.Id == x.Id)
            {
                //first- remove the existing item
                DataSource.Dependencies.Remove(x);
                //second- adding the new item
                DataSource.Dependencies.Add(item);
            }
        }
    }
    /// <summary>
    /// function for reset all the list of Dependency
    /// </summary>
    public void Reset()
    {

        DO.Dependency[] arrDpnd = DataSource.Dependencies.ToArray();
        for (int i = 0; i < arrDpnd.Length; i++)
        {
            try
            {
                Delete(arrDpnd[i].Id);
            }
            catch (DalDeletionImpossible e) { Console.WriteLine(e); }
        }
    }
}
