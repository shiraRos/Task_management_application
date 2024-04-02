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


namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerListWindow.xaml
/// </summary>
public partial class EngineerListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public IEnumerable<BO.Engineer> EngineerList
    {
        get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }

    public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));
   
    public EngineerListWindow()
    {
        InitializeComponent();
        EngineerList = s_bl?.Engineer.ReadAll()!;
    }

    public BO.EngineerExperience EnigeerExper { get; set; } = BO.EngineerExperience.None;
    private void EnguneerExper_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        EngineerList = (EnigeerExper == BO.EngineerExperience.None) ?
            s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == EnigeerExper)!;
    }

    private void AddEngineerWindow_click(object sender, RoutedEventArgs e)
    {
        new EngineerWindow().ShowDialog();
    }

    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        // Get the selected item from the ListView
       
            // Do something with the selected item
            // For example, you can cast the selected item to its type and access its properties
            var selectedEngineer = (sender as ListView)!.SelectedItem as BO.Engineer; // Change 'Engineer' to your actual item type
            new EngineerWindow(selectedEngineer!.Id).ShowDialog();                                                   // Now you can work with the selectedEngineer object

    }

   

}
