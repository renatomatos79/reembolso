using System;

namespace HackathonReembolso.Framework.Helpers
{
    public class HelperDate
    {
        /// <summary>
        /// Get the DateTime of a future weekday
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="day">Day of week</param>
        /// <param name="weeksCountFromNow">Number of weeks to calculate from now</param>
        /// <returns>DateTime of the day of week calculated</returns>
        public static DateTime GetNextByWeekday(DateTime startDate, DayOfWeek day, int weekInterval)
        {
            if (weekInterval == 0)
                return new DateTime();

            if (day == startDate.DayOfWeek)
                weekInterval++;

            int daysToAdd = (((int)day - (int)startDate.DayOfWeek + 7) % 7);
            daysToAdd = daysToAdd + (7 * (weekInterval - 1));
            return startDate.AddDays(daysToAdd);
        }
    }
}
