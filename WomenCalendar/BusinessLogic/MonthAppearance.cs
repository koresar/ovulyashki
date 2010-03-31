using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    /// <summary>
    /// The settings of the calendar month appearance.
    /// </summary>
    public static class MonthAppearance
    {
        private static Brush monthHeaderBrush = 
            new LinearGradientBrush(new Point(0, 0), new Point(120, 0), Color.FromArgb(248, 153, 250), Color.White)
            {
                WrapMode = WrapMode.TileFlipXY
            };

        private static Brush weekDayHeaderBrush =
            new LinearGradientBrush(new Point(0, 0), new Point(0, 16), Color.RoyalBlue, ControlPaint.LightLight(Color.RoyalBlue))
            {
                WrapMode = WrapMode.TileFlipXY,
            };

        private static Brush monthNameBrush = Brushes.Purple;
        private static Brush weekDayTextBrush = Brushes.White;
        private static Brush weekDayHolidayTextBrush = Brushes.Salmon;
        private static Pen monthEdgePen = new Pen(Brushes.Gray, 6);
        private static Pen weekDayEdgePen = new Pen(Brushes.White, 1);
        private static Pen todayEdgePen = new Pen(Brushes.Blue, 6);

        /// <summary>
        /// The month header back brush.
        /// </summary>
        public static Brush MonthHeaderBrush
        {
            get { return monthHeaderBrush; }
        }

        /// <summary>
        /// The day of week name brush.
        /// </summary>
        public static Brush WeekDayHeaderBrush
        {
            get { return weekDayHeaderBrush; }
        }

        /// <summary>
        /// The month name brush.
        /// </summary>
        public static Brush MonthNameBrush
        {
            get { return monthNameBrush; }
        }

        /// <summary>
        /// The day of week number brush.
        /// </summary>
        public static Brush WeekDayTextBrush
        {
            get { return weekDayTextBrush; }
        }

        /// <summary>
        /// The holiday number brush.
        /// </summary>
        public static Brush WeekDayHolidayTextBrush
        {
            get { return weekDayHolidayTextBrush; }
        }

        /// <summary>
        /// The edge pen of one month.
        /// </summary>
        public static Pen MonthEdgePen
        {
            get { return monthEdgePen; }
        }

        /// <summary>
        /// Week day edge pen.
        /// </summary>
        public static Pen WeekDayEdgePen
        {
            get { return weekDayEdgePen; }
        }

        /// <summary>
        /// Troday month edge pen.
        /// </summary>
        public static Pen TodayEdgePen
        {
            get { return todayEdgePen; }
        }
    }
}
