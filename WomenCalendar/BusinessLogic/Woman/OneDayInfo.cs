using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class OneDayInfo
    {
        public static readonly string[] Header = 
        { 
            "Дата", "Менструации", "Интенсивность", "Был секс", "БTT", "Самочувствие (0-10)", "Заметка"
        };

        public static OneDayInfo GetByDate(Woman w, DateTime day)
        {
            return new OneDayInfo()
            {
                Date = day,
                IsMentruation = w.Menstruations.IsMenstruationDay(day),
                Egesta = w.Menstruations.GetEgestaAmount(day),
                HadSex = w.HadSexList[day],
                BBT = w.BBT.GetBBT(day),
                Health = w.Health[day],
                Note = w.Notes[day],
            };
        }

        public DateTime Date { get; set; }
        public bool IsMentruation { get; set; }
        public int Egesta { get; set; }
        public bool HadSex { get; set; }
        public double BBT { get; set; }
        public int Health { get; set; }
        public string Note { get; set; }
    }
}
