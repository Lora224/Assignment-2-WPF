// AddActivityView.xaml.cs

using Assignment_2_WPF.ViewModels;
using Assignment_2_WPF.Models;
using System.Windows;
using System.Drawing.Drawing2D;
using System.Windows.Media.Media3D;
using static Assignment_2_WPF.Models.Activity;

namespace Assignment_2_WPF.Views
{
    /// <summary>
    /// Interaction logic for AddActivityView.xaml
    /// </summary>
    public partial class AddActivityView : Window
    {
        private readonly ActivityViewModel _viewModel;
        public AddActivityView(ActivityViewModel viewModel)
        {
            InitializeComponent();
            if (viewModel == null)
            {
                _viewModel = new ActivityViewModel();
            }
            else
            {
                _viewModel = viewModel;
            }

            // Make sure to set the DataContext
            DataContext = _viewModel;

            // Debug output to verify pets are loaded
            System.Diagnostics.Debug.WriteLine($"Pets count in constructor: {_viewModel.Pets?.Count ?? 0}");
          
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(Name.Text))
                {
                    System.Windows.MessageBox.Show("Please enter an activity name");
                    return;
                }

                if (_viewModel.SelectedPet == null)
                {
                    System.Windows.MessageBox.Show("Please select a pet");
                    return;
                }
                // Validate based on activity type
                if (_viewModel.SelectedActivityType == ActivityType.Playing)
                {
                    if (_viewModel.Duration <= 0)
                    {
                        System.Windows.MessageBox.Show("Please enter a valid duration");
                        return;
                    }
                }
                else if (_viewModel.SelectedActivityType == ActivityType.Walking)
                {
                    if (_viewModel.Duration <= 0 || _viewModel.Distance <= 0)
                    {
                        System.Windows.MessageBox.Show("Please enter valid duration and distance");
                        return;
                    }
                }


                if (_viewModel.AddActivity(Name.Text, Description.Text, _viewModel.SelectedDate))
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding activity: {ex.Message}", "Error");
            }
        }

        private void ActivityType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_viewModel.SelectedActivityType == ActivityType.Playing)
            {
                PlayingFields.Visibility = Visibility.Visible;
                WalkingFields.Visibility = Visibility.Collapsed;
            }
            else if (_viewModel.SelectedActivityType == ActivityType.Walking)
            {
                PlayingFields.Visibility = Visibility.Collapsed;
                WalkingFields.Visibility = Visibility.Visible;
            }
            else
            {
                PlayingFields.Visibility = Visibility.Collapsed;
                WalkingFields.Visibility = Visibility.Collapsed;
            }
        }
    }
}