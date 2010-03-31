using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The collection of methods widely used everywhere.
    /// </summary>
    public static class UtilityMethods
    {
        /// <summary>
        /// Check if date is within start and stop.
        /// </summary>
        /// <param name="date">Date to check/</param>
        /// <param name="from">Boundary start.</param>
        /// <param name="to">Boundary stop.</param>
        /// <returns>True if date within given start and stop.</returns>
        public static bool Within(DateTime date, DateTime from, DateTime to)
        {
            return from <= date && date <= to;
        }
    }
}
