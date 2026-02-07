using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp
    {
    public static class InputValidator
        {
        // Validates and formats phone number. Returns formatted string (XXX-XXX-XXXX) if valid, otherwise null.
        public static string ValidateAndFormatPhone(string input)
            {
            if (string.IsNullOrWhiteSpace(input)) return null;

            // Removes non-digits
            string digits = new string(input.Where(char.IsDigit).ToArray());

            // Requires exactly 10 digits
            if (digits.Length != 10)
                return null;

            // Formats as XXX-XXX-XXXX
            return $"{digits.Substring(0, 3)}-{digits.Substring(3, 3)}-{digits.Substring(6)}";
            }

        // Business hours validation helper
        public static bool ValidateBusinessHours(DateTime startUtc, DateTime endUtc)
            {
            if (endUtc <= startUtc) return false;

            TimeZoneInfo eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            DateTime startEst = TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(startUtc, DateTimeKind.Utc), eastern);
            DateTime endEst = TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(endUtc, DateTimeKind.Utc), eastern);

            bool startIsWeekday = startEst.DayOfWeek >= DayOfWeek.Monday && startEst.DayOfWeek <= DayOfWeek.Friday;
            bool endIsWeekday = endEst.DayOfWeek >= DayOfWeek.Monday && endEst.DayOfWeek <= DayOfWeek.Friday;

            bool sameEasternDay = startEst.Date == endEst.Date; // Appointments must start and end on the same calendar day in EST

            TimeSpan open = new TimeSpan(9, 0, 0);
            TimeSpan close = new TimeSpan(17, 0, 0);

            bool withinHours =
                startEst.TimeOfDay >= open &&
                endEst.TimeOfDay <= close;

            return startIsWeekday && endIsWeekday && sameEasternDay && withinHours;
            }

        }
    }
