using System;
using System.Collections.Generic;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// The exception is used if any problems with translation files is happened.
    /// </summary>
    [global::System.Serializable]
    public class TranslationException : Exception
    {
        /// <summary>
        /// The mssage of what is happened.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public TranslationException(string message)
            : base(message)
        {
        }
    }
}
