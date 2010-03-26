using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    /// <summary>
    /// Represents one menstruation.
    /// </summary>
    public class MenstruationPeriod : ICloneable
    {
        /// <summary>
        /// The minimal normal number of days between two menstruations.
        /// </summary>
        public const int NormalMinimalPeriod = 21;

        /// <summary>
        /// The maximal normal number of days between two menstruations.
        /// </summary>
        public const int NormalMaximalPeriod = 35;

        [XmlIgnore]
        private DateTime ovulationDate;

        /// <summary>
        /// Create empty period.
        /// </summary>
        public MenstruationPeriod()
        {
            this.Egestas = new EgestasCollection();
        }

        /// <summary>
        /// Create period from its start and bleeding length.
        /// </summary>
        /// <param name="startDay">Menstruations start.</param>
        /// <param name="length">Bleeding number of days.</param>
        public MenstruationPeriod(DateTime startDay, int length)
        {
            this.StartDay = startDay;
            this.Egestas = new EgestasCollection(startDay, length);
        }

        /// <summary>
        /// The bleeding lengths.
        /// </summary>
        public int Length
        {
            get
            {
                return this.Egestas.Count;
            }
        }

        /// <summary>
        /// The menstruations start.
        /// </summary>
        public DateTime StartDay { get; set; }

        /// <summary>
        /// The egesta values collection.
        /// </summary>
        public EgestasCollection Egestas { get; set; }

        /// <summary>
        /// Indicates if it is conceive period.
        /// </summary>
        public bool HasPregnancy { get; set; }

        /// <summary>
        /// Las bleeding day.
        /// </summary>
        public DateTime LastDay
        {
            get
            {
                return this.StartDay.AddDays(this.Length - 1);
            }
        }

        /// <summary>
        /// Check if the day is within period.
        /// </summary>
        /// <param name="day">Day to check.</param>
        /// <returns>True if the day is in the period.</returns>
        public bool IsDayInPeriod(DateTime day)
        {
            return this.StartDay <= day && day <= this.LastDay;
        }

        /// <summary>
        /// Detect and return the ovulation day.
        /// </summary>
        /// <param name="w">The current woman we are working with.</param>
        /// <returns>Predicted ovulation day.</returns>
        public DateTime GetOvulationDate(Woman w)
        {
            if (this.ovulationDate == default(DateTime))
            {
                var period2 = w.Menstruations.GetClosestPeriodAfterDay(this.StartDay.AddDays(1));
                DateTime nextPeriodFirstDat = period2 == null ? this.StartDay.AddDays(w.ManualPeriodLength) : period2.StartDay;
                this.ovulationDate = w.OvDetector.EstimateOvulationDate(nextPeriodFirstDat);
            }

            return this.ovulationDate;
        }

        /// <summary>
        /// Forces to repredict ovulation day.
        /// </summary>
        public void ResetOvulyationDay()
        {
            this.ovulationDate = default(DateTime);
        }

        #region ICloneable Members

        /// <summary>
        /// Create copy of itself.
        /// </summary>
        /// <returns>The object copy.</returns>
        public object Clone()
        {
            var copy = new MenstruationPeriod()
            {
                Egestas = this.Egestas.Clone() as EgestasCollection,
                HasPregnancy = this.HasPregnancy,
                ovulationDate = this.ovulationDate,
                StartDay = this.StartDay
            };
            return copy;
        }

        #endregion

        /// <summary>
        /// Check if the object has the same data.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if equal.</returns>
        public override bool Equals(object obj)
        {
            var secondValue = obj as MenstruationPeriod;
            return secondValue != null &&
                secondValue.HasPregnancy.Equals(this.HasPregnancy) &&
                secondValue.StartDay.Equals(this.StartDay) &&
                secondValue.Egestas.Equals(this.Egestas);
        }

        /// <summary>
        /// Calculate object hash code.
        /// </summary>
        /// <returns>The object hash code.</returns>
        public override int GetHashCode()
        {
            return this.HasPregnancy.GetHashCode() ^ this.StartDay.GetHashCode() ^ this.Egestas.GetHashCode();
        }

        /// <summary>
        /// Set new bleeding length.
        /// </summary>
        /// <param name="value">The new number of bleeding days.</param>
        public void SetLength(int value)
        {
            if (this.Length == value)
            {
                return;
            }

            if (value > this.Length)
            {
                for (int i = this.Length; i < value; i++)
                {
                    this.Egestas[this.StartDay.AddDays(i)] = EgestasCollection.MaximumEgestaValue / 2;
                }
            }
        }
    }
}
