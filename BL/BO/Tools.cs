﻿
using System.Reflection;
using System.Runtime.CompilerServices;


namespace BO
{
    static class Tools
    {
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t!.GetType().GetProperties())
            {
                str += "\n" + item.Name + ": " + item.GetValue(t, null);
                bool isEnumerable = item.PropertyType.IsGenericType &&
                                    item.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (isEnumerable)
                    // Check if the property is of type IEnumerable<>
                    //if (typeof(IEnumerable<>).IsAssignableFrom(item.PropertyType))
                {
                    // If it is, get the value of the property
                    var propertyValue = (IEnumerable<object>?)item.GetValue(t);


                    // Check if the value is not null
                    if (propertyValue != null)
                    {
                        // Iterate over the items in the collection and append them to the result string
                        foreach (var subItem in (IEnumerable<object>)propertyValue)
                        {
                            str += "\n  " + subItem.ToString();
                        }
                    }
                }
            }
            return str;
        }


    }
}