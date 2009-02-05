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
                if (MessageBox.Show("После этих менструашек у тебя были другие!\nТЫ УВЕРЕНА В ТОМ ЧТО ДЕЛАЕШЬ?", 
                    "Ты с ума сошла?", MessageBoxButtons.YesNo) != DialogResult.Yes)
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
                        askMessage += "Между менструашками меньше 21-го дня! Такой маленький цикл мы не будем учитывать при прогнозировании.";
                    }
                    else if (distance > MenstruationPeriod.NormalMaximalPeriod)
                    {
                        askMessage += "Между менструашками больше 35-ти дней! Такой большой цикл мы не будем учитывать при прогнозировании.";
                    }
                }

                if (date > DateTime.Today)
                {
                    askMessage += "\nЭто же день из будущего! Он еще не настал. Записалась в Нострадамусы?";
                }

                if (!string.IsNullOrEmpty(askMessage) && MessageBox.Show(askMessage + "\nТЫ УВЕРЕНА В ТОМ ЧТО ДЕЛАЕШЬ?",
                    "Ухты, какая необычная ситуация!", MessageBoxButtons.YesNo) != DialogResult.Yes)
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

        public MenstruationPeriod SetPeriodLength(DateTime date, int length)
        {
            MenstruationPeriod period = GetPeriodByDate(date);
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
            //return (from p in this where p.StartDay < date orderby p.StartDay descending select p).FirstOrDefault();
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

                if (period.StartDay > date && (resultPeriodAfter == null || period.StartDay < resultPeriodAfter.StartDay))
                {
                    resultPeriodAfter = period;
                }
            }

            return resultPeriodBefore.StartDay.AddDays(((resultPeriodAfter.StartDay - resultPeriodBefore.StartDay).Days / 2));

            //return resultPeriodBefore.StartDay.AddDays(((resultPeriodAfter.StartDay - resultPeriodBefore.StartDay).Days / 2));
            //return (from p1 in this
            //        where p1.StartDay > date
            //        from p2 in this
            //        where p2.StartDay < date
            //        orderby p2.StartDay descending
            //        select p1.StartDay.AddDays((p2.StartDay - p1.StartDay).Days / 2)).First();
        }

        public int CalculateAveragePeriodLength()
        {
            if (Count < 2)
            {
                return 28;
            }

            return (int)((double)((Last.StartDay - First.StartDay).Days) / (Count - 1) + 0.5);
        }
    }
}
