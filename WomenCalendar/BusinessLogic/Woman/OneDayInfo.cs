using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The information about a day.
    /// </summary>
    public class OneDayInfo
    {
        private static TranslationsList header = new TranslationsList()
        {
            "Date", "Menses", "Intensity", "Ovulation", "Had_sex", 
            "BBT", "Wellbeing_1_10", "CF_full", "Note"
        };

        /// <summary>
        /// The list of translations of the report header.
        /// </summary>
        public static TranslationsList Header
        {
            get { return header; }
        }

        /// <summary>
        /// The date of the data.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Is there bleeding that day.
        /// </summary>
        public bool IsMentruation { get; set; }

        /// <summary>
        /// The amount of excretion.
        /// </summary>
        public int Egesta { get; set; }

        /// <summary>
        /// Is the day was predicted as ovulation.
        /// </summary>
        public bool IsOvulation { get; set; }

        /// <summary>
        /// Is there was sex that day.
        /// </summary>
        public bool HadSex { get; set; }

        /// <summary>
        /// Basal Body Temperature of the day (if present).
        /// </summary>
        public double BBT { get; set; }

        /// <summary>
        /// The woman health value on that day.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Cervical Fluid on that day.
        /// </summary>
        public CervicalFluid CF { get; set; }

        /// <summary>
        /// The note of the day.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Generate the object from the given woman and the date.
        /// </summary>
        /// <param name="w">Woman to use.</param>
        /// <param name="day">The date of the future data.</param>
        /// <returns>Day info data.</returns>
        public static OneDayInfo GetByDate(Woman w, DateTime day)
        {
            return new OneDayInfo()
            {
                Date = day,
                IsMentruation = w.Menstruations.IsMenstruationDay(day),
                Egesta = w.Menstruations.GetEgestaAmount(day),
                IsOvulation = w.IsPredictedAsOvulationDay(day),
                HadSex = w.HadSexList[day],
                BBT = w.BBT.GetBBT(day),
                Health = w.Health[day],
                CF = w.CFs[day],
                Note = w.Notes[day],
            };
        }
    }
}
