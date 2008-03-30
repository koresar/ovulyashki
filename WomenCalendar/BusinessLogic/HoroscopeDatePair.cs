using System;

namespace WomenCalendar
{
    public class HoroscopDatePair
    {
        private DateTime From = DateTime.MinValue;
        private DateTime To = DateTime.MinValue;

        public HoroscopDatePair(int fromMonth, int fromDay, int toMonth, int toDay)
        {
            From = new DateTime(DateTime.MinValue.Year, fromMonth, fromDay);
            To = new DateTime(DateTime.MinValue.Year, toMonth, toDay);
        }

        public bool IsInRange(DateTime date)
        {
            DateTime d = new DateTime(DateTime.MinValue.Year, date.Month, date.Day);
            return d >= From && d <= To;
        }

        public enum ZodiacSign 
            { Aries = 0, Taurus, Gemini, Cancer, Leo, Virgo, Libra, Scorpio, Sagittarius, Capricorn, Aquarius, Pisces }

        public static string[] ZodiacNames = new string[]
            { "Овен", "Телец", "Близнецы", "Рак", "Лев", "Дева", "Весы", "Скорпион", "Стрелец", "Козерог", "Водолей", "Рыбы" };

        public static HoroscopDatePair[] ZodiacSigns = new HoroscopDatePair[]
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
                new HoroscopDatePair(12, 22, 1, 19),
                new HoroscopDatePair(1, 20, 2, 18),
                new HoroscopDatePair(2, 19, 3, 20)
            };

        public static string GetZodiacSignName(DateTime date)
        {
            for (int i = 0; i < ZodiacSigns.Length; i++)
            {
                if (ZodiacSigns[i].IsInRange(date))
                {
                    return ZodiacNames[i];
                }
            }
            throw new Exception("AAAAAAAAAAAA!!! That must never happen!");
        }
    }
}
