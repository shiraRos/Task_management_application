using BO;
using PL.Task;
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

namespace PL.EngineerOptions
{
    /// <summary>
    /// Interaction logic for CurrentTaskData.xaml
    /// </summary>
    public partial class CurrentTaskData : Window
    {
       
       
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public static DependencyProperty CurrentEngineerTask = DependencyProperty.Register("EngTask", typeof(BO.Task), typeof(CurrentTaskData), new PropertyMetadata(null));
        public BO.EngineerExperience EnigeerExper { get; set; } = BO.EngineerExperience.None;
        public BO.Task EngTask
        {
            get { return (BO.Task)GetValue(CurrentEngineerTask); }
            set { SetValue(CurrentEngineerTask, value); }
        }


         int myEng ;
        public CurrentTaskData(int takId=0,int engId=0)
        {
            myEng = engId; 
            InitializeComponent();
            if (takId == 0)
                new chooseTask(engId).ShowDialog();
            else
            {
                try
                {
                    EngTask = s_bl.Task.Read(takId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Closed += TaskWindow_Closed!;
            }
        }
        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
           

                try
                {
                    EngTask.CompleteDate = s_bl.Clock;
                EngTask.Status = (Status)3;
                    s_bl.Task.Update(EngTask);
                    MessageBox.Show("the task updated succefully");
                new chooseTask(myEng).ShowDialog();
            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Close();
            
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
