namespace Dal;
using DalApi;
using DO;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// implementation off the dependency entity
public class DependencyImplementation : IDependency
{
    // function for create an item of Dependency object
    public int Create(Dependency item)
    { 
        int newId= DataSource.Config.NextDepenId ;
        Dependency dep = new Dependency(newId,item.DependenTask,item.DependensOnTask);
        DataSource.Dependencies.Add(dep);
        return newId;   
    }
    // function for delete an item of dependency object
    public void Delete(int id)
    {
        //if the id of dependency not exists -no need for delete
        if (!(DataSource.Dependencies.Exists(dpn => dpn.Id == id)))
            throw new Exception($"Dependency with ID={id} does Not exist");
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
        return DataSource.Dependencies.Find(dpn => dpn.Id == id);
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        if (!(DataSource.Dependencies.Exists(dpn => dpn.Id == item.Id)))
            throw new Exception($"Dependency with ID={item.Id} does Not exist");
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
        foreach (var item in DataSource.Dependencies)
        {
            try
            {
                Delete(item.Id);
            }
            catch(Exception e) { Console.WriteLine(e); }
        }
    }

    public void Reset()
    {

        DO.Dependency[] arrDpnd = DataSource.Dependencies.ToArray();
        for(int i=0;i<arrDpnd.Length;i++)
        {
            try
            {
                Delete(arrDpnd[i].Id);
            }
            catch(Exception e) { Console.WriteLine(e); }
        }
    }
}
