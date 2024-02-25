﻿using PL.Engineer;
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

namespace PL; 
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    ///  Method for show all the list of enginner  by click of the "Handle engineer"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonEngineerList_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }
    /// <summary>
    ///   Method for init all data base by click of the "init DB "
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonInitDB_Click(object sender, RoutedEventArgs e)
    {
        // Display a confirmation message box
            MessageBoxResult result = MessageBox.Show("Are you sure you want to init data?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

        // Check the user's response
        if (result == MessageBoxResult.Yes)
        {
            // Call the method to perform the reset
             BlApi.Factory.Get().InitializeDB();
        }
    }
    /// <summary>
    ///  Method for restart all data base by click of the "restart "
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        // Display a confirmation message box
        MessageBoxResult result = MessageBox.Show("Are you sure you want to reset data?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

        // Check the user's response
        if (result == MessageBoxResult.Yes)
        {
            // Call the method to perform the reset
            BlApi.Factory.Get().Reset();
        }
    }

    public MainWindow()
    {
        InitializeComponent();
    }

   
}
