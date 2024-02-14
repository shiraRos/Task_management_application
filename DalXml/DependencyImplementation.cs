
using DalApi;
using DO;
using System.Data.Common;
using System.Xml.Linq;

namespace Dal;

internal class DependencyImplementation : IDependency
{

    const string s_dependencies = "dependencys";

    /// <summary>
    /// the function gets xelemnt type item and turn it into a dependency type item
    /// </summary>
    /// <param name="d">the accepted xelement</param>
    /// <returns>new dependency</returns>
    /// <exception cref="DalXmlFormatException">in case of an error in xelement</exception>
    static Dependency? createDependencyFromXElement(XElement d)
    {
        return new Dependency
        {
            Id = d.ToIntNullable("Id") ?? throw new DalXmlFormatException("id"),
            DependenTask = d.ToIntNullable("DependenTask") ?? throw new DalXmlFormatException("id"),
            DependensOnTask = d.ToIntNullable("DependensOnTask") ?? throw new DalXmlFormatException("id")
        };
    }
    /// <summary>
    /// function for getting an item and save it as a xelement
    /// </summary>
    /// <param name="item">the data of the dependency from the user</param>
    /// <returns>the id of the new dependency we create</returns>
    public int Create(Dependency item)
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencies);
        int elemId = Config.StartDepenId;
        XElement dependElemnt = new XElement("Dependency",
          new XElement("Id", elemId),
          new XElement("DependenTask", item.DependenTask),
          new XElement("DependensOnTask", item.DependensOnTask)
          );
        rootDep.Add(dependElemnt);
        XMLTools.SaveListToXMLElement(rootDep, s_dependencies);
        return elemId;

    }

    /// <summary>
    /// function for deleting an item by its id
    /// </summary>
    /// <param name="id">the code of the dependency to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencies);
        XElement? dep = (from dp in rootDep.Elements()
                         where (int?)dp.Element("Id") == id
                         select dp).FirstOrDefault() ?? throw new DalDoesNotExistException("missing Id");
        dep.Remove();
        XMLTools.SaveListToXMLElement(rootDep, s_dependencies);
    }

    /// <summary>
    /// reading a dependency by its id
    /// </summary>
    /// <param name="id">the id of the dependency to read</param>
    /// <returns>The dependency we found after filtering</returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public Dependency? Read(int id)
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencies);
        return (from dp in rootDep?.Elements()
                where dp.ToIntNullable("Id") == id
                select (Dependency?)createDependencyFromXElement(dp)).FirstOrDefault() ?? throw new DalDoesNotExistException("missing id");
    }


    /// <summary>
    /// A function to return a dependency according to a filter received from the user
    /// </summary>
    /// <param name="filter">filter received from the user</param>
    /// <returns>The dependency we found after filtering</returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencies);
        return (from dp in rootDep?.Elements()
                let dependency = createDependencyFromXElement(dp)
                where dependency != null && filter(dependency)
                select (Dependency?)dependency).FirstOrDefault() ?? throw new DalDoesNotExistException("matching dependency not found");
    }


    /// <summary>
    /// Returning all dependencies according to a received filter 
    /// If no filter was received, the function will return all dependencies
    /// </summary>
    /// <param name="filter">A condition received from the user is set to null by default</param>
    /// <returns>all the  dependencies according to the filter</returns>
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencies);
        if (filter != null)
        {
            return from dp in rootDep.Elements()
                   let doDep = createDependencyFromXElement(dp)
                   where filter(doDep)
                   select (Dependency?)doDep;
        }
        else
        {
            return from dp in rootDep.Elements()
                   select createDependencyFromXElement(dp);
        }
    }

    /// <summary>
    /// function for reset all the list of Dependency
    /// </summary>
    public void Reset()
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencies);

        // Clear all elements
        rootDep.Elements().Remove();

        // Save the modified XML
        XMLTools.SaveListToXMLElement(rootDep, s_dependencies);
    }

    /// <summary>
    /// updating the saved dependency
    /// </summary>
    /// <param name="item">the id of the dependency to update</param>

    public void Update(Dependency item)
    {
        //calling the other crud functions
        Delete(item.Id);
        //creating a new dependency in xxelement way
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencies);
        XElement dependElemnt = new XElement("Dependency",
          new XElement("Id", item.Id),
          new XElement("DependenTask", item.DependenTask),
          new XElement("DependensOnTask", item.DependensOnTask)
          );
        rootDep.Add(dependElemnt);
        XMLTools.SaveListToXMLElement(rootDep, s_dependencies);
    }
}
