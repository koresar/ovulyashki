using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WomenCalendar.Properties;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace WomenCalendar
{
    public partial class MonthsControl : UserControl
    {
        private OneMonthControl lastDroppedMenuMonth;
        private Dictionary<string, Dictionary<int, string>> calendars = new Dictionary<string, Dictionary<int, string>>();

        public List<OneMonthControl> singleMonths = new List<OneMonthControl>();
        public int VisibleMonthsCount = 1;

        public delegate void FocusDateChangedDelegate(object sender, FocusDateChangedEventArgs e);
        public event FocusDateChangedDelegate FocusDateChanged;

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
                    StartMonth = _value.AddMonths(-1 * (VisibleMonthsCount - 1) / 2);
                    oneMonth = singleMonths[
                        (_value.Year - singleMonths[0].Date.Year) * 12 + _value.Month - singleMonths[0].Date.Month];
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


        public ContextMenuStrip MonthMenu
        {
            get { return monthMenu; }
        }

        public MonthsControl()
        {
            InitializeComponent();

            cellPopupControl = new DayCellPopupControl(this);

            oneMonthControl.OwnerMonthsControl = this;
            oneMonthControl.MonthDayClicked += new OneMonthControl.DayClicked(control_MonthDayClicked);
            singleMonths.Add(oneMonthControl);
            CreateAndAdjustMonthsAmount();
            InitializeContextPregnancySubmenu();
        }

        private void InitializeContextPregnancySubmenu()
        {
            var obj = Resources.ResourceManager.GetObject("calendar");
            var doc = new XmlDocument();
            doc.Load(new MemoryStream(Encoding.GetEncoding(1251).GetBytes(obj.ToString())));
            foreach (XmlNode calendar in doc.ChildNodes[1])
            {
                if (calendar.Attributes == null) continue;
                var weeks = new Dictionary<int, string>();
                calendars.Add(calendar.Attributes["Name"].Value, weeks);
                foreach (XmlNode week in calendar.ChildNodes)
	            {
                    if (calendar.Attributes == null) continue;
                    weeks.Add(int.Parse(week.Attributes["Number"].Value), week.InnerXml);
                }
            }

            List<ToolStripMenuItem> subitems = new List<ToolStripMenuItem>();
            foreach (var calendarName in calendars.Keys)
            {
                var item = new ToolStripMenuItem(calendarName);
                item.Click += new EventHandler(calendarItem_Click);
                item.Tag = calendars[calendarName];
                subitems.Add(item);
            }
            this.calendarMenu.DropDownItems.AddRange(subitems.ToArray());
        }

        void calendarItem_Click(object sender, EventArgs e)
        {
            var dic = ((sender as ToolStripDropDownItem).Tag as Dictionary<int, string>);
            var week = Program.CurrentWoman.Conceptions.GetPregnancyWeekNumber(FocusDate);
            if (dic.ContainsKey(week)) Process.Start(dic[week]);
        }

        public bool IsDateVisible(DateTime date)
        {
            return singleMonths[0].Date < date.Date &&
                   date.Date < singleMonths[0].Date.AddMonths(VisibleMonthsCount); //singleMonths[VisibleMonthsCount - 1].Date.AddMonths(1);
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

        public void ShowDayContextMenu()
        {
            bool isPregnancyDay = Program.CurrentWoman.Conceptions.IsPregnancyDay(FocusDate);
            dayContextMenu.Items["setAsConceptionDay"].Visible = !isPregnancyDay;
            dayContextMenu.Items["removeConceptionDay"].Visible = isPregnancyDay;
            dayContextMenu.Items["setLastPregnancyDay"].Visible = isPregnancyDay;
            dayContextMenu.Items["showBirthDate"].Visible = isPregnancyDay;
            dayContextMenu.Items["calendarMenu"].Visible = isPregnancyDay;
            if (isPregnancyDay)
            {
                dayContextMenu.Items["calendarMenu"].Text = string.Format(
                    "Подсказка беременным на {0}-й неделе с сайта...",
                    Program.CurrentWoman.Conceptions.GetPregnancyWeekNumber(FocusDate));
            }

            bool isMentruationDay = Program.CurrentWoman.Menstruations.IsMenstruationDay(FocusDate);
            dayContextMenu.Items["setAsMenstruationDay"].Visible = !isMentruationDay && !isPregnancyDay;
            dayContextMenu.Items["removeMenstruationDay"].Visible = isMentruationDay && !isPregnancyDay;

            dayContextMenu.Show(FocusMonth, FocusMonth.PointToClient(MousePosition));
        }

        public void CreateAndAdjustMonthsAmount()
        {
            CreateAndAdjustMonthsAmount(false);
        }
        
        public void CreateAndAdjustMonthsAmount(bool force)
        { // adjust amount of month calendars in the control according to its size.
            CellPopupControl.Visible = false;

            int monthesX = Size.Width / (oneMonthControl.Width + MonthsMarginX);
            if (monthesX == 0) monthesX = 1;
            int monthesY = Size.Height / (oneMonthControl.Height + MonthsMarginY);
            if (monthesY == 0) monthesY = 1;
            int monthsAmount = monthesX*monthesY; // new amount of visible monthes

            if (!force && VisibleMonthsCount == monthsAmount)
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

        public void Redraw()
        {
            CellPopupControl.Visible = false;
            Invalidate(true);
            Update();
        }

        public void DropMonthMenu(Point screenLocation, OneMonthControl control)
        {
            lastDroppedMenuMonth = control;
            CellPopupControl.Visible = false;
            MonthMenu.Show(screenLocation);
        }

        public void RedrawFocusDay()
        {
            ((MainForm)ParentForm).UpdateDayInformation(FocusDate);
            FocusDay.Invalidate();
            FocusDay.Update();
        }

        private void control_MonthDayClicked(object sender, DayCellClickEventArgs e)
        {
            FocusDate = e.NewDate;
            if (e.Button == MouseButtons.Right)
            {
                ShowDayContextMenu();
            }
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

        private void setAsMenstruationDay_Click(object sender, EventArgs e)
        {
            if (Program.CurrentWoman.AddMenstruationDay(FocusDate))
            {
                ((MainForm)ParentForm).UpdateWomanInformation();
                ((MainForm)ParentForm).UpdateDayInformation(FocusDate);
                Redraw();
                new DayEditForm(FocusDay, DayEditFocus.Length).ShowDialog(this);
            }
        }

        private void removeMenstruationDay_Click(object sender, EventArgs e)
        {
            if (Program.CurrentWoman.RemoveMenstruationDay(FocusDate))
            {
                ((MainForm)ParentForm).UpdateWomanInformation();
                ((MainForm)ParentForm).UpdateDayInformation(FocusDate);
                Redraw();
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

        private void ToolStripBBTGraph_Click(object sender, EventArgs e)
        {
            DateTime d = lastDroppedMenuMonth.Date;
            new BBTForm(d, DateTime.DaysInMonth(d.Year, d.Month)).Show();
        }

        private void ToolStripHealthesGraph_Click(object sender, EventArgs e)
        {
            DateTime d = lastDroppedMenuMonth.Date;
            new HealthForm(d, DateTime.DaysInMonth(d.Year, d.Month)).Show();
        }

        private void setAsConceptionDay_Click(object sender, EventArgs e)
        {
            if (Program.CurrentWoman.AddConceptionDay(FocusDate))
            {
                ((MainForm)ParentForm).UpdateDayInformation(FocusDate);
                Redraw();
            }
        }

        private void removeConceptionDay_Click(object sender, EventArgs e)
        {
            if (Program.CurrentWoman.RemovePregnancy(FocusDate))
            {
                ((MainForm)ParentForm).UpdateDayInformation(FocusDate);
                Redraw();
            }
        }

        private void editDay_Click(object sender, EventArgs e)
        {
            CellPopupControl.Visible = false;
            new DayEditForm(FocusDay).ShowDialog(this);
            RedrawFocusDay();
        }

        private void setLastPregnancyDay_Click(object sender, EventArgs e)
        {
            var period = Program.CurrentWoman.Conceptions.GetConceptionByDate(FocusDate);
            if (period != null)
            {
                if (MessageBox.Show("Если сейчас нажмёшь 'Да', то потом нельзя будет удлиннить срок беременности. Уверена, что хочешь укоротить срок этой беременности?", 
                    "Ты уверена?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    period.LastDay = FocusDate;
                    Redraw();
                }
            }
        }

        private void showBirthDate_Click(object sender, EventArgs e)
        {
            var period = Program.CurrentWoman.Conceptions.GetConceptionByDate(FocusDate);
            if (period != null)
            {
                FocusDate = period.LastDay;
            }
        }
    }
}
