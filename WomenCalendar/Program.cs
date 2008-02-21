using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using WomenCalendar.Properties;
using System.ComponentModel;
using System.Resources;

namespace WomenCalendar
{
    static class Program
    {
        public static MainForm ApplicationForm;

        public static Woman _currentWoman;
        public static Woman CurrentWoman
        {
            get { return _currentWoman ?? (_currentWoman = new Woman()); }
            set
            {
                if (_currentWoman != null)
                {
                    _currentWoman.AveragePeriodLengthChanged -= ApplicationForm.UpdateWomanInformation;
                    _currentWoman.Menstruations.CollectionChanged -= ApplicationForm.UpdateWomanInformation;
                }
                _currentWoman = value;
                if (_currentWoman != null)
                {
                    _currentWoman.AveragePeriodLengthChanged += ApplicationForm.UpdateWomanInformation;
                    _currentWoman.Menstruations.CollectionChanged += ApplicationForm.UpdateWomanInformation;
                }
                ApplicationForm.UpdateWomanInformation();
            }
        }

        public static ResourceManager IconResource;

        public static ApplicationSettings Settings;

        private static string SettingsFileName
        {
            get { return Path.Combine(Application.StartupPath, "WomenCalendar.settings"); }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationForm = new MainForm();
            IconResource = Resources.ResourceManager;
                //new System.Resources.ResourceManager(System.Reflection.Assembly.GetExecutingAssembly());
            Application.Run(ApplicationForm);
        }

        public static class MonthAppearance
        {
            public static Brush HeaderBrush = Brushes.White;
        }

        public static class DayCellAppearance
        {
            public static Pen FocusEdgePen = new Pen(Brushes.Red, 2);
            public static Pen EdgePen = Pens.Black;
        }

        public static bool SaveSettings()
        {
            return Settings.Write(SettingsFileName);
        }

        public static bool LoadSettings()
        {
            Settings = ApplicationSettings.Read(SettingsFileName);
            if (!string.IsNullOrEmpty(Settings.DefaultWomanPath))
            {
                CurrentWoman = Woman.ReadFrom(Settings.DefaultWomanPath);
            }
            return true;
        }
    }
}
