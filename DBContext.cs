using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_2_WPF.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_2_WPF
{
    public class DBContext
    {
        // Represents the "User" table in database
        public List<User> Users { get; set; }
        
        // Represents the "Pet" table in database
        public List<Pet> Pets { get; set; }
        // Represents the "Activity" table in database
        public List<Activity> Activities { get; set; }
        // Represents the "Schedule" table in database
        public List<Schedule> Schedules { get; set; }


    }
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Schedule> Schedules { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // create a .db file in the project directory
            optionsBuilder.UseSqlite("Data Source=PetApp.db");
        }        
    }
}
