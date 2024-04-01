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
using PL;
using PL.Engineer;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        int pageStatus = 0;
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public static DependencyProperty CurrentTask = DependencyProperty.Register("TaskItem", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));
        public BO.EngineerExperience EnigeerExper { get; set; } = BO.EngineerExperience.None;
        public BO.Task TaskItem
        {
            get { return (BO.Task)GetValue(CurrentTask); }
            set { SetValue(CurrentTask, value); }
        }

        public static readonly DependencyProperty EnginnerInTaskProperty =DependencyProperty.Register("EngineerInTask", typeof(IEnumerable<BO.EngineerInTask>), typeof(TaskWindow), new PropertyMetadata(null));
        public IEnumerable<BO.EngineerInTask> EngineerInTask
        {
            get { return (IEnumerable<BO.EngineerInTask>)GetValue(EnginnerInTaskProperty); }
            set { SetValue(EnginnerInTaskProperty, value); }
        }

 public static readonly DependencyProperty CurrentTaskDependency =DependencyProperty.Register("TaskDependencies", typeof(IEnumerable<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));
        public IEnumerable<BO.TaskInList> TaskDependencies
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(CurrentTaskDependency); }
            set { SetValue(CurrentTaskDependency, value); }
        }


        public TaskWindow(int Id = 0)
        {
            InitializeComponent();
            pageStatus = Id;
            if (Id == 0)
                TaskItem = new BO.Task();
            else
                try { TaskItem = s_bl.Task.Read(Id); }
                catch { }
            Closed += TaskWindow_Closed!;
            EngineerInTask = s_bl.Task.GetAllAvialbleEngineers(Id, EnigeerExper);
            TaskDependencies = s_bl.Task.GetAllDependenciesOptions();


        }

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (pageStatus == 0)
            {
                try
                {
                    s_bl.Task.Create(TaskItem);
                    MessageBox.Show("the task added succefully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // Display a confirmation message box
                Close();

            }
            else
            {

                try
                {
                    s_bl.Task.Update(TaskItem);
                    MessageBox.Show("the user updated succefully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Close();
            }
        }

        private void TaskWindow_Closed(object sender, EventArgs e)
        {
            // An instance of the main window EngineerListWindow
            var mainWindow = Application.Current.Windows
                                            .OfType<TaskListWindow>()
                                            .FirstOrDefault();
            if (mainWindow != null)
            {
                // Updating the list of engineers in the main window by calling the BL
                // function that returns the list of engineers
                mainWindow.TaskList = s_bl.Task.ReadAll()!;
            }
        }

    }
}
