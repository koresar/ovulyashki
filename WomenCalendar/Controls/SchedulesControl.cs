using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar.Controls
{
    public partial class SchedulesControl : UserControl
    {
        public Action<Schedule> SelectedScheduleChanged;

        public DateTime DefaultStartDate { get; set; }

        public SchedulesControl()
        {
            InitializeComponent();
        }

        public void UpdateList()
        {
            gridSchedules.Rows.Clear();
            if (Program.CurrentWoman.Schedules.ContainsKey(DefaultStartDate))
            {
                foreach (var schedule in Program.CurrentWoman.Schedules[DefaultStartDate])
                {
                    gridSchedules.Rows[gridSchedules.Rows.Add(schedule.GetObjectsForGrid())].Tag = schedule;
                }
            }
        }

        private void gridSchedules_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectedScheduleChanged != null && gridSchedules.SelectedRows.Count > 0)
            {
                SelectedScheduleChanged(gridSchedules.SelectedRows[0].Tag as Schedule);
            }
        }

        public void Select(Schedule s)
        {
            foreach (DataGridViewRow row in gridSchedules.Rows)
            {
                if ((row.Tag as Schedule) == s)
                {
                    row.Selected = true;
                    return;
                }
            }            
        }
    }
}
