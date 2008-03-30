using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    public class MenstruationPeriod
    {
        public const int NormalMinimalPeriod = 21;
        public const int NormalMaximalPeriod = 35;

        public DateTime StartDay;
        public int length;
        public EgestasCollection Egestas;

        public MenstruationPeriod()
        {
            Egestas = new EgestasCollection();
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
                        Egestas[StartDay.AddDays(i)] = EgestasCollection.MaximumEgestaValue/2;
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
    }
}
