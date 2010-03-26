using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The serializable list of cervical fluids (CF).
    /// </summary>
    public class CFCollection : SerializableEventsCollection<CervicalFluid>
    {
        /// <summary>
        /// The constructor of the list.
        /// </summary>
        public CFCollection()
            : base("CervicalFluid")
        {
        }

        /// <summary>
        /// Return the CF for the specific day.
        /// </summary>
        /// <param name="date">Day value.</param>
        /// <returns>The CF of the day; if not found - return CervicalFluid.Undefined.</returns>
        public new CervicalFluid this[DateTime date]
        {
            get
            {
                CervicalFluid ret;
                return TryGetValue(date, out ret) ? ret : CervicalFluid.Undefined;
            }

            set
            {
                if (value == CervicalFluid.Undefined)
                {
                    Remove(date);
                }
                else
                {
                    base[date] = value;
                }
            }
        }
    }
}
