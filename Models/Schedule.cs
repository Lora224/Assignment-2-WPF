using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Assignment_2_WPF.Models
{
    using Microsoft.VisualBasic.ApplicationServices;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Windows;
    public class Schedule
    {
        public int petId, scheduleId, userID;
        public string petName, scheduleType, description;
        public DateTime date;

        [Key]
        public int ScheduleId { get; set; }

        public int PetId { get; set; }
        [ForeignKey("PetId")]

        public int? UserId { get; set; }
        [ForeignKey("UserId")]

        public string PetName { get; set; }

        public ScheduleType Type { get; set; }
        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public enum ScheduleType
        {
            VetCheckIn,
            Vaccination,
            Grooming,
            Other
        }

        public Schedule()
        {
            ScheduleId = new Random().Next(10000, 99999);
            Date = DateTime.Today;
            PetId = -1;
            UserId = -1;
            Description = "null";
        }

        public Schedule(int petId, string petName, int userId, ScheduleType type, DateTime date, string description)
        {
            ScheduleId = new Random().Next(10000, 99999);
            PetId = petId;
            PetName = petName;
            UserId = userId;
            Type = type;
            Date = date;
            Description = description;
        }



    }
}

