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
            // Generate random ID between 10000 and 99999
            Id = new Random().Next(10000, 99999);
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
    

    }
}
    

