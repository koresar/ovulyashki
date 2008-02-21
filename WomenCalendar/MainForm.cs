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
            if (AskAndSaveCurrentWoman())
            {
                Program.CurrentWoman = new Woman();
                monthControl.Redraw();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveCurrentWoman();
        }

        private bool AskAndSaveCurrentWoman()
        {
            DialogResult res =
                MessageBox.Show(this, "Сохранить эту женщину, прежде чем продолжить?", Text,
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
                    break;
            }
            return true;
        }

        private bool SaveCurrentWoman()
        {
            if (string.IsNullOrEmpty(Program.CurrentWoman.AssociatedFile))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Woman files (*.woman)|*.woman";
                dialog.AddExtension = true;
                dialog.DefaultExt = ".woman";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Woman.SaveTo(Program.CurrentWoman, dialog.FileName);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Woman.SaveTo(Program.CurrentWoman, Program.CurrentWoman.AssociatedFile);
            }
            return true;
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (AskAndSaveCurrentWoman())
            {
                OpenWoman();
            }
        }

        private bool OpenWoman()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Woman files (*.woman)|*.woman";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                monthControl.Visible = false;
                Program.CurrentWoman = Woman.ReadFrom(dialog.FileName);
                Program.Settings.DefaultWomanPath = dialog.FileName;
                monthControl.Visible = true;
                return true;
            }
            return false;
        }

        private void monthControl_FocusDateChanged(object sender, FocusDateChangedEventArgs e)
        {
            UpdateDayInformation(e.NewDate);
        }

        public void UpdateDayInformation(DateTime date)
        {
            lblDayDescription.Text = GenerateDayInfo(date);
            xDay.Height = lblDayDescription.Height + 32;
        }

        public void UpdateWomanInformation()
        {
            if (rbAuto.Checked)
            {
                Program.CurrentWoman.ManualPeriodLength = Program.CurrentWoman.AveragePeriodLength;
            }

            lblWomanDescription.Text = GenerateWomanInformation();

            numMenstruationPeriod.Value = Program.CurrentWoman.ManualPeriodLength;
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
                sb.Append("Менструальный день");
            }

            if (Program.CurrentWoman.IsPredictedAsMenstruationDay(date))
            {
                sb.AppendLine();
                sb.Append("Вероятны выделения");
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !AskAndSaveCurrentWoman();
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
            if (rbAuto.Checked)
            {
                numMenstruationPeriod.Value = Program.CurrentWoman.AveragePeriodLength;
            }
        }

        private void numMenstruationPeriod_ValueChanged(object sender, EventArgs e)
        {
            Program.CurrentWoman.ManualPeriodLength = (int)numMenstruationPeriod.Value;
        }
    }
}
