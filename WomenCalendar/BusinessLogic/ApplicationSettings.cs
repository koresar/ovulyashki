using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;

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

        private bool defaultWindowIsMaximized = true;
        public bool DefaultWindowIsMaximized
        {
            get { return defaultWindowIsMaximized; }
            set { defaultWindowIsMaximized = value; }
        }

        private string applicationLanguage;
        public string ApplicationLanguage
        {
            get { return applicationLanguage; }
            set { applicationLanguage = value; }
        }

        private DayCellAppearance dayCellAppearance;
        public DayCellAppearance DayCellAppearance
        {
            get { return dayCellAppearance ?? (dayCellAppearance = new DayCellAppearance()); }
            set { dayCellAppearance = value; }
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

        public static readonly string settingsFileName = "Ovulyashki.settings";

        public static string GetApplicationSettingsFile()
        {
            var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Ovulyashki");
            var appDataFile = Path.Combine(appDataPath, settingsFileName);
            var localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var localFile = Path.Combine(localPath, settingsFileName);
            if (HaveAccessToCurrentFolder())
            { // we have access to own folder.
                if (!File.Exists(localFile) && File.Exists(appDataFile))
                { // we have previous settings in appData. Let's copy to own folder.
                    File.Copy(appDataFile, localFile);
                }
                return localFile;
            }
            else
            { // we do not have access to own folder.
                if (!Directory.Exists(appDataPath))
                {
                    Directory.CreateDirectory(appDataPath);
                }
                if (!File.Exists(appDataFile) && File.Exists(localFile))
                { // but suddenly we have settings in own folder, so let's use it.
                    File.Copy(localFile, appDataFile);
                }
                return appDataFile;
            }
        }

        private static bool HaveAccessToCurrentFolder()
        {
            var tmpFileName = Assembly.GetExecutingAssembly().Location + Guid.NewGuid().ToString();
            try
            {
                File.Create(tmpFileName).Close();
                File.Delete(tmpFileName);
            }
            catch
            {
                return false;
            }
            return true;
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
