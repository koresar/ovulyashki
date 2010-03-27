using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The Basal Body Temperatures serializable list.
    /// </summary>
    public class BBTCollection : SerializableEventsCollection<double>
    {
        private const double MaxmimalBBT = 43.0;
        private const double MinimalBBT = 35.0;

        /// <summary>
        /// Create the BBT list.
        /// </summary>
        public BBTCollection()
            : base("BBT")
        {
        }

        /// <summary>
        /// Check if given BBT is on human temperatures range.
        /// </summary>
        /// <param name="bbt">BBT to check.</param>
        /// <returns>True if human temperature; otherwise - false.</returns>
        public static bool IsBBTInCorrectRange(double bbt)
        {
            return bbt >= MinimalBBT && bbt <= MaxmimalBBT;
        }

        /// <summary>
        /// Get BBT even if the list has no date like this.
        /// </summary>
        /// <param name="date">The date of the BBT.</param>
        /// <returns>0 if no such day item exist, otherwise a BBT value.</returns>
        public double GetBBT(DateTime date)
        {
            return this.ContainsKey(date) ? this[date] : 0;
        }

        /// <summary>
        /// Get hte BBT as the string even if the list has no date like this.
        /// </summary>
        /// <param name="date">">The date of the BBT.</param>
        /// <returns>Empty string if no such day item exist, otherwise a string BBT value.</returns>
        public string GetBBTString(DateTime date)
        {
            return this.ContainsKey(date) ? this[date].ToString() : string.Empty;
        }

        /// <summary>
        /// Set the BBT. If the string is empty, then remove the item.
        /// </summary>
        /// <param name="date">Day of the BBT.</param>
        /// <param name="value">The BBT value.</param>
        public void SetBBT(DateTime date, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                this.Remove(date);
            }
            else
            {
                base[date] = Convert.ToDouble(value);
            }
        }

        /// <summary>
        /// Get all temperatures since the given day.
        /// </summary>
        /// <param name="date">The start day.</param>
        /// <param name="count">The max count of the returned BBTs.</param>
        /// <returns>The BBT values.</returns>
        public double[] GetTemperaturesSince(DateTime date, int count)
        {
            if (count <= 0)
            {
                return new double[0];
            }

            double[] ret = new double[count];
            DateTime d = date.Date;
            for (int i = 0; i < count; i++, d = d.AddDays(1))
            {
                ret[i] = this.GetBBT(d);
            }

            return ret;
        }
    }
}
