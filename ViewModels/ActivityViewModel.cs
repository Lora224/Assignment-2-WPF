using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Debug = System.Diagnostics.Debug;
using System.Net.WebSockets;
using System.Windows.Input;
using Assignment_2_WPF.Models;

namespace Assignment_2_WPF.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ActivityViewModel : ViewModelBase
    {
        private readonly AppDbContext _context;
        private DateTime _selectedDate;
        private Activity _selectedActivity;
        private ObservableCollection<Activity> _activities;

        public ActivityViewModel()
        {
            _context = new AppDbContext();
            SelectedDate = DateTime.Today;

            foreach (var activity in _context.Activities)
            {
                Debug.WriteLine(activity);
            }
            foreach (var pet in _context.Pets)
            {
                Debug.WriteLine(pet);
            }

            LoadActivities();
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                LoadActivities();
            }
        }

        public Activity SelectedActivity
        {
            get => _selectedActivity;
            set
            {
                _selectedActivity = value;
                OnPropertyChanged(nameof(SelectedActivity));
            }
        }
        public ObservableCollection<Activity> Activities
        {
            get => _activities;
            private set
            {
                _activities = value;
                OnPropertyChanged(nameof(Activities));
            }
        }

        public void LoadActivities(String pet)
        {
           
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var _activities = context.Activities.ToList();
                if (_activities.Count == 0)
                {
                    //if no activity show "No Activity"
                    Activities = new ObservableCollection<Activity>();

                }
                else
                {
                    var activities = _context.Activities
        .Where(a => a.Date == SelectedDate.Date )   //filter pet
        .ToList();
                    Activities = new ObservableCollection<Activity>(activities);
                }
            }

          
        }
        public void LoadActivities()  //overload
        {

            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var _activities = context.Activities.ToList();
                if (_activities.Count == 0)
                {
                    //if no activity show "No Activity"
                    Activities = new ObservableCollection<Activity>();

                }
                else
                {
                    var activities = _context.Activities
        .Where(a => a.Date == SelectedDate.Date)
        .ToList();
                    Activities = new ObservableCollection<Activity>(activities);
                }
            }


        }
    }
}