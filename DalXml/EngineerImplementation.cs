
using DalApi;
using DO;
using System.Data.Common;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    const string s_engineer = @"engineers";
    const string s_task = @"tasks";
    /// <summary>
    /// the function gets an engineer and save it it the list in XMLSerializer way
    /// </summary>
    /// <param name="item">the engineer to add</param>
    /// <returns>the id ot the added engineer</returns>
    /// <exception cref="DalAlreadyExistsException"></exception>
    public int Create(Engineer item)
    {
        List<Engineer> eng = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        if(eng.FirstOrDefault(en=>en.Id==item.Id)!=null)
        {
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        }
        eng.Add(item);
        XMLTools.SaveListToXMLSerializer(eng, s_engineer);
        return item.Id;
    }

    /// <summary>
    /// function for delete an item of Engineer object
    /// </summary>
    /// <param name="id">the id of the engineer to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    /// <exception cref="DalDeletionImpossible"></exception>
    public void Delete(int id)
    {
        List<Engineer> eng = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        List<DO.Task> tsk = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task);
        //if the id of engineer not exists -no need for delete
        if (!(eng.Any(eng => eng.Id == id)))
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");
        //if the id of engineer exists in the task-cannot be deleted
        if (tsk.Any(tsk => tsk.EngineerId == id))
            throw new DalDeletionImpossible($"Engineer with ID={id} can not be deleted because, he still has task");
        foreach (var x in eng)
        {
            if (id == x.Id)
            {
                eng.Remove(x);
                break;
            }
        }
        XMLTools.SaveListToXMLSerializer(eng, s_engineer);
    }

    /// <summary>
    /// function for get an item of Engineer by checking the id
    /// </summary>
    /// <param name="id">the id ot the engineer to read</param>
    /// <returns>the engineer with the accepted id</returns>
    public Engineer? Read(int id)
    {
        List<Engineer> eng = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
         return  eng.FirstOrDefault(eng => eng.Id == id);
    }

    /// <summary>
    /// function to read a single object by a condition
    /// </summary> 
    /// <param name="filter">filter received from the user</param>
    /// <returns>The engineer we found after filtering</returns> /// <param name="filter">filter received from the user</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> eng = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        return eng.FirstOrDefault(filter);
    }

    /// <summary>
    /// function for reading all of the objects in the list
    /// </summary>
    /// Returning all engineers according to a received filter 
    /// If no filter was received, the function will return all engineers
    /// </summary>
    /// <param name="filter">A condition received from the user is set to null by default</param>
    /// <returns>all the  engineers according to the filter</returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        List<Engineer> eng = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        if (filter == null)
            return eng.Select(item => item);
        else if (eng == null)
            throw new DalDoesNotExistException("this list is not exist");
        return eng.Where(filter);
    }

    /// <summary>
    /// function for reset all the list of Engineer
    /// </summary>
    public void Reset()
    {
        List<Engineer> eng = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        DO.Engineer[] arreng = eng.ToArray();
        for (int i = 0; i < arreng.Length; i++)
        {
            try
            {
                Delete(arreng[i].Id);
            }
            catch (DalDeletionImpossible e) { Console.WriteLine(e); }
        }
    }

    /// <summary>
    /// function for update details of Engineer
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Engineer item)
    {
        List<Engineer> eng = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        //if id doesnt exist-no need for updating
        if (!(eng.Any(eng => eng.Id == item.Id)))
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} does Not exist");
        eng.RemoveAll(x => x.Id == item.Id);
        eng.Add(item);
        XMLTools.SaveListToXMLSerializer(eng, s_engineer);
    }
}
