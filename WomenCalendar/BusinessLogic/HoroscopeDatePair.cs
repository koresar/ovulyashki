using System;

namespace WomenCalendar
{
    /// <summary>
    /// Class for different horoscop stuff.
    /// </summary>
    public class HoroscopDatePair
    {
        private static TranslationsList zodiacNames = new TranslationsList()
        { 
            "Aries", "Sagittarius", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces"
        };

        private static HoroscopDatePair[] zodiacSigns = new HoroscopDatePair[]
        {
            new HoroscopDatePair(3, 21, 4, 19),
            new HoroscopDatePair(4, 20, 5, 20),
            new HoroscopDatePair(5, 21, 6, 20),
            new HoroscopDatePair(6, 21, 7, 22),
            new HoroscopDatePair(7, 23, 8, 22),
            new HoroscopDatePair(8, 23, 9, 22),
            new HoroscopDatePair(9, 23, 10, 22),
            new HoroscopDatePair(10, 23, 11, 21),
            new HoroscopDatePair(11, 22, 12, 21),
            new HoroscopDatePair(12, 22, 13, 19),
            new HoroscopDatePair(1, 20, 2, 18),
            new HoroscopDatePair(2, 19, 3, 20)
        };

        private DateTime from = DateTime.MinValue;
        private DateTime to = DateTime.MinValue;

        /// <summary>
        /// The zodiac sign.
        /// </summary>
        /// <param name="fromMonth">Zodiac sign start month.</param>
        /// <param name="fromDay">Zodiac sign start day.</param>
        /// <param name="toMonth">Zodiac sign end month.</param>
        /// <param name="toDay">Zodiac sign end day.</param>
        public HoroscopDatePair(int fromMonth, int fromDay, int toMonth, int toDay)
        {
            this.from = new DateTime(DateTime.MinValue.Year, fromMonth, fromDay);
            this.to = new DateTime(DateTime.MinValue.Year + ((toMonth - 1) / 12), ((toMonth - 1) % 12) + 1, toDay);
        }

        /// <summary>
        /// Return zodiac sign translated name for any date.
        /// </summary>
        /// <param name="date">Date to find sign for.</param>
        /// <returns>Translated zodiac sign name.</returns>
        public static string GetZodiacSignName(DateTime date)
        {
            for (int i = 0; i < zodiacSigns.Length; i++)
            {
                if (zodiacSigns[i].IsInRange(date))
                {
                    return zodiacNames[i];
                }
            }

            throw new Exception("Unable to find zodiac sign for " + date.ToString());
        }

        private bool IsInRange(DateTime date)
        {
            DateTime d = new DateTime(DateTime.MinValue.Year, date.Month, date.Day);
            if (d.Month == 1 && d.Day < 20)
            {
                d = d.AddYears(1);
            }

            return d >= this.from && d <= this.to;
        }
    }
}
