using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The concrete schedule. Fired several days one after another, then make pause fro some more days.
    /// Repeat till the end date.
    /// </summary>
    public class OnceAPeriod : Schedule
    {
        /// <summary>
        /// Create the schedule object with the given description.
        /// </summary>
        /// <param name="text">The schedule instance description.</param>
        public OnceAPeriod(string text)
            : base(text)
        {
        }

        /// <summary>
        /// Fired for that number of days.
        /// </summary>
        public int TakeDays { get; set; }

        /// <summary>
        /// Number of days to pause.
        /// </summary>
        public int PauseDays { get; set; }

        /// <summary>
        /// The display string to user.
        /// </summary>
        public override string DisplayTypeName
        {
            get { return "Once a given period"; }
        }

        /// <summary>
        /// Check if fired on the given day.
        /// </summary>
        /// <param name="day">The day to check.</param>
        /// <returns>True if scheduled for the given date.</returns>
        public override bool IsAlarmAtDay(DateTime day)
        {
            if (day < this.Start || day > this.End)
            {
                return false;
            }

            int daysTotal = (this.End - this.Start).Days;
            int daysInOneCycle = daysTotal % (this.TakeDays + this.PauseDays);
            int dayNumber = (day - this.Start).Days;
            int dayNumberInCyle = dayNumber % (this.TakeDays + this.PauseDays);

            return dayNumberInCyle < this.TakeDays;
        }

        /// <summary>
        /// The data representation to the user.
        /// </summary>
        /// <returns>Text for user read.</returns>
        public override string ToString()
        {
            return string.Format(
                "Do {0} days, pause {1} days. From {2} to {3}",
                this.TakeDays,
                this.PauseDays,
                this.Start.ToShortDateString(), 
                this.End.ToShortDateString());
        }

        /// <summary>
        /// Create that schedule instance with default data. 
        /// Length - one month. 
        /// Take - one day. 
        /// Pause - 0 days. 
        /// </summary>
        /// <param name="day">The start date.</param>
        /// <returns>New instance of the schedule.</returns>
        public override Schedule CreateDefault(DateTime day)
        {
            return new OnceAPeriod(string.Empty)
            {
                Start = day,
                End = day.AddMonths(1).AddDays(-1),
                TakeDays = 1,
                PauseDays = 0,
            };
        }
    }
}
