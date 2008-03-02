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

        public static ResourceManager IconResource;

        public static ApplicationSettings Settings;

        private static string SettingsFileName
        {
            get { return Path.Combine(Application.StartupPath, "WomenCalendar.settings"); }
        }

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

        public static bool SaveCurrentWoman()
        {
            if (string.IsNullOrEmpty(CurrentWoman.AssociatedFile))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Woman files (*.woman)|*.woman";
                dialog.AddExtension = true;
                dialog.DefaultExt = ".woman";
                if (dialog.ShowDialog(ApplicationForm) == DialogResult.OK)
                {
                    Woman.SaveTo(CurrentWoman, dialog.FileName);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Woman.SaveTo(CurrentWoman, CurrentWoman.AssociatedFile);
            }
            return true;
        }

        public static bool OpenWoman()
        {
            if (!AskAndSaveCurrentWoman())
            {
                return false;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Woman files (*.woman)|*.woman";
            if (dialog.ShowDialog(ApplicationForm) == DialogResult.OK)
            {
                CurrentWoman = Woman.ReadFrom(dialog.FileName);
                //Settings.DefaultWomanPath = dialog.FileName;
                return true;
            }
            return false;
        }

        public static bool NewWoman()
        {
            if (AskAndSaveCurrentWoman())
            {
                CurrentWoman = new Woman();
                return true;
            }
            return false;
        }
        
        public static bool AskAndSaveCurrentWoman()
        {
            DialogResult res = MessageBox.Show("Сохранить эту женщину, прежде чем продолжить?", ApplicationForm.Text,
                                MessageBoxButtons.YesNoCancel);
            switch (res)
            {
                case DialogResult.Yes:
                    if (!SaveCurrentWoman()) // save the woman
                    {
                        return false; // saving aborted. do nothing in this case.
                    }
                    break;
                case DialogResult.No: // proceed without saving
                    break;
                default:
                    return false; // do nothing.
            }
            return true;
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
                if (File.Exists(Settings.DefaultWomanPath))
                {
                    CurrentWoman = Woman.ReadFrom(Settings.DefaultWomanPath);
                }
                else
                {
                    Settings.DefaultWomanPath = string.Empty;
                }
            }
            return true;
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
            public static Pen HeaderPen = new Pen(HeaderBrush, OneMonthControl.EdgeWidth + 1);
            public static Pen TodayEdgePen = new Pen(Brushes.Blue, 10);
        }

        public static class DayCellAppearance
        {
            public static Pen FocusEdgePen = new Pen(Brushes.Red, 2);
            public static Pen EdgePen = Pens.Black;
        }
    }
}
