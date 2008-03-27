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

        public new bool this[DateTime date]
        {
            get
            {
                bool ret;
                return TryGetValue(date, out ret) ? ret : false;
            }
            set
            {
                if (value == false) Remove(date);
                else base[date] = value;
            }
        }
    }
}
