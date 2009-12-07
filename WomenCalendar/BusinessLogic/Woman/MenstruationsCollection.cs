using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public class MenstruationsCollection : List<MenstruationPeriod>
    {
        public delegate void CollectionChangedDelegate();
        public event CollectionChangedDelegate CollectionChanged;

        public MenstruationPeriod Last
        {
            get
            {
                return this[Count - 1];
            }
        }

        public MenstruationPeriod First
        {
            get
            {
                return this[0];
            }
        }

        public bool Add(DateTime date, int length)
        {
            MenstruationPeriod newPeriod = new MenstruationPeriod(date, length);
            MenstruationPeriod closestPeriod = GetClosestPeriodAfterDay(date);
            if (closestPeriod != null)
            {
                if (MessageBox.Show(TEXT.Get["Menstr_after_day"] + TEXT.Get["Are_you_sure_capital"], 
                    TEXT.Get["Are_you_crazy"], MessageBoxButtons.YesNo) != DialogResult.Yes)
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
                string askMessage = string.Empty;
                closestPeriod = GetClosestPeriodBeforeDay(date);
                if (closestPeriod != null)
                {
                    int distance = (date - closestPeriod.StartDay).Days;
                    if (distance < MenstruationPeriod.NormalMinimalPeriod)
                    {
                        askMessage += TEXT.Get.Format("Msg_short_menstr_period", 
                            MenstruationPeriod.NormalMinimalPeriod, TEXT.GetDaysString(MenstruationPeriod.NormalMinimalPeriod));
                    }
                    else if (distance > MenstruationPeriod.NormalMaximalPeriod)
                    {
                        askMessage += TEXT.Get.Format("Msg_large_menstr_period",
                            MenstruationPeriod.NormalMaximalPeriod, TEXT.GetDaysString(MenstruationPeriod.NormalMaximalPeriod));
                    }
                }

                if (date > DateTime.Today)
                {
                    askMessage += "\n" + TEXT.Get["Future_day_question"];
                }

                if (!string.IsNullOrEmpty(askMessage) && MessageBox.Show(askMessage + TEXT.Get["Are_you_sure_capital"],
                    TEXT.Get["What_a_situation"], MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return false;
                }

                Add(newPeriod);
            }

            return true;
        }

        new private void Add(MenstruationPeriod period)
        {
            base.Add(period);
            FireCollectionChangedEvent();
        }

        public bool IsMenstruationDay(DateTime day)
        {
            MenstruationPeriod period = GetPeriodByDate(day);
            return period != null;
        }

        public int GetEgestaAmount(DateTime day)
        {
            MenstruationPeriod period = GetPeriodByDate(day);
            if (period != null)
            {
                return period.Egestas[day];
            }
            return -1;
        }

        public bool SetEgesta(DateTime day, int egesta)
        {
            MenstruationPeriod period = GetPeriodByDate(day);
            if (period != null)
            {
                period.Egestas[day] = egesta;
                return true;
            }
            return false;
        }

        public MenstruationPeriod SetPeriodLength(MenstruationPeriod period, int length)
        {
            if (period != null && period.Length != length)
            {
                period.Length = length;
                FireCollectionChangedEvent();
                return period;
            }
            return period;
        }

        public MenstruationPeriod GetPeriodByDate(DateTime date)
        {
            if (Count == 0 || date < First.StartDay || date > Last.LastDay) return null;
            foreach (var p in this)
                if (p.IsDayInPeriod(date))
                    return p;
            return null;
            //return null; return (from p in this where p.IsDayInPeriod(date) select p).FirstOrDefault();
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
            if (Count == 0 || date > Last.StartDay) return null;
            MenstruationPeriod resultPeriod = null;
            foreach (MenstruationPeriod period in this)
            {
                if (period.StartDay > date && (resultPeriod == null || period.StartDay < resultPeriod.StartDay))
                {
                    resultPeriod = period;
                }
            }
            return resultPeriod;
            //return (from p in this where p.StartDay > date select p).FirstOrDefault();
        }

        public MenstruationPeriod GetClosestPeriodBeforeDay(DateTime date)
        {
            if (Count == 0 || date < First.StartDay) return null;
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

        public DateTime GetClosestOvulationDay(DateTime date)
        {
            if (Count < 2) throw new Exception("No menstruations. The method call prohibited.");
            MenstruationPeriod resultPeriodBefore = null;
            MenstruationPeriod resultPeriodAfter = null;
            foreach (MenstruationPeriod period in this)
            {
                if (period.StartDay < date && (resultPeriodBefore == null || period.StartDay > resultPeriodBefore.StartDay))
                {
                    resultPeriodBefore = period;
                }

                if (period.StartDay >= date && (resultPeriodAfter == null || period.StartDay < resultPeriodAfter.StartDay))
                {
                    resultPeriodAfter = period;
                }
            }

            return resultPeriodBefore.StartDay.AddDays(((resultPeriodAfter.StartDay - resultPeriodBefore.StartDay).Days / 2));
        }

        public int CalculateAveragePeriodLength()
        {
            if (Count < 2)
            {
                return 28;
            }

            double sum = 0;
            int count = 0;
            for (int i = 1; i < this.Count; i++)
            {
                if (!this[i - 1].HasPregnancy)
                {
                    int periodLength = (this[i].StartDay - this[i - 1].StartDay).Days;
                    if (periodLength < MenstruationPeriod.NormalMinimalPeriod ||
                        periodLength > MenstruationPeriod.NormalMaximalPeriod)
                    {
                        continue;
                    }
                    sum += periodLength;
                    count++;
                }
            }
            return sum == 0 ? 28 : (int)(sum / count + 0.5);
        }
    }
}
