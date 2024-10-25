using Assignment_2_WPF.Models;
using Assignment_2_WPF.Utilities;
using System;
using System.Windows;

namespace Assignment_2_WPF.Views
{
    public partial class DatabaseManagerView : Window
    {
        public DatabaseManagerView()
        {
            InitializeComponent();
        }

        private void ViewStats_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseManager.PrintDatabaseStats();
                System.Windows.MessageBox.Show("Database statistics have been written to the debug output.", "Success");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error viewing database stats: {ex.Message}", "Error");
            }
        }

        private void AddTestPet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pet = new Pet(12345, "Test Pet", "Test Breed", DateTime.Today, 25);
                DatabaseManager.AddTestPet(pet);  // Use the new method
                System.Windows.MessageBox.Show("Test pet added successfully!", "Success");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding pet: {ex.Message}", "Error");
            }
        }

        private void ResetDatabase_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show(
                "This will delete all data and create a new database with initial test data! Are you sure?",
                "Warning",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    DatabaseManager.ResetDatabase();
                    System.Windows.MessageBox.Show("Database reset complete with initial test data.", "Success");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error resetting database: {ex.Message}", "Error");
                }
            }
        }
    }
}