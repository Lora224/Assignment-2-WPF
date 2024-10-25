using Assignment_2_WPF.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Assignment_2_WPF.Utilities
{
    public class DatabaseManager
    {
        // Method to add a test pet with necessary dependencies
        public static void AddTestPet(Pet pet)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    // Check if we have any users
                    var user = context.Users.FirstOrDefault();
                    if (user == null)
                    {
                        // Create a test user if none exists
                        user = new User("Test User", "test@test.com", "password");
                        context.Users.Add(user);
                        context.SaveChanges();
                        Debug.WriteLine($"Created new test user with ID: {user.Id}");
                    }

                    // Assign the user ID to the pet
                    pet.UserId = user.Id;

                    // Add the pet
                    context.Pets.Add(pet);
                    context.SaveChanges();
                    Debug.WriteLine($"Added pet {pet.PetName} with ID: {pet.Id}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error adding pet: {ex.Message}");
                    throw;
                }
            }
        }

        // Method to reset database with initial test data
        public static void ResetDatabase()
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    // Add initial test user
                    var user = new User("Test User", "test@test.com", "password");
                    context.Users.Add(user);
                    context.SaveChanges();
                    Debug.WriteLine("Database reset complete with initial test user");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error resetting database: {ex.Message}");
                    throw;
                }
            }
        }

        // Enhanced database statistics
        public static void PrintDatabaseStats()
        {
            using (var context = new AppDbContext())
            {
                var userCount = context.Users.Count();
                var petCount = context.Pets.Count();
                var activityCount = context.Activities.Count();

                Debug.WriteLine("=== Database Statistics ===");
                Debug.WriteLine($"Total Users: {userCount}");
                Debug.WriteLine($"Total Pets: {petCount}");
                Debug.WriteLine($"Total Activities: {activityCount}");

                // Print more detailed information
                foreach (var user in context.Users)
                {
                    Debug.WriteLine($"\nUser: {user.Name} (ID: {user.Id})");
                    var userPets = context.Pets.Where(p => p.UserId == user.Id).ToList();
                    foreach (var pet in userPets)
                    {
                        Debug.WriteLine($"  - Pet: {pet.PetName} (ID: {pet.Id})");
                        var petActivities = context.Activities.Where(a => a.PetId == pet.Id).ToList();
                        foreach (var activity in petActivities)
                        {
                            Debug.WriteLine($"    * Activity: {activity.Name} on {activity.Date:d}");
                        }
                    }
                }
            }
        }

       
    }
}