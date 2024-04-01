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
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public static DependencyProperty CurrentEngineer = DependencyProperty.Register("EngineerItem", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
    public BO.EngineerExperience EnigeerExper { get; set; } = BO.EngineerExperience.None;
    public BO.Engineer EngineerItem
    {
        get { return (BO.Engineer)GetValue(CurrentEngineer); }
        set { SetValue(CurrentEngineer, value); }
    }

    public EngineerWindow(int Id = 0)
    {

        InitializeComponent();
        pageStatus = Id;
        if (Id == 0)
            EngineerItem = new BO.Engineer();
        else
            try { EngineerItem = s_bl.Engineer.Read(Id); }
            catch { }
        Closed += EngineerWindow_Closed!;
    }

    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (pageStatus == 0)
        {
            try
            {
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
