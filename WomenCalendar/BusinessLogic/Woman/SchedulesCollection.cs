using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WomenCalendar
{
    public class SchedulesCollection : List<Schedule>
    {
        public List<Schedule> GetFiredSchedulesForDay(DateTime fireDate)
        {
            return this.Where(s => s.IsAlarmAtDay(fireDate)).ToList();
        }

        public void UpdateData(List<Schedule> schedules)
        {
            foreach (var s in schedules)
            {
                if (!this.Contains(s))
                {
                    Add(s);
                }
            }
        }
    }
}
