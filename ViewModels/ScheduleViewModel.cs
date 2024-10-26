using Assignment_2_WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Debug = System.Diagnostics.Debug;

namespace Assignment_2_WPF.ViewModels
{

    
    public class ScheduleViewModel:ViewModelBase
    {
        private readonly AppDbContext _context;
        private DateTime _selectedDate;
        private Schedule _selectedSchedule;
        private ObservableCollection<Schedule> _schedules;
        private int _currentUserId;
        private Pet _selectedPet;
        private ObservableCollection<Pet> _pets;
        private string _name;
        private string _description;
        public Schedule SelectedSchedule
        {
            get => _selectedSchedule;
            set
            {
                _selectedSchedule = value;
                OnPropertyChanged(nameof(SelectedSchedule));
            }
        }
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        public ObservableCollection<Schedule> Schedules
        {
            get => _schedules;
            set
            {
                _schedules = value;
                OnPropertyChanged(nameof(Schedules));
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
        public ObservableCollection<Pet> Pets
        {
            get => _pets;
            set
            {
                _pets = value;
                OnPropertyChanged(nameof(Pets));
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
        public void LoadSchedules()  //overload
        {

            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var _schedules = context.Schedules.ToList();
                if (_schedules.Count == 0)
                {
                    //if no schedule"
                    MessageBox.Show("No Schedule, please add one.");
                    Schedules = new ObservableCollection<Schedule>();

                }
                else
                {
                    var schedules = _context.Schedules
                        .Where(a => a.Date == SelectedDate.Date)
                        .ToList();
                    Schedules = new ObservableCollection<Schedule>(schedules);

                }
            }

        }

        public ScheduleViewModel()
        {
            _context = new AppDbContext();
            Schedules = new ObservableCollection<Schedule>(_context.Schedules.ToList());
            _currentUserId = GetCurrentUserId();
            foreach (var activity in _context.Schedules)
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
            LoadSchedules();
            CheckDatabaseConstraints();

        }
        public void AddSchedule(int petId, string petName, int scheduleId, string scheduleType, DateTime date, string description)
        {
            Schedule schedule = new Schedule(petId, petName, scheduleId, scheduleType, date, description);
            _context.Schedules.Add(schedule);
            _context.SaveChanges();
            Schedules.Add(schedule);
        }



    }
}
