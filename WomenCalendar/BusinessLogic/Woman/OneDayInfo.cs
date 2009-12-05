using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class OneDayInfo
    {
        private static string[] header;
        public static string[] Header
        {
            get
            {
                if (header == null)
                {
                    var ids = new string[] { "Date", "Menses", "Intensity", "Had_sex", "BBT", "Wellbeing_1_10", "Note" };
                    header = new string[ids.Length];
                    for (int i = 0; i < ids.Length; i++)
                        header[i] = TEXT.Get[ids[i]];
                }
                return header;
            }
        }

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
