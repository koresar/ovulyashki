using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    /// <summary>
    /// Very important class. The container of menstruations. Contains lots of business logic.
    /// </summary>
    public class MenstruationsCollection : List<MenstruationPeriod>, ICloneable
    {
        /// <summary>
        /// Fired on major changes. Add, Remove, change length, etc.
        /// </summary>
        public event Action CollectionChanged;

        /// <summary>
        /// Last recorded cycle. May thrown exception if collection is empty.
        /// </summary>
        public MenstruationPeriod Last
        {
            get
            {
                return this[Count - 1];
            }
        }

        /// <summary>
        /// First recorded cycle. May thrown exception if collection is empty.
        /// </summary>
        public MenstruationPeriod First
        {
            get
            {
                return this[0];
            }
        }

        /// <summary>
        /// Adds one more cycle to the collection. Interacts with user if something.
        /// </summary>
        /// <param name="date">The red days start.</param>
        /// <param name="length">The egesting number of days.</param>
        /// <returns>True if sucsessfully added.</returns>
        public bool Add(DateTime date, int length)
        {
            MenstruationPeriod newPeriod = new MenstruationPeriod(date, length);
            MenstruationPeriod closestPeriod = this.GetClosestPeriodAfterDay(date);
            if (closestPeriod != null)
            {
                if (MessageBox.Show(
                    TEXT.Get["Menstr_after_day"] + TEXT.Get["Are_you_sure_capital"], 
                    TEXT.Get["Are_you_crazy"], 
                    MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return false;
                }

                int distance = (closestPeriod.StartDay - date).Days;
                if (distance < length)
                {
                    newPeriod.SetLength(distance);
                }

                this.Insert(IndexOf(closestPeriod), newPeriod); // we must always keep the collection sorted.
            }
            else
            {
                string askMessage = string.Empty;
                closestPeriod = this.GetClosestPeriodBeforeDay(date);
                if (closestPeriod != null)
                {
                    int distance = (date - closestPeriod.StartDay).Days;
                    if (distance < MenstruationPeriod.NormalMinimalPeriod)
                    {
                        askMessage += TEXT.Get.Format(
                            "Msg_short_menstr_period", 
                            MenstruationPeriod.NormalMinimalPeriod, 
                            TEXT.GetDaysString(MenstruationPeriod.NormalMinimalPeriod));
                    }
                    else if (distance > MenstruationPeriod.NormalMaximalPeriod)
                    {
                        askMessage += TEXT.Get.Format(
                            "Msg_large_menstr_period",
                            MenstruationPeriod.NormalMaximalPeriod, 
                            TEXT.GetDaysString(MenstruationPeriod.NormalMaximalPeriod));
                    }
                }

                if (date > DateTime.Today)
                {
                    askMessage += "\n" + TEXT.Get["Future_day_question"];
                }

                if (!string.IsNullOrEmpty(askMessage) && 
                    MessageBox.Show(
                        askMessage + TEXT.Get["Are_you_sure_capital"],
                        TEXT.Get["What_a_situation"], 
                        MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return false;
                }

                this.Add(newPeriod);
            }

            return true;
        }

        /// <summary>
        /// Checks if it is the red day.
        /// </summary>
        /// <param name="day">The day to check.</param>
        /// <returns>True if there was bleeding that day.</returns>
        public bool IsMenstruationDay(DateTime day)
        {
            MenstruationPeriod period = this.GetPeriodByDate(day);
            return period != null;
        }

        /// <summary>
        /// Return the egestion amount by the day.
        /// </summary>
        /// <param name="day">The day to lookup.</param>
        /// <returns>Egestion amount, or -1 if not present.</returns>
        public int GetEgestaAmount(DateTime day)
        {
            MenstruationPeriod period = this.GetPeriodByDate(day);
            if (period != null)
            {
                return period.Egestas[day];
            }

            return -1;
        }

        /// <summary>
        /// Set the egestion value if there was bleeding that day.
        /// </summary>
        /// <param name="day">Day to set.</param>
        /// <param name="egesta">Value to set.</param>
        /// <returns>True if there was bleeding that day and the egestion is set.</returns>
        public bool SetEgesta(DateTime day, int egesta)
        {
            MenstruationPeriod period = this.GetPeriodByDate(day);
            if (period != null)
            {
                period.Egestas[day] = egesta;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Set the egestion length.
        /// </summary>
        /// <param name="period">The period to change.</param>
        /// <param name="length">New egestion length.</param>
        /// <returns>The period which was changed.</returns>
        public MenstruationPeriod SetPeriodLength(MenstruationPeriod period, int length)
        {
            if (period != null && period.Length != length)
            {
                period.SetLength(length);
                this.FireCollectionChangedEvent();
                return period;
            }

            return period;
        }

        /// <summary>
        /// Finds the cycle period by its any date.
        /// </summary>
        /// <param name="date">Date to use for search.</param>
        /// <returns>The found period; null if not found.</returns>
        public MenstruationPeriod GetPeriodByDate(DateTime date)
        {
            if (this.Count == 0 || date < this.First.StartDay || date > this.Last.LastDay)
            {
                return null;
            }

            return this.FirstOrDefault(p => p.IsDayInPeriod(date));
        }

        /// <summary>
        /// Safely removes oeriod by its any date.
        /// </summary>
        /// <param name="day">Date to use for search.</param>
        /// <returns>rue if period was found and removed.</returns>
        public bool Remove(DateTime day)
        {
            MenstruationPeriod period = this.GetPeriodByDate(day);
            if (period != null)
            {
                this.Remove(period);
                this.FireCollectionChangedEvent();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Find a period which happened after the given day.
        /// </summary>
        /// <param name="date">Day search from.</param>
        /// <returns>Found period.</returns>
        public MenstruationPeriod GetClosestPeriodAfterDay(DateTime date)
        {
            if (this.Count == 0 || date > this.Last.StartDay)
            {
                return null;
            }

            MenstruationPeriod resultPeriod = null;
            foreach (MenstruationPeriod period in this)
            { // periods may not be sorted by start, so we have to keep an eye on all of the periods.
                if (period.StartDay > date && (resultPeriod == null || period.StartDay < resultPeriod.StartDay))
                {
                    resultPeriod = period;
                }
            }

            return resultPeriod;
        }

        /// <summary>
        /// Find nearest bleeding that happened just before given day.
        /// </summary>
        /// <param name="date">The day to search till.</param>
        /// <returns>PEriod if found one; otherwise false.</returns>
        public MenstruationPeriod GetClosestPeriodBeforeDay(DateTime date)
        {
            if (this.Count == 0 || date < this.First.StartDay)
            {
                return null;
            }

            MenstruationPeriod resultPeriod = null;
            foreach (MenstruationPeriod period in this)
            {
                if (period.StartDay < date && (resultPeriod == null || period.StartDay > resultPeriod.StartDay))
                {
                    resultPeriod = period;
                }
            }

            return resultPeriod;
        }

        /// <summary>
        /// Finds average length of the cycle.
        /// </summary>
        /// <returns>Average length between bleedings. Or 28 if the data is not enough.</returns>
        public int CalculateAveragePeriodLength()
        {
            if (this.Count < 2)
            {
                return 28;
            }

            double sum = 0;
            int count = 0;
            for (int i = 1; i < this.Count; i++)
            {
                if (!this[i - 1].HasPregnancy)
                {
                    int periodLength = (this[i].StartDay - this[i - 1].StartDay).Days;
                    if (periodLength < MenstruationPeriod.NormalMinimalPeriod ||
                        periodLength > MenstruationPeriod.NormalMaximalPeriod)
                    {
                        continue;
                    }

                    sum += periodLength;
                    count++;
                }
            }

            return sum == 0 ? 28 : (int)((sum / count) + 0.5);
        }

        /// <summary>
        /// Reset all predicted ovulations.
        /// </summary>
        public void ResetOvulyationsDates()
        {
            this.ForEach(m => m.ResetOvulyationDay());
        }

        #region ICloneable Members

        /// <summary>
        /// Create copy of myself.
        /// </summary>
        /// <returns>Complete copy of myself.</returns>
        public object Clone()
        {
            var copy = new MenstruationsCollection();
            foreach (var item in this)
            {
                copy.Add(item.Clone() as MenstruationPeriod);
            }

            return copy;
        }

        #endregion

        /// <summary>
        /// Compare two objects.
        /// </summary>
        /// <param name="obj">Period to compare with.</param>
        /// <returns>True if data is the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is MenstruationsCollection))
            {
                return false;
            }

            var secondValue = obj as MenstruationsCollection;
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
        /// Get object hash code.
        /// </summary>
        /// <returns>Object hash code.</returns>
        public override int GetHashCode()
        {
            return this.Count.GetHashCode() ^ this.Aggregate(0, (seed, period) => seed ^ period.GetHashCode());
        }

        /// <summary>
        /// The function is overriden in order to forbid the function usage from outer space.
        /// </summary>
        /// <param name="period">New cycle.</param>
        private new void Add(MenstruationPeriod period)
        {
            base.Add(period);
            this.FireCollectionChangedEvent();
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
