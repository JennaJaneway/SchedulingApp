using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp
    {
    public static class TimeConverter
        {
        // Converts a UTC DateTime to Eastern and Local Time
        public static (string Eastern, string Local) FormatAppointmentTimes(DateTime utcDateTime)
            {
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, easternZone);

            DateTime localTime = utcDateTime.ToLocalTime();

            string easternFormatted = easternTime.ToString("MMM dd, yyyy h:mm tt");
            string localFormatted = localTime.ToString("MMM dd, yyyy h:mm tt");

            return (easternFormatted, localFormatted);
            }

        // Converts a local time (entered by the user) to UTC for DB storage
        public static DateTime ConvertLocalToUtc(DateTime localDateTime)
            {
            return localDateTime.ToUniversalTime();
            }
        }
    }
