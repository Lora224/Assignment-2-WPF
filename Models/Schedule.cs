using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_WPF.Models
{
    public class Schedule
    {
        public int petId, scheduleId;
        public string petName, scheduleType, description;
        public DateTime date, time;
        public int PetId { get; set; }
        public string PetName { get; set; }
        public int ScheduleId { get; set; }
        public string ScheduleType { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }

        // constructor of class Schedule, with petId and petName from class Pet, scheduleID auto generated
        public Schedule(int petId, string petName, int scheduleId, string scheduleType, DateTime date, DateTime time, string description)
        {
            this.petId = petId;
            this.petName = petName;
            Random random = new Random();
            this.scheduleId = random.Next(100000, 999999);
            this.scheduleType = scheduleType;
            this.date = date;
            this.time = time;
            this.description = description;
        }

    }
}


