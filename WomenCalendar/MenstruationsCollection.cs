using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class MenstruationsCollection : List<MenstruationPeriod>
    {
        public bool Add(DateTime date, int length)
        {
            Add(new MenstruationPeriod(date, length));
            return true;
        }

        public bool IsMenstruationDay(DateTime day)
        {
            foreach (MenstruationPeriod period in this)
            {
                if (period.IsDayInPeriod(day))
                {
                    return true;
                }
            }
            return false;
        }

        public MenstruationPeriod GetPeriodByDate(DateTime date)
        {
            foreach (MenstruationPeriod period in this)
            {
                if (period.IsDayInPeriod(date))
                {
                    return period;
                }
            }
            return null;
        }

        public bool Remove(DateTime day)
        {
            MenstruationPeriod period = GetPeriodByDate(day);
            if (period != null)
            {
                Remove(period);
                return true;
            }
            return false;
        }
    }
}
