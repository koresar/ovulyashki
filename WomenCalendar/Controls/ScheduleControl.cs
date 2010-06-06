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
        protected bool subscribed;
        protected Schedule currentSchedule;

        public ScheduleControl()
        {
            InitializeComponent();

            SubscribeEvents();
        }

        protected virtual void SubscribeEvents()
        {
            if (!subscribed)
            {
                this.dateStart.ValueChanged += new System.EventHandler(this.controls_ValueChanged);
                this.dateEnd.ValueChanged += new System.EventHandler(this.controls_ValueChanged);
                this.numTake.ValueChanged += new System.EventHandler(this.controls_ValueChanged);
                this.numPause.ValueChanged += new System.EventHandler(this.controls_ValueChanged);
                subscribed = true;
            }
        }

        protected virtual void UnSubscribeEvents()
        {
            if (subscribed)
            {
                this.dateStart.ValueChanged -= new System.EventHandler(this.controls_ValueChanged);
                this.dateEnd.ValueChanged -= new System.EventHandler(this.controls_ValueChanged);
                this.numTake.ValueChanged -= new System.EventHandler(this.controls_ValueChanged);
                this.numPause.ValueChanged -= new System.EventHandler(this.controls_ValueChanged);
                subscribed = false;
            }
        }

        public virtual void ApplyDefaultData(string scheduleText, DateTime defaultDate)
        {
            OnceAPeriod defaultData = new OnceAPeriod(scheduleText).CreateDefault(defaultDate) as OnceAPeriod;
            defaultData.Text = scheduleText;
            ApplyData(defaultData);
        }

        public virtual void ApplyData(Schedule schedule)
        {
            UnSubscribeEvents();

            currentSchedule = schedule;
            OnceAPeriod data = schedule as OnceAPeriod;
            dateStart.Value = data.Start;
            dateEnd.Value = data.End;
            numTake.Value = data.TakeDays;
            numPause.Value = data.PauseDays;

            SubscribeEvents();
            OnChanged();
        }

        public virtual Schedule GetSchedule()
        {
            OnceAPeriod data = currentSchedule as OnceAPeriod;
            data.Start = dateStart.Value;
            data.End = dateEnd.Value;
            data.TakeDays = (int)numTake.Value;
            data.PauseDays = (int)numPause.Value;
            return data;
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
    }
}
