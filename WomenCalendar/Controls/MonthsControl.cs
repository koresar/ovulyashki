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
    public partial class MonthsControl : UserControl, ITranslatable
    {
        private OneMonthControl lastDroppedMenuMonth;
        private OneMonthControl firstMonthControl;
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
            get { return firstMonthControl.Date; }
            set
            {
                firstMonthControl.Date = value.Date;
                Invalidate();
                for (int i = 0; i < singleMonths.Count; i++)
                {
                    singleMonths[i].Date = value.AddMonths(i);
                    singleMonths[i].Invalidate();
                }
                CreateAndAdjustMonthsAmount();
            }
        }

        readonly Point MonthesMargin = new Point(10, 10);
        readonly Size OneMonthSize = new Size(230, 230);

        public ContextMenuStrip MonthMenu
        {
            get { return monthMenu; }
        }

        #region ITranslatable interface impementation

        public void ReReadTranslations()
        {
            singleMonths.ForEach(m => m.ReReadTranslations());
            this.setAsMenstruationDay.Text = TEXT.Get["Set_menses_start"];
            this.removeMenstruationDay.Text = TEXT.Get["Cancel_menses"];
            this.setLastPregnancyDay.Text = TEXT.Get["Set_last_pregn_day"];
            this.setAsConceptionDay.Text = TEXT.Get["Set_pregnancy"];
            this.removeConceptionDay.Text = TEXT.Get["Cancel_pregnancy"];
            this.showBirthDate.Text = TEXT.Get["Show_childbirth_day"];
            this.ediDayToolStripMenuItem.Text = TEXT.Get["Edit_day"];
            this.ToolStripBBTGraph.Text = TEXT.Get["Show_BBT_graph"];
            this.ToolStripHealthGraph.Text = TEXT.Get["Show_wellbeing_graph"];
            this.ToolStripCycleLengthGraph.Text = TEXT.Get["Show_cycle_length_graph"];
        }

        #endregion

        public MonthsControl()
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();

            cellPopupControl = new DayCellPopupControl(this);

            firstMonthControl = CreateNewDefaultOneMonth();
            firstMonthControl.OwnerMonthsControl = this;
            firstMonthControl.Visible = true;
            firstMonthControl.MonthDayClicked += new OneMonthControl.DayClicked(control_MonthDayClicked);
            singleMonths.Add(firstMonthControl);
            Controls.Add(firstMonthControl);

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
            control.Location = new Point(MonthesMargin.X, MonthesMargin.Y);// oneMonthControl.Location;
            control.Size = OneMonthSize;// oneMonthControl.Size;
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
                dayContextMenu.Items["calendarMenu"].Text = TEXT.Get.Format("Pregn_week_help",
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
            if (CellPopupControl == null || singleMonths == null || Controls == null || singleMonths.Count == 0)
                return;

            CellPopupControl.Visible = false;

            int monthesX = Size.Width / (OneMonthSize.Width + MonthesMargin.X);
            if (monthesX == 0) monthesX = 1;
            int monthesY = Size.Height / (OneMonthSize.Height + MonthesMargin.Y);
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
                        (i % monthesX) * (OneMonthSize.Width) + MonthesMargin.X * (i % monthesX + 1),
                        (i / monthesX) * (OneMonthSize.Height) + MonthesMargin.Y * (i / monthesX + 1));
                    if (firstMonthControl.Date != DateTime.MinValue)
                    {
                        singleMonths[i].Date = firstMonthControl.Date.AddMonths(i);
                    }
                }
            }
        }

        internal void ScrollMonthes(int p)
        {
            DateTime prevFocusDate = FocusDate;
            FocusDay = null;
            StartMonth = firstMonthControl.Date.AddMonths(p);
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
            int i = firstMonthControl.Date.Month - date.Month;
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
            CellPopupControl.Forbidden = true;

            // These lines are fix for Mono. The only way I found to readraw all calendar.
            this.Visible = false;
            this.Visible = true;

            // These two lines work under MS .Net only.
            //Invalidate(true);
            //Update();
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
                new DayEditForm(FocusDay.Date, DayEditFocus.Length).ShowDialog(this);
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
            new BBTForm(d).Show();
        }

        private void ToolStripHealthesGraph_Click(object sender, EventArgs e)
        {
            DateTime d = lastDroppedMenuMonth.Date;
            new HealthForm(d).Show();
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
            new DayEditForm(FocusDay.Date).ShowDialog(this);
        }

        private void setLastPregnancyDay_Click(object sender, EventArgs e)
        {
            var period = Program.CurrentWoman.Conceptions.GetConceptionByDate(FocusDate);
            if (period != null)
            {
                if (MsgBox.YesNo(TEXT.Get["Shorten_pregn_question"], 
                    TEXT.Get["Are_you_sure"]))
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

        private void ToolStripCycleLengthGraph_Click(object sender, EventArgs e)
        {
            new CycleLengthForm().Show();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            CellPopupControl.Forbidden = false;
        }
    }
}
