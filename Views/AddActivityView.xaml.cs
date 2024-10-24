// AddActivityView.xaml.cs

using Assignment_2_WPF.ViewModels;
using Assignment_2_WPF.Models;
using System.Windows;

namespace Assignment_2_WPF.Views
{
    /// <summary>
    /// Interaction logic for AddActivityView.xaml
    /// </summary>
    public partial class AddActivityView : Window
    {

        public AddActivityView(DateTime selectedDate, Pet selectedPet)
        {
            InitializeComponent();
            DataContext = new AddActivityViewModel(selectedDate, selectedPet);
        }
    }
}