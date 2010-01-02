using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    public class MenstruationPeriod : ICloneable
    {
        public const int NormalMinimalPeriod = 21;
        public const int NormalMaximalPeriod = 35;

        public DateTime StartDay { get; set; }
        private int length;
        public EgestasCollection Egestas = new EgestasCollection();
        public bool HasPregnancy { get; set; }

        [XmlIgnore]
        private DateTime ovulationDate;

        public MenstruationPeriod()
        {
        }

        public MenstruationPeriod(DateTime startDay, int length)
        {
            StartDay = startDay;
            Length = length;

            Egestas = new EgestasCollection(startDay, length);
        }

        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                if (length == value) return;
                if (value > length)
                {
                    for (int i = length; i < value; i++)
                    {
                        Egestas[StartDay.AddDays(i)] = EgestasCollection.MaximumEgestaValue / 2;
                    }
                }
                length = value;
            }
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
                ovulationDate = OvulationDetector.EstimateOvulationDate(w, nextPeriodFirstDat);
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
                length = this.length,
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
                secondValue.Length.Equals(this.Length) &&
                secondValue.StartDay.Equals(this.StartDay) &&
                secondValue.Egestas.Equals(this.Egestas);
        }
    }
}
