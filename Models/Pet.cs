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
        public DateOnly? dob { get; set; }
        public int Weight { get; set; }
        public List<Activity> activities;
        public List<Schedule> schedules;

        //generate pet ID 
        public void generatePetId()
        {
            Console.WriteLine("Generate pet ID ");
        }
        public void showAllPetDetails()
        {
            Console.WriteLine("Show all pet ");
     
        }
        public void showPetDetails()
        {
            Console.WriteLine("Show pet details ");
        }
        public void addNewPet()
        {
            Console.WriteLine("Add pet ");
        }
        public void editPetDetails()
        {
            Console.WriteLine("Update pet details ");
        }
        public void deletePet()
        {
            Console.WriteLine("Delete pet ");
        }

    }
}
