using PL.Engineer;
using PL.Gantt;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {

        public Admin()
        {
            InitializeComponent();
            CurrentTime = s_bl.Clock;
            DataContext = this;
        }
        /// <summary>
        ///  Method for show all the list of enginner  by click of the "Handle engineer"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEngineerList_Click(object sender, RoutedEventArgs e)
            {
                new EngineerListWindow().Show();
            }

            private void ButtonTaskList_Click(object sender, RoutedEventArgs e)
            {
                new TaskListWindow().Show();
            }

            private void ButtonGanttView_Click(object sender, RoutedEventArgs e)
            {
                new GanttWindow().Show();
            }

            static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

            public DateTime CurrentTime
            {
                get { return (DateTime)GetValue(CurrentTimeProperty); }
                set { SetValue(CurrentTimeProperty, value); }
            }
            public static readonly DependencyProperty CurrentTimeProperty = DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(default(DateTime)));

            private void ButtonAdvanceYear_Click(object sender, RoutedEventArgs e)
            {
                s_bl.AdvanceTimeByYear();
                CurrentTime = s_bl.Clock;
            }
            private void ButtonAdvanceDay_Click(object sender, RoutedEventArgs e)
            {
                s_bl.AdvanceTimeByDay();
                CurrentTime = s_bl.Clock;
            }
            private void ButtonAdvanceHour_Click(object sender, RoutedEventArgs e)
            {
                s_bl.AdvanceTimeByHour();
                CurrentTime = s_bl.Clock;
            }
            private void ButtonInitializeClock_Click(object sender, RoutedEventArgs e)
            {
                s_bl.InitializeTime();
                CurrentTime = s_bl.Clock;
            }

        }
    
}
