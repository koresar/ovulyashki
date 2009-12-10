using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class OneDayInfo
    {
        public static TranslationsList Header = new TranslationsList()
        {
            "Date", "Menses", "Intensity", "Had_sex", "BBT", "Wellbeing_1_10", "Note"
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
