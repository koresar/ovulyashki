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
            new HoroscopDatePair(3, 21, 4, 20, "Aries"), // Овен
            new HoroscopDatePair(4, 21, 5, 21, "Taurus"), // Телец
            new HoroscopDatePair(5, 22, 6, 21, "Gemini"), // Близнецы
            new HoroscopDatePair(6, 22, 7, 22, "Cancer"), // Рак
            new HoroscopDatePair(7, 23, 8, 23, "Leo"), // Лев
            new HoroscopDatePair(8, 24, 9, 23, "Virgo"), // Дева
            new HoroscopDatePair(9, 24, 10, 23, "Libra"), // Весы
            new HoroscopDatePair(10, 24, 11, 22, "Scorpio"), // Скорпион
            new HoroscopDatePair(11, 23, 12, 21, "Sagittarius"), // Стрелец
            new HoroscopDatePair(12, 22, 13, 20, "Capricorn"), // Козерог
            new HoroscopDatePair(1, 21, 2, 18, "Aquarius"), // Водолей
            new HoroscopDatePair(2, 19, 3, 20, "Pisces") // Рыбы
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
            return TEXT.Get[zodiacSigns.First(sign => sign.IsInRange(date)).name];
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
