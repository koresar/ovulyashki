using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class FocusDateChangedEventArgs : EventArgs
    {
        public DateTime OldDate;
        public DateTime NewDate;

        public FocusDateChangedEventArgs(DateTime oldDate, DateTime newDate)
        {
            OldDate = oldDate;
            NewDate = newDate;
        }
    }
}
