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
    }
}