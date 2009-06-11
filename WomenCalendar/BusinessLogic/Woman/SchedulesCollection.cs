using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    public class SchedulesCollection : SerializableEventsCollection<List<Schedule>>
    {
        public void Add(DateTime day, Schedule schedule)
        {
            if (ContainsKey(day))
            {
                if (!this[day].Exists(s => s.Equals(schedule))) this[day].Add(schedule);
            }
            else
            {
                this[day] = new List<Schedule>() { schedule };
            }
        }

        public void Remove(DateTime day, Schedule schedule)
        {
            if (ContainsKey(day)) this[day].Remove(schedule);
        }

        public SchedulesCollection()
            : base("Schedules")
        {

        }
    }
}
