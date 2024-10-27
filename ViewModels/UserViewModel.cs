using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_2_WPF.Models;
namespace Assignment_2_WPF.ViewModels
{
    class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }



        public UserViewModel()
        {

        }
        public int GetUser(string email)
        {
            using (var context = new AppDbContext())
            {
                var users = context.Users.ToList();
                foreach (var user in users)
                {
                    if (user.Email == email)
                    {
                        return user.Id;
                    }
                }
            }
            return 0;
        }

    }
}
