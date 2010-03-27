using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The collaction of calendar schedules.
    /// </summary>
    public class SchedulesCollection : List<Schedule>
    {
        /// <summary>
        /// Get schedule object scheduled to fire at the given date.
        /// </summary>
        /// <param name="fireDate">Scheduled date.</param>
        /// <returns>List of fired schedules.</returns>
        public List<Schedule> GetFiredSchedulesForDay(DateTime fireDate)
        {
            return this.Where(s => s.IsAlarmAtDay(fireDate)).ToList();
        }

        /// <summary>
        /// Add some more schedules to the colletion (if was not already added).
        /// </summary>
        /// <param name="schedules">The items wich we should try to add.</param>
        public void UpdateData(List<Schedule> schedules)
        {
            foreach (var s in schedules)
            {
                if (!this.Contains(s))
                {
                    Add(s);
                }
            }
        }
    }
}
