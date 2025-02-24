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
using static Assignment_2_WPF.Models.Schedule;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Assignment_2_WPF.Ultilities;
using static Assignment_2_WPF.Models.Schedule;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;
using static Assignment_2_WPF.Models.Activity;


namespace Assignment_2_WPF.ViewModels
{
    public class ScheduleViewModel : ViewModelBase
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
        private DateTime _scheduleDate;
        private ScheduleType _selectedScheduleType;
        private ObservableCollection<Schedule> _allSchedule;
        public Array ScheduleTypes => Enum.GetValues(typeof(ScheduleType));

        public ScheduleViewModel(int UserId)
        {
            _context = new AppDbContext();
            Schedules = new ObservableCollection<Schedule>(_context.Schedules.ToList());
            _currentUserId = UserId;
            SelectedScheduleType = ScheduleType.Other;
            SelectedDate = DateTime.Today;
            if (SelectedDate == null)
            {
                ScheduleDate = DateTime.Today;
            }
            else
            {
                ScheduleDate = SelectedDate;
            }
            foreach (var schedule in _context.Schedules)
            {
                Debug.WriteLine(schedule);
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

        public ScheduleType SelectedScheduleType
        {
            get => _selectedScheduleType;
            set
            {
                _selectedScheduleType = value;
                OnPropertyChanged(nameof(SelectedScheduleType));
            }
        }

        public DateTime ScheduleDate
        {
            get => _scheduleDate;
            set
            {
                _scheduleDate = value;
                OnPropertyChanged(nameof(ScheduleDate));
            }
        }
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
                LoadSchedules();
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

        public ObservableCollection<Schedule> AllSchedule
        {
            get => _allSchedule;
            set
            {
                _allSchedule = value;
                OnPropertyChanged(nameof(AllSchedule));
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
        public void LoadSchedules()  
        {

            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                    var _schedules = context.Schedules
                        .Where(a => a.UserId==_currentUserId)
                        .ToList();

                if (_schedules.Count == 0)
                {
                    //if no schedule"
                    MessageBox.Show("No Schedule, please add one.");
                    Schedules = new ObservableCollection<Schedule>();
                    AllSchedule = new ObservableCollection<Schedule>();

                }
                else
                {
                    if (SelectedDate == null)
                    {
                        var _schedules2 = context.Schedules //all schedules
                              .Where(a => a.UserId == _currentUserId)
                              .ToList();
                        AllSchedule = new ObservableCollection<Schedule>(_schedules2);
                        Schedules = new ObservableCollection<Schedule>(_schedules2);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Selected Date: {SelectedDate.Date}");
                        var _schedules1 = context.Schedules
                             .Where(a => a.Date == SelectedDate.Date && a.UserId == _currentUserId)
                             .ToList();


                        Schedules = new ObservableCollection<Schedule>(_schedules1);
                        AllSchedule = new ObservableCollection<Schedule>(_schedules1);
                    }

                }
            }

        }

        public void ShowAllSchedules()
        {
            using (var context = new AppDbContext())
            {
                var _schedules = context.Schedules.ToList();

                if (_schedules.Count == 0)
                {
                    MessageBox.Show("No Schedule, please add one.");
                    Schedules = new ObservableCollection<Schedule>();
                }
                else
                {
                    var schedules = context.Schedules
                        .Where(a => a.UserId == _currentUserId)
                        .ToList();
                    Schedules = new ObservableCollection<Schedule>(schedules);
                }
            }
        }

        // add new schedule for the existed pet
        public void AddSchedule(string description, DateTime date)
        {
            using (var context = new AppDbContext())
            {
                // Verify existing schedules for the pet
                var existingSchedules = context.Schedules
                    .Where(a => a.PetId == SelectedPet.Id)
                    .ToList();
                var newSchedule = new Schedule
                {
                    Description = description,
                    Date = date,
                    PetId = SelectedPet.Id,
                    PetName = SelectedPet.PetName,
                    UserId = _currentUserId,  // get this from logged-in user
                    Type = SelectedScheduleType,
                };
                context.Schedules.Add(newSchedule);
                context.SaveChanges();
                // verify after saving
                var newSchedules = context.Schedules
                    .Where(a => a.PetId == SelectedPet.Id)
                    .ToList();
                System.Diagnostics.Debug.WriteLine($"Added new schedule for pet {SelectedPet.PetName} (ID: {SelectedPet.Id})");
                System.Diagnostics.Debug.WriteLine($"PetId: {newSchedule.PetId}");
                System.Diagnostics.Debug.WriteLine($"UserID: {newSchedule.UserId}");

                // Verify after saving
                var savedSchedule = context.Schedules
                    .FirstOrDefault(a => a.ScheduleId == newSchedule.ScheduleId);
                System.Diagnostics.Debug.WriteLine($"Saved Schedule ID: {savedSchedule?.ScheduleId}");


                // add to the list of schedules
                //Schedules.Add(newSchedule);
                // Create new ObservableCollection
                //Schedules = new ObservableCollection<Schedule>(Schedules);
                // notify user
                MessageBox.Show("New schedule added successfully.");
            }
        }


        public void RemoveSchedule()
        {

            try
            {
                if (_selectedSchedule == null)
                {
                    System.Windows.MessageBox.Show("Please select a schedule to remove", "Error");
                    return;
                }
                using (var context = new AppDbContext()) // Use a new context instance
                {
                    // Find the schedule in the database using its ID
                    var scheduleToRemove = context.Schedules.Find(_selectedSchedule.ScheduleId);

                    if (scheduleToRemove != null)
                    {
                        context.Schedules.Remove(scheduleToRemove);
                        context.SaveChanges();
                        LoadSchedules(); // Refresh the list
                        System.Windows.MessageBox.Show("Schedule removed successfully", "Success");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Schedule not found", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error removing: {ex.Message}", "Error");
            }
        }

        public void ShowParticularSchedudle()
        {
            if (_selectedSchedule == null)
            {
                System.Windows.MessageBox.Show("Please select a schedule to show", "Error");
                return;
            }
            else
            {
                System.Windows.MessageBox.Show($"Schedule: {_selectedSchedule.Type}\nDate: {_selectedSchedule.Date}\nPet: {_selectedSchedule.PetName}\nDescription: {_selectedSchedule.Description}", "Schedule Details");
            }

        }

    }
}
