using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace WomenCalendar.Controls
{
    public partial class ColoredSchedulerCalendarControl : UserControl
    {
        private const int CellSize = 10;

        private Woman w = Program.CurrentWoman;
        public DateTime StartMonth { get; set; }

        public Font monthFont;
        public Font MonthFont
        {
            get
            {
                return monthFont ?? (monthFont = new Font("Microsoft Sans Serif", 8F));
            }
        }

        public ColoredSchedulerCalendarControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var appearance = Program.Settings == null ? new DayCellAppearance() : Program.Settings.DayCellAppearance;

            var backEmptyBrush = new SolidBrush(appearance.BackEmpty);
            pe.Graphics.FillRectangle(backEmptyBrush, 0, 0, Size.Width, Size.Height);

            var monthNames = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedMonthNames;
            var month = StartMonth;
            var leftSpace = this.Width - 31 * CellSize;
            var xOffset = leftSpace > 0 ? leftSpace - 1 : 0;
            var yOffset = CellSize;
            for (int m = 0; m <= 12; m++)
            {
                pe.Graphics.DrawLine(Pens.Black, 
                    new PointF(xOffset, m * CellSize + yOffset),
                    new PointF(xOffset + 31 * CellSize, m * CellSize + yOffset));

                if (m < 12)
                {
                    var s = monthNames[month.Month - 1];
                    pe.Graphics.DrawString(s, MonthFont, Brushes.Black, 
                        new Point(0, m * CellSize + yOffset));
                    month = month.AddMonths(1);
                }
            }
            for (int d = 0; d <= 31; d++)
            {
                pe.Graphics.DrawLine(Pens.Black,
                    new PointF(xOffset + d * CellSize, yOffset),
                    new PointF(xOffset + d * CellSize, 12 * CellSize + yOffset));

                if (d % 2 == 1 && d > 0)
                {
                    var s = d.ToString();

                    SizeF textSize = pe.Graphics.MeasureString(s, Font);
                    pe.Graphics.DrawString(s, MonthFont, Brushes.Black,
                        xOffset - CellSize + d * CellSize + (CellSize - textSize.Width) / 2, -1);
                }
            }

            var day = new DateTime(StartMonth.Year, StartMonth.Month, 1);
            var final = day.AddYears(1);
            var backMensesBrush = new SolidBrush(appearance.BackMenstruationDay);
            var backPredictedMensesBrush = new SolidBrush(appearance.BackPredictedMenstruationDay);
            var backPredictedOvBrush = new SolidBrush(appearance.BackOvulationDay);
            var backBrush = new SolidBrush(this.BackColor);
            int rectSize = CellSize - 1;
            while (day < final)
            {
                var x = xOffset + CellSize * (day.Day - 1) + 1;
                var y = yOffset + CellSize * ((12 + day.Month - StartMonth.Month) % 12) + 1;
                if (w.Menstruations.IsMenstruationDay(day))
                {
                    pe.Graphics.FillRectangle(backMensesBrush, x, y, rectSize, rectSize);
                }
                else if (w.IsPredictedAsMenstruationDay(day))
                {
                    pe.Graphics.FillRectangle(backPredictedMensesBrush, x, y, rectSize, rectSize);
                }
                else if (w.IsPredictedAsOvulationDay(day))
                {
                    pe.Graphics.FillRectangle(backPredictedOvBrush, x, y, rectSize, rectSize);
                }

                var nextDay = day.AddDays(1);
                if (nextDay.Month != day.Month && day.Day < 31)
                {
                    pe.Graphics.FillRectangle(backBrush, x, y, this.Size.Width, rectSize);
                }
                day = nextDay;
            }
        }
    }
}
