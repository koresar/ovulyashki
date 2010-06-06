using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace WomenCalendar.Controls
{
    public partial class SchedulesListControl : UserControl
    {
        public event Action<List<Schedule>> SelectedScheduleChanged;
        private Timer changeDelayTimer = null;

        public SchedulesListControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Make sure to properly dispose of the timer
        /// </summary>
        ~SchedulesListControl()
        {
            if (changeDelayTimer != null)
            {
                changeDelayTimer.Tick -= ChangeDelayTimerTick;
                changeDelayTimer.Dispose();
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (changeDelayTimer == null)
            {
                changeDelayTimer = new Timer();
                changeDelayTimer.Tick += ChangeDelayTimerTick;
                changeDelayTimer.Interval = 40;
            }
            changeDelayTimer.Enabled = false;
            changeDelayTimer.Enabled = true;
        }

        private void ChangeDelayTimerTick(object sender, EventArgs e)
        {
            changeDelayTimer.Enabled = false;
            changeDelayTimer.Dispose();
            changeDelayTimer = null;

            OnSelectedScheduleChanged(GetAllSchedules());
        }

        public List<Schedule> GetAllSchedules()
        {
            return listView.Items.
                Cast<ListViewItem>().
                Select(item => item.Tag as Schedule).
                ToList();
        }

        public void SetSchedules(List<Schedule> schedules)
        {
            schedules.ForEach(s => AddSchedule(s));
        }

        public void AddSchedule(Schedule schedule)
        {
            ListViewItem listViewItem1 = new ListViewItem(new string[] {
                schedule.Text,
                schedule.Start.ToShortDateString(),
                schedule.DisplayTypeName,
                schedule.ToString()}, -1) { Tag = schedule };
            listView.Items.Add(listViewItem1);
            this.columnName.Width = -1;
            this.columnStart.Width = -1;
            this.columnType.Width = -1;
            this.columnParameters.Width = -1;
        }

        private void OnSelectedScheduleChanged(List<Schedule> schedule)
        {
            if (SelectedScheduleChanged != null)
            {
                SelectedScheduleChanged(schedule);
            }
        }
    }
}
