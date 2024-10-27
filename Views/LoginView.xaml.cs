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



namespace Assignment_2_WPF.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly UserViewModel ViewModel;
        public LoginView()
        {
            InitializeComponent();
            ViewModel = new UserViewModel();
        }

        private void Login1_Click(object sender, RoutedEventArgs e)
        {            
            // get input from textbox
            string email = EmailInput.Text;
            string password = PasswordBox.Password;
            //check email and password is valid or not in database
            if (Models.User.CheckValidate(email, password))
            {
                //if valid, go to main window
                //get the user from database
                int userId = ViewModel.GetUser(email);
                MainWindow mainWindow = new MainWindow(userId);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                //if not valid, notice the user
                System.Windows.Forms.MessageBox.Show("Email or Password is incorrect");
            }

        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            //go to registerview
            RegisterView registerView = new RegisterView();
            registerView.Show();
            this.Close();
        }
    }
}
