using Calendar.Models;
using System;
using System.ComponentModel;
using System.Timers;

namespace Calendar.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        private System.Timers.Timer _timer;
        public DateTime SelectedDate { get; set; } = DateTime.Now;

        public int WorkDaysUntilSummer => WorkDaysCalculator.GetWorkDaysUntilSummer(SelectedDate);
        public int WorkHoursUntilSummer => WorkDaysCalculator.GetWorkHoursUntilSummer(SelectedDate);
        public int WorkMinutesUntilSummer => WorkDaysCalculator.GetWorkMinutesUntilSummer(SelectedDate);
        public int WorkSecondsUntilSummer => WorkDaysCalculator.GetWorkSecondsUntilSummer(SelectedDate);

        public event PropertyChangedEventHandler PropertyChanged;

        public CalendarViewModel()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (sender, args) =>
            {
                SelectedDate = DateTime.Now;
                OnPropertyChanged(nameof(WorkDaysUntilSummer));
                OnPropertyChanged(nameof(WorkHoursUntilSummer));
                OnPropertyChanged(nameof(WorkMinutesUntilSummer));
                OnPropertyChanged(nameof(WorkSecondsUntilSummer));
            };
            _timer.Start();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
