using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Debug = System.Diagnostics.Debug;
using System.Net.WebSockets;
using System.Windows.Input;
using Assignment_2_WPF.Models;
using System.Drawing.Drawing2D;
using System.Drawing;
using Microsoft.EntityFrameworkCore;


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
        private int _currentUserId;
        private Pet _selectedPet;
        private ObservableCollection<Pet> _pets;
        private string _name;
        private string _description;

        public ActivityViewModel()
        {
            _context = new AppDbContext();
            SelectedDate = DateTime.Today;
            _currentUserId = GetCurrentUserId();
            foreach (var activity in _context.Activities)
            {
                Debug.WriteLine(activity);
            }
            foreach (var pet in _context.Pets)
            {
                Debug.WriteLine(pet);
            }
            // Ensure database schema is up-to-date
            using (var context = new AppDbContext())
            {
                context.CheckTableSchemas();
            }

            LoadPets();
            LoadActivities();
            CheckDatabaseConstraints();
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
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
        public Pet SelectedPet
        {
            get => _selectedPet;
            set
            {
                _selectedPet = value;
                OnPropertyChanged(nameof(SelectedPet));
                LoadActivities(_selectedPet);
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
        public ObservableCollection<Pet> Pets
        {
            get => _pets;
            set
            {
                _pets = value;
                OnPropertyChanged(nameof(Pets));
            }
        }
        private void CheckDatabaseConstraints()
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    // Check indexes and constraints
                    command.CommandText = @"
                SELECT name, sql 
                FROM sqlite_master 
                WHERE type='index' AND tbl_name='activity';";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            System.Diagnostics.Debug.WriteLine($"Index/Constraint: {reader.GetString(0)}");
                            System.Diagnostics.Debug.WriteLine($"SQL: {reader.GetString(1)}");
                        }
                    }
                }
            }
        }
        private int GetCurrentUserId()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Get the first user (or you could modify this to get the logged-in user)
                    var user = context.Users.FirstOrDefault();
                    if (user != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Found user: {user.Name} with ID: {user.Id}");
                        return user.Id;
                    }
                    else
                    {
                        // If no user exists, create one
                        var newUser = new User("Default User", "default@test.com", "password");
                        context.Users.Add(newUser);
                        context.SaveChanges();
                        System.Diagnostics.Debug.WriteLine($"Created new user with ID: {newUser.Id}");
                        return newUser.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting user ID: {ex.Message}");
                System.Windows.MessageBox.Show("Error getting user information. Please try again.");
                return -1; // Return invalid ID to indicate error
            }
        }

        public bool AddActivity(string activityName, string description, DateTime date) //pass user id
        {

            try
            {

                using (var context = new AppDbContext())
                {
                   
                    // Create new pet with user input
                    var newActivity = new Activity
                    {
                        Name = activityName,
                        Description = description,
                        Date = SelectedDate,
                        PetId = SelectedPet.Id,
                        Pet = _selectedPet,
                        UserId = _currentUserId  // get this from logged-in user
                    };
                    System.Diagnostics.Debug.WriteLine($"Adding new activity: {newActivity.Id},{newActivity.Name}, {newActivity.Date},{newActivity.PetId},{newActivity.UserId}");
                    context.Activities.Add(newActivity);
                    context.SaveChanges();

                    // Add to observable collection
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        Activities.Add(newActivity);
                        SelectedActivity = newActivity;
                    });

                    // Create new ObservableCollection and notify change

                    System.Windows.MessageBox.Show("Activity added successfully!", "Success");

                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding Activity: {ex.Message}", "Error");
                if (ex.InnerException != null)
                {
                    System.Windows.MessageBox.Show($"Inner Exception: {ex.InnerException.Message}", "Error");
                }
                return false;

            }
        }
        public void RemoveActivity()
        {
            try
            {
                _context.Activities.Remove(_selectedActivity);
                _context.SaveChanges();
                LoadActivities();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"You must choose an activity to remove: {ex.Message}", "Error");
            }
        }
        public void LoadActivities(Pet pet)
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
        .Where(a => a.Date == SelectedDate.Date&& a.Pet==SelectedPet)   //filter pet
        .ToList();
                    Activities = new ObservableCollection<Activity>(activities);
                }
            }

          
        }
        public void LoadPets()
        {
            try
            {
                if (Pets == null)
                {
                    Pets = new ObservableCollection<Pet>();
                }

                using (var context = new AppDbContext())
                {
                    // Get pets for the current user
                    var userPets = context.Pets
                        .Where(p => p.UserId == _currentUserId)
                        .ToList();

                    Pets.Clear();
                    foreach (var pet in userPets)
                    {
                        Pets.Add(pet);
                        System.Diagnostics.Debug.WriteLine($"Added pet: {pet.PetName} (ID: {pet.Id})"); //debug
                    }

                    System.Diagnostics.Debug.WriteLine($"Loaded {Pets.Count} pets for user {_currentUserId}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading pets: {ex.Message}");
                MessageBox.Show("Error loading pets. Please try again.");
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
                    MessageBox.Show("No Activity, please add one.");
                    Activities = new ObservableCollection<Activity>();

                }
                else
                {
                    var activities = _context.Activities
        .Where(a => a.Date == SelectedDate.Date )   
        .ToList();
                    Activities = new ObservableCollection<Activity>(activities);

                }
            }

        }
    }
}