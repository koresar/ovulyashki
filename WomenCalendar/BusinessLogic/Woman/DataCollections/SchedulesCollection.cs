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
        /// Quickly checks if anything is being fired that day.
        /// </summary>
        /// <param name="fireDate">Scheduled date.</param>
        /// <returns>True if any schedule is fired that day.</returns>
        public bool HasAFiredSchedule(DateTime fireDate)
        {
            return this.Where(s => s.IsAlarmAtDay(fireDate)).Any();
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

        /// <summary>
        /// Retrund the text which is being shown to the used about the schedules on the provided day.
        /// </summary>
        /// <param name="date">Day to provide schedules info for.</param>
        /// <returns>Nice looking text for the day. Null in case nothing to show.</returns>
        public string GetFormattedSchedulesText(DateTime date)
        {
            return "TODO";
        }
    }
}
