using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class ConceptionsCollection : List<ConceptionPeriod>
    {
        public delegate void CollectionChangedDelegate();
        public event CollectionChangedDelegate CollectionChanged;

        public ConceptionPeriod Last
        {
            get
            {
                return Count > 0 ? this[Count - 1] : null;
            }
        }

        public ConceptionPeriod First
        {
            get
            {
                return Count > 0 ? this[0] : null;
            }
        }

        public ConceptionPeriod GetConceptionByDate(DateTime date)
        {
            if (Count == 0 || date < First.StartDay || date > Last.LastDay) return null;
            foreach (var p in this)
                if (p.IsDayInPeriod(date))
                    return p;
            return null;
            //return (from p in this where p.IsDayInPeriod(date) select p).FirstOrDefault();
        }

        public ConceptionPeriod GetConceptionAfterDate(DateTime date)
        {
            if (Count == 0 || date > Last.LastDay) return null;
            if (Count == 1) return this[0];
            ConceptionPeriod period = null;
            foreach (var p in this)
                if (date < p.StartDay && (period == null || period.StartDay < p.StartDay))
                    period = p;
            return period;
            //return (from p in this where p.IsDayInPeriod(date) select p).FirstOrDefault();
        }

        public bool IsConceptionDay(DateTime date)
        {
            if (Count == 0) return false;
            if (Count == 1) return this[0].StartDay == date;
            foreach (var p in this)
                if (p.StartDay == date)
                    return true;
            return false;
            //return this.Where(p => p.StartDay == date).FirstOrDefault() != null;
        }

        public bool IsPregnancyDay(DateTime date)
        {
            ConceptionPeriod period = GetConceptionByDate(date);
            return period != null;
        }

        public int GetPregnancyWeekNumberWhenFirstWeekDay(DateTime date)
        {
            ConceptionPeriod period = GetConceptionByDate(date);
            if (period == null) return 0;
            int daysFromConc = (date - period.StartDay).Days;
            if (daysFromConc <= 0 || daysFromConc % 7 != 0) return 0;
            return daysFromConc / 7 + 1;
        }

        public int GetPregnancyWeekNumber(DateTime date)
        {
            ConceptionPeriod period = GetConceptionByDate(date);
            if (period == null) return 0;
            int daysFromConc = (date - period.StartDay).Days;
            if (daysFromConc <= 0) return 0;
            return daysFromConc / 7 + 1;
        }

        public bool Add(DateTime date)
        {
            ConceptionPeriod period = new ConceptionPeriod() { StartDay = date, LastDay = date.AddDays(40 * 7) };
            Add(period);
            FireCollectionChangedEvent();
            return true;
        }

        public bool Remove(DateTime date)
        {
            if (IsConceptionDay(date))
            {
                ConceptionPeriod period = GetConceptionByDate(date);
                Remove(period);
                return true;
            }
            return false;
        }

        public bool RemoveByDate(DateTime date)
        {
            ConceptionPeriod period = GetConceptionByDate(date);
            if (period != null)
            {
                Remove(period);
                return true;
            }
            return false;
        }

        private void FireCollectionChangedEvent()
        {
            if (CollectionChanged != null)
            {
                CollectionChanged();
            }
        }
    }
}
