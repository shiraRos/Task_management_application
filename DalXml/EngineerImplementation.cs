
using DalApi;
using DO;
using System.Data.Common;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    const string s_engineer = @"engineers";
    const string s_task = @"tasks";
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

    // function for delete an item of Engineer object
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
            }
        }
        //XMLTools.SaveListToXMLSerializer(tsk, s_task);
        XMLTools.SaveListToXMLSerializer(eng, s_engineer);
    }

    // function for get an item of Engineer by checking the id
    public Engineer? Read(int id)
    {
        List<Engineer> eng = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
         return  eng.FirstOrDefault(eng => eng.Id == id);
    }

    //function to read a single object by a condition
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> eng = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
       return eng.FirstOrDefault(filter) ?? throw new DalDoesNotExistException("No engineer found matching the specified condition.");
    }

    /// function for reading all of the objects in the list
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        List<Engineer> eng = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        if (filter == null)
            return eng.Select(item => item);
        else if (eng == null)
            throw new DalDoesNotExistException("this list is not exist");
        return eng.Where(filter);
    }

    //function for reset all the list of Engineer
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

    //function for update details of Engineer
    public void Update(Engineer item)
    {
        List<Engineer> eng = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer);
        //if id doesnt exist-no need for updating
        if (!(eng.Any(eng => eng.Id == item.Id)))
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} does Not exist");
        //foreach (var x in eng)
        //{
        //    if (item.Id == x.Id)
        //    {
        //        //first- remove the existing item
        //        eng.Remove(x);
        //        //second- adding the new item
        //        eng.Add(item);
        //    }
        //}
        eng.RemoveAll(x => x.Id == item.Id);
        eng.Add(item);
        XMLTools.SaveListToXMLSerializer(eng, s_engineer);
    }
}
