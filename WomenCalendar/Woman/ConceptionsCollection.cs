using System;
using System.Collections.Generic;
using System.Linq;
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
            return (from p in this where p.IsDayInPeriod(date) select p).FirstOrDefault();
        }

        public bool IsConceptionDay(DateTime date)
        {
            if (Count == 0) return false;
            return this.Where(p => p.StartDay == date).FirstOrDefault() != null;
        }

        public bool IsPregnancyDay(DateTime date)
        {
            ConceptionPeriod period = GetConceptionByDate(date);
            return period != null;
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

        private void FireCollectionChangedEvent()
        {
            if (CollectionChanged != null)
            {
                CollectionChanged();
            }
        }
    }
}
