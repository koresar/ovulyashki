using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The changing focus data.
    /// </summary>
    public class FocusDateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Create the class instance.
        /// </summary>
        /// <param name="oldDate">Previouse focus day.</param>
        /// <param name="newDate">Newly clicked focus day.</param>
        public FocusDateChangedEventArgs(DateTime oldDate, DateTime newDate)
        {
            this.OldDate = oldDate;
            this.NewDate = newDate;
        }

        /// <summary>
        /// Previous focused day.
        /// </summary>
        public DateTime OldDate { get; set; }

        /// <summary>
        /// Newly focused dat.
        /// </summary>
        public DateTime NewDate { get; set; }
    }
}
