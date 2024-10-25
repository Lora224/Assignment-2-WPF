using Assignment_2_WPF.ViewModels;
using System.Windows;

namespace Assignment_2_WPF.Views
{
    public partial class AddNewPet : Window
    {
        private readonly PetViewModel _viewModel;

        public AddNewPet(PetViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void AddNewPet1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get values from controls
                string petName = Convert.ToString(PetName.Text);
                string breed = Convert.ToString(Breed.Text);
                string weight = Convert.ToString(Weight.Text);
                var dob = Dob1.SelectedDate ?? DateTime.Today;

                // Try to add the pet
                if (_viewModel.AddNewPet(petName, dob, breed, weight))
                {
                    // If successful, close the window
                    this.Close();
                }
                // If not successful, the AddNewPet method will show appropriate error messages
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding pet: {ex.Message}", "Error");
            }
        }
    }
}