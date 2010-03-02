using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar.Controls
{
    public partial class ScheduleControl : UserControl
    {
        public event Action<Schedule> Changed;

        public string ScheduleText { get; set; }

        public ScheduleControl()
        {
            InitializeComponent();

            ApplyDefaultData();
        }

        protected virtual void ApplyDefaultData()
        {
            OnceAPeriod defaultData = new OnceAPeriod().CreateDefault(DateTime.Today) as OnceAPeriod;
            dateStart.Value = defaultData.Start;
            dateEnd.Value = defaultData.End;
            numTake.Value = defaultData.TakeDays;
            numPause.Value = defaultData.PauseDays;
        }

        public virtual Schedule GetSchedule()
        {
            return new OnceAPeriod(ScheduleText)
            {
                Start = dateStart.Value,
                End = dateEnd.Value,
                TakeDays = (int)numTake.Value,
                PauseDays = (int)numPause.Value,
            };
        }

        private void controls_ValueChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void OnChanged()
        {
            if (Changed != null)
            {
                Changed(GetSchedule());
            }
        }

        private void ScheduleControl_Load(object sender, EventArgs e)
        {
            OnChanged();
        }
    }
}
