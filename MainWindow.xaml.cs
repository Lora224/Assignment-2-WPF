using Assignment_2_WPF.Models;
using Assignment_2_WPF.Views;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Security.Claims;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;


/*
    Views /: Contains XAML files for each class's UI representation.
    ViewModels /: Contains ViewModel classes that handle the logic for the corresponding views, implementing the MVVM pattern.
    Models/: Contains data models for each class (Pet, User, Reminder, Activity).
    Data /: Contains the DbContext for Entity Framework and repositories for data access, adhering to the repository pattern for better separation of concerns.
    Utilities/: Contains helper classes for shared functionality (e.g., notifications).
    Tests/: Contains unit tests for your models and any other relevant logic.
*/


namespace Assignment_2_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        private readonly string _dbPath;

        public MainWindow()
        {
            InitializeComponent();
            _dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PetApp.db");
            InitializeDatabase();

        }

        private void InitializeDatabase()
        {
            try

            {
                // Initialize database and add test data
                using (var context = new AppDbContext())
                {
                    context.Database.EnsureCreated();

                    // Only add test data if the database is empty
                    if (!context.Users.Any())
                    {
                        // Add test user
                        var user = new User("Test User", "test@test.com", "password");
                        context.Users.Add(user);
                        context.SaveChanges();
                        System.Diagnostics.Debug.WriteLine($"Added User ID: {user.Id}");

                        // Add test pet
                        var pet = new Pet
                        {
                            PetName = "Max",
                            UserId = user.Id,
                            Breed = "Labrador",
                            Dob = DateTime.Today,
                            Weight = 30
                        };
                        context.Pets.Add(pet);
                        context.SaveChanges();
                        System.Diagnostics.Debug.WriteLine($"Added Pet ID: {pet.Id}");

                        // Add test activity
                        var activity = new Activity
                        {
                            Name = "Morning Walk",
                            Date = DateTime.Today,
                            PetId = pet.Id,
                            PetName = pet.PetName,
                            UserId = user.Id,
                            Description = "30 minute walk"
                        };
                        context.Activities.Add(activity);
                        context.SaveChanges();
                        System.Diagnostics.Debug.WriteLine($"Added Activity for Pet ID: {activity.PetId}");
                    }

                    System.Diagnostics.Debug.WriteLine("Database initialized successfully");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error initializing database: {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner error: {ex.InnerException.Message}");
                }
                System.Windows.MessageBox.Show($"Database initialization error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    
        private void PetButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Pet view
            PetView petView = new PetView();
            // Show the PetView window, can do petView.ShowDialog(); also Opens as a modal dialog that will block MainWindow until it's closed
            petView.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ActivityView activityView = new ActivityView(); 
            activityView.ShowDialog();
        }



        private void GoToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DatabaseManagerView databaseManagerView = new DatabaseManagerView();
            databaseManagerView.Show();
        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            ScheduleView scheduleView = new ScheduleView();
            scheduleView.Show();
        }
    }
}