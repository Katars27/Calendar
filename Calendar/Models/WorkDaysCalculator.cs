using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public class WorkDaysCalculator
    {
        private static HashSet<DateTime> GetHolidays(int year)
        {
          return new HashSet<DateTime>
                 {
                     // Ноябрь 2024
                     new DateTime(year, 11, 4),  // День народного единства

                     // Декабрь 2024
                    new DateTime(year, 12, 30),  // Новый год
                    new DateTime(year, 12, 31),  // Новый год

                     // Январь 2025
                     new DateTime(year +1, 1, 1),  // Новый год
                     new DateTime(year +1, 1, 2),  // Новогодние каникулы
                     new DateTime(year +1, 1, 3),  // Новогодние каникулы
                     new DateTime(year +1, 1, 4),  // Новогодние каникулы
                     new DateTime(year +1, 1, 5),  // Новогодние каникулы
                     new DateTime(year +1, 1, 6),  // Новогодние каникулы
                     new DateTime(year +1, 1, 7),  // Рождество Христово
                     new DateTime(year +1, 1, 8),  // Новогодние каникулы

                     // Февраль 2025
                     new DateTime(year +1, 2, 23),  // 23 февраля

                    // Март 2025
                    new DateTime(year +1, 3, 8),   // Международный женский день 

                    // Май 2025
                    new DateTime(year +1, 5, 1),   // Праздник Весны и Труда
                    new DateTime(year +1, 5, 2),   // Майские праздники
                    new DateTime(year +1, 5, 3),   // Майские праздники
                    new DateTime(year +1, 5, 4),   // Майские праздники

                    new DateTime(year +1, 5, 8),   // Праздник Дня Победы
                    new DateTime(year +1, 5, 9),   // День Победы
                 };
        }

        public static int GetWorkDaysUntilSummer(DateTime currentDate)
        {
            DateTime summerStartDate = new DateTime(currentDate.Year, 6, 1);

            if (currentDate >= summerStartDate)
            {
                summerStartDate = new DateTime(currentDate.Year + 1, 6, 1);
            }

            HashSet<DateTime> holidays = GetHolidays(currentDate.Year);

            int workDays = 0;

            for (DateTime date = currentDate; date < summerStartDate; date = date.AddDays(1))
            {
                if (IsWorkDay(date, holidays))
                {
                    workDays++;
                }
            }

            return workDays;
        }

        public static int GetWorkHoursUntilSummer(DateTime currentDate)
        {
            int workDays = GetWorkDaysUntilSummer(currentDate);
            int currentHour = currentDate.Hour;
            int workHoursToday = (currentHour < 8) ? 8 : (currentHour < 16 ? 16 - currentHour : 0);
            return (workDays - 1) * 8 + workHoursToday;
        }

        public static int GetWorkMinutesUntilSummer(DateTime currentDate)
        {
            int workHours = GetWorkHoursUntilSummer(currentDate);
            int currentMinute = currentDate.Minute;
            int workMinutesToday = (currentMinute < 60) ? 60 - currentMinute : 0;
            return (workHours - 1) * 60 + workMinutesToday;
        }

        public static int GetWorkSecondsUntilSummer(DateTime currentDate)
        {
            int workMinutes = GetWorkMinutesUntilSummer(currentDate);
            int currentSecond = currentDate.Second;
            int workSecondsToday = (currentSecond < 60) ? 60 - currentSecond : 0;
            return (workMinutes - 1) * 60 + workSecondsToday;
        }

        private static bool IsWorkDay(DateTime date, HashSet<DateTime> holidays)
        {
            return date.DayOfWeek != DayOfWeek.Saturday &&
                   date.DayOfWeek != DayOfWeek.Sunday &&
                   !holidays.Contains(date.Date);
        }
    }
}
