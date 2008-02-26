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
        private const int CellsAmount = 42;
        private readonly List<DayCellControl> _cells = new List<DayCellControl>(CellsAmount);

        /// <summary>
        /// Returns integer value of week day of that month 1-st day. Values are 0-6 only. 0 - Monday, 7 - Sunday.
        /// </summary>
        private int DayOfWeekZero
        {
            get
            {
                return (Date.DayOfWeek == System.DayOfWeek.Sunday ? 7 : (int)Date.DayOfWeek) - 1;
            }
        }

        private DateTime _dateTime = DateTime.MinValue;
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

        public DayCellControl CreateNewDefaultCellControl(int number)
        {
            DayCellControl control = new DayCellControl(this);
            control.Location = new Point(DayCellControl.DefaultCellWidth * (number % 7),
                                         DayCellControl.DefaultCellHeight * (number / 7 + 1));
            control.Size = new Size(DayCellControl.DefaultCellWidth, DayCellControl.DefaultCellHeight);
            control.TabIndex = number + 1;
            control.CellClick += new DayCellControl.DayCellClick(control_CellClick);
            return control;
        }

        public delegate void DayClicked(object sender, DayCellClickEventArgs e);
        public event DayClicked MonthDayClicked;

        void control_CellClick(object sender, DayCellClickEventArgs e)
        {
            if (MonthDayClicked != null)
            { // fire event if here were no focus before or not focused day clicked
                MonthDayClicked(this, e);
            }
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

        private MonthsControl _ownerMonthsControl;
        public MonthsControl OwnerMonthsControl
        {
            get { return _ownerMonthsControl; }
            set { _ownerMonthsControl = value; }
        }

        public OneMonthControl(MonthsControl parent) : this()
        {
            _ownerMonthsControl = parent;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (Date != DateTime.MinValue)
            {
                Rectangle headerRect = new Rectangle(0, 0, Size.Width, DayCellControl.DefaultCellHeight);
                pe.Graphics.FillRectangle(Program.MonthAppearance.HeaderBrush, headerRect);

                string monthNameAndYear = Date.ToString("MMMM yyyy");
                SizeF textSize = pe.Graphics.MeasureString(monthNameAndYear, Font, Size.Width);
                pe.Graphics.DrawString(monthNameAndYear, Font, Brushes.Black,
                    (Size.Width - textSize.Width) / 2, 0);

                for (int i = 0; i < 7; i++)
                {
                    string aDay = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames[i];
                    textSize = pe.Graphics.MeasureString(aDay, Font, DayCellControl.DefaultCellWidth);
                    pe.Graphics.DrawString(aDay, Font,
                        Brushes.Black, 
                        DayCellControl.DefaultCellWidth*i + (DayCellControl.DefaultCellWidth - textSize.Width) / 2, 
                        DayCellControl.DefaultCellHeight - textSize.Height);
                }
            }
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
    }
}
