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
            // Connection string to MySQL database
            optionsBuilder.UseMySql("Server=localhost;Database=kul.luv13@gmail.com;User=root;Password=Abc123def@;",
                new MySqlServerVersion(new Version(8, 0, 39)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // maping user class to user table in MySQL
            modelBuilder.Entity<User>().ToTable("user");
            // Mapping Pet class to pet table in MySQL
            modelBuilder.Entity<Pet>().ToTable("pet");
            // Mapping Activity class to activity table in MySQL
            modelBuilder.Entity<Activity>().ToTable("activity");
            // Mapping Schedule class to schedule table in MySQL
            modelBuilder.Entity<Schedule>().ToTable("schedule");
        }
    }
}
