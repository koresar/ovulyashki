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
    }
}
