using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WomenCalendar.Properties;

namespace WomenCalendar.Controls
{
    /// <summary>
    /// The control displays the smallest possible calendar with scheduled days mark.
    /// </summary>
    public partial class ColoredSchedulerCalendarControl : UserControl
    {
        private const int CellSize = 10;

        private DateTime startMonth;
        private Font monthFont;

        private Woman woman = Program.CurrentWoman;
        private List<Schedule> currentSchedules;
        private Dictionary<DateTime, bool> marks = new Dictionary<DateTime, bool>();

        /// <summary>
        /// Creates the control.
        /// </summary>
        public ColoredSchedulerCalendarControl()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        /// <summary>
        /// The month to start view from.
        /// </summary>
        public DateTime StartMonth
        {
            get
            {
                return this.startMonth;
            }

            set
            {
                this.startMonth = value;
                this.ReinitMarks();
            }
        }

        /// <summary>
        /// The font to draw month names.
        /// </summary>
        public Font MonthFont
        {
            get
            {
                return this.monthFont ?? (this.monthFont = new Font("Microsoft Sans Serif", 8F));
            }
        }

        private List<Schedule> CurrentSchedules
        {
            get
            {
                return this.currentSchedules;
            }

            set
            {
                this.currentSchedules = value;
                this.ReinitMarks();
            }
        }

        /// <summary>
        /// Apply several new schedules to this control.
        /// </summary>
        /// <param name="schedule">List of schedules to show.</param>
        public void ApplySchedules(List<Schedule> schedule)
        {
            this.CurrentSchedules = schedule;
            this.Redraw();
        }

        /// <summary>
        /// Apply new schedule to this control.
        /// </summary>
        /// <param name="schedule">New schedule,</param>
        public void ApplySchedule(Schedule schedule)
        {
            this.CurrentSchedules = new List<Schedule>(1) { schedule };
            this.Redraw();
        }

        /// <summary>
        /// Repaint control.
        /// </summary>
        public void Redraw()
        {
            this.Invalidate();
            this.Refresh();
        }

        /// <summary>
        /// Repaint the control.
        /// </summary>
        /// <param name="pe">Paint event arguments.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            var appearance = Program.Settings == null ? new DayCellAppearance() : Program.Settings.DayCellAppearance;
            var checkmark = Resources.ResourceManager.GetObject("Checkmark_32") as Image;

            var monthNames = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedMonthNames;
            var month = this.StartMonth;
            var leftSpace = this.Width - (31 * CellSize);
            var xOffset = leftSpace > 0 ? leftSpace - 1 : 0;
            var yOffset = CellSize + 2;
            for (int m = 0; m < 12; m++)
            {
                var s = monthNames[month.Month - 1];
                pe.Graphics.DrawString(
                    s,
                    this.MonthFont, 
                    Brushes.Black,
                    new Point(0, (m * CellSize) + yOffset - 2));
                month = month.AddMonths(1);
            }

            for (int d = 1; d <= 31; d++)
            {
                if (d % 2 == 1 && d > 0)
                {
                    var s = d.ToString();

                    SizeF textSize = pe.Graphics.MeasureString(s, Font);
                    pe.Graphics.DrawString(
                        s,
                        this.MonthFont, 
                        Brushes.Black,
                        xOffset - CellSize + (d * CellSize) + ((CellSize - textSize.Width) / 2), 
                        -1);
                }
            }

            var day = new DateTime(this.StartMonth.Year, this.StartMonth.Month, 1);
            var final = day.AddYears(1);
            var backMensesBrush = new SolidBrush(appearance.BackMenstruationDay);
            var backPredictedMensesBrush = new SolidBrush(appearance.BackPredictedMenstruationDay);
            var backPredictedOvBrush = new SolidBrush(appearance.BackOvulationDay);
            var backBrush = new SolidBrush(this.BackColor);
            var backEmptyBrush = new SolidBrush(appearance.BackEmpty);
            while (day < final)
            {
                var x = xOffset + (CellSize * (day.Day - 1));
                var y = yOffset + (CellSize * ((12 + day.Month - this.StartMonth.Month) % 12));
                var usedBackColor =
                    this.woman.Menstruations.IsMenstruationDay(day) ? backMensesBrush :
                    this.woman.IsPredictedAsMenstruationDay(day) ? backPredictedMensesBrush :
                    this.woman.IsPredictedAsOvulationDay(day) ? backPredictedOvBrush :
                    backEmptyBrush;

                pe.Graphics.FillRectangle(usedBackColor, x, y, CellSize, CellSize);

                pe.Graphics.DrawRectangle(Pens.Black, x - 1, y - 1, CellSize, CellSize);

                if (this.marks.Count > 0 && this.marks.ContainsKey(day))
                {
                    pe.Graphics.DrawImage(checkmark, new Rectangle(x, y, CellSize - 1, CellSize - 1));
                }

                day = day.AddDays(1);
            }
        }

        private void ReinitMarks()
        {
            this.marks = new Dictionary<DateTime, bool>();
            if (this.currentSchedules != null && this.currentSchedules.Count > 0)
            {
                var day = new DateTime(this.StartMonth.Year, this.StartMonth.Month, 1);
                var final = day.AddYears(1);
                while (day < final)
                {
                    if (this.currentSchedules.Any(s => s.IsAlarmAtDay(day)))
                    {
                        this.marks[day] = true;
                    }

                    day = day.AddDays(1);
                }
            }
        }
    }
}
