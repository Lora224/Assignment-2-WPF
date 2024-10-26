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
using System.Windows.Shapes;
using Assignment_2_WPF.Models;
using Assignment_2_WPF.ViewModels;
using Assignment_2_WPF.Views;

namespace Assignment_2_WPF.Views
{
    /// <summary>
    /// Interaction logic for AddNewSchedule.xaml
    /// </summary>
    public partial class AddNewSchedule : Window
    {
        public AddNewSchedule()
        {
            InitializeComponent();
        }

        //method to list all the pet that userID have and show the pet name in the combobox
        private void ShowPetList(object sender, RoutedEventArgs e)
        {
            List<Pet> pets = Pet.GetPetsByUserID(User.CurrentUser.UserID);
            PetComboBox.ItemsSource = pets;
            PetComboBox.DisplayMemberPath = "Name";
            PetComboBox.SelectedValuePath = "PetID";
        }

        //method to show options in the combobox for schedule type
        private void ShowScheduleType(object sender, RoutedEventArgs e)
        {
            List<string> scheduleTypes = new List<string> { "Vaccination", "Grooming", "Vet Visit" };
            SelectedScheduleTypeId.ItemsSource = scheduleTypes;
        }
        private void SaveButton(object sender, RoutedEventArgs e)
        {
            
            // petID is the value of the selected item in the combobox
            int petID = (int)PetComboBox.SelectedValue;
            string petName = PetComboBox.Text;
            //scheduleID auto generated
            int scheduleID = 0;
            // schedule type is the value of the selected item in the combobox
            string scheduleType = ScheduleTypeComboBox.Text;
            DateTime date = DateTime.SelectedDate.Value;
            string description = Description.Text;
            
            // notice the user
            System.Windows.Forms.MessageBox.Show("Schedule created successfully");
            // close the window
            this.Close();
        }
    }
}
