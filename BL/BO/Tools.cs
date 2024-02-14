
//using System.Reflection;
//using System.Runtime.CompilerServices;


//namespace BO
//{
//    static class Tools
//    {
//        public static string ToStringProperty<T>(this T t)
//        {
//            string str = "";
//            foreach (PropertyInfo item in t!.GetType().GetProperties())
//            {
//                str += "\n" + item.Name + ": " + item.GetValue(t, null);
//                bool isEnumerable = item.PropertyType.IsGenericType &&
//                                    item.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

//                if (isEnumerable)
//                    // Check if the property is of type IEnumerable<>
//                    //if (typeof(IEnumerable<>).IsAssignableFrom(item.PropertyType))
//                {
//                    // If it is, get the value of the property
//                    var propertyValue = (IEnumerable<object>?)item.GetValue(t);


//                    // Check if the value is not null
//                    if (propertyValue != null)
//                    {
//                        // Iterate over the items in the collection and append them to the result string
//                        foreach (var subItem in (IEnumerable<object>)propertyValue)
//                        {
//                            str += "\n  " + subItem.ToString();
//                        }
//                    }
//                }
//            }
//            return str;
//        }


//    }
//}
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

        //public static string ToStringProperty<T>(this T t)
        //{
        //    string str = "";
        //    foreach (PropertyInfo item in t!.GetType().GetProperties())
        //    {
        //        str += "\n" + item.Name + ": ";

        //        bool isEnumerable = item.PropertyType.IsGenericType &&
        //                            item.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

        //        if (isEnumerable)
        //        {
        //            var propertyValue = (IEnumerable<object>?)item.GetValue(t);

        //            if (propertyValue != null)
        //            {
        //                if (propertyValue.All(IsSimpleType))
        //                {
        //                    // Concatenate simple types
        //                    str += string.Join(", ", propertyValue);
        //                }
        //                else
        //                {
        //                    // Iterate over objects
        //                    foreach (var subItem in propertyValue)
        //                    {
        //                        str += "\n  " + subItem.ToString();
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            str += item.GetValue(t, null);
        //        }
        //    }
        //    return str;
        //}

        //private static bool IsSimpleType(object obj)
        //{
        //    return obj == null || obj.GetType().IsPrimitive || obj.GetType() == typeof(string) || obj.GetType() == typeof(DateTime);
        //}
    }
}
