using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    [XmlRoot("WomanCalendarSettings")]
    public class ApplicationSettings
    {
        private string defaultWomanPath;
        public string DefaultWomanPath
        {
            get { return defaultWomanPath; }
            set { defaultWomanPath = value; }
        }

        private Point defaultWindowPosition;
        public Point DefaultWindowPosition
        {
            get { return defaultWindowPosition; }
            set { defaultWindowPosition = value; }
        }

        private Size defaultWindowSize;
        public Size DefaultWindowSize
        {
            get { return defaultWindowSize; }
            set { defaultWindowSize = value; }
        }

        private bool defaultWindowIsMaximized;
        public bool DefaultWindowIsMaximized
        {
            get { return defaultWindowIsMaximized; }
            set { defaultWindowIsMaximized = value; }
        }

        public static ApplicationSettings Read(string fileName)
        {
            ApplicationSettings settings;
            if (File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                settings = (ApplicationSettings) (new XmlSerializer(typeof (ApplicationSettings)).Deserialize(fs));
                fs.Close();
            }
            else
            {
                settings = new ApplicationSettings();
            }
            return settings;
        }

        public bool Write(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            new XmlSerializer(GetType()).Serialize(fs, this);
            fs.Close();
            return true;
        }
    }
}
