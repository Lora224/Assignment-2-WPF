using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
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

        public ActivityView(int UserId)  //pass UserId to ActivityViewModel
        {
            InitializeComponent();

            _viewModel = new ActivityViewModel(UserId);
            DataContext = _viewModel;


        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _viewModel?.Dispose();
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

        private DateTime? _lastSelectedDate;

        private void Calendar_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var calendar = sender as Calendar;
            var viewModel = DataContext as ActivityViewModel;

            if (calendar != null && viewModel != null)
            {
                // If clicking currently selected date
                if (_lastSelectedDate.HasValue && calendar.SelectedDate == _lastSelectedDate)
                {
                    calendar.SelectedDate = null;
                    viewModel.SelectedDate = null;
                }

                // Store the newly selected date
                _lastSelectedDate = calendar.SelectedDate;
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowParticularActivity();
        }
    }
}