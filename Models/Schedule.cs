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


        public void scheduleCheck()
        {
            Pet pet = new Pet(0, "", "", DateTime.Now, 0);
            if (Pet.countPet() != 0)
            { 
                if (countSchedule() != 0)
                {
                    scheduleMenu();
                }
                else
                {
                    scheduleMenuNoSchedule();
                }
            }
            else
            {
                pet.petMenuNoPet();
            }
        }
        // using Entity framework to query all schedule from table schedule in PetApp.db, and then count the number of schedule
        public int countSchedule()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var schedules = context.Schedules.ToList();
                return schedules.Count;
            }
        }

        public void scheduleMenuNoSchedule()
        {
            Console.WriteLine("Your account donot have any schedule, please create a new one.");
            Console.WriteLine("1. Add Schedule");
            Console.WriteLine("2. Back");
            Console.WriteLine("Please enter your choice: ");
            string choice = Console.ReadLine(); // in WPF this should be a button to choose the option/ or a textbox?
            switch (choice)
            {
                case "1":
                    addSchedule();
                    break;
                case "2":
                    scheduleCheck();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again");
                    scheduleMenuNoSchedule();
                    break;
            }
        }
        public void scheduleMenu()
        {
            Console.WriteLine("Schedule Menu");
            Console.WriteLine("1. Add Schedule");
            Console.WriteLine("2. Show All Schedule");
            Console.WriteLine("3. Show Schedule Details"); // choose schedule by id
            Console.WriteLine("4. Edit schedule");
            Console.WriteLine("5. Remove Schedule");
            Console.WriteLine("6. Return");
            Console.WriteLine("7. Exit");
            Console.WriteLine("Please enter your choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    addSchedule();
                    break;
                case "2":
                    showAllSchedule();
                    break;
                case "3":
                    showScheduleDetails();
                    break;
                case "4":
                    editSchedule();
                    break;
                case "5":
                    removeSchedule();
                    break;
                case "6":
                    break;
                case "7":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again");
                    scheduleMenu();
                    break;
            }
        }

        public void showAllSchedule()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var schedules = context.Schedules.ToList();
                foreach (var schedule in schedules)
                {
                    //Console.WriteLine("Schedule ID: " + schedule.ScheduleId);
                    //Console.WriteLine("Pet ID: " + schedule.PetId);
                    Console.WriteLine("Pet Name: " + schedule.PetName);
                    Console.WriteLine("Schedule Type: " + schedule.ScheduleType);
                    Console.WriteLine("Date: " + schedule.Date);
                    Console.WriteLine("Time: " + schedule.Time);
                    Console.WriteLine("Description: " + schedule.Description);
                    Console.WriteLine();
                }
            }
            scheduleMenu();
        }

        public void showScheduleDetails()
        {
            Console.WriteLine("Enter the schedule ID: "); // in WPF: choose schedule to see details
            int scheduleId = Convert.ToInt32(Console.ReadLine()); // in WPF: click to choose schedule
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var schedule = context.Schedules.Where(s => s.ScheduleId == scheduleId).FirstOrDefault();
                if (schedule != null)
                {
                    //Console.WriteLine("Schedule ID: " + schedule.ScheduleId);
                    //Console.WriteLine("Pet ID: " + schedule.PetId);
                    Console.WriteLine("Pet Name: " + schedule.PetName);
                    Console.WriteLine("Schedule Type: " + schedule.ScheduleType);
                    Console.WriteLine("Date: " + schedule.Date);
                    Console.WriteLine("Time: " + schedule.Time);
                    Console.WriteLine("Description: " + schedule.Description);
                }
                else
                {
                    Console.WriteLine("Schedule not found");
                }
            }
            scheduleMenu();
        }
        // method to add new schedule into the pet existed in the database
        public void addSchedule()
        {
            Console.WriteLine("Enter the pet ID: "); // choose pet from dropdown list
            int petId = Convert.ToInt32(Console.ReadLine());
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var pet = context.Pets.Where(p => p.Id == petId).FirstOrDefault();
                if (pet != null)
                {
                    Console.WriteLine("Enter the schedule type: "); // choose from dropdown list
                    string scheduleType = Console.ReadLine();
                    Console.WriteLine("Enter the date: "); // choose date from calendar
                    DateTime date = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter the time: "); // choose time from calendar
                    DateTime time = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter the description: ");
                    string description = Console.ReadLine();
                    Schedule schedule = new Schedule(petId, pet.PetName, 0, scheduleType, date, time, description);
                    context.Schedules.Add(schedule);
                    context.SaveChanges();
                    Console.WriteLine("Schedule added successfully");
                }
                else
                {
                    Console.WriteLine("Pet not found");
                }
            }
            scheduleMenu();
        }

        // method to edit schedule details: choose schedule by ID to edit from PetApp.db, then edit the details, then save new details to database
        public void editSchedule()
        {
            Console.WriteLine("Enter the schedule ID: "); // choose schedule from calendar
            int scheduleId = Convert.ToInt32(Console.ReadLine());
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var schedule = context.Schedules.Where(s => s.ScheduleId == scheduleId).FirstOrDefault();
                if (schedule != null)
                {
                    Console.WriteLine("Enter the schedule type: "); // choose from dropdown list
                    string scheduleType = Console.ReadLine();
                    Console.WriteLine("Enter the date: ");
                    DateTime date = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter the time: ");
                    DateTime time = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter the description: ");
                    string description = Console.ReadLine();
                    schedule.ScheduleType = scheduleType;
                    schedule.Date = date;
                    schedule.Time = time;
                    schedule.Description = description;
                    context.SaveChanges();
                    Console.WriteLine("Schedule updated successfully");
                }
                else
                {
                    Console.WriteLine("Schedule not found");
                }
            }
        }

        // method to remove schedule from the database
        public void removeSchedule()
        {
            Console.WriteLine("Enter the schedule ID: "); // choose schedule from calendar
            int scheduleId = Convert.ToInt32(Console.ReadLine());
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var schedule = context.Schedules.Where(s => s.ScheduleId == scheduleId).FirstOrDefault();
                if (schedule != null)
                {
                    context.Schedules.Remove(schedule);
                    context.SaveChanges();
                    Console.WriteLine("Schedule removed successfully");
                }
                else
                {
                    Console.WriteLine("Schedule not found");
                }
            }

        }
    }
}


