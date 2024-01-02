
using DalApi;
using DO;
using System.Data.Common;
using System.Xml.Linq;

namespace Dal;

internal class DependencyImplementation : IDependency
{

    const string s_dependencies = "dependencys";

    static Dependency? createDependencyFromXElement(XElement d)
    {
        return new Dependency
        {
            Id = d.ToIntNullable("Id") ?? throw new DalXmlFormatException("id"),
            DependenTask = d.ToIntNullable("DependenTask") ?? throw new DalXmlFormatException("id"),
            DependensOnTask = d.ToIntNullable("DependensOnTask") ?? throw new DalXmlFormatException("id")
        };
    }
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

    public void Delete(int id)
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencies);
        XElement? dep = (from dp in rootDep.Elements()
                         where (int?)dp.Element("Id") == id
                         select dp).FirstOrDefault() ?? throw new DalDoesNotExistException("missing Id");
        dep.Remove();
        XMLTools.SaveListToXMLElement(rootDep, s_dependencies);
    }

    public Dependency? Read(int id)
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencies);
        return (from dp in rootDep?.Elements()
                where dp.ToIntNullable("Id") == id
                select (Dependency?)createDependencyFromXElement(dp)).FirstOrDefault() ?? throw new DalDoesNotExistException("missing id");
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencies);
        return (from dp in rootDep?.Elements()
                let dependency = createDependencyFromXElement(dp)
                where dependency != null && filter(dependency)
                select (Dependency?)dependency).FirstOrDefault() ?? throw new DalDoesNotExistException("matching dependency not found");
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
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

    //function for reset all the list of Dependency
    public void Reset()
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencies);

        // Clear all elements
        rootDep.Elements().Remove();

        // Save the modified XML
        XMLTools.SaveListToXMLElement(rootDep, s_dependencies);
    }

    public void Update(Dependency item)
    {
        //calling the other crud functions
        Delete(item.Id);
        Create(item);
    }
}
