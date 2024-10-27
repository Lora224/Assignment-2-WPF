using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_WPF.Models
{
    using Microsoft.VisualBasic.ApplicationServices;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Windows;

    public class Activity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public int PetId { get; set; }  //not nullable foreign key

        [ForeignKey("PetId")]

        public int? Duration { get; set; }
        public int? Distance { get; set; }
        public ActivityLevel? Level { get; set; }
        public string PetName { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public string? Description { get; set; }
        public ActivityType Type { get; set; }

 
        public enum ActivityLevel { 
            Low,
            Medium,
            High
        }
        public enum ActivityType { 
            Feeding,
            Playing,
            Walking,
            Other
        }

        public Activity(int id, string name, DateTime date, int petId, string description,ActivityType type, int userId)
        {
            Id = new Random().Next(10000, 99999);
            Name = name;
            Date = date;
            PetId = petId;
            UserId = userId;
            Description = description;
            Type = type;
           
        }
        public Activity() {
            Id = new Random().Next(10000, 99999);
            Name = "null";
            Date = DateTime.Today;
            PetId = -1;
            UserId = -1;
            Description = "null";       
        }

        public bool ValidateActivityData()
        {
            if (PetId == -1)
                return false;

            if (Duration <= 0)
                return false;

            if (Type == ActivityType.Walking && Distance <= 0)
                return false;

            return true;
        }

  

    }
}


