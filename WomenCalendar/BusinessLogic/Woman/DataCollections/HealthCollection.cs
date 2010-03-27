using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// Serializable woman healthes collection.
    /// </summary>
    public class HealthCollection : SerializableEventsCollection<int>
    {
        private const int DefaultHealthValue = 5;

        /// <summary>
        /// The list constructor.
        /// </summary>
        public HealthCollection()
            : base("Health")
        {
        }

        /// <summary>
        /// Get or set collection value (health).
        /// </summary>
        /// <param name="date">Date to use.</param>
        /// <returns>The health value; or default value.</returns>
        public new int this[DateTime date]
        {
            get
            {
                int ret;
                return this.TryGetValue(date, out ret) ? ret : DefaultHealthValue;
            }

            set
            {
                if (value == DefaultHealthValue)
                {
                    this.Remove(date);
                }
                else
                {
                    base[date] = value;
                }
            }
        }

        /// <summary>
        /// Get healthes list since given date.
        /// </summary>
        /// <param name="date">The day to start from.</param>
        /// <param name="count">The maximum count of items.</param>
        /// <returns>Health items.</returns>
        public double[] GetHealthesSince(DateTime date, int count)
        {
            if (count <= 0)
            {
                return new double[0];
            }

            double[] ret = new double[count];
            DateTime d = date.Date;
            for (int i = 0; i < count; i++, d = d.AddDays(1))
            {
                ret[i] = this[d];
            }

            return ret;
        }
    }
}
