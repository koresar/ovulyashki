using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class SchedulesEditForm : ModalBaseForm
    {
        private DateTime date;

        public SchedulesEditForm()
        {
            InitializeComponent();
        }

        public SchedulesEditForm(DateTime date) : this()
        {
            this.date = date;
            scheduleControl1.DefaultStartDate = date;
            schedulesControl1.DefaultStartDate = date;
            Text = "Расписание на " + date.ToLongDateString();

            schedulesControl1.SelectedScheduleChanged = 
                (s => scheduleControl1.EditingSchedule = s);
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            var s = scheduleControl1.EditingSchedule.Clone();
            Program.CurrentWoman.Schedules.Add(date, s);
            schedulesControl1.UpdateList();
            schedulesControl1.Select(s);
        }

        private void btnDeleteSchedule_Click(object sender, EventArgs e)
        {
            Program.CurrentWoman.Schedules.Remove(date, scheduleControl1.EditingSchedule.Clone());
            schedulesControl1.UpdateList();
        }

        private void ScxhedulesEditForm_Load(object sender, EventArgs e)
        {
            if (Program.CurrentWoman.Schedules.ContainsKey(date))
            {
                scheduleControl1.EditingSchedule = Program.CurrentWoman.Schedules[date][0];
            }
        }
    }
}
