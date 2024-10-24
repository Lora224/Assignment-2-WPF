using Assignment_2_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_WPF.Ultilities
{
    class DatabaseManager
    {
        public static bool DatabaseExists()
        {
            using (var context = new AppDbContext())
            {
                return context.Database.CanConnect();
            }
        }

        // Method to add a new pet
        public static void AddPet(Pet pet)
        {
            using (var context = new AppDbContext())
            {
                context.Pets.Add(pet);
                context.SaveChanges();
            }
        }

        // Method to add a new activity
        public static void AddActivity(Activity activity)
        {
            using (var context = new AppDbContext())
            {
                context.Activities.Add(activity);
                context.SaveChanges();
            }
        }

        // Method to clear all data (use carefully!)
        public static void ClearAllData()
        {
            using (var context = new AppDbContext())
            {
                // Remove all activities first (due to foreign key constraints)
                context.Activities.RemoveRange(context.Activities);

                // Remove all pets
                context.Pets.RemoveRange(context.Pets);

                context.SaveChanges();
            }
        }

        // Method to reset database to initial state
        public static void ResetDatabase()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        // Method to print current database statistics
        public static void PrintDatabaseStats()
        {
            using (var context = new AppDbContext())
            {
                var petCount = context.Pets.Count();
                var activityCount = context.Activities.Count();

                System.Diagnostics.Debug.WriteLine($"Database Statistics:");
                System.Diagnostics.Debug.WriteLine($"Total Pets: {petCount}");
                System.Diagnostics.Debug.WriteLine($"Total Activities: {activityCount}");
            }
        }
    }
}

