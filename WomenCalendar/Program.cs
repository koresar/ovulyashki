using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using WomenCalendar.Properties;
using System.ComponentModel;

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
                _currentWoman.AveragePeriodLengthChanged -= ApplicationForm.UpdateWomanInformation;
                _currentWoman = value;
                _currentWoman.AveragePeriodLengthChanged += ApplicationForm.UpdateWomanInformation;
                ApplicationForm.UpdateWomanInformation();
            }
        }

        public static ComponentResourceManager IconResource;

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
            IconResource = new ComponentResourceManager(typeof(MainForm));
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
