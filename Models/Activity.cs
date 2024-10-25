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

        public virtual Pet? Pet { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public string Description { get; set; }

        public Activity(int id, string name, DateTime date, int petId, Pet? pet, string description,int userId)
        {
            Id = new Random().Next(10000, 99999);
            Name = name;
            Date = date;
            PetId = petId;
            UserId = userId;
            Pet = pet;
            Description = description;
           
        }
        public Activity() {
            Id = new Random().Next(10000, 99999);
            Name = "null";
            Date = DateTime.Today;
            PetId = -1;
            UserId = -1;
            Pet = null;
            Description = "null";       
        }  
        
    }
}


