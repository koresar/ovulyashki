using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public class DayCellClickEventArgs : MouseEventArgs
    {
        public DateTime NewDate;

        public DayCellClickEventArgs(MouseEventArgs e, DateTime newDate)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            NewDate = newDate;
        }
    }
}
