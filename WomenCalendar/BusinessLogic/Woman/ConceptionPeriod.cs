using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public class ConceptionPeriod : ICloneable
    {
        public const int StandardLength = 40 * 7;

        public DateTime StartDay { get; set; }
        public DateTime LastDay { get; set; }

        public bool IsDayInPeriod(DateTime day)
        {
            return StartDay <= day && day <= LastDay;
        }


        #region ICloneable Members

        public object Clone()
        {
            return new ConceptionPeriod() { StartDay = this.StartDay, LastDay = this.LastDay };
        }

        #endregion

        public override bool Equals(object obj)
        {
            var secondValue = obj as ConceptionPeriod;
            return secondValue != null &&
                secondValue.StartDay.Equals(this.StartDay) &&
                secondValue.LastDay.Equals(this.LastDay);
        }

        public override int GetHashCode()
        {
            return StartDay.GetHashCode() ^ LastDay.GetHashCode();
        }
    }
}
