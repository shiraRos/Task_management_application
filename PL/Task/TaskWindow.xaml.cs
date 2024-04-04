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
using BO;
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

        // Static reference to the business logic layer
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        /// <summary>
        /// Dependency property for the current task
        /// </summary>
        public static DependencyProperty CurrentTask = DependencyProperty.Register("TaskItem", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

        /// <summary>
        /// Property representing the engineer's experience
        /// </summary>
        public BO.EngineerExperience EnigeerExper { get; set; } = BO.EngineerExperience.None;


        /// <summary>
        ///  Property representing the current task
        /// </summary>
        public BO.Task TaskItem
        {
            get { return (BO.Task)GetValue(CurrentTask); }
            set { SetValue(CurrentTask, value); }
        }

        /// <summary>
        /// Constructor for TaskWindow
        /// </summary>
        /// <param name="Id"></param>
        public TaskWindow(int Id = 0)
        {
            InitializeComponent();
            pageStatus = Id;
            if (Id == 0)
                TaskItem = new BO.Task();
            else
                try
                {
                    TaskItem = s_bl.Task.Read(Id);
                    //EngineerInTask = s_bl.Task.GetAllAvialbleEngineers(Id, TaskItem?.ComplexityLevel);
                }
                catch { }
            Closed += TaskWindow_Closed!;
            //TaskDependencies = s_bl.Task.GetAllDependenciesOptions();

        }

        /// <summary>
        /// Event handler for the Add/Update button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (pageStatus == 0)
            {
                // If in add mode, try to create a new task
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
                // If in update mode, try to update the existing task
                try
                {
                    s_bl.Task.Update(TaskItem);
                    MessageBox.Show("the task updated succefully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Close();
            }
        }

        /// <summary>
        ///    // Event handler for the Closed event of the TaskWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskWindow_Closed(object sender, EventArgs e)
        {
            // Find the instance of the main window TaskListWindow
            var mainWindow = Application.Current.Windows
                                            .OfType<TaskListWindow>()
                                            .FirstOrDefault();
            if (mainWindow != null)
            {
                // Update the list of tasks in the main window by calling the BL function that returns the list of tasks
                mainWindow.TaskList = s_bl.Task.GetAllTasksForList()!;
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox != null)
            {
                var dependencies = listBox.SelectedItems.OfType<TaskInList>();

                foreach (var selectedItem in dependencies)
                {
                    if (TaskItem.Dependencies == null)
                    {
                        TaskItem.Dependencies = new List<TaskInList>();
                    }
                    List<TaskInList> currentDep=TaskItem.Dependencies.ToList();
                    currentDep.Add(selectedItem);
                   TaskItem.Dependencies=currentDep;

                }
            }
        }
    }
}
