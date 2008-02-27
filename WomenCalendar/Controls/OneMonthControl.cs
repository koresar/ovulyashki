using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace WomenCalendar
{
    public partial class OneMonthControl : UserControl
    {
        public const int EdgeWidth = 3;

        private const int CellsAmount = 42;
        private readonly List<DayCellControl> _cells = new List<DayCellControl>(CellsAmount);

        public delegate void DayClicked(object sender, DayCellClickEventArgs e);
        public event DayClicked MonthDayClicked;

        /// <summary>
        /// Returns integer value of week day of that month 1-st day. Values are 0-6 only. 0 - Monday, 7 - Sunday.
        /// </summary>
        private int DayOfWeekZero
        {
            get
            {
                return (Date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)Date.DayOfWeek) - 1;
            }
        }

        private DateTime _dateTime = DateTime.Today;
        /// <summary>
        /// The month which control represents.
        /// </summary>
        public DateTime Date
        {
            get { return _dateTime; }
            set
            {
                _dateTime = new DateTime(value.Year, value.Month, 1);

                int correction = (DayOfWeekZero)*(-1);
                for (int i = 0; i < CellsAmount; i++)
                {
                    _cells[i].Date = Date.AddDays(i + correction);
                }
            }
        }

        private DayCellControl _focusDay;
        public DayCellControl FocusDay
        {
            get { return _focusDay; }
            set
            {
                if (value == _focusDay) return;
                DayCellControl prevFocusDay = _focusDay;
                _focusDay = value;
                if (prevFocusDay != null)
                {
                    prevFocusDay.Invalidate();
                    prevFocusDay.Update();
                }
                if (_focusDay != null)
                {
                    _focusDay.Invalidate();
                    _focusDay.Update();
                }
            }
        }

        public DateTime FocusDate
        {
            get { return FocusDay == null ? DateTime.MinValue : FocusDay.Date; }
            set
            {
                if (FocusDay != null && (value.Month != Date.Month || value.Year != Date.Year || value.Date == FocusDate.Date))
                {
                    return; // no focus at all, wrong month or same date.
                }
                FocusDay = GetCellByDate(value);
            }
        }

        private MonthsControl _ownerMonthsControl;
        public MonthsControl OwnerMonthsControl
        {
            get { return _ownerMonthsControl; }
            set { _ownerMonthsControl = value; }
        }

        public OneMonthControl(MonthsControl parent)
            : this()
        {
            _ownerMonthsControl = parent;
        }

        public OneMonthControl()
        {
            InitializeComponent();

            SuspendLayout();

            for (int i = 0; i < 42; i++)
            {
                DayCellControl control = CreateNewDefaultCellControl(i);
                _cells.Add(control);
                Controls.Add(control);
            }

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);

            ResumeLayout();
        }

        public DayCellControl CreateNewDefaultCellControl(int number)
        {
            DayCellControl control = new DayCellControl(this);
            control.Location = new Point(EdgeWidth + DayCellControl.DefaultCellWidth * (number % 7),
                                         EdgeWidth + DayCellControl.DefaultCellHeight * (number / 7 + 1));
            control.Size = new Size(DayCellControl.DefaultCellWidth, DayCellControl.DefaultCellHeight);
            control.TabIndex = number + 1;
            control.CellClick += new DayCellControl.DayCellClick(control_CellClick);
            return control;
        }

        public DayCellControl GetCellByDate(DateTime date)
        {
            int i = DayOfWeekZero + date.Day - 1;
            if (i >= 0 && i < _cells.Count)
            {
                return _cells[i];
            }
            return null;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (Date != DateTime.MinValue)
            {
                Rectangle headerRect = new Rectangle(EdgeWidth, EdgeWidth, DayCellControl.DefaultCellWidth*7, DayCellControl.DefaultCellHeight);
                pe.Graphics.FillRectangle(Program.MonthAppearance.HeaderBrush, headerRect);

                string monthNameAndYear = Date.ToString("MMMM yyyy");
                SizeF textSize = pe.Graphics.MeasureString(monthNameAndYear, Font, Size.Width);
                pe.Graphics.DrawString(monthNameAndYear, Font, Brushes.Black,
                    EdgeWidth + (Size.Width - textSize.Width) / 2, EdgeWidth);

                for (int i = 0; i < 7; i++)
                {
                    string aDay = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames[(i+1)%7];
                    textSize = pe.Graphics.MeasureString(aDay, Font, DayCellControl.DefaultCellWidth);
                    pe.Graphics.DrawString(aDay, Font,
                        Brushes.Black,
                        EdgeWidth + DayCellControl.DefaultCellWidth * i + (DayCellControl.DefaultCellWidth - textSize.Width) / 2,
                        EdgeWidth + DayCellControl.DefaultCellHeight - textSize.Height);
                }

                pe.Graphics.DrawRectangle((DateTime.Today.Month == Date.Month) ? 
                    Program.MonthAppearance.TodayEdgePen : Program.MonthAppearance.HeaderPen, ClientRectangle);
            }
        }

        void control_CellClick(object sender, DayCellClickEventArgs e)
        {
            if (MonthDayClicked != null)
            { // fire event if here were no focus before or not focused day clicked
                MonthDayClicked(this, e);
            }
        }
    }
}
