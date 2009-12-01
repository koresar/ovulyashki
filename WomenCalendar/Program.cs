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

namespace WomenCalendar
{
    static class Program
    {
        public static MainForm ApplicationForm;

        public static ResourceManager IconResource;

        public static ApplicationSettings Settings;

        private static string SettingsFileName
        {
            get
            {
                var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Ovulyashki");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                return Path.Combine(dir, "Ovulyashki.settings");
            }
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
            while (true)
            {
                if (form.ShowDialog() != DialogResult.OK) break;
                if (form.Password != woman.Password)
                {
                    if (MessageBox.Show("Неправильный пароль! Попытаемся еще раз?\n\n" +
                        "Если нажмёшь 'Нет', то создадим новую женжину.", "Ошибка!", MessageBoxButtons.YesNo) != DialogResult.Yes)
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
            form.Text = "Изменяем женщину";
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
            dialog.Filter = "Excel files (*.xls)|*.xls|Comma Separated Values files (*.csv)|*.csv";
            dialog.RestoreDirectory = true;
            dialog.CheckPathExists = true;
            dialog.Title = "Укажите файл";
            dialog.SupportMultiDottedExtensions = true;
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            string fileName = dialog.FileName;
            if (!fileName.EndsWith(".csv") && !fileName.EndsWith(".xls"))
            {
                MessageBox.Show("Расширение файла должно быть .xls или .csv!", "Ошибка!");
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
                MessageBox.Show("Не могу экспортировать в файл. Сообщение об ошибке:\n" + 
                    ex.Message, "Ошибка!");
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

        public static class MonthAppearance
        {
            public static Brush MonthHeaderBrush = new LinearGradientBrush(new Point(0, 0), new Point(120, 0), 
                Color.FromArgb(248, 153, 250), Color.White) { WrapMode = WrapMode.TileFlipXY };
            public static Brush WeekDayHeaderBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, 16),
                Color.RoyalBlue, ControlPaint.LightLight(Color.RoyalBlue)) { WrapMode = WrapMode.TileFlipXY, };
            public static Brush MonthNameBrush = Brushes.Purple;
            public static Brush WeekDayTextBrush = Brushes.White;
            public static Brush WeekDayHolidayTextBrush = Brushes.Salmon;
            public static Pen MonthEdgePen = new Pen(Brushes.Gray, 6);
            public static Pen WeekDayEdgePen = new Pen(Brushes.White, 1);
            public static Pen TodayEdgePen = new Pen(Brushes.Blue, 6);
        }

        public static class DayCellAppearance
        {
            public static Pen FocusEdgePen = new Pen(Brushes.Red, 2);
            public static Pen TodayEdgePen = new Pen(Brushes.Blue, 2);
            public static Pen EdgePen = Pens.Black;
            public static Brush DayNumberBrush = Brushes.RoyalBlue;
            public static Brush PregnancyWeekNumberBrush = Brushes.Green;
            public static Brush MenstruationPredictionBrush = Brushes.Red;
            public static Brush HadSexBrush = Brushes.Red;

            // Back colors
            public static Color BackConceptionDay = Color.DeepSkyBlue;
            public static Color BackPregnancyDay = Color.LightCyan;
            public static Color BackMenstruationDay = Color.LightPink;
            public static Color BackPredictedMenstruationDay = Color.LightGreen;
            public static Color BackOvulationDay = Color.Gold;
            public static Color BackSafeSex = Color.LightGreen;
            public static Color BackEmpty = Color.White;
        }
    }
}
