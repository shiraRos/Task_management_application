using PL.Engineer;
using PL.Task;
using PL.Gantt;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.EngineerOptions;


namespace PL;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static readonly DependencyProperty CurrentTimeProperty = DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(default(DateTime)));
    public DateTime CurrentTime
    {
        get { return (DateTime)GetValue(CurrentTimeProperty); }
        set { SetValue(CurrentTimeProperty, value); }
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
   

    public MainWindow()
    {
        InitializeComponent();
        CurrentTime = s_bl.Clock;
        DataContext = this;
    }

    private void ButtonEngineer_Click(object sender, RoutedEventArgs e)
    {
        if (s_bl.isProjectStarted()== true )
        {
            int id;
            string input = Microsoft.VisualBasic.Interaction.InputBox("please enter your ID:", "Enginner Enter");


            if (int.TryParse(input, out id))
            {
                try
                {
                    BO.Engineer newUser = s_bl.Engineer.Read(id)!;
                    int takId = newUser.Task != null ? newUser.Task.Id : 0;
                    new CurrentTaskData(takId, newUser.Id).ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }

private void ButtonAdmin_Click(object sender, RoutedEventArgs e)
    {
        new Admin().Show();
    }

  

}

