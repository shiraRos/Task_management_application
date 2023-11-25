namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        foreach (var x in DataSource.Engineers)
        {
            if(item.Id==x.Id) {
                throw new Exception($"Engineer with ID={item.Id} does Not exist");
            }
        }
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        if (!(DataSource.Engineers.Exists(eng => eng.Id == id)))
            throw new Exception($"Engineer with ID={id} does Not exist");
        if( DataSource.Tasks.Exists(tsk=>tsk.EngineerId==id) )
            throw new Exception($"Engineer with ID={id} can not be deleted because, he still has task");
        foreach (var x in DataSource.Engineers)
        {
            if (id == x.Id)
            {
                DataSource.Engineers.Remove(x);
            }
        }
    }

    public Engineer? Read(int id)
    {
            return DataSource.Engineers.Find(eng => eng.Id ==id);
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        if(!(DataSource.Engineers.Exists(eng => eng.Id == item.Id)))
          throw new Exception($"Engineer with ID={item.Id} does Not exist");
        foreach (var x in DataSource.Engineers)
        {
            if (item.Id == x.Id)
            {
                DataSource.Engineers.Remove(x);
                DataSource.Engineers.Add(item);    
            }
        }
    }
    public void Reset()
    {
        foreach (var item in DataSource.Engineers)
        {
            try
            {
                Delete(item.Id);
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}
