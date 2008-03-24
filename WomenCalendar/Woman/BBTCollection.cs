using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class BBTCollection : SerializableEventsCollection<double>
    {
        public const double MaxmimalBBT = 43.0;
        public const double MinimalBBT = 35.0;

        public BBTCollection()
            : base("BBT")
        {
        }

        public static bool IsBBTInCorrectRange(double bbt)
        {
            return bbt >= MinimalBBT && bbt <= MaxmimalBBT;
        }

        public double GetBBT(DateTime date)
        {
            return ContainsKey(date) ? this[date] : 0;
        }

        public double[] GetTemperaturesSince(DateTime date, int count)
        {
            double[] ret = new double[count];
            DateTime d = date.Date;
            for (int i = 0; i < count; i++, d = d.AddDays(1))
            {
                ret[i] = ContainsKey(d) ? this[d] : 0;
            }
            return ret;
        }
    }
}
