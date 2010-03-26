using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WomenCalendar
{
    public class OnceAPeriod : Schedule
    {
        public int TakeDays { get; set; }
        public int PauseDays { get; set; }

        public override string DisplayTypeName { get { return "Once a given period"; } }

        public OnceAPeriod() : base()
        {
        }

        public OnceAPeriod(string text) : base(text)
        {
        }

        public override Schedule CreateDefault(DateTime day)
        {
            return new OnceAPeriod()
            {
                Start = day,
                End = day.AddMonths(1).AddDays(-1),
                TakeDays = 1,
                PauseDays = 0,
            };
        }

        public override bool IsAlarmAtDay(DateTime day)
        {
            if (day < Start || day > End) return false;

            int daysTotal = (End - Start).Days;
            int daysInOneCycle = daysTotal % (TakeDays + PauseDays);
            int dayNumber = (day - Start).Days;
            int dayNumberInCyle = dayNumber % (TakeDays + PauseDays);

            return dayNumberInCyle < TakeDays;
        }

        public override string ToString()
        {
            return string.Format("Do {0} days, pause {1} days. From {2} to {3}", TakeDays, PauseDays, Start.ToShortDateString(), End.ToShortDateString());
        }
    }

    public abstract class Schedule
    {
        public Schedule()
        {
        }

        public Schedule(string text)
        {
            this.Text = text;
            this.GUID = System.Guid.NewGuid();
        }

        public Guid GUID { get; private set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual string Text { get; set; }
        public abstract string DisplayTypeName { get; }

        public abstract Schedule CreateDefault(DateTime day);
        public abstract bool IsAlarmAtDay(DateTime day);


        public override bool Equals(object obj)
        {
            return this.GUID == (obj as Schedule).GUID;
        }

        public override int GetHashCode()
        {
            return this.GUID.GetHashCode();
        }
    }
}
