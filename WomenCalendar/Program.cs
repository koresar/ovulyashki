using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using WomenCalendar.Properties;
using System.ComponentModel;
using System.Resources;
using CarlosAg.ExcelXmlWriter;
using System.Drawing.Drawing2D;
using WomenCalendar.Forms;
using System.Reflection;
using System.Globalization;
using System.Xml;
using System.Text;
using System.Net;
using System.Threading;
using ICSharpCode.SharpZipLib.BZip2;
using System.Xml.Serialization;

namespace WomenCalendar
{
    static class Program
    {
        public static MainForm ApplicationForm;

        public static ResourceManager IconResource;

        public static ApplicationSettings Settings;

        private static string settingsFileName;
        private static string SettingsFileName
        {
            get
            {
                if (string.IsNullOrEmpty(settingsFileName))
                {
                    settingsFileName = ApplicationSettings.GetApplicationSettingsFile();
                }
                return settingsFileName;
            }
        }

        public static Woman _currentWomanClone;
        public static Woman _currentWoman;
        public static Woman CurrentWoman
        {
            get
            {
                if (_currentWoman == null)
                {
                    _currentWoman = new Woman();
                    _currentWomanClone = _currentWoman.Clone() as Woman;
                }
                return _currentWoman;
            }
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
                _currentWomanClone = _currentWoman.Clone() as Woman;
                if (_currentWoman != null)
                {
                    _currentWoman.AveragePeriodLengthChanged += ApplicationForm.UpdateWomanInformation;
                    _currentWoman.Menstruations.CollectionChanged += ApplicationForm.UpdateWomanInformation;
                    _currentWoman.Menstruations.CollectionChanged += ApplicationForm.RedrawCalendar;
                    _currentWoman.Conceptions.CollectionChanged += ApplicationForm.RedrawCalendar;
                }
                ApplicationForm.UpdateWomanInformation();
                ApplicationForm.SetWomanName(_currentWoman.Name);
                ApplicationForm.ResumeLayout();
            }
        }

        public static bool SaveCurrentWoman()
        {
            if (string.IsNullOrEmpty(CurrentWoman.AssociatedFile))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = TEXT.Get["Woman_files"] + " (*.woman)|*.woman";
                dialog.AddExtension = true;
                dialog.DefaultExt = ".woman";
                if (dialog.ShowDialog(ApplicationForm) == DialogResult.OK)
                {
                    SaveCurrentWomanTo(dialog.FileName);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                SaveCurrentWomanTo(CurrentWoman.AssociatedFile);
            }
            return true;
        }

        public static bool SaveCurrentWomanTo(string path)
        {
            var s = new BZip2OutputStream(new FileStream(path, FileMode.Create), 9);
            new XmlSerializer(_currentWoman.GetType()).Serialize(s, _currentWoman);
            s.Close();

            _currentWoman.AssociatedFile = path;

            _currentWomanClone = _currentWoman.Clone() as Woman;

            return true;
        }

        public static bool OpenWoman()
        {
            if (!AskAndSaveCurrentWoman())
            {
                return false;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = TEXT.Get["Woman_files"] + " (*.woman)|*.woman";
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
            if (_currentWoman.Equals(_currentWomanClone))
            { // no changes were done to current woman, thus just allow procceding.
                return true;
            }

            DialogResult res = MessageBox.Show(TEXT.Get["Save_woman_question"], ApplicationForm.Text,
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
            while (true)
            {
                if (form.ShowDialog() != DialogResult.OK) break;
                if (form.Password != woman.Password)
                {
                    if (MessageBox.Show(TEXT.Get["Wrong_password_question"], TEXT.Get["Error"], MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        break;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        
        public static bool EditWoman()
        {
            NewEditWomanForm form = new NewEditWomanForm();
            form.WomanName = CurrentWoman.Name;
            form.WomanPassword = CurrentWoman.Password;
            form.Text = TEXT.Get["Edit_woman"];
            if (form.ShowDialog() == DialogResult.OK)
            {
                CurrentWoman.Name = form.WomanName;
                CurrentWoman.Password = form.WomanPassword;
                if (!string.IsNullOrEmpty(CurrentWoman.Password))
                {
                    CurrentWoman.AllwaysAskPassword = true;
                }
                ApplicationForm.SetWomanName(CurrentWoman.Name);
                return true;
            }
            return false;            
        }

        public static bool ExportWoman()
        {
            DateRangeForm form = new DateRangeForm();
            if (form.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = 
                TEXT.Get["Excel_files"] + " (*.xls)|*.xls|" + 
                TEXT.Get["CSV_files"] + " (*.csv)|*.csv";
            dialog.RestoreDirectory = true;
            dialog.CheckPathExists = true;
            dialog.Title = TEXT.Get["Point_the_file"];
            dialog.SupportMultiDottedExtensions = true;
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            string fileName = dialog.FileName;
            if (!fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) && 
                !fileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(TEXT.Get["File_ext_must_to_be"], TEXT.Get["Error"]);
                    return false;
            }

            try
            {
                using (ReportWriter report = fileName.EndsWith(".csv") ?
                    (ReportWriter)(new CsvWriter(fileName)) : (ReportWriter)(new XlsWriter(fileName)))
                {
                    report.WriteHeader();
                    for (DateTime day = form.From; day <= form.To; day = day.AddDays(1))
                    {
                        report.WriteDay(CurrentWoman.GetOneDayInfo(day));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(TEXT.Get["Cant_export"] + "\n" + ex.Message, TEXT.Get["Error"]);
                return false;
            }
            return true;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Settings = ApplicationSettings.Read(SettingsFileName);

                TEXT.InitializeLanguage(Settings.ApplicationLanguage);

                ApplicationForm = new MainForm();

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
            catch (Exception ex)
            {
                ErrorForm.Show(ex);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            ErrorForm.Show(ex);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            ErrorForm.Show(ex);
        }
    }
}
