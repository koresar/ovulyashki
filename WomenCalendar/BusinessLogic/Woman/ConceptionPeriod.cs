using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class ConceptionPeriod
    {
        public const int StandardLength = 40 * 7;

        public DateTime StartDay { get; set; }
        public DateTime LastDay { get; set; }

        public bool IsDayInPeriod(DateTime day)
        {
            return StartDay <= day && day <= LastDay;
        }

    }
}
