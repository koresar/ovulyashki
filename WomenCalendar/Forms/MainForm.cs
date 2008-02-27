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
            lblWomanDescription.Text = string.Empty;
        }

        public void UpdateDayInformation(DateTime date)
        {
            lblDayDescription.Text = GenerateDayInfo(date);
            xDay.Height = lblDayDescription.Height + 32;
        }

        public void UpdateWomanInformation()
        {
            rbManual.Checked = Program.CurrentWoman.UseManualPeriodLength;
            rbAuto.Checked = !rbManual.Checked;
            if (rbAuto.Checked)
            {
                Program.CurrentWoman.ManualPeriodLength = Program.CurrentWoman.AveragePeriodLength;
            }

            lblWomanDescription.Text = GenerateWomanInformation();

            SetNumMenstrulationPriod(Program.CurrentWoman.ManualPeriodLength);

            numMenstruationLength.Value = Program.CurrentWoman.DefaultMenstruationLength;
        }

        public string GenerateWomanInformation()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Средний цикл: ");
            sb.Append(Program.CurrentWoman.AveragePeriodLength == 0 ? 28 : Program.CurrentWoman.AveragePeriodLength);
            sb.Append(" дней");

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
                Program.SaveSettings();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.LoadSettings();

            monthControl.FocusDate = DateTime.Today;
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
            if (Program.CurrentWoman.ManualPeriodLength <= 35 && newValue > 35)
            {
                if (MessageBox.Show(this, "Ты хочешь чтобы цикл был больше 35-ти дней?", "Вот это цикл!",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    numMenstruationPeriod.Value = Program.CurrentWoman.ManualPeriodLength;
                    return;
                }
            }

            Program.CurrentWoman.ManualPeriodLength = newValue;
            monthControl.Redraw();
        }

        private void MainForm_MouseLeave(object sender, EventArgs e)
        {
            monthControl.CellPopupControl.Visible = false;
        }

        private void numEmnstruationLength_ValueChanged(object sender, EventArgs e)
        {
            if (numMenstruationLength.Value == 1)
                lblMenstruationLength2.Text = "день";
            else if (numMenstruationLength.Value < 5)
                lblMenstruationLength2.Text = "дня";
            else
                lblMenstruationLength2.Text = "дней";

            Program.CurrentWoman.DefaultMenstruationLength = (int)numMenstruationLength.Value;

            monthControl.Redraw();
        }
    }
}
