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
                ApplicationForm.SuspendLayout();
                if (_currentWoman != null)
                {
                    _currentWoman.AveragePeriodLengthChanged -= ApplicationForm.UpdateWomanInformation;
                    _currentWoman.Menstruations.CollectionChanged -= ApplicationForm.UpdateWomanInformation;
                    _currentWoman.Menstruations.CollectionChanged -= ApplicationForm.RedrawCalendar;
                    _currentWoman.Conceptions.CollectionChanged -= ApplicationForm.RedrawCalendar;
                }
                _currentWoman = value;
                if (ApplicationForm != null)
                {
                    if (_currentWoman != null)
                    {
                        _currentWoman.AveragePeriodLengthChanged += ApplicationForm.UpdateWomanInformation;
                        _currentWoman.Menstruations.CollectionChanged += ApplicationForm.UpdateWomanInformation;
                        _currentWoman.Menstruations.CollectionChanged += ApplicationForm.RedrawCalendar;
                        _currentWoman.Conceptions.CollectionChanged += ApplicationForm.RedrawCalendar;
                    }
                    ApplicationForm.UpdateWomanInformation();
                }
                ApplicationForm.ResumeLayout();
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
                return LoadWoman(dialog.FileName);
            }
            return false;
        }

        public static bool NewWoman()
        {
            if (AskAndSaveCurrentWoman())
            {
                NewEditWomanForm form = new NewEditWomanForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Woman w = new Woman();
                    w.Name = form.WomanName;
                    w.Password = form.WomanPassword;
                    if (!string.IsNullOrEmpty(w.Password))
                    {
                        w.AllwaysAskPassword = true;
                    }
                    CurrentWoman = w;
                    return true;
                }
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

        public static bool LoadWoman(string path)
        {
            Woman w = Woman.ReadFrom(path);
            if (w.AllwaysAskPassword && !AskPassword(w)) return false;

            CurrentWoman = w;
            return true;
        }

        public static bool AskPassword(Woman woman)
        {
            LoginForm form = new LoginForm();
            form.Text = woman.Name + ", " + form.Text;
            return form.ShowDialog() == DialogResult.OK && form.Password == woman.Password;
        }
        
        public static bool EditWoman()
        {
            NewEditWomanForm form = new NewEditWomanForm();
            form.WomanName = CurrentWoman.Name;
            form.WomanPassword = CurrentWoman.Password;
            form.Text = "Изменяем женщину";
            if (form.ShowDialog() == DialogResult.OK)
            {
                CurrentWoman.Name = form.WomanName;
                CurrentWoman.Password = form.WomanPassword;
                if (!string.IsNullOrEmpty(CurrentWoman.Password))
                {
                    CurrentWoman.AllwaysAskPassword = true;
                }
                return true;
            }
            return false;            
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ApplicationForm = new MainForm();

            Settings = ApplicationSettings.Read(SettingsFileName);

            if (args.Length == 0 || !File.Exists(args[0]) || !LoadWoman(args[0])) // command line
            {
                if (string.IsNullOrEmpty(Settings.DefaultWomanPath) ||
                    !File.Exists(Settings.DefaultWomanPath) || !LoadWoman(Settings.DefaultWomanPath))
                {
                    Settings.DefaultWomanPath = string.Empty;
                }
            }

            bool isMaximazed = Settings.DefaultWindowIsMaximized;
            ApplicationForm.Location = Settings.DefaultWindowPosition;
            ApplicationForm.Size = Settings.DefaultWindowSize;
            if (isMaximazed)
            {
                ApplicationForm.WindowState = FormWindowState.Maximized;
            }

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
            public static Pen TodayEdgePen = new Pen(Brushes.Blue, 2);
            public static Pen EdgePen = Pens.Black;
        }
    }
}
