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
    /// Interaction logic for chooseTask.xaml
    /// </summary>
    public partial class chooseTask : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        BO.Engineer currentEng;
        public IEnumerable<BO.TaskInEngineer> EngineerTaskList
        {
            get { return (IEnumerable<BO.TaskInEngineer>)GetValue(EngineerTaskListProperty); }
            set { SetValue(EngineerTaskListProperty, value); }
        }

        public static readonly DependencyProperty EngineerTaskListProperty =
            DependencyProperty.Register("EngineerTaskList", typeof(IEnumerable<BO.TaskInEngineer>), typeof(chooseTask), new PropertyMetadata(null));
       
        public BO.EngineerExperience EnigeerExper { get; set; } = BO.EngineerExperience.None;

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the selected item from the ListView

            // Do something with the selected item
            // For example, you can cast the selected item to its type and access its properties
            var selectedTask = (sender as ListView)!.SelectedItem as BO.TaskInEngineer; // Change 'Task' to your actual item type
            currentEng.Task = selectedTask;
            s_bl.Engineer.Update(currentEng);
            Close();

        }
    
    public chooseTask(int id)
        {
            InitializeComponent();
            EngineerTaskList = s_bl?.Task.GetAvailableTasksForEngineer(id)!;
            currentEng = s_bl?.Engineer.Read(id)!;
        }
    }
}
