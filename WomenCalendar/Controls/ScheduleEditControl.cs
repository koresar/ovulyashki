using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class ScheduleEditControl : UserControl
    {
        public DateTime DefaultStartDate { get; set; }

        private Schedule editingSchedule;
        [Browsable(false)]
        public Schedule EditingSchedule
        {
            get
            {
                if (editingSchedule == null)
                {
                    editingSchedule = new Schedule();
                    editingSchedule.Start = DefaultStartDate;
                    editingSchedule.Stop = dateTill.Value;
                    editingSchedule.TakeDays = (int)numTake.Value;
                    editingSchedule.PauseDays = (int)numPause.Value;
                    editingSchedule.Name = txtName.Text;
                    UpdateControlsWithData();
                }
                return editingSchedule;
            }
            set
            {
                editingSchedule = value;
                UpdateControlsWithData();
            }
        }

        private void UpdateControlsWithData()
        {
            txtName.Text = EditingSchedule.Name;
            numTake.Value = EditingSchedule.TakeDays;
            numPause.Value = EditingSchedule.PauseDays;
            dateTill.Value = EditingSchedule.Stop != DateTime.MinValue ? 
                EditingSchedule.Stop : DateTime.Today;
        }

        public ScheduleEditControl()
        {
            InitializeComponent();
        }

        private void numTake_ValueChanged(object sender, EventArgs e)
        {
            lblDays1.Text = TEXT.GetDaysString((int)numTake.Value);
            EditingSchedule.TakeDays = (int)numTake.Value;
        }

        private void numPause_ValueChanged(object sender, EventArgs e)
        {
            lblDays2.Text = TEXT.GetDaysString((int)numPause.Value);
            EditingSchedule.PauseDays = (int)numPause.Value;
        }

        private void ScheduleEditControl_Load(object sender, EventArgs e)
        {
            dateTill.MinDate = DefaultStartDate;
        }

        private void dateTill_ValueChanged(object sender, EventArgs e)
        {
            EditingSchedule.Stop = dateTill.Value;
        }
    }
}
