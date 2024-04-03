using BO;
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

        //listTaskScheduale=new BO.Task() { Id = 1, Alias = "T1", StartDate = new DateTime(2024, 2, 21), CompleteDate = new DateTime(2024, 2, 22) });
        StartProject = listTaskScheduale.Min(t => t.StartDate);
        EndProject = listTaskScheduale.Max(t => t.EndDate);

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
        dataTable.Columns.Add("Status", typeof(string)); // Use PascalCase for consistency
        dataTable.Columns.Add("Start Date", typeof(DateTime)); // Use DateTime for date representation
        dataTable.Columns.Add("End Date", typeof(DateTime)); // Use DateTime for date representation

        // Enumerate dates efficiently using Span<DateTime>
        var dateRange = Enumerable.Range(0, (EndProject - StartProject).Days + 1)
                                    .Select(i => StartProject.AddDays(i));

        foreach (var day in dateRange)
        {
            dataTable.Columns.Add(day.ToString("dd-MM-yyyy"), typeof(string));
        }

        var orderedTasks = listTaskScheduale.OrderBy(t => t.StartDate);
        foreach (var task in orderedTasks)
        {
            DataRow row = dataTable.NewRow();
            row["Id"] = task.Id;
            row["Alias"] = task.Alias;
            row["EngineerId"] = task.EngineerId;
            row["Engineer Name"] = task.EngineerName;
            row["Status"] = task.TaskStaus; // Use PascalCase
            row["Start Date"] = task.StartDate;
            row["End Date"] = task.EndDate;

            foreach (var day in dateRange)
            {
                if (day < task.StartDate || day > task.EndDate)
                {
                    row[day.ToString("dd-MM-yyyy")] = "None";
                }
                else
                {
                    row[day.ToString("dd-MM-yyyy")] = task.TaskStaus; // Use PascalCase
                }
            }

            dataTable.Rows.Add(row);
        }

    }
}



        //private void buildDataTable()
        //{
        //    dataTable = new DataTable();
        //    dataTable.Columns.Add("Id", typeof(int));

//    dataTable.Columns.Add("Alias", typeof(string));

//    dataTable.Columns.Add("EngineerId", typeof(int));

//    dataTable.Columns.Add("Engineer Name", typeof(string));
//    dataTable.Columns.Add("status", typeof(string));
//    dataTable.Columns.Add("start date", typeof(string));
//    dataTable.Columns.Add("end date", typeof(string));

//    int col = 7;
//    for (DateTime day = StartProject; day <= EndProject; day = day.AddDays(1))
//    {
//        string strDay = $"{day.Day}-{day.Month}-{day.Year}";
//        dataTable.Columns.Add(strDay, typeof(string));
//        col++;
//    }

//    IEnumerable<BO.TasksForScheduale> orderedlistTasksScheduale = listTaskScheduale.OrderBy(t => t.StartDate);
//    foreach (BO.TasksForScheduale task in orderedlistTasksScheduale)
//    {
//        DataRow row = dataTable.NewRow();
//        row[0] = task.Id;
//        row[1] = task.Alias;
//        row[2] = task.EngineerId;
//        row[3] = task.EngineerName;
//        row[4] = task.TaskStaus;
//        row[5] = task.StartDate;
//        row[6] = task.EndDate;
//        for (DateTime day = StartProject; day <= EndProject; day = day.AddDays(1))
//        {
//            string strDay = $"{day.Day}-{day.Month}-{day.Year}";
//            if (day < task.StartDate || day > task.EndDate)
//                row[strDay] = "None";
//            else
//            {
//                row[strDay] = task.TaskStaus;
//            }
//            dataTable.Rows.Add(row);
//        }
//    }
//}





