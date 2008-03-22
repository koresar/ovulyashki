using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class BBTCollection : SerializableEventsCollection<double>
    {
        public BBTCollection()
            : base("BBT")
        {
        }
    }
}
