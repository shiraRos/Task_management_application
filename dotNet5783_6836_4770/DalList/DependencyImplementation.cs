
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int newId= DataSource.Config.NextDepenId ;
        Dependency dep = new Dependency(newId,item.DependenTask,item.DependensOnTask);
        DataSource.Dependencies.Add(dep);
        return newId;   
    }

    public void Delete(int id)
    {
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
                DataSource.Dependencies.Remove(x);
                DataSource.Dependencies.Add(item);
            }
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
