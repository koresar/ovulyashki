﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using CarlosAg.ExcelXmlWriter;
using ICSharpCode.SharpZipLib.BZip2;
using WomenCalendar.Forms;
using WomenCalendar.Properties;

namespace WomenCalendar
{
    /// <summary>
    /// The main application entrance class.
    /// </summary>
    public static class Program
    {
        private static Woman currentWomanClone;

        private static Woman currentWoman;

        private static string settingsFileName;

        public static MainForm ApplicationForm { get; set; }

        public static ApplicationSettings Settings { get; set; }

        /// <summary>
        /// Currently opened woman.
        /// </summary>
        public static Woman CurrentWoman
        {
            get
            {
                if (currentWoman == null)
                {
                    currentWoman = new Woman();
                    currentWomanClone = currentWoman.Clone() as Woman;
                }

                return currentWoman;
            }

            set
            {
                ApplicationForm.SuspendLayout();
                if (currentWoman != null)
                {
                    currentWoman.AveragePeriodLengthChanged -= ApplicationForm.UpdateWomanInformation;
                    currentWoman.Menstruations.CollectionChanged -= ApplicationForm.UpdateWomanInformation;
                    currentWoman.Menstruations.CollectionChanged -= ApplicationForm.RedrawCalendar;
                    currentWoman.Conceptions.CollectionChanged -= ApplicationForm.RedrawCalendar;
                }

                currentWoman = value;
                currentWomanClone = currentWoman.Clone() as Woman;
                if (currentWoman != null)
                {
                    currentWoman.AveragePeriodLengthChanged += ApplicationForm.UpdateWomanInformation;
                    currentWoman.Menstruations.CollectionChanged += ApplicationForm.UpdateWomanInformation;
                    currentWoman.Menstruations.CollectionChanged += ApplicationForm.RedrawCalendar;
                    currentWoman.Conceptions.CollectionChanged += ApplicationForm.RedrawCalendar;
                }

                ApplicationForm.UpdateWomanInformation();
                ApplicationForm.SetWomanName(currentWoman.Name);
                ApplicationForm.ResumeLayout();
            }
        }

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

        /// <summary>
        /// Saves current woman to the given file path.
        /// </summary>
        /// <param name="path">Path to the new/overwritten file.</param>
        /// <returns>True if file successfully saved.</returns>
        public static bool SaveCurrentWomanTo(string path)
        {
            BZip2OutputStream stream;
            try
            {
                stream = new BZip2OutputStream(new FileStream(path, FileMode.Create), 9);
            }
            catch (IOException ex)
            {
                MessageBox.Show(TEXT.Get["Unable_to_save_file"] + ex.Message, TEXT.Get["Error"]);
                return false;
            }

            new XmlSerializer(currentWoman.GetType()).Serialize(stream, currentWoman);
            stream.Close();

            currentWoman.AssociatedFile = path;

            currentWomanClone = currentWoman.Clone() as Woman;

            return true;
        }

        /// <summary>
        /// Ask the user and try to load a woman file.
        /// </summary>
        /// <returns>True if successfully loaded.</returns>
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

        /// <summary>
        /// Make an attempt to leave current woman and create a new one.
        /// </summary>
        /// <returns>Truye if new woman was created and applied.</returns>
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
        
        /// <summary>
        /// Make an attempt to save the current woman. Ask the user the file placement if necessary.
        /// </summary>
        /// <returns>True if file was successfully saved.</returns>
        public static bool AskAndSaveCurrentWoman()
        {
            if (currentWoman.Equals(currentWomanClone))
            { // no changes were done to current woman, thus just allow procceding.
                return true;
            }

            DialogResult res = MessageBox.Show(
                TEXT.Get["Save_woman_question"], 
                ApplicationForm.Text,
                MessageBoxButtons.YesNoCancel);
            switch (res)
            {
                case DialogResult.Yes:
                    // save the woman
                    if (!SaveCurrentWoman())
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

        /// <summary>
        /// Save the settings woman amde during the usage.
        /// </summary>
        /// <returns>True if file successfully saved.</returns>
        public static bool SaveSettings()
        {
            return Settings.Write(SettingsFileName);
        }

        /// <summary>
        /// Loads woman from the given file. Interfacts with user if needed. Set the current woman to the data just read.
        /// </summary>
        /// <param name="path">The path to the owman file.</param>
        /// <returns>False if the file was not loaded.</returns>
        public static bool LoadWoman(string path)
        {
            Woman w = Woman.ReadFrom(path);
            if (w == null || (w.AllwaysAskPassword && !AskPassword(w)))
            {
                return false;
            }

            CurrentWoman = w;
            return true;
        }

        /// <summary>
        /// Ask the woman for her password.
        /// </summary>
        /// <param name="woman">The woman to ask. Also stores credentials.</param>
        /// <returns>True if authetification succeded.</returns>
        public static bool AskPassword(Woman woman)
        {
            LoginForm form = new LoginForm();
            form.Text = woman.Name + ", " + form.Text;
            while (true)
            {
                if (form.ShowDialog() != DialogResult.OK)
                {
                    break;
                }

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
        
        /// <summary>
        /// Show the window to enter some winfo about woman (login, password, etc.)
        /// </summary>
        /// <returns>True if all went till the end.</returns>
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

        /// <summary>
        /// Ask user and save an Excel sheet or something he's selected.
        /// </summary>
        /// <returns>True if exporting gone till the end.</returns>
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

        public static void InitializeEnvironmentStuff()
        {
            Settings = ApplicationSettings.Read(SettingsFileName);

            TEXT.InitializeLanguage(Settings.ApplicationLanguage);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        [STAThread]
        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                InitializeEnvironmentStuff();

                ApplicationForm = new MainForm();

                // command line
                if (args.Length == 0 || !File.Exists(args[0]) || !LoadWoman(args[0]))
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

                Application.Run(ApplicationForm);
            }
            catch (Exception ex)
            {
                ErrorForm.Show(ex);
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            ErrorForm.Show(ex);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            ErrorForm.Show(ex);
        }
    }
}
