using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The place on the day edit window. This enum used to identify focus position.
    /// </summary>
    public enum DayEditFocus
    {
        /// <summary>
        /// The note edit text box.
        /// </summary>
        Note, 

        /// <summary>
        /// The Basal Body Temperature edit box.
        /// </summary>
        BBT, 
        
        /// <summary>
        /// The length of blooding numeric edit box.
        /// </summary>
        Length, 

        /// <summary>
        /// The schdules.
        /// </summary>
        Schedules
    };
}
