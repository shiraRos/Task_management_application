namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// implementation of the Engineer entity
/// </summary>
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
    
    /// <summary>
    /// function for delete an item of Engineer object
    /// </summary>
    /// <param name="id">getting the id of the deleted item</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    /// <exception cref="DalDeletionImpossible"></exception>
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
    /// <summary>
    /// function for get an item of Engineer by checking the id
    /// </summary>
    /// <param name="id">id of the item the user asked for</param>
    /// <returns>the item with the accepted id</returns>
    public Engineer? Read(int id)
    {
            return DataSource.Engineers.FirstOrDefault(eng => eng.Id ==id) ?? throw new DalDoesNotExistException($" engineer with id:{id} is not found ");
    }

    /// <summary>
    /// function to read a single object by a condition
    /// </summary>
    /// <param name="filter">a condition from the user</param>
    /// <returns>the item wich suit the accepted filter</returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public Engineer? Read(Func<Engineer, bool> filter) // stage 2
    {
        return DataSource.Engineers.FirstOrDefault(filter) ?? throw new DalDoesNotExistException("No engineer found matching the specified condition.");
    }

    /// <summary>
    /// function for reading all of the objects in the list
    /// </summary>
    /// <param name="filter">a condition for group of items</param>
    /// <returns>all of the items in the list who suit the condition</returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else if (DataSource.Engineers == null)
            throw new DalDoesNotExistException("this list is not exist");
        return DataSource.Engineers.Where(filter);
    }



    /// <summary>
    /// function for update details of Engineer
    /// </summary>
    /// <param name="item">getting the updated item and replace it</param>
    /// <exception cref="DalDoesNotExistException"></exception>
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
    
    /// <summary>
    /// function for reset all the list of Engineer
    /// </summary>
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
