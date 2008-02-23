using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class MonthsControl : UserControl
    {
        public List<OneMonthControl> singleMonths = new List<OneMonthControl>();
        public int VisibleMonthsCount = 1;

        private OneMonthControl _focusMonth;
        public OneMonthControl FocusMonth
        {
            get { return _focusMonth; }
            set { _focusMonth = value; }
        }

        private DayCellPopupControl cellPopupControl;
        public DayCellPopupControl CellPopupControl
        {
            get { return cellPopupControl; }
            set { cellPopupControl = value; }
        }

        public DayCellControl FocusDay
        {
            get
            {
                return FocusMonth == null ? null : FocusMonth.FocusDay;
            }
            set
            {
                if (FocusMonth != null) FocusMonth.FocusDay = value;
            }
        }

        public bool IsDateVisible(DateTime date)
        {
            return date.Date >= singleMonths[0].Date &&
                   date.Date < singleMonths[VisibleMonthsCount - 1].Date.AddMonths(1);
        }

        public DateTime FocusDate
        {
            get { return FocusDay == null ? DateTime.MinValue : FocusMonth.FocusDay.Date; }
            set
            {
                DateTime _value = value.Date;
                if (FocusDate == _value) return;
                
                if (FocusDay != null)
                {
                    FocusDay = null; // remove focus
                }

                OneMonthControl oneMonth;
                if (IsDateVisible(_value))
                {
                    oneMonth = singleMonths[
                        (_value.Year - singleMonths[0].Date.Year) * 12 + _value.Month - singleMonths[0].Date.Month];
                }
                else
                {
                    StartMonth = _value.Date;
                    oneMonth = singleMonths[0];
                }

                FocusDateChangedEventArgs ea = new FocusDateChangedEventArgs(oneMonth.FocusDate, _value.Date);

                oneMonth.FocusDate = _value.Date; // set focus
                FocusMonth = oneMonth;

                if (FocusDateChanged != null)
                {
                    FocusDateChanged(this, ea);
                }
            }
        }

        public delegate void FocusDateChangedDelegate(object sender, FocusDateChangedEventArgs e);
        public event FocusDateChangedDelegate FocusDateChanged;

        public DateTime StartMonth
        {
            get { return oneMonthControl.Date; }
            set
            {
                oneMonthControl.Date = value.Date;
                Invalidate();
                for (int i = 0; i < singleMonths.Count; i++)
                {
                    singleMonths[i].Date = value.AddMonths(i);
                    singleMonths[i].Invalidate();
                }
                CreateAndAdjustMonthsAmount();
            }
        }

        public MonthsControl()
        {
            InitializeComponent();

            cellPopupControl = new DayCellPopupControl(this);

            oneMonthControl.OwnerMonthsControl = this;
            oneMonthControl.MonthDayClicked += new OneMonthControl.DayClicked(control_MonthDayClicked);
            singleMonths.Add(oneMonthControl);
            CreateAndAdjustMonthsAmount();
        }

        private OneMonthControl CreateNewDefaultOneMonth()
        {
            OneMonthControl control = new OneMonthControl();
            control.Location = oneMonthControl.Location;
            control.Size = oneMonthControl.Size;
            control.OwnerMonthsControl = this;
            control.MonthDayClicked += new OneMonthControl.DayClicked(control_MonthDayClicked);
            return control;
        }

        private void control_MonthDayClicked(object sender, DayCellClickEventArgs e)
        {
            FocusDate = e.NewDate;
            if (e.Button == MouseButtons.Right)
            {
                ShowPopupMenu();
            }
        }

        public void ShowPopupMenu()
        {
            bool isMentruationDay = Program.CurrentWoman.Menstruations.IsMenstruationDay(FocusDate);
            contextMenu.Items["setAsMenstruationDay"].Visible = !isMentruationDay;
            contextMenu.Items["removeMenstruationDay"].Visible = isMentruationDay;

            bool haveNote = Program.CurrentWoman.Notes.ContainsKey(FocusDate);
            contextMenu.Items["addNote"].Visible = !haveNote;
            contextMenu.Items["removeNote"].Visible = haveNote;
            contextMenu.Items["editNote"].Visible = haveNote;

            contextMenu.Show(FocusMonth, FocusMonth.PointToClient(MousePosition));
        }

        private void CreateAndAdjustMonthsAmount()
        { // adjust amount of month calendars in the control according to its size.
            CellPopupControl.Visible = false;

            int monthesX = Size.Width / (oneMonthControl.Width + MonthsMarginX);
            if (monthesX == 0) monthesX = 1;
            int monthesY = Size.Height / (oneMonthControl.Height + MonthsMarginY);
            if (monthesY == 0) monthesY = 1;
            int monthsAmount = monthesX*monthesY; // new amount of visible monthes

            if (VisibleMonthsCount == monthsAmount)
            { // amount of visible months not changed. Do nothing.
                return;
            }

            VisibleMonthsCount = monthsAmount;
            int maxOfTwo = Math.Max(singleMonths.Count, monthsAmount);
            for (int i = 0; i < maxOfTwo; i++)
            {
                if (singleMonths.Count <= i) // if there is not enough controls for now create one more.
                {
                    OneMonthControl control = CreateNewDefaultOneMonth();
                    singleMonths.Add(control);
                    Controls.Add(control);
                }

                singleMonths[i].Visible = (i < monthsAmount); // the control is visible if his number is in visible list
                if (singleMonths[i].Visible)
                {
                    singleMonths[i].Location = new Point(
                        (i % monthesX) * (oneMonthControl.Width) + MonthsMarginX * (i % monthesX + 1),
                        (i / monthesX) * (oneMonthControl.Height) + MonthsMarginY * (i / monthesX + 1));
                    if (oneMonthControl.Date != DateTime.MinValue)
                    {
                        singleMonths[i].Date = oneMonthControl.Date.AddMonths(i);
                    }
                }
            }
        }

        private int _monthsMarginX = 0;
        public int MonthsMarginX
        {
            get { return _monthsMarginX; }
            set { _monthsMarginX = value; }
        }

        private int _monthsMarginY = 0;
        public int MonthsMarginY
        {
            get { return _monthsMarginY; }
            set { _monthsMarginY = value; }
        }

        private void MonthControl_SizeChanged(object sender, EventArgs e)
        {
            SuspendLayout();
            CreateAndAdjustMonthsAmount();

            // That piece of code insures that focus date is still visible and was not hidden by resizing
            DateTime focusDate = FocusDate;
            if (focusDate != DateTime.MinValue && !IsDateVisible(focusDate))
            {
                FocusDay = null;
                FocusDate = focusDate;
            }

            ResumeLayout();
        }

        internal void ScrollMonthes(int p)
        {
            DateTime prevFocusDate = FocusDate;
            FocusDay = null;
            StartMonth = oneMonthControl.Date.AddMonths(p);
            if (IsDateVisible(prevFocusDate))
            {
                FocusDate = prevFocusDate;
            }
            else
            {
                FocusDate = prevFocusDate.AddMonths(p);
            }
        }

        public OneMonthControl GetOneMonthByDate(DateTime date)
        {
            int i = oneMonthControl.Date.Month - date.Month;
            if (i >= 0 && i < VisibleMonthsCount)
            {
                return singleMonths[i];
            }
            return null;
        }

        public DayCellControl GetCellByDate(DateTime date)
        {
            OneMonthControl month = GetOneMonthByDate(date);
            if (month != null)
            {
                return month.GetCellByDate(date);
            }
            return null;
        }

        private void setAsMenstruationDay_Click(object sender, EventArgs e)
        {
            if (Program.CurrentWoman.AddMenstruationDay(FocusDate))
            {
                Redraw();
            }
        }

        private void removeMenstruationDay_Click(object sender, EventArgs e)
        {
            if (Program.CurrentWoman.RemoveMenstruationDay(FocusDate))
            {
                Redraw();
            }
        }

        private void addNote_Click(object sender, EventArgs e)
        {
            NoteEditForm form = new NoteEditForm();
            if (form.ShowDialog() == DialogResult.OK && Program.CurrentWoman.AddNote(FocusDate, form.NoteText))
            {
                RedrawFocusDay();
            }
        }

        private void removeNote_Click(object sender, EventArgs e)
        {
            if (Program.CurrentWoman.RemoveNote(FocusDate))
            {
                RedrawFocusDay();
            }
        }

        public void Redraw()
        {
            Invalidate(true);
            Update();
        }

        public void RedrawFocusDay()
        {
            ((MainForm)ParentForm).UpdateDayInformation(FocusDate);
            FocusDay.Invalidate();
            FocusDay.Update();
        }

        private void editNote_Click(object sender, EventArgs e)
        {
            NoteEditForm form = new NoteEditForm(Program.CurrentWoman.Notes[FocusDate]);
            if (form.ShowDialog() == DialogResult.OK && Program.CurrentWoman.AddNote(FocusDate, form.NoteText))
            {
                RedrawFocusDay();
            }
        }

        private void MonthsControl_MouseEnter(object sender, EventArgs e)
        {
            CellPopupControl.Visible = false;
        }

        private void MonthsControl_MouseLeave(object sender, EventArgs e)
        {
            CellPopupControl.Visible = false;
        }
    }

    public class FocusDateChangedEventArgs : EventArgs
    {
        public DateTime OldDate;
        public DateTime NewDate;

        public FocusDateChangedEventArgs(DateTime oldDate, DateTime newDate)
        {
            OldDate = oldDate;
            NewDate = newDate;
        }
    }
}
