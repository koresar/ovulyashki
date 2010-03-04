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
            this.columnName.Width = -1;
            this.columnStart.Width = -1;
            this.columnType.Width = -1;
            this.columnParameters.Width = -1;
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedScheduleChanged(
                listView.SelectedItems.
                Cast<ListViewItem>().
                Select(item => item.Tag as Schedule).
                ToList());
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
