using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    /// <summary>
    /// The application settings. Serialized into XML. May contain any XML serializable property.
    /// </summary>
    [XmlRoot("WomanCalendarSettings")]
    public class ApplicationSettings
    {
        private static readonly string SettingsFileName = "Ovulyashki.settings"; 
        
        private string defaultWomanPath;
        private Point defaultWindowPosition;
        private Size defaultWindowSize;
        private bool defaultWindowIsMaximized = true;
        private string applicationLanguage;
        private DayCellAppearance dayCellAppearance;
        
        /// <summary>
        /// The path of the woman to open by default. Used to open same woman by simply starting the applcation. 
        /// If the string is empty then do not open any woman on application start.
        /// </summary>
        public string DefaultWomanPath
        {
            get { return this.defaultWomanPath; }
            set { this.defaultWomanPath = value; }
        }

        /// <summary>
        /// The window position. It is saved on applocation close and restored on application start.
        /// </summary>
        public Point DefaultWindowPosition
        {
            get { return this.defaultWindowPosition; }
            set { this.defaultWindowPosition = value; }
        }

        /// <summary>
        /// The window size. It is saved on applocation close and restored on application start.
        /// </summary>
        public Size DefaultWindowSize
        {
            get { return this.defaultWindowSize; }
            set { this.defaultWindowSize = value; }
        }

        /// <summary>
        /// The window state. It is saved on applocation close and restored on application start.
        /// </summary>
        public bool DefaultWindowIsMaximized
        {
            get { return this.defaultWindowIsMaximized; }
            set { this.defaultWindowIsMaximized = value; }
        }

        /// <summary>
        /// The language which was last used on application close. Use this one as default until changed by user.
        /// </summary>
        public string ApplicationLanguage
        {
            get { return this.applicationLanguage; }
            set { this.applicationLanguage = value; }
        }

        /// <summary>
        /// The xml serializable class of user customized background colors of calendar cells.
        /// </summary>
        public DayCellAppearance DayCellAppearance
        {
            get { return this.dayCellAppearance ?? (this.dayCellAppearance = new DayCellAppearance()); }
            set { this.dayCellAppearance = value; }
        }

        /// <summary>
        /// Read settings from file and create the settings object.
        /// </summary>
        /// <param name="fileName">File with settings.</param>
        /// <returns>New applicastion settings object.</returns>
        public static ApplicationSettings Read(string fileName)
        {
            ApplicationSettings settings;
            if (File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                settings = (ApplicationSettings)new XmlSerializer(typeof(ApplicationSettings)).Deserialize(fs);
                fs.Close();
            }
            else
            {
                settings = new ApplicationSettings();
            }

            return settings;
        }

        /// <summary>
        /// Find the place of application settings. 
        /// Default place is the application executable folder. If the access denied then use [user]/AppData/ folder.
        /// Sometimes we might have no access to current folderm but settings file placed there, - 
        /// in this case we copy file to AppData folder and use it from there.
        /// </summary>
        /// <returns>Full path to application settings file.</returns>
        public static string GetApplicationSettingsFile()
        {
            var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Ovulyashki");
            var appDataFile = Path.Combine(appDataPath, SettingsFileName);
            var localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var localFile = Path.Combine(localPath, SettingsFileName);
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

        /// <summary>
        /// Save the settings object to the file. May throw an exception.
        /// </summary>
        /// <param name="fileName">The file so save settings to.</param>
        /// <returns>True if saved. Otherwise - throws exception.</returns>
        public bool Write(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            new XmlSerializer(GetType()).Serialize(fs, this);
            fs.Close();
            return true;
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
    }
}
