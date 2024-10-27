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
using Assignment_2_WPF.Models;
using Assignment_2_WPF.ViewModels;
using Assignment_2_WPF.Views;
using static Assignment_2_WPF.Models.Schedule;

namespace Assignment_2_WPF.Views
{
    /// <summary>
    /// Interaction logic for AddNewSchedule.xaml
    /// </summary>
    public partial class AddNewSchedule : Window
    {
        private readonly ScheduleViewModel _viewModel;
        public AddNewSchedule(ScheduleViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;

            // Make sure to set the DataContext
            DataContext = _viewModel;
            // Debug output to verify schedules are loaded
            System.Diagnostics.Debug.WriteLine($"Schedules count in constructor: {_viewModel.Schedules?.Count ?? 0}");
        }

        public AddNewSchedule()
        {
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {

            try {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(Description.Text))
                {
                    System.Windows.MessageBox.Show("Please enter an activity name");
                    return;
                }

                if (_viewModel.SelectedPet == null)
                {
                    System.Windows.MessageBox.Show("Please select a pet");
                    return;
                }
                if (DateTime.SelectedDate.Value == null)
                {
                    System.Windows.MessageBox.Show("Please select a date");
                    return;
                }
                // Retrieve description and date from the UI elements
                string description = Description.Text;
            DateTime date = DateTime.SelectedDate.Value;

            // Call method addSchedule from ScheduleViewModel with required parameters
            _viewModel.AddSchedule(description, date);
            // Close the window
            this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding schedule: {ex.Message}", "Error");
            }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            // Close the window
            this.Close();
        }
    }
}
