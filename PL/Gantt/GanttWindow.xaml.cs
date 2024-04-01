using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Gantt
{
    /// <summary>
    /// Interaction logic for GanttWindow.xaml
    /// </summary>
    public partial class GanttWindow : Window
    {
        public GanttWindow()
        {
            InitializeComponent();
            DataContext = new GanttViewModel();
        }
    }
    public class Activity : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }

        private TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set { _startTime = value; NotifyPropertyChanged("StartTime"); }
        }

        private TimeSpan _endTime;
        public TimeSpan EndTime
        {
            get { return _endTime; }
            set { _endTime = value; NotifyPropertyChanged("EndTime"); }
        }

        public int DurationInPixels => (int)((EndTime - StartTime).TotalMinutes); // Assuming 1 pixel per minute

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class GanttViewModel
    {
        public ObservableCollection<Activity> Activities { get; set; }

        public GanttViewModel()
        {
            Activities = new ObservableCollection<Activity>
            {
                new Activity { Name = "Activity 1", StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(16) },
                new Activity { Name = "Activity 2", StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(12) },
                new Activity { Name = "Activity 3", StartTime = TimeSpan.FromHours(12), EndTime = TimeSpan.FromHours(13) }
            };
        }
    }
}

