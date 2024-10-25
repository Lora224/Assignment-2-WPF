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
           
        }

        private void AddNewPetButton_Click(object sender, RoutedEventArgs e)
        {

            var addPetWindow = new AddNewPet(this.DataContext as PetViewModel);
            addPetWindow.ShowDialog();
        }

        private void EditPetButton_Click(object sender, RoutedEventArgs e)
        {
            //new screen for editing or ?
        }

        private void RemovePetButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RemovePet();  //remove the selected pet
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PetsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
