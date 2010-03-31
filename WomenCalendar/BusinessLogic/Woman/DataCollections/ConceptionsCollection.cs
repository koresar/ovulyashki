using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The list of woman pregnancies.
    /// </summary>
    public class ConceptionsCollection : List<ConceptionPeriod>, ICloneable
    {
        /// <summary>
        /// The event fired on each collection change.
        /// </summary>
        public event Action CollectionChanged;

        /// <summary>
        /// The last conception (or null).
        /// </summary>
        public ConceptionPeriod Last
        {
            get
            {
                return this.Count > 0 ? this[this.Count - 1] : null;
            }
        }

        /// <summary>
        /// The first conception (or null).
        /// </summary>
        public ConceptionPeriod First
        {
            get
            {
                return this.Count > 0 ? this[0] : null;
            }
        }

        /// <summary>
        /// Return a conception by its day.
        /// </summary>
        /// <param name="date">They of the pregnancy period.</param>
        /// <returns>The pregnancy period class.</returns>
        public ConceptionPeriod GetConceptionByDate(DateTime date)
        {
            if (this.Count == 0 || date < this.First.StartDay || date > this.Last.LastDay)
            {
                return null;
            }

            return this.FirstOrDefault(p => p.IsDayInPeriod(date));
        }

        /// <summary>
        /// Get first pergnancy period after given day.
        /// </summary>
        /// <param name="date">A date to use.</param>
        /// <returns>The pregnancy period class.</returns>
        public ConceptionPeriod GetConceptionAfterDate(DateTime date)
        {
            if (this.Count == 0 || date > this.Last.LastDay)
            {
                return null;
            }

            if (this.Count == 1)
            {
                return this[0];
            }

            ConceptionPeriod period = null;
            foreach (var p in this)
            {
                if (date < p.StartDay && (period == null || period.StartDay < p.StartDay))
                {
                    period = p;
                }
            }

            return period;
        }

        /// <summary>
        /// Check if it is a conception day.
        /// </summary>
        /// <param name="date">Day to check.</param>
        /// <returns>True if it is a conception day.</returns>
        public bool IsConceptionDay(DateTime date)
        {
            if (this.Count == 0)
            {
                return false;
            }

            if (this.Count == 1)
            {
                return this[0].StartDay == date;
            }

            foreach (var p in this)
            {
                if (p.StartDay == date)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Cechk it the day is within any pregnancy period.
        /// </summary>
        /// <param name="date">Day to check.</param>
        /// <returns>True if it is a pregnancy day.</returns>
        public bool IsPregnancyDay(DateTime date)
        {
            ConceptionPeriod period = this.GetConceptionByDate(date);
            return period != null;
        }

        /// <summary>
        /// Return the week number if it is a first day of the pregnancy week.
        /// </summary>
        /// <param name="date">Date to use.</param>
        /// <returns>Week number if the date is a first day of the week.</returns>
        public int GetPregnancyWeekNumberWhenFirstWeekDay(DateTime date)
        {
            ConceptionPeriod period = this.GetConceptionByDate(date);
            if (period == null)
            {
                return 0;
            }

            int daysFromConc = (date - period.StartDay).Days;
            if (daysFromConc <= 0 || daysFromConc % 7 != 0)
            {
                return 0;
            }

            return (daysFromConc / 7) + 1;
        }

        /// <summary>
        /// Get the week number of the pregnancy day.
        /// </summary>
        /// <param name="date">Date to use.</param>
        /// <returns>The week number.</returns>
        public int GetPregnancyWeekNumber(DateTime date)
        {
            ConceptionPeriod period = this.GetConceptionByDate(date);
            if (period == null)
            {
                return 0;
            }

            int daysFromConc = (date - period.StartDay).Days;
            if (daysFromConc < 0)
            {
                return 0;
            }

            return (daysFromConc / 7) + 1;
        }

        /// <summary>
        /// Add one more pregnancy period to the list.
        /// </summary>
        /// <param name="date">The day of the pregnancy start.</param>
        /// <returns>True if sucessfilly added.</returns>
        public bool Add(DateTime date)
        {
            ConceptionPeriod period = new ConceptionPeriod()
            {
                StartDay = date,
                LastDay = date.AddDays(ConceptionPeriod.StandardLength)
            };

            this.Add(period);
            this.FireCollectionChangedEvent();
            return true;
        }

        /// <summary>
        /// Remove the pregnancy period by a conception day.
        /// </summary>
        /// <param name="date">A conception day.</param>
        /// <returns>True if the day is a conception one and the period was removed.</returns>
        public bool Remove(DateTime date)
        {
            if (this.IsConceptionDay(date))
            {
                ConceptionPeriod period = this.GetConceptionByDate(date);
                this.Remove(period);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Remove the pregnancy period by any pregnancy day.
        /// </summary>
        /// <param name="date">A pregnancy day.</param>
        /// <returns>True if the day is a pregnancy one and the period was removed.</returns>
        public bool RemoveByDate(DateTime date)
        {
            ConceptionPeriod period = this.GetConceptionByDate(date);
            if (period != null)
            {
                this.Remove(period);
                return true;
            }

            return false;
        }

        #region ICloneable Members

        /// <summary>
        /// Create the copy of the object.
        /// </summary>
        /// <returns>Cloned object copy.</returns>
        public object Clone()
        {
            var copy = new ConceptionsCollection();
            foreach (var item in this)
            {
                copy.Add(item.Clone() as ConceptionPeriod);
            }

            return copy;
        }

        #endregion

        /// <summary>
        /// Compare list to the other one.
        /// </summary>
        /// <param name="obj">The list to compate with.</param>
        /// <returns>True if lists are equal; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ConceptionsCollection))
            {
                return false;
            }

            var secondValue = obj as ConceptionsCollection;
            if (secondValue.Count != this.Count)
            {
                return false;
            }

            for (int i = 0; i < this.Count; i++)
            {
                if (!this[i].Equals(secondValue[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get hash code of the object.
        /// </summary>
        /// <returns>Object hash code.</returns>
        public override int GetHashCode()
        {
            return this.Aggregate(0, (seed, period) => seed ^ period.GetHashCode());
        }

        private void FireCollectionChangedEvent()
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged();
            }
        }
    }
}
