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

            monthControl.FocusDate = DateTime.Today;
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
            StringBuilder sb = new StringBuilder();

            sb.Append("Дата: ");
            sb.Append(date.ToShortDateString());
            if (date == DateTime.Today)
            {
                sb.Append(" (сегодня)");
            }

            if (Program.CurrentWoman.MenstruationDates.Contains(date))
            {
                sb.AppendLine();
                sb.Append("Менструальный день");
            }



            // got to be last
            string text;
            if (Program.CurrentWoman.Notes.TryGetValue(date, out text))
            {
                sb.AppendLine();
                sb.Append("Заметка: ");
                sb.Append(text);
            }

            lblDayDescription.Text = sb.ToString();

            xDay.Height = lblDayDescription.Height + 32;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !AskAndSaveCurrentWoman();
        }
    }
}
