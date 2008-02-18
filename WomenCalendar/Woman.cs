using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace WomenCalendar
{
    [XmlRoot("Woman")]
    public class Woman
    {
//        [XmlIgnore()]
//        private List<DateTime> _menstruationDates;
//        [XmlArrayItem("Date", typeof(DateTime))]
//        [XmlArray("MenstruationDates")]
//        public List<DateTime> MenstruationDates
//        {
//            get { return _menstruationDates; }
//            set { _menstruationDates = value; }
//        }

        private MenstruationsCollection _menstruations;
        public MenstruationsCollection Menstruations
        {
            get { return _menstruations; }
            set { _menstruations = value; }
        }

        [XmlIgnore()]
        private bool isNew = true;
        [XmlIgnore()]
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        [XmlIgnore()]
        private string associatedFile;
        [XmlIgnore()]
        public string AssociatedFile
        {
            get { return associatedFile; }
            set { associatedFile = value; }
        }

        [XmlIgnore()]
        private int periodLength;
        [XmlAttribute("PeriodLength")]
        public int PeriodLength
        {
            get { return periodLength; }
            set { periodLength = value; }
        }

        [XmlIgnore()]
        private NotesCollection notes;
        public NotesCollection Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public Woman()
        {
//            _menstruationDates = new List<DateTime>();
            notes = new NotesCollection();
            periodLength = 5;
            _menstruations = new MenstruationsCollection();
        }

        public static bool SaveTo(Woman w, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            new XmlSerializer(w.GetType()).Serialize(fs, w);
            fs.Close();
            w.associatedFile = path;
            return true;
        }

        public static Woman ReadFrom(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            Woman w = (Woman)(new XmlSerializer(typeof(Woman)).Deserialize(fs));
            fs.Close();
            w.AssociatedFile = path;
            return w;
        }

        public bool AddMenstruationDay(DateTime date)
        {
            if (Menstruations.IsMenstruationDay(date))
            {
                return false;
            }

            return Menstruations.Add(date, PeriodLength);
        }

        public bool RemoveMenstruationDay(DateTime date)
        {
            return Menstruations.Remove(date);
        }

        public bool AddNote(DateTime date, string text)
        {
            Notes[date] = text;
            return true;
        }

        public bool RemoveNote(DateTime date)
        {
            return notes.Remove(date);
        }
    }
}
