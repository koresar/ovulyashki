using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace WomenCalendar
{
    [XmlRoot("Notes")]
    public class NotesCollection : SerializableEventsCollection<string>
    {
        public NotesCollection() : base("Note")
        {
        }

        public new string this[DateTime date]
        {
            get
            {
                string ret;
                return TryGetValue(date, out ret) ? ret : string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) Remove(date);
                else base[date] = value;
            }
        }
    }
}