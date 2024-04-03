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
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status status;
        if (!Enum.TryParse(value.ToString(), out status))
        {
            return Brushes.Black;
        }
        //string status = (string)value;
        switch ((int)status)
        {
            case 0:
                return Brushes.RosyBrown;
            case 1:
                return Brushes.Blue;
            case 2:
                return Brushes.Orange;
            case 3:
                return Brushes.Green;
            case 4:
                return Brushes.Red;
            default:
                return Brushes.Black;
        }


    }


//    static Dictionary<BO.Status, Color> _statusToColorMap = new Dictionary<BO.Status, Color>
//{
//  { BO.Status.Unscheduled, Colors.Yellow },
//  { BO.Status.Scheduled, Colors.Beige },
//  { BO.Status.OnTrack, Colors.Orange },
//  { BO.Status.Done, Colors.Green },
//  { BO.Status.None, Colors.Red }

//};

//    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        BO.Status status;
//        if (!Enum.TryParse(value.ToString(), out status))
//        {
//            return Brushes.Black;
//        }

//        if (_statusToColorMap.TryGetValue(status, out Color color))
//        {
//            return new SolidColorBrush(color);
//        }
//        else
//        {
//            return Brushes.Black;
//        }
//    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
