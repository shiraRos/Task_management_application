
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BO
{
    static class Tools
    {
       
        public static string ToStringProperty<T>(this T t)
        {
            // Initialize an empty string to hold the property string representation
            string str = "";

            // Iterate through each property of the object using reflection
            foreach (PropertyInfo item in t!.GetType().GetProperties())
            {
                // Append property name to the string
                str += "\n" + item.Name + ": ";

                // Check if the property is an enumerable type
                bool isEnumerable = item.PropertyType.IsGenericType &&
                                    item.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (isEnumerable)
                {
                    // Get the value of the enumerable property
                    var propertyValue = (IEnumerable<object>?)item.GetValue(t);

                    if (propertyValue != null)
                    {
                        // Check if all elements of the enumerable are simple types
                        if (propertyValue.All(IsSimpleType))
                        {
                            // Concatenate simple types
                            str += string.Join(", ", propertyValue);
                        }
                        else
                        {
                            // Iterate over objects in the enumerable
                            foreach (var subItem in propertyValue)
                            {
                                // Append each object's string representation with indentation
                                str += "\n  " + subItem.ToString();
                            }
                        }
                    }
                }
                else
                {
                    // If the property is not enumerable, append its value to the string
                    str += item.GetValue(t, null);
                }
            }

            // Return the final string representation of the object's properties
            return str;
        }

        // Helper method to check if an object is a simple type
        private static bool IsSimpleType(object obj)
        {
            return obj == null || obj.GetType().IsPrimitive || obj.GetType() == typeof(string) || obj.GetType() == typeof(DateTime);
        }

       
    }
}
