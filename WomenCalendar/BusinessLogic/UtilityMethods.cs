using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public static class UtilityMethods
    {
        public static bool Within(DateTime date, DateTime from, DateTime to)
        {
            return from <= date && date <= to;
        }

        public static DateTime Middle(params DateTime[] days)
        {
            if (days.Length == 0) { throw new ArgumentException("There must be at least one date.", "days"); }
            DateTime first = days[0];
            TimeSpan total = new TimeSpan();
            foreach (var day in days)
            {
                total += day - first;
            }
            return first.AddTicks(total.Ticks / days.Length).AddHours(12).Date;
        }
    }
}
