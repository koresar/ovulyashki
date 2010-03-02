using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using WomenCalendar.Properties;

namespace WomenCalendar.Controls
{
    public partial class ColoredSchedulerCalendarControl : UserControl
    {
        private const int CellSize = 10;

        private Woman w = Program.CurrentWoman;
        private Schedule currentSchedule;
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
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var appearance = Program.Settings == null ? new DayCellAppearance() : Program.Settings.DayCellAppearance;
            var checkmark = Resources.ResourceManager.GetObject("Checkmark_32") as Image;

            var monthNames = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedMonthNames;
            var month = StartMonth;
            var leftSpace = this.Width - 31 * CellSize;
            var xOffset = leftSpace > 0 ? leftSpace - 1 : 0;
            var yOffset = CellSize + 2;
            for (int m = 0; m < 12; m++)
            {
                    var s = monthNames[month.Month - 1];
                    pe.Graphics.DrawString(s, MonthFont, Brushes.Black, 
                        new Point(0, m * CellSize + yOffset - 2));
                    month = month.AddMonths(1);
                
            }
            for (int d = 1; d <= 31; d++)
            {
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
            var backEmptyBrush = new SolidBrush(appearance.BackEmpty);
            while (day < final)
            {
                var x = xOffset + CellSize * (day.Day - 1);
                var y = yOffset + CellSize * ((12 + day.Month - StartMonth.Month) % 12);
                var usedBackColor = 
                    w.Menstruations.IsMenstruationDay(day) ? backMensesBrush :
                    w.IsPredictedAsMenstruationDay(day) ? backPredictedMensesBrush :
                    w.IsPredictedAsOvulationDay(day) ? backPredictedOvBrush :
                    backEmptyBrush;

                pe.Graphics.FillRectangle(usedBackColor, x, y, CellSize, CellSize);

                pe.Graphics.DrawRectangle(Pens.Black, x - 1, y - 1, CellSize, CellSize);

                if (currentSchedule != null && currentSchedule.AlarmAtDay(day))
                {
                    pe.Graphics.DrawImage(checkmark, new Rectangle(x, y, CellSize - 1, CellSize - 1));
                }

                day = day.AddDays(1);
            }
        }

        public void ApplySchedule(Schedule schedule)
        {
            currentSchedule = schedule;
            Redraw();
        }

        public void Redraw()
        {
            Invalidate();
            Refresh();
        }
    }
}
