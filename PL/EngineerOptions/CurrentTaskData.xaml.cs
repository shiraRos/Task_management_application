using BO;
using DO;
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

        // Static reference to the business logic layer
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        /// <summary>
        /// Dependency property for the current engineer task
        /// </summary>
        public static DependencyProperty CurrentEngineerTask = DependencyProperty.Register("EngTask", typeof(BO.Task), typeof(CurrentTaskData), new PropertyMetadata(null));

        /// <summary>
        /// Property representing the engineer's experience
        /// </summary>
        public BO.EngineerExperience EnigeerExper { get; set; } = BO.EngineerExperience.None;

        /// <summary>
        /// Property representing the current task
        /// </summary>
        public BO.Task EngTask
        {
            get { return (BO.Task)GetValue(CurrentEngineerTask); }
            set { SetValue(CurrentEngineerTask, value); }
        }


        // Field to store engineer ID
        int myEng;

        /// <summary>
        /// Constructor for CurrentTaskData window
        /// </summary>
        /// <param name="takId">Task ID</param>
        /// <param name="engId">Engineer ID</param>
        public CurrentTaskData(int takId=0,int engId=0)
        {
            myEng = engId; 
            InitializeComponent();
            // If task ID is not provided or task status is 'Completed', open the chooseTask window
            if (takId == 0 || s_bl.Task.Read(takId).Status == (BO.Status)3)
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
                Close();
            }
        }
        /// <summary>
        /// Event handler for the 'Done' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
           

                try
                {
                    EngTask.CompleteDate = s_bl.Clock;// Set task completion date
                EngTask.Status = (Status)3;// Set task status to 'Completed'
                s_bl.Task.Update(EngTask);// Update task in the database
                MessageBox.Show("the task updated succefully"); // Show success message
                new chooseTask(myEng).ShowDialog();// Open chooseTask window in order o choose new available task
            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Close();
            
        }

      
    }
}
