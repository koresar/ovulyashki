using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class MenstruationPeriod
    {
        public DateTime StartDay;
        public int Length;

        public MenstruationPeriod()
        {
        }

        public MenstruationPeriod(DateTime startDay, int length)
        {
            StartDay = startDay;
            Length = length;
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
