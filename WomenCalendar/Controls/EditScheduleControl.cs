﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace WomenCalendar.Controls
{
    public partial class EditScheduleControl : UserControl
    {
        public DateTime initialDate;
        public DateTime InitialDate
        {
            get { return initialDate; }
            set
            {
                initialDate = value;
                SetupNewDate();
            }
        }

        private ScheduleControl currentScheduleControl;

        public EditScheduleControl()
        {
            InitializeComponent();
            TieEvents();
            InitializeScheduleTypes();
        }

        private void TieEvents()
        {
            schedulesListControl1.SelectedScheduleChanged += coloredSchedulerCalendarControl1.ApplySchedules;
        }

        private void InitializeScheduleTypes()
        {
            var scheduleType = typeof(Schedule);
            var items =
                scheduleType.Assembly.GetTypes().
                Where(t => t.IsSubclassOf(scheduleType)).
                Select(t => (Activator.CreateInstance(t) as Schedule).DisplayTypeName).
                ToArray();
            cmbScheduleType.Items.AddRange(items);
            cmbScheduleType.SelectedIndex = 0;
        }

        private void SetupNewDate()
        {
            coloredSchedulerCalendarControl1.StartMonth = initialDate;
        }

        private void btnApplySchedule_Click(object sender, EventArgs e)
        {
            ApplyNewShedule();

            txtScheduleText.Text = string.Empty;
            RemoveScheduleEditControl();
            EnableEdition(false);
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            AddScheduleEditControl();
            EnableEdition(true);
        }

        private void txtScheduleText_TextChanged(object sender, EventArgs e)
        {
            btnAddSchedule.Enabled = txtScheduleText.Text.Length > 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RemoveScheduleEditControl();
            EnableEdition(false);
        }

        private void AddScheduleEditControl()
        {
            currentScheduleControl = new WomenCalendar.Controls.ScheduleControl();
            currentScheduleControl.Visible = false;
            currentScheduleControl.Location = new System.Drawing.Point(0, 175);
            currentScheduleControl.Size = new System.Drawing.Size(353, 62);
            currentScheduleControl.TabIndex = 10018;
            currentScheduleControl.ScheduleText = txtScheduleText.Text;
            currentScheduleControl.Changed += coloredSchedulerCalendarControl1.ApplySchedule;
            this.Controls.Add(currentScheduleControl);
            currentScheduleControl.BringToFront();
            currentScheduleControl.Visible = true;
        }

        private void RemoveScheduleEditControl()
        {
            currentScheduleControl.Visible = false;
            this.Controls.Remove(currentScheduleControl);
            currentScheduleControl.Dispose();
            currentScheduleControl = null;
        }

        private void ApplyNewShedule()
        {
            var schedule = currentScheduleControl.GetSchedule();
            coloredSchedulerCalendarControl1.ApplySchedule(schedule);
            schedulesListControl1.AddSchedule(schedule);
        }

        private void EnableEdition(bool enable)
        {
            btnAddSchedule.Visible = txtScheduleText.Visible = lblScheduleText.Visible = !enable;
            btnApplySchedule.Visible = btnCancel.Visible = enable;
        }
    }
}
