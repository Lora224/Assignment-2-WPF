using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_2_WPF.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_2_WPF.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }  // Primary key should not be nullable

        public string PetName { get; set; }
        public int UserId { get; set; }
        public string Breed { get; set; }
        public DateTime Dob { get; set; }
        public int Weight { get; set; }

        // Navigation properties
        public virtual List<Activity>? Activities { get; set; }
        public virtual List<Schedule>? Schedules { get; set; }

        // Default constructor
        public Pet()
        {
            Id = -1;
            PetName ="null";
            Breed = "null";
            Weight = 0;
            UserId = -1;
            Dob = DateTime.Today;
            Activities = new List<Activity>();
            Schedules = new List<Schedule>();
        }

        // Constructor with parameters
        public Pet(int userId, string petName, string breed, DateTime dob, int weight)
        {
            // Generate random ID between 10000 and 99999
            Id = new Random().Next(10000, 99999);
            UserId = userId;
            PetName = petName;
            Breed = breed;
            Dob = dob;
            Weight = weight;
            Activities = new List<Activity>();
            Schedules = new List<Schedule>();
        }
    

    // using Entity framework to query all pet from table pet in PetApp.db, and then count the number of pets
    public static int countPet()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var pets = context.Pets.ToList();
                return pets.Count;
            }
        }


        // if pets.Count ==0, show petMenuNoPet, else show petMenu
        public void petCheck()
        {
            if (countPet() != 0)
            {
                petMenu();
            }
            else
            {
                petMenuNoPet();
            }
        }

        // Pet menu when account has no pet
        public void petMenuNoPet()
        {
            // design in WPF
            Console.WriteLine("Your account donot have any pet, please create a new one.");
            Console.WriteLine("1. Add new pet");
            Console.WriteLine("2. Back");
            Console.WriteLine("Please enter your choice: "); // in wpf this should be a button to choose the option
            string choice = Console.ReadLine();  
            switch (choice)
            {
                case "1":
                    addNewPet();
                    break;
                case "2":
                    petCheck();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again");
                    petMenuNoPet();
                    break;
            }
        }

        // pet menu when account has pet
        public void petMenu()
        {
            // design in WPF
            Console.WriteLine("Pet Menu");
            Console.WriteLine("1. Show all pets");
            Console.WriteLine("2. Show pet details");
            Console.WriteLine("3. Add new pet");
            Console.WriteLine("4. Edit pet details");
            Console.WriteLine("5. Remove pet");
            Console.WriteLine("6. Return");
            Console.WriteLine("7. Exit");
            Console.WriteLine("Please enter your choice: "); // in wpf this should be a button to choose the option
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    showAllPets();
                    break;
                case "2":
                    showPetDetails();
                    break;
                case "3":
                    addNewPet();
                    break;
                case "4":
                    editPetDetails();
                    break;
                case "5":
                    removePet();
                    break;
                case "6":
                    break;
                case "7":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again");
                    break;
            }
        }

        public void showAllPets()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                // Query all pets from the database
                var pets = context.Pets.ToList();

                // Display the results
                foreach (var pet in pets)
                {
                    //Console.WriteLine("Pet ID: " + pet.PetId);
                    Console.WriteLine("Pet Name: " + pet.PetName);
                    Console.WriteLine("Breed: " + pet.Breed);
                    Console.WriteLine("DOB: " + pet.Dob);
                    Console.WriteLine("Weight: " + pet.Weight);
                    Console.WriteLine();
                }
            }
        }
        public void showPetDetails()
        {
            Console.WriteLine("Enter the pet ID: "); //in WPF this should be a button to choose the pet? or textbox to input petId
            int petId = Convert.ToInt32(Console.ReadLine());
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                // Query the pet from the database
                var pet = context.Pets.Where(p => p.Id == petId).FirstOrDefault();
                // Display the results
                if (pet != null)
                {
                    Console.WriteLine("Pet Name: " + pet.PetName);
                    Console.WriteLine("Breed: " + pet.Breed);
                    Console.WriteLine("DOB: " + pet.Dob);
                    Console.WriteLine("Weight: " + pet.Weight);
                    // activity list
                    // schedule list - toString() in schedule class
                }
                else
                {
                    Console.WriteLine("Pet not found");
                }
            }
            // press any key to return to the pet menu
            Console.WriteLine("Press any key to return to the pet menu");
            Console.ReadKey();
        }

        public void addNewPet()
        {
            Console.WriteLine("Add pet ");
            string? nameTemp;
            //add pet with details filled in forms to construct a new pet object? and write in to database
            Console.Write("name");
            nameTemp = Console.ReadLine();
            DateTime date = new DateTime(2000, 2, 24); //test data for date

            Pet newPet = new Pet(UserId, nameTemp, "Dog", date, 50);
            using (var context = new AppDbContext())
                  {
                      context.Pets.Add(newPet);
                      context.SaveChanges();
                  }

              

        }
        public void editPetDetails()
        {
            Console.WriteLine("Update pet details ");
        }
        public void removePet()
        {
            Console.WriteLine("Remove pet ");
        }

    }
}
    

