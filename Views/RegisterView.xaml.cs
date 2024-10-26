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


namespace Assignment_2_WPF.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void Submit_Button(object sender, RoutedEventArgs e)
        {
            // get input from textbox
            string email = EmailInput.Text;
            string password = PasswordBox.Password;
            string name = NameInput.Text;
            // check email is existed in database or not
            using (var context = new AppDbContext())
            {
                // ensure database is created
                context.Database.EnsureCreated();
                var users = context.Users.ToList();
                foreach (var existingUser in users)
                {
                    if (existingUser.Email == email)
                    {
                        // if existed, notice the user
                        System.Windows.Forms.MessageBox.Show("Email is already existed");
                        return;
                    }
                }
                // if not existed, create a new account
                User newUser = new User(name, email, password);
                context.Users.Add(newUser);
                context.SaveChanges();
                // notice the user
                System.Windows.Forms.MessageBox.Show("Account created successfully");
            }
        }

        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            // go back to login view
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }
    }
}
