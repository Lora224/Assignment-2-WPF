using Assignment_2_WPF.Views;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PetButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Pet view
            PetView petView = new PetView();
            // Show the PetView window, can do petView.ShowDialog(); also Opens as a modal dialog that will block MainWindow until it's closed
            petView.Show();
        }
    }
}