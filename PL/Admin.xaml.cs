﻿using PL.Engineer;
using PL.EngineerOptions;
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
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
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
        /// Method for show all the list of Task  by click of the "Handle tasks"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTaskList_Click(object sender, RoutedEventArgs e)
        {
            new TaskListWindow().Show();
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
                Close();
            }
        }


        private void ButtonCreateScheduale_Click(object sender, RoutedEventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter the project start date (MM/dd/yyyy):", "Date Enter");

            if (DateTime.TryParse(input, out DateTime startDate))
            {
                try
                {
                    s_bl.createSchedule(startDate);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid date format. Please enter the date in MM/dd/yyyy format.");
            }
        }

        private void ButtonGanttView_Click(object sender, RoutedEventArgs e)
        {
            new GanttWindow().Show();
        }

        public Admin()
        {
            InitializeComponent();
           
        }
    }
    
}
