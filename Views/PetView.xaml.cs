using System.Windows;
using System.Windows.Controls;
using Assignment_2_WPF.ViewModels;

namespace Assignment_2_WPF.Views
{
    public partial class PetView : Window
    {
        private PetViewModel viewModel;
       // private System.Windows.Controls.ListBox petslist;
        public PetView()
        {
            InitializeComponent();
            viewModel = new PetViewModel();
            this.DataContext = viewModel;
            if (viewModel.Pets.Count == 0)
            {
                System.Windows.MessageBox.Show("You have no pets. Please add a pet.");
                var addNewPet = new AddNewPet(this.DataContext as PetViewModel);
                addNewPet.ShowDialog();
            }
           
        }

        private void AddNewPetButton_Click(object sender, RoutedEventArgs e)
        {

            var addPetWindow = new AddNewPet(this.DataContext as PetViewModel);
            addPetWindow.ShowDialog();
        }



        private void RemovePetButton_Click(object sender, RoutedEventArgs e)
        {
            //show confirmation
            var result = System.Windows.MessageBox.Show("Are you sure you want to remove this pet?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                viewModel.RemovePet();  //remove the selected pet
            }
            if (viewModel.Pets.Count == 0)
            {
                System.Windows.MessageBox.Show("You have no pets. Please add a pet.");
                var addNewPet = new AddNewPet(this.DataContext as PetViewModel);
                addNewPet.ShowDialog();
            }
        }


        private void PetsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            //show the details of the selected pet
            viewModel.ShowDetails();
        }
    }
}
