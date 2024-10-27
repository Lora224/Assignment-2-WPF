using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_WPF.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Pet> Pets { get; set; }

        // Constructor of class user, with ID generated automatically
        public User(string name, string email, string password)
        {
            Random random = new Random();
            Id = random.Next(100000, 999999);
            Name = name;
            Email = email;
            Password = password;
            Pets = new List<Pet>();
        }

        // method to crosscheck the password and email in database
        public static bool CheckValidate(string email, string password)
        {
            using (var context = new AppDbContext())
            {
                var users = context.Users.ToList();
                foreach (var user in users)
                {
                    if (user.Email == email && user.Password == password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // method to sign up an account with email and password input from user, first check if the email is already in the database, if not, create an account, if yes, notice the user


    }
}
