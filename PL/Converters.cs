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
                return Brushes.White;
            default:
                return Brushes.Black;
        }


    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

internal class ConvertTaskStatusToBackgroundColor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status status;
        if (!Enum.TryParse(value.ToString(), out status))
        {
            return Brushes.White;
        }
        //string status = (string)value;
        switch ((int)status)
        {
            case 0:
                return Brushes.White;
            case 1:
                return Brushes.Blue;
            case 2:
                return Brushes.Orange;
            case 3:
                return Brushes.Green;
            case 4:
                return Brushes.White;
            default:
                return Brushes.White;
        }


    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
    public class ConvertVisibiltyByStartTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BlApi.Factory.Get().isProjectStarted()? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

