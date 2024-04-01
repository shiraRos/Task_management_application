using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PL;

public class ConvertIdToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Update";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
internal class IsEnableConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((int)value != 0)
        {
            return false; //Visibility.Collapsed;
        }
        else
        {
            return true;
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


internal class IsEnableConverterTask : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((int)value != 0)
            return true; //Visibility.Collapsed;
        return false;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

//internal class ConvertYearToColor : IValueConverter
//{
//    //convert from source property type to target property type
//    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//    {
        
//    }
//    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        throw new NotImplementedException();
//    }
//}

internal class ConvertTaskStatusToForegroundColor : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string status = (string)value;
        switch(status) {
            case "None":
                return Brushes.White;
            case "TaskIsScheduled":
                return Brushes.Beige;
            case "TaskIsRunning":
                return Brushes.Orange;
            case "TaskIsCompleted":
                return Brushes.Green;
            case "TaskIsInRisk":
                return Brushes.Red;
            default:
                return Brushes.Black;
        }

    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
