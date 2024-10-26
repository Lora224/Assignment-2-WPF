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
using static Assignment_2_WPF.Models.Activity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Http;
using Assignment_2_WPF.Ultilities;
using System.Text.Json;
using Timer = System.Threading.Timer;

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
        private DateTime? _selectedDate;
        private Activity _selectedActivity;
        private ObservableCollection<Activity> _activities;
        private int _currentUserId;
        private Pet _selectedPet;
        private ObservableCollection<Pet> _pets;
        private string _name;
        private string _description;
        private ActivityType _selectedActivityType;
        private int _duration;
        private int _distance;
        private ActivityLevel _selectedActivityLevel;
        private int _activityCount;
        public DateTime ActivityDate;
        public Array ActivityTypes => Enum.GetValues(typeof(ActivityType));
        public Array ActivityLevels => Enum.GetValues(typeof(ActivityLevel));
        private string _temperature;
        private string _weatherCondition;
        private string _walkingSuggestion;
        private bool _isGoodForWalking;
        private Timer _weatherUpdateTimer;


        public ActivityViewModel()
        {
            _context = new AppDbContext();
            SelectedDate = null;
            if (SelectedDate == null)
            {
                ActivityDate = DateTime.Today;
            }
            else
            {
                ActivityDate = SelectedDate.Value;
            }
            _currentUserId = GetCurrentUserId();
            SelectedActivityType = ActivityType.Other;
           ActivityCount = Activities.Count;

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
            UpdateWeather();
            _weatherUpdateTimer = new Timer(async (state) => await UpdateWeather(),
            null, TimeSpan.Zero, TimeSpan.FromMinutes(30));
            CheckDatabaseConstraints();
        }
        public ActivityType SelectedActivityType
        {
            get => _selectedActivityType;
            set
            {
                _selectedActivityType = value;
                OnPropertyChanged(nameof(SelectedActivityType));
            }
        }
        public int Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        public int Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        public ActivityLevel SelectedActivityLevel
        {
            get => _selectedActivityLevel;
            set
            {
                _selectedActivityLevel = value;
                OnPropertyChanged(nameof(SelectedActivityLevel));
            }
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

        public DateTime? SelectedDate
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
        public int ActivityCount
        {
            get => _activityCount;
            set
            {
                _activityCount = value;
                OnPropertyChanged(nameof(ActivityCount));
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


        public string Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }

        public string WeatherCondition
        {
            get => _weatherCondition;
            set
            {
                _weatherCondition = value;
                OnPropertyChanged(nameof(WeatherCondition));
            }
        }

        public string WalkingSuggestion
        {
            get => _walkingSuggestion;
            set
            {
                _walkingSuggestion = value;
                OnPropertyChanged(nameof(WalkingSuggestion));
            }
        }

        public bool IsGoodForWalking
        {
            get => _isGoodForWalking;
            set
            {
                _isGoodForWalking = value;
                OnPropertyChanged(nameof(IsGoodForWalking));
            }
        }

//weather update
        private async Task UpdateWeather()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    System.Diagnostics.Debug.WriteLine("Updating weather...");
                    //get weather of Sydney or any user city
                    var response = await client.GetAsync("http://api.weatherapi.com/v1/current.json?key=e15814d4c83147d5b5f120120242610&q=Sydney&aqi=no");
                   System.Diagnostics.Debug.WriteLine($"Response: {response.StatusCode}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var weather = JsonSerializer.Deserialize<WeatherResponse>(jsonString);

                        if (weather?.Current != null)  // Add null check
                        {
                           System.Windows.Application.Current.Dispatcher.Invoke(() =>
                            {
                                Temperature = $"{weather.Current.Temperature:F1}";
                                WeatherCondition = weather.Current.Condition?.Text ?? "Unknown";

                                IsGoodForWalking = weather.Current.Temperature < 30 &&
                                                 weather.Current.Temperature > 10 &&
                                                 !weather.Current.Condition.Text.Contains("rain", StringComparison.OrdinalIgnoreCase);

                                WalkingSuggestion = IsGoodForWalking
                                    ? "Perfect conditions for a walk!"
                                    : GetWeatherWarning(weather.Current.Temperature,
                                                      weather.Current.Condition?.Text);
                            });

                            System.Diagnostics.Debug.WriteLine($"Weather updated: {Temperature}°C, {WeatherCondition}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating weather: {ex.Message}");
                // Update UI to show error state
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    Temperature = "N/A";
                    WeatherCondition = "Unable to fetch weather";
                    WalkingSuggestion = "Weather information unavailable";
                    IsGoodForWalking = false;
                });
            }
        }

        private string GetWeatherWarning(double temperature, string condition)
        {
            if (temperature > 30)
                return "Too hot for walking. Consider indoor activities.";
            if (temperature < 10)
                return "Too cold for walking. Consider indoor activities.";
            if (condition == "Rain")
                return "Rainy conditions. Indoor activities recommended.";

            return "Weather conditions not ideal for walking.";
        }

        public void Dispose()
        {
            _weatherUpdateTimer?.Dispose();
        }
    // weather end
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
                    // Verify existing activities
                    var existingActivities = context.Activities
                        .Where(a => a.PetId == SelectedPet.Id)
                        .ToList();

                    System.Diagnostics.Debug.WriteLine($"Found {existingActivities.Count} existing activities for pet {SelectedPet.Id}");

                    var newActivity = new Activity
                    {
                        Name = activityName,
                        Description = description,
                        Date = date,
                        PetId = SelectedPet.Id,
                        PetName = SelectedPet.PetName,
                        UserId = _currentUserId,  // get this from logged-in user
                        Type = SelectedActivityType,
                        Duration = Duration,
                        Distance = Distance,
                        Level = SelectedActivityLevel
                    }; 
                    context.Activities.Add(newActivity);

                    // Verify before saving
                    var entry = context.Entry(newActivity);
                    System.Diagnostics.Debug.WriteLine($"Entity State: {entry.State}");
                    System.Diagnostics.Debug.WriteLine($"PetId: {newActivity.PetId}");
                    System.Diagnostics.Debug.WriteLine($"ActivityID: {newActivity.Id}");
                    System.Diagnostics.Debug.WriteLine($"PetId Property: {entry.Property("PetId").CurrentValue}");

                    context.SaveChanges();
  
                    // Verify after saving
                    var savedActivity = context.Activities
                        .FirstOrDefault(a => a.Id == newActivity.Id);
                    System.Diagnostics.Debug.WriteLine($"Saved activity ID: {savedActivity?.Id}");
                  

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
        public void ShowParticularActivity()
         {
            if (_selectedActivity == null)
            {
                System.Windows.MessageBox.Show("Please select an activity to show", "Error");
                return;
            }
            else
            {
                System.Windows.MessageBox.Show($"Activity: {_selectedActivity.Name}\nDate: {_selectedActivity.Date}\nPet: {_selectedActivity.PetName}\nDescription: {_selectedActivity.Description}\nType: {_selectedActivity.Type}\nDuration: {_selectedActivity.Duration}\nDistance: {_selectedActivity.Distance}\nLevel: {_selectedActivity.Level}", "Activity Details");
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
                        SelectedPet = Pets[0];
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
        public void ShowAllActivities()
        {
            using (var context = new AppDbContext())
            {
                var _activities = context.Activities.ToList();
 
                if (_activities.Count == 0)
                {
                    //if no activity show "No Activity"
                    MessageBox.Show("No Activity, please add one.");
                    Activities = new ObservableCollection<Activity>();
                }
                else
                {
                    var activities = context.Activities
                        .Where(a =>a.UserId == _currentUserId)
                        .ToList();
                    Activities = new ObservableCollection<Activity>(activities);
                }
            }
        }
        public void LoadActivities()  
        {

            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                    if (SelectedDate is not null)
                        {

                        var activities = context.Activities
                            .Where(a => a.UserId == _currentUserId && a.Date == SelectedDate)
                            .ToList();
                        Activities = new ObservableCollection<Activity>(activities);
                         }
                    else
                    {

                    var activities = context.Activities
                        .Where(a => a.UserId == _currentUserId)
                        .ToList();
                    if (activities.Count == 0 && _currentUserId!=0)
                        {
                            //if no activity show "No Activity"
                            MessageBox.Show("No Activity, please add one.");
                            Activities = new ObservableCollection<Activity>();
                            

                        }
                        else
                        {
                            Activities = new ObservableCollection<Activity>(activities);
                        }  
                    }
                 ActivityCount = context.Activities.Where(Activities => Activities.UserId == _currentUserId).Count();


               
            }

        }
    }
}