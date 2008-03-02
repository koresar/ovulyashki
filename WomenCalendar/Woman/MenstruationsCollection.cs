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

        public bool Add(DateTime date, int length)
        {
            MenstruationPeriod newPeriod = new MenstruationPeriod(date, length);
            MenstruationPeriod closestPeriod = GetClosestPeriodAfterDay(date);
            if (closestPeriod != null)
            {
                if (MessageBox.Show("После этих овуляшек у тебя были другие!\nТЫ УВЕРЕНА В ТОМ ЧТО ДЕЛАЕШЬ?", 
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
                        askMessage += "Между овуляшками меньше 21-го дня! Такой маленький цикл мы не будем учитывать при прогнозировании.";
                    }
                    else if (distance > MenstruationPeriod.NormalMaximalPeriod)
                    {
                        askMessage += "Между овуляшками больше 35-ти дней! Такой большой цикл мы не будем учитывать при прогнозировании.";
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
            FireCollectionChangedEvent();
            base.Add(period);
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

        public int GetEgestaAmount(DateTime day)
        {
            foreach (MenstruationPeriod period in this)
            {
                if (period.IsDayInPeriod(day))
                {
                    return period.Egestas[day];
                }
            }
            return -1;
        }

        public bool SetEgesta(DateTime day, int egesta)
        {
            foreach (MenstruationPeriod period in this)
            {
                if (period.IsDayInPeriod(day))
                {
                    period.Egestas[day] = egesta;
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
