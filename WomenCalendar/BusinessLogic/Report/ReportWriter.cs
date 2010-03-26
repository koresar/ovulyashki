using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The base class for report writer classes.
    /// </summary>
    public abstract class ReportWriter : IDisposable
    {
        /// <summary>
        /// Write the report header part.
        /// </summary>
        public abstract void WriteHeader();

        /// <summary>
        /// Write the report one day part.
        /// </summary>
        /// <param name="day">One day info.</param>
        public abstract void WriteDay(OneDayInfo day);

        #region IDisposable Members

        /// <summary>
        /// Dispose all the opened resources.
        /// </summary>
        public abstract void Dispose();

        #endregion

        /// <summary>
        /// Get string from DateTime.
        /// </summary>
        /// <param name="d">Date time to print.</param>
        /// <returns>String of the DateTime.</returns>
        protected string D(DateTime d)
        {
            return d.ToShortDateString();
        }

        /// <summary>
        /// Get string from bool.
        /// </summary>
        /// <param name="b">Bool to print.</param>
        /// <returns>String of the bool.</returns>
        protected string B(bool b)
        {
            return b ? TEXT.Get["Yes"] : TEXT.Get["No"];
        }

        /// <summary>
        /// Get string from double.
        /// </summary>
        /// <param name="d">Value to print.</param>
        /// <returns>String of the double value.</returns>
        protected string D(double d)
        {
            return d != 0 ? d.ToString().Replace(',', '.') : string.Empty;
        }

        /// <summary>
        /// Get string from int.
        /// </summary>
        /// <param name="i">Value to print.</param>
        /// <param name="preserveNegativeValues">Indicates if negative value should be printer or not.</param>
        /// <returns>String of the int value.</returns>
        protected string I(int i, bool preserveNegativeValues)
        {
            return !preserveNegativeValues && i < 0 ? string.Empty : i.ToString();
        }

        /// <summary>
        /// Get string from int.
        /// </summary>
        /// <param name="i">Value to print.</param>
        /// <returns>String of the int value.</returns>
        protected string I(int i)
        {
            return i.ToString();
        }

        /// <summary>
        /// Get string from CF vlue.
        /// </summary>
        /// <param name="cf">Value to print.</param>
        /// <returns>String of the int VF value.</returns>
        protected string CF(CervicalFluid cf)
        {
            if (cf == CervicalFluid.Undefined)
            {
                return string.Empty;
            }

            if (cf == CervicalFluid.Tacky)
            {
                return TEXT.Get["CF_tacky"];
            }

            if (cf == CervicalFluid.Stretchy)
            {
                return TEXT.Get["CF_stretchy"];
            }

            if (cf == CervicalFluid.Water)
            {
                return TEXT.Get["CF_water"];
            }

            return string.Empty;
        }
    }
}
