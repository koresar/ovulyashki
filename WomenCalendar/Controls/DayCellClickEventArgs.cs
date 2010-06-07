using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    /// <summary>
    /// Mouse event args but with date clicked property.
    /// </summary>
    public class DayCellClickEventArgs : MouseEventArgs
    {
        /// <summary>
        /// Create event args.
        /// </summary>
        /// <param name="e">Mouse event args received from mouse click.</param>
        /// <param name="newDate">The date which was clicked.</param>
        public DayCellClickEventArgs(MouseEventArgs e, DateTime newDate)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            this.NewDate = newDate;
        }

        /// <summary>
        /// The date clicked.
        /// </summary>
        public DateTime NewDate { get; private set; }
    }
}
