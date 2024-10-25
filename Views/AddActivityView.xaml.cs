// AddActivityView.xaml.cs

using Assignment_2_WPF.ViewModels;
using Assignment_2_WPF.Models;
using System.Windows;
using System.Drawing.Drawing2D;
using System.Windows.Media.Media3D;

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
    }
}