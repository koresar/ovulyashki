using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class HealthCollection : SerializableEventsCollection<int>
    {
        public const int DefaultHealthValue = 5;

        public HealthCollection()
            : base("Health")
        {
        }

        public new int this[DateTime date]
        {
            get
            {
                int ret;
                return TryGetValue(date, out ret) ? ret : DefaultHealthValue;
            }
            set
            {
                if (value == DefaultHealthValue) Remove(date);
                else base[date] = value;
            }
        }

        public double[] GetHealthesSince(DateTime date, int count)
        {
            if (count <= 0) return new double[0];
            double[] ret = new double[count];
            DateTime d = date.Date;
            for (int i = 0; i < count; i++, d = d.AddDays(1))
            {
                ret[i] = this[d];
            }
            return ret;
        }
    }
}
