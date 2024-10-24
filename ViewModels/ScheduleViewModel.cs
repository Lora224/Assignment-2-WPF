using Assignment_2_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_WPF.ViewModels
{

    public class ScheduleViewModel:ViewModelBase
    {
        private readonly AppDbContext _context;
        private DateTime _selectedDate;
        private Schedule _selectedSchedule;
        private ObservableCollection<Schedule> _schedules;
        public Schedule SelectedSchedule
        {
            get => _selectedSchedule;
            set
            {
                _selectedSchedule = value;
                OnPropertyChanged(nameof(SelectedSchedule));
            }
        }
    }
}
