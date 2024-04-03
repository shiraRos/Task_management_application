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
    // Static reference to the business logic layer.
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    /// <summary>
    /// Property representing the list of engineers.
    /// </summary>
    public IEnumerable<BO.Engineer> EngineerList
    {
        get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }

    /// <summary>
    /// Dependency property for binding EngineerList to XAML.
    /// </summary>
    public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

    /// <summary>
    /// Constructor for EngineerListWindow
    /// </summary>
    public EngineerListWindow()
    {
        // Initialize the window components.
        InitializeComponent();
        EngineerList = s_bl?.Engineer.ReadAll()!;
    }
    /// <summary>
    /// Property representing the selected engineer's experience level.
    /// </summary>
    public BO.EngineerExperience EnigeerExper { get; set; } = BO.EngineerExperience.None;

    /// <summary>
    /// Event handler for the selection changed event of the experience level ComboBox.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EnguneerExper_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Update the EngineerList based on the selected experience level
        EngineerList = (EnigeerExper == BO.EngineerExperience.None) ?
            s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == EnigeerExper)!;
    }

    /// <summary>
    /// Event handler for the click event of the "Add Engineer" button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddEngineerWindow_click(object sender, RoutedEventArgs e)
    {
        // Open a new EngineerWindow for adding a new engineer.
        new EngineerWindow().ShowDialog();
    }

    /// <summary>
    /// Event handler for the double click event of the ListView.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        // Get the selected engineer from the ListView and open the EngineerWindow for editing.
        var selectedEngineer = (sender as ListView)!.SelectedItem as BO.Engineer; 
            new EngineerWindow(selectedEngineer!.Id).ShowDialog();                                      

    }

   

}
