using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar.Controls
{
    public partial class SchedulesListControl : UserControl
    {
        public event Action<Schedule> SelectedScheduleChanged;

        public SchedulesListControl()
        {
            InitializeComponent();
        }

        public void AddSchedule(Schedule schedule)
        {
            ListViewItem listViewItem1 = new ListViewItem(new string[] {
                schedule.Text,
                schedule.Start.ToShortDateString(),
                schedule.DisplayTypeName,
                schedule.ToString()}, -1) { Tag = schedule };
            listView.Items.Add(listViewItem1);
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 1)
            {
                OnSelectedScheduleChanged(listView.SelectedItems[0].Tag as Schedule);
            }
        }

        private void OnSelectedScheduleChanged(Schedule schedule)
        {
            if (SelectedScheduleChanged != null)
            {
                SelectedScheduleChanged(schedule);
            }
        }
    }
}
