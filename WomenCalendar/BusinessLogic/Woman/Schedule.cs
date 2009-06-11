using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class Schedule
    {
        public Schedule()
        {
            TakeDays = 1;
        }

        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public int TakeDays { get; set; }
        public int PauseDays { get; set; }

        public object[] GetObjectsForGrid()
        {
            object[] ret = new object[5];
            ret[0] = Name;
            ret[1] = Start.ToLongDateString();
            ret[2] = Stop.ToLongDateString();
            ret[3] = TakeDays;
            ret[4] = PauseDays;
            return ret;
        }

        public Schedule Clone()
        {
            return new Schedule()
            {
                Name = this.Name,
                Start = this.Start,
                Stop = this.Stop,
                TakeDays = this.TakeDays,
                PauseDays = this.PauseDays
            };
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Start.GetHashCode() ^ Stop.GetHashCode() ^ 
                TakeDays ^ PauseDays;
        }

        public override bool Equals(object obj)
        {
            var s = obj as Schedule;
            return s != null && s.Name == this.Name && s.Start == this.Start && 
                s.Stop == this.Stop && s.TakeDays == this.TakeDays && s.PauseDays == this.PauseDays;
        }
    }
}
