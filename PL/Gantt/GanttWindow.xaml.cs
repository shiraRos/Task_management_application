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
    // Static reference to the BL
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    // Collection of tasks for scheduling
    IEnumerable<BO.TasksForScheduale> listTaskScheduale;
    DateTime StartProject;
    DateTime EndProject;

 
    /// <summary>
    /// Dependency property for data binding
    /// </summary>
    public DataTable dataTable
    {
        get { return (DataTable)GetValue(dataTableProperty); }
        set { SetValue(dataTableProperty, value); }

    }
    /// <summary>
    /// Dependency property definition for dataTable
    /// </summary>
    public static readonly DependencyProperty dataTableProperty =
          DependencyProperty.Register("dataTable", typeof(DataTable), typeof(GanttWindow), new PropertyMetadata(null));


 
    /// <summary>
    /// constructor for GanttWindow
    /// </summary>
    public GanttWindow()
    {
        // Retrieve tasks for the Gantt from the Task in the BL
        listTaskScheduale = s_bl.Task.GetAllTaskForGantt();

        
        StartProject = listTaskScheduale.Min(t => t.StartDate);
        EndProject = listTaskScheduale.Max(t => t.EndDate);

        // Build data table for visualization
        buildDataTable();

        InitializeComponent();
        // DataContext = new GanttViewModel();
    }

    /// <summary>
    /// Method to build data table for visualization and store Data 
    /// </summary>
    private void buildDataTable()
    {
        dataTable = new DataTable();

        //adding all the columns in ths table with fields
        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("Alias", typeof(string));
        dataTable.Columns.Add("EngineerId", typeof(int));
        dataTable.Columns.Add("Engineer Name", typeof(string));
        dataTable.Columns.Add("Status", typeof(string)); // Use PascalCase for consistency
        dataTable.Columns.Add("Start Date", typeof(DateTime)); // Use DateTime for date representation
        dataTable.Columns.Add("End Date", typeof(DateTime)); // Use DateTime for date representation

        // Generate date range for the project
        var dateRange = Enumerable.Range(0, (EndProject - StartProject).Days + 1)
                                    .Select(i => StartProject.AddDays(i));
        // Add columns for each date in the date range
        foreach (var day in dateRange)
        {
            dataTable.Columns.Add(day.ToString("dd-MM-yyyy"), typeof(string));
        }

        // Order tasks by start date
        var orderedTasks = listTaskScheduale.OrderBy(t => t.StartDate);
        foreach (var task in orderedTasks)
        {
            // Create a new row for each task
            DataRow row = dataTable.NewRow();
            row["Id"] = task.Id;
            row["Alias"] = task.Alias;
            row["EngineerId"] = task.EngineerId;
            row["Engineer Name"] = task.EngineerName;
            row["Status"] = task.TaskStaus; // Use PascalCase
            row["Start Date"] = task.StartDate;
            row["End Date"] = task.EndDate;

            // Fill in task status for each day in the date range
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





