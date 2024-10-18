using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_WPF.Models
{
    public class Pet
    {
        private int petId, userId, weight;
        private string petName, breed;
        public int PetId { get; set; }
        public string PetName { get; set; }
        public int UserId { get; set; }
        public string Breed { get; set; }
        public DateTime dob { get; set; }
        public int Weight { get; set; }
        public List<Activity> activities;
        public List<Schedule> schedules;
                              

        //constructor of class Pet, with petId generated automatically
        public Pet(int userId, string petName, string breed, DateTime dob, int weight)
        {
            Random random = new Random();
            petId = random.Next(100000, 999999);
            this.userId = userId;
            this.petName = petName;
            this.breed = breed;
            this.dob = dob;
            this.weight = weight;
            activities = new List<Activity>();
            schedules = new List<Schedule>();
        }

        // using Entity framework to query all pet from table pet in MySQL, and then count the number of pets
        public int countPet()
        {
            using (var context = new AppDbContext())
            {
                var pets = context.Pets.ToList();
                return pets.Count;
            }
        }

        // if pets.Count ==0, show petMenuNoPet, else show petMenu
        public void petMenuCheck()
        {
            if (countPet() == 0)
            {
                petMenuNoPet();
            }
            else
            {
                petMenu();
            }
        }

        // Pet menu when account has no pet
        public void petMenuNoPet()
        {
            Console.WriteLine("Your account donot have any pet, please create a new one.");
            addNewPet();
        }

        // pet menu when account has pet
        public void petMenu()
        {
            Console.WriteLine("Pet Menu");
            Console.WriteLine("1. Show all pets");
            Console.WriteLine("2. Show pet details");
            Console.WriteLine("3. Add new pet");
            Console.WriteLine("4. Edit pet details");
            Console.WriteLine("5. Remove pet");
            Console.WriteLine("6. Return");
            Console.WriteLine("7. Exit");
        }
        // switch case for pet menu
        public void petMenuSwitch(int choice)
        {
            switch (choice)
            {
                case 1:
                    showAllPets();
                    break;
                case 2:
                    showPetDetails();
                    break;
                case 3:
                    addNewPet();
                    break;
                case 4:
                    editPetDetails();
                    break;
                case 5:
                    removePet();
                    break;
                case 6:
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        public void showAllPets()
        {
            using (var context = new AppDbContext())
            {
                // Query all pets from the database
                var pets = context.Pets.ToList();

                // Display the results
                foreach (var pet in pets)
                {
                    Console.WriteLine($"Pet ID: {pet.PetId}, Name: {pet.PetName}, Breed: {pet.Breed}, Weight: {pet.Weight}");
                }
            }
        }
        public void showPetDetails()
        {
            Console.WriteLine("Enter the pet ID: ");
            int petId = Convert.ToInt32(Console.ReadLine());
            using (var context = new AppDbContext())
            {
                // Query the pet from the database
                var pet = context.Pets.Where(p => p.PetId == petId).FirstOrDefault();
                // Display the results
                if (pet != null)
                {
                    Console.WriteLine($"Pet ID: {pet.PetId}, Name: {pet.PetName}, Breed: {pet.Breed}, Weight: {pet.Weight}");
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
            string ?nameTemp;
            //add pet with details filled in forms to construct a new pet object? and write in to database
            Console.Write("name");
            nameTemp = Console.ReadLine();
            DateTime date = new DateTime(2000, 2, 24); //test data for date
            Pet newPet = new Pet(userId, nameTemp,"Dog",date,50);
          using (var context = new AppDbContext())
            {
                context.Pets.Add(newPet);
                context.SaveChanges();
            }
            // press any key to return to the pet menu
            Console.WriteLine("Press any key to return to the pet menu");
            Console.ReadKey();


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
