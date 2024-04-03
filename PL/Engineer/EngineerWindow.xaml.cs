using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    int pageStatus = 0;
    // Static reference to the business logic layer.
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <summary>
    /// Dependency property for binding the current engineer item.
    /// </summary>
    public static DependencyProperty CurrentEngineer = DependencyProperty.Register("EngineerItem", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

    /// <summary>
    /// Property representing the selected engineer's experience level.
    /// </summary>
    public BO.EngineerExperience EnigeerExper { get; set; } = BO.EngineerExperience.None;

    /// <summary>
    /// Property representing the current engineer item.
    /// </summary>
    public BO.Engineer EngineerItem
    {
        get { return (BO.Engineer)GetValue(CurrentEngineer); }
        set { SetValue(CurrentEngineer, value); }
    }

    /// <summary>
    /// Constructor for EngineerWindow.
    /// </summary>
    /// <param name="Id"></param>
    public EngineerWindow(int Id = 0)
    {

        InitializeComponent();
        // Set the page status
        pageStatus = Id;
        if (Id == 0)
            EngineerItem = new BO.Engineer();
        else
            try { EngineerItem = s_bl.Engineer.Read(Id)!; }
            catch { }
        Closed += EngineerWindow_Closed!;
    }

    /// <summary>
    ///  Event handler for the button click event to add or update an engineer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (pageStatus == 0)
        {
            try
            {
                // create a new engineer
                s_bl.Engineer.Create(EngineerItem);
                MessageBox.Show("the user added succefully");
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
            // Update an existing engineer
            try
            {
                s_bl.Engineer.Update(EngineerItem);
                MessageBox.Show("the user updated succefully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }
    }

    /// <summary>
    ///  Event handler for the Closed event of the EngineerWindow.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EngineerWindow_Closed(object sender, EventArgs e)
    {
        // An instance of the main window EngineerListWindow
        var mainWindow = Application.Current.Windows
                                        .OfType<EngineerListWindow>()
                                        .FirstOrDefault();
        if (mainWindow != null)
        {
            // Updating the list of engineers in the main window by calling the BL
            // function that returns the list of engineers
            mainWindow.EngineerList = s_bl.Engineer.ReadAll()!;
        }
    }

   
}
