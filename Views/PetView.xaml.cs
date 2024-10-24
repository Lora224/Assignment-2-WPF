using System.Windows;
using System.Windows.Controls;
using Assignment_2_WPF.ViewModels;

namespace Assignment_2_WPF.Views
{
    public partial class PetView : Window
    {
        private PetViewModel viewModel;

        public PetView()
        {
            InitializeComponent();
            viewModel = new PetViewModel();
            DataContext = viewModel;
        }

        private void AddNewPetButton_Click(object sender, RoutedEventArgs e)
        {
           AddNewPet addNewPet = new AddNewPet();
           addNewPet.Show();
        }

        private void EditPetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemovePetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
