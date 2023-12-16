namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

// implementation of the Engineer entity
internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        // Checking if the id is new
        if (DataSource.Engineers.FirstOrDefault(x => x.Id == item.Id) != null)
        {
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        }

        // Adding the item
        DataSource.Engineers.Add(item);
        return item.Id;
    }
    // function for delete an item of Engineer object
    public void Delete(int id)
    {
        //if the id of engineer not exists -no need for delete
        if (!(DataSource.Engineers.Any(eng => eng.Id == id)))
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");
        //if the id of engineer exists in the task-cannot be deleted
        if( DataSource.Tasks.Any(tsk=>tsk.EngineerId==id) )
            throw new DalDeletionImpossible($"Engineer with ID={id} can not be deleted because, he still has task");
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
            return DataSource.Engineers.FirstOrDefault(eng => eng.Id ==id);
    }

    //function to read a single object by a condition
    public Engineer? Read(Func<Engineer, bool> filter) // stage 2
    {
        return DataSource.Engineers.FirstOrDefault(filter) ?? throw new DalDoesNotExistException("No engineer found matching the specified condition.");
    }

    /// function for reading all of the objects in the list
    /// <param name="filter"> the condition what to read</param>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else if (DataSource.Engineers == null)
                  throw new DalDoesNotExistException("this list is not exist");
            return DataSource.Engineers.Where(filter);
    }



    //function for update details of Engineer
    public void Update(Engineer item)
    {
        //if id doesnt exist-no need for updating
        if(!(DataSource.Engineers.Any(eng => eng.Id == item.Id)))
          throw new DalDoesNotExistException($"Engineer with ID={item.Id} does Not exist");
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
            catch (DalDeletionImpossible e) { Console.WriteLine(e); }
        }
    }
}
