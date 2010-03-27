using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    /// <summary>
    /// Text notes serializable collection.
    /// </summary>
    [XmlRoot("Notes")]
    public class NotesCollection : SerializableEventsCollection<string>
    {
        /// <summary>
        /// The default contructor.
        /// </summary>
        public NotesCollection() : base("Note")
        {
        }

        /// <summary>
        /// Get or set day note.
        /// </summary>
        /// <param name="date">Day of note.</param>
        /// <returns>strin.Empty if no note is present; otherwise the note.</returns>
        public new string this[DateTime date]
        {
            get
            {
                string ret;
                return this.TryGetValue(date, out ret) ? ret : string.Empty;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.Remove(date);
                }
                else
                {
                    base[date] = value;
                }
            }
        }
    }
}