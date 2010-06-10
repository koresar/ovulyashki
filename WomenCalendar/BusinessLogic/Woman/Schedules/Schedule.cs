using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    /// <summary>
    /// Base class for schedule types.
    /// </summary>
    [XmlInclude(typeof(OnceAPeriod))]
    public abstract class Schedule
    {
        /// <summary>
        /// The contructor with the schedule description.
        /// </summary>
        /// <param name="text">Text description of the schedule instance.</param>
        public Schedule(string text)
            : this()
        {
            this.Text = text;
        }

        /// <summary>
        /// Default contructor.
        /// </summary>
        public Schedule()
        {
            this.Text = string.Empty;
            this.GUID = System.Guid.NewGuid();
        }

        /// <summary>
        /// The scheduling start.
        /// </summary>
        public virtual DateTime Start { get; set; }

        /// <summary>
        /// The scheduling finish date.
        /// </summary>
        public virtual DateTime End { get; set; }

        /// <summary>
        /// The schedule description.
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// The type fo schedule to be shown to user.
        /// </summary>
        public abstract string DisplayTypeName { get; }

        /// <summary>
        /// Unique id of the object.
        /// </summary>
        private Guid GUID { get; set; }

        /// <summary>
        /// Check if it is scheduled to the given date.
        /// </summary>
        /// <param name="day">The date to check.</param>
        /// <returns>True if scheduled for thet day.</returns>
        public abstract bool IsAlarmAtDay(DateTime day);

        /// <summary>
        /// Compare two schedule instances. The schedule must always be passed by reference, but not by copying.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>Truw if it is same object.</returns>
        public override bool Equals(object obj)
        {
            return this.GUID == (obj as Schedule).GUID;
        }

        /// <summary>
        /// Calc hash code.
        /// </summary>
        /// <returns>Object hash code.</returns>
        public override int GetHashCode()
        {
            return this.GUID.GetHashCode();
        }

        /// <summary>
        /// Create default schedule filled with default data for any concrete schedule.
        /// </summary>
        /// <param name="day">Start day of the future object.</param>
        /// <returns>Newly created object.</returns>
        public abstract Schedule CreateDefault(DateTime day);
    }
}
