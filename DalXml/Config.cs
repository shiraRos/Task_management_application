
using System.Xml.Linq;

namespace Dal;

internal static class Config
{
   
    static string s_data_config_xml = "data-config";
    //id for dependency entity

    internal static int StartDepenId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "startDepenId"); }
    //id for Task entity
    internal static int StartTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "startTaskId"); }

    internal static DateTime? startDate=null;
    internal static DateTime? endDate=null;
}
