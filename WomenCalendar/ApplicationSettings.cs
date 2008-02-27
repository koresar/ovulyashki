using System;
using System.Collections.Generic;
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

        public static ApplicationSettings Read(string fileName)
        {
            ApplicationSettings settings;
            if (File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                settings =
                    (ApplicationSettings) (new XmlSerializer(typeof (ApplicationSettings)).Deserialize(fs));
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
