using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Linq;

namespace WomenCalendar
{
    public class MenstruationPeriod : ICloneable
    {
        public const int NormalMinimalPeriod = 21;
        public const int NormalMaximalPeriod = 35;

        public int Length
        {
            get
            {
                return Egestas.Count;
            }
        }

        public DateTime StartDay { get; set; }
        public EgestasCollection Egestas { get; set; }
        public bool HasPregnancy { get; set; }

        [XmlIgnore]
        private DateTime ovulationDate;

        public MenstruationPeriod()
        {
            Egestas = new EgestasCollection();
        }

        public MenstruationPeriod(DateTime startDay, int length)
        {
            StartDay = startDay;
            Egestas = new EgestasCollection(startDay, length);
        }

        public DateTime LastDay
        {
            get
            {
                return StartDay.AddDays(Length - 1);
            }
        }

        public bool IsDayInPeriod(DateTime day)
        {
            return StartDay <= day && day <= LastDay;
        }

        public DateTime GetOvulationDate(Woman w)
        {
            if (ovulationDate == default(DateTime))
            {
                var period2 = w.Menstruations.GetClosestPeriodAfterDay(StartDay.AddDays(1));
                DateTime nextPeriodFirstDat = period2 == null ? StartDay.AddDays(w.ManualPeriodLength) : period2.StartDay;
                ovulationDate = w.OvDetector.EstimateOvulationDate(nextPeriodFirstDat);
            }
            return ovulationDate;
        }

        public void ResetOvulyationDay()
        {
            ovulationDate = default(DateTime);
        }

        #region ICloneable Members

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

        public override bool Equals(object obj)
        {
            var secondValue = obj as MenstruationPeriod;
            return secondValue != null &&
                secondValue.HasPregnancy.Equals(this.HasPregnancy) &&
                secondValue.StartDay.Equals(this.StartDay) &&
                secondValue.Egestas.Equals(this.Egestas);
        }

        public override int GetHashCode()
        {
            return HasPregnancy.GetHashCode() ^ StartDay.GetHashCode() ^ Egestas.GetHashCode();
        }

        public void SetLength(int value)
        {
            if (Length == value) return;
            if (value > Length)
            {
                for (int i = Length; i < value; i++)
                {
                    Egestas[StartDay.AddDays(i)] = EgestasCollection.MaximumEgestaValue / 2;
                }
            }
        }
    }
}
