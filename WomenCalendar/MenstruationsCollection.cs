using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public class MenstruationsCollection : List<MenstruationPeriod>
    {
        public bool Add(DateTime date, int length)
        {
            MenstruationPeriod newPeriod = new MenstruationPeriod(date, length);
            MenstruationPeriod closestPeriod = GetClosestPeriodAfterDay(date);
            if (closestPeriod != null)
            {
                if (MessageBox.Show("У вас перед этими выделениями были другие!\nВЫ УВЕРЕНЫ В ТОМ ЧТО ДЕЛАЕТЕ?", 
                    "Ты сума сошла?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return false;
                }

                int distance = (closestPeriod.StartDay - date).Days;
                if (distance < length)
                {
                    newPeriod.Length = distance;
                }

                Insert(IndexOf(closestPeriod), newPeriod); // we must always keep the collection sorted.
            }
            else
            {
                closestPeriod = GetClosestPeriodBeforeDay(date);
                if (closestPeriod != null)
                {
                    int distance = (date - closestPeriod.StartDay).Days;
                    if (distance < 21)
                    {
                        if (MessageBox.Show("Между менструациями меньше 21-го дня!\nВЫ УВЕРЕНЫ В ТОМ ЧТО ДЕЛАЕТЕ?",
                            "Ухты, какая необычная ситуация!", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            return false;
                        }
                    }
                }

                Add(newPeriod);
            }

            return true;
        }

        new private void Add(MenstruationPeriod period)
        {
            FireCollectionChangedEvent();
            base.Add(period);
        }

        public delegate void CollectionChangedDelegate();
        public event CollectionChangedDelegate CollectionChanged;

        public MenstruationPeriod Last
        {
            get
            {
                return this[Count - 1];
            }
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
                FireCollectionChangedEvent();
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

        public MenstruationPeriod GetClosestPeriodAfterDay(DateTime date)
        {
            MenstruationPeriod resultPeriod = null;
            foreach (MenstruationPeriod period in this)
            {
                if (period.StartDay > date && (resultPeriod == null || period.StartDay < resultPeriod.StartDay))
                {
                    resultPeriod = period;
                }
            }
            return resultPeriod;
        }

        public MenstruationPeriod GetClosestPeriodBeforeDay(DateTime date)
        {
            MenstruationPeriod resultPeriod = null;
            foreach (MenstruationPeriod period in this)
            {
                if (period.StartDay < date && (resultPeriod == null || period.StartDay > resultPeriod.StartDay))
                {
                    resultPeriod = period;
                }
            }
            return resultPeriod;
        }

        public int CalculateAveragePeriodLength()
        {
            if (Count < 2)
            {
                return 0;
            }

            double result = 0;
            for (int i = 1; i < Count; i++)
            {
                result += (this[i].StartDay - this[i - 1].StartDay).Days;
            }

            return (int) ((result/(Count - 1)) + 0.5);
        }
    }
}
