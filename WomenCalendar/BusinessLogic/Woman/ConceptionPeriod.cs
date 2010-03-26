using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The pregnancy period class.
    /// </summary>
    public class ConceptionPeriod : ICloneable
    {
        /// <summary>
        /// The default length of the pregnancy.
        /// </summary>
        public const int StandardLength = 40 * 7;

        /// <summary>
        /// The pregnancy start day.
        /// </summary>
        public DateTime StartDay { get; set; }

        /// <summary>
        /// The pregnancy end day.
        /// </summary>
        public DateTime LastDay { get; set; }

        /// <summary>
        /// Check if day is within pregnancy period.
        /// </summary>
        /// <param name="day">The day to check.</param>
        /// <returns>True if it is one of pregnancy days.</returns>
        public bool IsDayInPeriod(DateTime day)
        {
            return this.StartDay <= day && day <= this.LastDay;
        }

        #region ICloneable Members

        /// <summary>
        /// Clone the object and its data.
        /// </summary>
        /// <returns>The cloned copy of the object.</returns>
        public object Clone()
        {
            return new ConceptionPeriod() { StartDay = this.StartDay, LastDay = this.LastDay };
        }

        #endregion

        /// <summary>
        /// Compare two periods.
        /// </summary>
        /// <param name="obj">The period to compare with.</param>
        /// <returns>True if it is same pregnancy period.</returns>
        public override bool Equals(object obj)
        {
            var secondValue = obj as ConceptionPeriod;
            return secondValue != null &&
                secondValue.StartDay.Equals(this.StartDay) &&
                secondValue.LastDay.Equals(this.LastDay);
        }

        /// <summary>
        /// Calculate the hash code of the object.
        /// </summary>
        /// <returns>The object hash code.</returns>
        public override int GetHashCode()
        {
            return this.StartDay.GetHashCode() ^ this.LastDay.GetHashCode();
        }
    }
}
