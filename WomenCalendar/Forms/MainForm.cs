using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            toolStrip1.Items.Add(new ToolStripControlHost(dateTimePicker1));
            lblAverageCycle.Text = string.Empty;
        }

        public void UpdateDayInformation(DateTime date)
        {
            lblDayDescription.Text = GenerateDayInfo(date);
            xDay.Height = lblDayDescription.Height + 32;
        }

        /// <summary>
        /// Fill all woman controls with CurrentWoman data.
        /// </summary>
        public void UpdateWomanInformation()
        {
            rbManual.Checked = Program.CurrentWoman.UseManualPeriodLength;
            rbAuto.Checked = !rbManual.Checked;
            if (rbAuto.Checked)
            {
                Program.CurrentWoman.ManualPeriodLength = Program.CurrentWoman.AveragePeriodLength;
            }

            lblAverageCycle.Text = GenerateWomanInformation();
            SetNumMenstrulationPriod(Program.CurrentWoman.ManualPeriodLength);
            numMenstruationLength.Value = Program.CurrentWoman.DefaultMenstruationLength;
            chbAskPassword.Checked = Program.CurrentWoman.AllwaysAskPassword;
        }

        public string GenerateWomanInformation()
        {
            StringBuilder sb = new StringBuilder();

            int usedDays = Program.CurrentWoman.AveragePeriodLength == 0 ? 28 : Program.CurrentWoman.AveragePeriodLength;
            sb.Append("Средний цикл: ");
            sb.Append(usedDays);
            sb.Append(' ');
            sb.Append(GetDaysString(usedDays));

            return sb.ToString();
        }

        private string GenerateDayInfo(DateTime date)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Дата: ");
            sb.Append(date.ToShortDateString());
            if (date == DateTime.Today)
            {
                sb.Append(" (сегодня)");
            }

            if (Program.CurrentWoman.Menstruations.IsMenstruationDay(date))
            {
                sb.AppendLine();
                sb.Append("Овуляшкин день");
            }

            if (Program.CurrentWoman.IsPredictedAsMenstruationDay(date))
            {
                sb.AppendLine();
                sb.Append("Вероятны овуляшки");
            }

            // got to be last
            string text;
            if (Program.CurrentWoman.Notes.TryGetValue(date, out text))
            {
                sb.AppendLine();
                sb.Append("Заметка: ");
                sb.Append(text);
            }

            return sb.ToString();
        }

        private void SetNumMenstrulationPriod(int length)
        {
            numMenstruationPeriod.Minimum = (length == 0) ? 0 : 10;
            numMenstruationPeriod.Value = length;
        }

        public static string GetDaysString(int days)
        {
            if (days > 4 && days < 21) return "дней";
            int tmpDays = days % 10;
            if (tmpDays == 1) return "день";
            if (tmpDays < 5 && tmpDays > 1) return "дня";
            return "дней";
        }

        private void prevStripButton_Click(object sender, EventArgs e)
        {
            monthControl.ScrollMonthes(-1);
        }

        private void nextStripButton_Click(object sender, EventArgs e)
        {
            monthControl.ScrollMonthes(1);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            monthControl.FocusDate = dateTimePicker1.Value;
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (Program.NewWoman())
            {
                chbDefaultWoman.Checked = false;
                monthControl.Redraw();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Program.SaveCurrentWoman();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (Program.OpenWoman())
            {
                monthControl.Redraw();
            }
        }

        private void monthControl_FocusDateChanged(object sender, FocusDateChangedEventArgs e)
        {
            UpdateDayInformation(e.NewDate);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !Program.AskAndSaveCurrentWoman();
            if (!e.Cancel)
            {

                // Beginning the part where we collect all application settings
                Program.Settings.DefaultWomanPath = chbDefaultWoman.Checked ? Program.CurrentWoman.AssociatedFile : string.Empty;
                // And of the settings collectioning part.

                Program.SaveSettings();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.LoadSettings();

            monthControl.FocusDate = DateTime.Today;
            chbDefaultWoman.Checked = !string.IsNullOrEmpty(Program.Settings.DefaultWomanPath);
        }

        private void rbAuto_CheckedChanged(object sender, EventArgs e)
        {
            numMenstruationPeriod.Enabled = !rbAuto.Checked;
            Program.CurrentWoman.UseManualPeriodLength = !rbAuto.Checked;
            if (rbAuto.Checked)
            {
                SetNumMenstrulationPriod(Program.CurrentWoman.AveragePeriodLength);
            }
        }

        private void numMenstruationPeriod_ValueChanged(object sender, EventArgs e)
        {
            int newValue = (int) numMenstruationPeriod.Value;
            if (Program.CurrentWoman.ManualPeriodLength <= MenstruationPeriod.NormalMaximalPeriod && 
                newValue > MenstruationPeriod.NormalMaximalPeriod)
            {
                if (MessageBox.Show(this, "Ты хочешь чтобы цикл был больше 35-ти дней?", "Вот это цикл!",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    numMenstruationPeriod.Value = Program.CurrentWoman.ManualPeriodLength;
                    lblMyCycle2.Text = GetDaysString(Program.CurrentWoman.ManualPeriodLength);
                    return;
                }
            }

            Program.CurrentWoman.ManualPeriodLength = newValue;
            lblMyCycle2.Text = GetDaysString(newValue);
            monthControl.Redraw();
        }

        private void MainForm_MouseLeave(object sender, EventArgs e)
        {
            monthControl.CellPopupControl.Visible = false;
        }

        private void numMenstruationLength_ValueChanged(object sender, EventArgs e)
        {
            lblMenstruationLength2.Text = GetDaysString((int)numMenstruationLength.Value);

            Program.CurrentWoman.DefaultMenstruationLength = (int)numMenstruationLength.Value;

            monthControl.Redraw();
        }

        private void chbDefaultWoman_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Program.CurrentWoman.AssociatedFile))
            {
                Program.Settings.DefaultWomanPath = chbDefaultWoman.Checked ? Program.CurrentWoman.AssociatedFile : string.Empty;
                Program.SaveSettings();
            }
        }

        private void chbAskPassword_CheckedChanged(object sender, EventArgs e)
        {
            Program.CurrentWoman.AllwaysAskPassword = chbAskPassword.Checked;
        }
    }
}
