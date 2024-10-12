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
            viewModel.AddNewPet();
        }

        private void EditPetButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPet != null)
            {
                viewModel.EditPet();
            }
            else
            {
                MessageBox.Show("Please select a pet to edit.");
            }
        }

        private void RemovePetButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPet != null)
            {
                viewModel.RemovePet();
            }
            else
            {
                MessageBox.Show("Please select a pet to remove.");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.HasUnsavedChanges)
            {
                viewModel.SaveChanges();
                MessageBox.Show("Changes saved successfully.");
            }
            else
            {
                MessageBox.Show("No changes to save.");
            }
        }
    }
}
