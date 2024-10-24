using System.Windows;
using System.Windows.Controls;
using Assignment_2_WPF.Models;
using Assignment_2_WPF.ViewModels;

namespace Assignment_2_WPF.Views
{
    public partial class ActivityView : Window
    {
        private ActivityViewModel _viewModel;

        public ActivityView()
        {
            InitializeComponent();
            _viewModel = DataContext as ActivityViewModel;

            // If DataContext isn't set in XAML, set it here
            if (_viewModel == null)
            {
                _viewModel = new ActivityViewModel();
                DataContext = _viewModel;
            }
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected activity
            var dataGrid = sender as DataGrid;
            var selectedActivity = dataGrid?.SelectedItem as Activity;

            if (selectedActivity != null)
            {
                // In MVVM, we should move this to the ViewModel and use commands
                _viewModel.SelectedActivity = selectedActivity;
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected item from the ListBox
            var listBox = sender as System.Windows.Controls.ListBox;
            var selectedItem = listBox?.SelectedItem;

            if (selectedItem != null)
            {
                // In MVVM, this should be handled by the ViewModel through bindings
                System.Windows.MessageBox.Show($"Selected: {selectedItem}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                // This should be replaced with a command in the ViewModel
                System.Windows.MessageBox.Show("Button clicked - implement your action here");
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Helper method to navigate to other views (if needed)
        private void NavigateToView(Window view)
        {
            view.Owner = this;
            view.ShowDialog();
        }
    }
}