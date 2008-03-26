using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class HadSexCollection : SerializableEventsCollection<bool>
    {
        public HadSexCollection()
            : base("HadSex")
        {
        }
    }
}
