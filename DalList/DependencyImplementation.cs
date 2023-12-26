namespace Dal;
using DalApi;
using DO;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

// implementation off the dependency entity
internal class DependencyImplementation : IDependency
{
    // function for create an item of Dependency object
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDepenId;
        Dependency dep = new Dependency(newId, item.DependenTask, item.DependensOnTask);
        DataSource.Dependencies.Add(dep);
        return newId;
    }
    // function for delete an item of dependency object
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

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(dpn => dpn.Id == id);
    }

    //function to read a single object by a condition
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
    //function for reset all the list of Dependency
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
