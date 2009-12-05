using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    public abstract class ReportWriter : IDisposable
    {
        public ReportWriter(string fileName)
        {
        }

        public abstract void WriteHeader();
        public abstract void WriteDay(OneDayInfo day);

        #region IDisposable Members

        public virtual void Dispose()
        {            
        }

        #endregion

        public static string D(DateTime d)
        {
            return d.ToShortDateString();
        }
        public static string B(bool b)
        {
            return b ? TEXT.Get["Yes"] : TEXT.Get["No"];
        }
        public static string D(double d)
        {
            return d != 0 ? d.ToString().Replace(',', '.') : string.Empty;
        }
        public static string I(int i, bool preserveNegativeValues)
        {
            return !preserveNegativeValues && i < 0 ? string.Empty : i.ToString();
        }
        public static string I(int i)
        {
            return i.ToString();
        }
    }
}
