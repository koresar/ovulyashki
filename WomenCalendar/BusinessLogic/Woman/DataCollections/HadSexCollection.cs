using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The serializable collection of the sex indicators.
    /// </summary>
    public class HadSexCollection : SerializableEventsCollection<bool>
    {
        /// <summary>
        /// The constructor of the list.
        /// </summary>
        public HadSexCollection()
            : base("HadSex")
        {
        }

        /// <summary>
        /// Get or set a collectoin value. False by default.
        /// </summary>
        /// <param name="date">The date of the sex.</param>
        /// <returns>True if there was sex; otherwise false.</returns>
        public new bool this[DateTime date]
        {
            get
            {
                bool ret;
                return this.TryGetValue(date, out ret) ? ret : false;
            }

            set
            {
                if (value == false)
                {
                    this.Remove(date);
                }
                else
                {
                    base[date] = value;
                }
            }
        }
    }
}
