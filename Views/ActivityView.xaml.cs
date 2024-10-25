using System.Windows;
using System.Windows.Controls;
using Assignment_2_WPF.Models;
using Assignment_2_WPF.ViewModels;

namespace Assignment_2_WPF.Views
{
    public partial class ActivityView : Window
    {
        private ActivityViewModel _viewModel;
        private Activity selectedActivity;
        private Pet selectedPet;
        private DateTime selectedDate;

        public ActivityView()  //User view model? or pass user object to activity view model?
        {
            selectedDate = DateTime.Today;
            InitializeComponent();
            _viewModel = DataContext as ActivityViewModel;
            _viewModel.LoadActivities();
            
            // If DataContext isn't set in XAML, set it here
            if (_viewModel == null)
            {
                _viewModel = new ActivityViewModel();
                DataContext = _viewModel;
            }
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e) //selected activity
        {
            // Get the selected activity
            var dataGrid = sender as DataGrid;
            selectedActivity = dataGrid?.SelectedItem as Activity;

            if (selectedActivity != null)
            { 
                _viewModel.SelectedActivity = selectedActivity;
            }
        }



        // Helper method to navigate to other views (if needed)
        private void NavigateToView(Window view)
        {
            view.Owner = this;
            view.ShowDialog();
        }



        private void EditActivity_Click(object sender, RoutedEventArgs e)
        {
            var editActivityWindow = new EditActivityView(this.DataContext as ActivityViewModel);
            editActivityWindow.ShowDialog();
        }

        private void AddNewActivity_Click(object sender, RoutedEventArgs e)
        {
            var addActivityWindow = new AddActivityView(this.DataContext as ActivityViewModel);
            addActivityWindow.ShowDialog();
        }

        private void RemoveActivity_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RemoveActivity();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}