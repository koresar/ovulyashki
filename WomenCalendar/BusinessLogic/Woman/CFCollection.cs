using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public enum CervicalFluid
    {
        Undefined = 0,
        /// <summary>
        /// 1. Glue like.
        /// </summary>
        Tacky = 1,
        /// <summary>
        /// 2. Egg like.
        /// </summary>
        Stretchy = 2,
        /// <summary>
        /// 3. Water like.
        /// </summary>
        Water = 3
    }

    public class CFCollection : SerializableEventsCollection<CervicalFluid>
    {
        public const int DefaultHealthValue = 5;

        public CFCollection()
            : base("CervicalFluid")
        {
        }

        public new CervicalFluid this[DateTime date]
        {
            get
            {
                CervicalFluid ret;
                return TryGetValue(date, out ret) ? ret : CervicalFluid.Undefined;
            }
            set
            {
                if (value == CervicalFluid.Undefined) Remove(date);
                else base[date] = value;
            }
        }
    }
}
