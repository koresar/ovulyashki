using System;
using System.Linq;

namespace WomenCalendar
{
    /// <summary>
    /// Class for different horoscop stuff.
    /// </summary>
    public class HoroscopDatePair
    {
        private static HoroscopDatePair[] zodiacSigns = new HoroscopDatePair[]
        {
            new HoroscopDatePair(3, 21, 4, 19, "Aries"),
            new HoroscopDatePair(4, 20, 5, 20, "Taurus"),
            new HoroscopDatePair(5, 21, 6, 20, "Gemini"),
            new HoroscopDatePair(6, 21, 7, 22, "Cancer"),
            new HoroscopDatePair(7, 23, 8, 22, "Leo"),
            new HoroscopDatePair(8, 23, 9, 22, "Virgo"),
            new HoroscopDatePair(9, 23, 10, 22, "Libra"),
            new HoroscopDatePair(10, 23, 11, 21, "Scorpio"),
            new HoroscopDatePair(11, 22, 12, 21, "Sagittarius"),
            new HoroscopDatePair(12, 22, 13, 19, "Capricorn"),
            new HoroscopDatePair(1, 20, 2, 19, "Aquarius"),
            new HoroscopDatePair(2, 20, 3, 20, "Pisces")
        };

        private DateTime from = DateTime.MinValue;
        private DateTime to = DateTime.MinValue;
        private string name;

        /// <summary>
        /// The zodiac sign.
        /// </summary>
        /// <param name="fromMonth">Zodiac sign start month.</param>
        /// <param name="fromDay">Zodiac sign start day.</param>
        /// <param name="toMonth">Zodiac sign end month.</param>
        /// <param name="toDay">Zodiac sign end day.</param>
        /// <param name="name">The sign string ID (name).</param>
        private HoroscopDatePair(int fromMonth, int fromDay, int toMonth, int toDay, string name)
        {
            this.from = new DateTime(DateTime.MinValue.Year, fromMonth, fromDay);
            this.to = new DateTime(DateTime.MinValue.Year + ((toMonth - 1) / 12), ((toMonth - 1) % 12) + 1, toDay);
            this.name = name;
        }

        /// <summary>
        /// Return zodiac sign translated name for any date.
        /// </summary>
        /// <param name="date">Date to find sign for.</param>
        /// <returns>Translated zodiac sign name.</returns>
        public static string GetZodiacSignName(DateTime date)
        {
            return zodiacSigns.First(sign => sign.IsInRange(date)).name;
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
