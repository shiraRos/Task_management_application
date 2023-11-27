namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

// implementation of the Engineer entity
public class EngineerImplementation : IEngineer
{
    // function for create an item of Engineer object
    public int Create(Engineer item)
    {
        //checking if the id is new
        foreach (var x in DataSource.Engineers)
        {
            if(item.Id==x.Id) {
                throw new Exception($"Engineer with ID={item.Id}is already exists");
            }
        }
        //adding the item
        DataSource.Engineers.Add(item);
        return item.Id;
    }
    // function for delete an item of Engineer object
    public void Delete(int id)
    {
        //if the id of engineer not exists -no need for delete
        if (!(DataSource.Engineers.Exists(eng => eng.Id == id)))
            throw new Exception($"Engineer with ID={id} does Not exist");
        //if the id of engineer exists in the task-cannot be deleted
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
    // function for get an item of Engineer by checking the id
    public Engineer? Read(int id)
    {
            return DataSource.Engineers.Find(eng => eng.Id ==id);
    }
    //function  for get all the list of Engineer items
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }
    //function for update details of Engineer
    public void Update(Engineer item)
    {
        //if id doesnt exist-no need for updating
        if(!(DataSource.Engineers.Exists(eng => eng.Id == item.Id)))
          throw new Exception($"Engineer with ID={item.Id} does Not exist");
        foreach (var x in DataSource.Engineers)
        {
            if (item.Id == x.Id)
            {
                //first- remove the existing item
                DataSource.Engineers.Remove(x);
                //second- adding the new item
                DataSource.Engineers.Add(item);    
            }
        }
    }
    //function for reset all the list of Engineer
    public void Reset()
    {
        DO.Engineer[] arreng = DataSource.Engineers.ToArray();
        for (int i = 0; i < arreng.Length; i++)
        {
            try
            {
                Delete(arreng[i].Id);
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}
