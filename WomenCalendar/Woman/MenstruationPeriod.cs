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
        public int Length;
        public EgestasCollection Egestas;

        public MenstruationPeriod()
        {
        }

        public MenstruationPeriod(DateTime startDay, int length)
        {
            StartDay = startDay;
            Length = length;

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
    }
}
