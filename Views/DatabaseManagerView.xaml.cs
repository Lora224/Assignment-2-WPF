using Assignment_2_WPF.Models;
using Assignment_2_WPF.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment_2_WPF.Views
{
    /// <summary>
    /// Interaction logic for DatabaseManagerView.xaml
    /// </summary>
    public partial class DatabaseManagerView : Window
    {
        public DatabaseManagerView()
        {
            InitializeComponent();
        }
        private void ViewStats_Click(object sender, RoutedEventArgs e)
        {
            DatabaseManager.PrintDatabaseStats();
        }

        private void AddTestPet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pet = new Pet(12345, "Test Pet", "Test Breed", DateTime.Today, 25);
                DatabaseManager.AddPet(pet);
                System.Windows.MessageBox.Show("Test pet added successfully!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding pet: {ex.Message}");
            }
        }

        private void ResetDatabase_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("This will delete all data! Are you sure?",
                               "Warning",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DatabaseManager.ResetDatabase();
                System.Windows.MessageBox.Show("Database reset complete.");
            }
        }
    }
}
