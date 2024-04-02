using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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

namespace PL.Gantt;

/// <summary>
/// Interaction logic for GanttWindow.xaml
/// </summary>
public partial class GanttWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    IEnumerable<BO.TasksForScheduale> listTaskScheduale;
    DateTime StartProject;
    DateTime EndProject;

    public DataTable dataTable
    {
        get { return (DataTable)GetValue(dataTableProperty); }
        set { SetValue(dataTableProperty, value); }

    }
    public static readonly DependencyProperty dataTableProperty =
          DependencyProperty.Register("dataTable", typeof(DataTable), typeof(GanttWindow), new PropertyMetadata(null));


    public GanttWindow()
    {
        listTaskScheduale = s_bl.Task.GetAllTaskForGantt();

        // listTaskScheduale.Add(new BO.Task() { Id = 1, Alias = "T1", StartDate = new DateTime(2024, 2, 21), CompleteDate = new DateTime(2024, 2, 22) });
        //StartProject = listTaskScheduale.Min(t => t.StartDate);
        //EndProject = listTaskScheduale.Max(t => t.CompleteDate);

        buildDataTable();

        InitializeComponent();
        // DataContext = new GanttViewModel();
    }
    private void buildDataTable()
    {
        dataTable = new DataTable();
        dataTable.Columns.Add("Id", typeof(int));

        dataTable.Columns.Add("Alias", typeof(string));

        dataTable.Columns.Add("EngineerId", typeof(int));

        dataTable.Columns.Add("Engineer Name", typeof(string));
        dataTable.Columns.Add("status", typeof(string));
        dataTable.Columns.Add("start date", typeof(string));
        dataTable.Columns.Add("end date", typeof(string));

        int col = 7;
        for (DateTime day = StartProject; day <= EndProject; day = day.AddDays(1))
        {
            string strDay = $"{day.Day}-{day.Month}-{day.Year}";
            dataTable.Columns.Add(strDay, typeof(string));
            col++;
        }

        IEnumerable<BO.TasksForScheduale> orderedlistTasksScheduale = listTaskScheduale.OrderBy(t => t.StartDate);
        foreach (BO.TasksForScheduale task in orderedlistTasksScheduale)
        {
            DataRow row = dataTable.NewRow();
            row[0] = task.Id;
            row[1] = task.Alias;
            row[2] = task.EngineerId;
            row[3] = task.EngineerName;
            row[4] = task.TaskStaus;
            row[5] = task.StartDate;
            row[6] = task.EndDate;
            for (DateTime day = StartProject; day <= EndProject; day = day.AddDays(1))
            {
                string strDay = $"{day.Day}-{day.Month}-{day.Year}";
                if (day < task.StartDate || day > task.EndDate)
                    row[strDay] = "None";
                else
                {
                    row[strDay] = task.TaskStaus;
                }
                dataTable.Rows.Add(row);
            }
        }
    }


}
//public class Activity : INotifyPropertyChanged
//{
//    private string _name;
//    public string Name
//    {
//        get { return _name; }
//        set { _name = value; NotifyPropertyChanged("Name"); }
//    }

//    private TimeSpan _startTime;
//    public TimeSpan StartTime
//    {
//        get { return _startTime; }
//        set { _startTime = value; NotifyPropertyChanged("StartTime"); }
//    }

//    private TimeSpan _endTime;
//    public TimeSpan EndTime
//    {
//        get { return _endTime; }
//        set { _endTime = value; NotifyPropertyChanged("EndTime"); }
//    }

//    public int DurationInPixels => (int)((EndTime - StartTime).TotalMinutes); // Assuming 1 pixel per minute

//    public event PropertyChangedEventHandler PropertyChanged;

//    private void NotifyPropertyChanged(string propertyName)
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//    }
//}

//public class GanttViewModel
//{
//    public ObservableCollection<Activity> Activities { get; set; }

//    public GanttViewModel()
//    {
//        Activities = new ObservableCollection<Activity>
//        {
//            new Activity { Name = "Activity 1", StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(16) },
//            new Activity { Name = "Activity 2", StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(12) },
//            new Activity { Name = "Activity 3", StartTime = TimeSpan.FromHours(12), EndTime = TimeSpan.FromHours(13) }
//        };
//    }
//}

