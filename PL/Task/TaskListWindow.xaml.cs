using PL.Engineer;
using System;
using System.Collections.Generic;
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



namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public IEnumerable<BO.TaskForlList> TaskList
        {
            get { return (IEnumerable<BO.TaskForlList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskForlList>), typeof(TaskListWindow), new PropertyMetadata(null));
        public TaskListWindow()
        {
            InitializeComponent();
            TaskList = s_bl?.Task.GetAllTasksForList()!;
        }
        public BO.EngineerExperience EnigeerExper { get; set; } = BO.EngineerExperience.None;
          private void EngineerExper_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
             TaskList = (EnigeerExper == BO.EngineerExperience.None) ?
            s_bl?.Task.GetAllTasksForList()! : s_bl?.Task.GetAllTasksForList(item => item.ComplexityLevel == EnigeerExper)!;
    }

    private void AddTaskWindow_click(object sender, RoutedEventArgs e)
    {
        new TaskWindow().ShowDialog();
    }

    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        // Get the selected item from the ListView
       
            // Do something with the selected item
            // For example, you can cast the selected item to its type and access its properties
            var selectedTask = (sender as ListView)!.SelectedItem as BO.TaskForlList; // Change 'Task' to your actual item type
            new TaskWindow(selectedTask!.Id).ShowDialog();                                                   // Now you can work with the selectedEngineer object

    
        }
    }
}
