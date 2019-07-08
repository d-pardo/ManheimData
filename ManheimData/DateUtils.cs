using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ManheimData
{
    public static class DateUtils
    {
        private static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long StringToUnixTime(string source)
        {
            return Convert.ToInt64((Convert.ToDateTime(source) - epoch).TotalSeconds);
        }

        public static DateTime UnixToDateTime(long source)
        {
            return epoch.AddSeconds(source);
        }

        public static Dictionary<string, DateTime> CurrentDate()
        {
            var currentYear = DateTime.Now.Year;
            var currentMont = DateTime.Now.Month;
            var lastDayInCurrentMonth = DateTime.DaysInMonth(currentYear, currentMont);
            var startDate = new DateTime(currentYear, currentMont, 1);
            var endDate = new DateTime(currentYear, currentMont, lastDayInCurrentMonth, 23, 59, 59);

            return new Dictionary<string, DateTime>
            {
                { "startCurrentDate", startDate },
                { "endCurrentDate", endDate }
            };
        }

        /// <summary>
        /// Ensure that a string can be successfully parsed to any possible provided formats
        /// </summary>
        /// <param name="value">String date</param>
        /// <param name="formats">Ex: {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt", "MM/dd/yyyy hh:mm:ss"}</param>
        /// <returns></returns>
        public static DateTime? ParseStringToDate(string value, string[] formats)
        {
            if (DateTime.TryParseExact(value, formats, null, DateTimeStyles.None, out DateTime dateValue))
                return dateValue;

            return null;
        }
    }
}
