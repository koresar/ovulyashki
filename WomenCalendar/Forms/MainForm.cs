using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using System.IO;

namespace WomenCalendar
{
    public partial class MainForm : Form, ITranslatable
    {
        public MainForm()
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();

            xLegend.Collapse();
            toolStrip1.Items.Insert(toolStrip1.Items.IndexOf(toolStripLabelJump) + 1, 
                new ToolStripControlHost(dateTimePicker1));
            helpToolStripButton.Alignment = ToolStripItemAlignment.Right;
        }
        
        #region ITranslatable interface impementation

        public void ReReadTranslations()
        {
            this.newToolStripButton.Text = TEXT.Get["Create_new_woman"];
            this.openToolStripButton.Text = TEXT.Get["Open_woman"];
            this.saveToolStripButton.Text = TEXT.Get["Save_woman"];
            this.exportToExcel.Text = TEXT.Get["Export_woman_to_excel"];
            this.toolStripLabelJump.Text = TEXT.Get["Jump_to_colon"];
            this.toolStripLabelJump.ToolTipText = TEXT.Get["Selected_date"];
            this.prevStripButton.Text = TEXT.Get["Shift_one_month_back"];
            this.nextStripButton.Text = TEXT.Get["Shift_one_month_forward"];
            this.helpToolStripButton.Text = TEXT.Get["Help"];
            this.helpToolStripButton.ToolTipText = TEXT.Get["About_application"];
            this.languageButton.Text = TEXT.Get["Language"];
            this.toolUpdate.Text = TEXT.Get["Update_application"];

            this.xLegend.CaptionText = TEXT.Get["Legend"];
            this.label13.Text = TEXT.Get["Legend_pregn_week"];
            this.label12.Text = TEXT.Get["Legend_conception_day"];
            this.label11.Text = TEXT.Get["Legend_pregnancy"];
            this.label10.Text = TEXT.Get["Legend_ovulation_day"];
            this.label9.Text = TEXT.Get["Legend_future_menses"];
            this.label8.Text = TEXT.Get["Legend_min_conception_probapility"];
            this.label7.Text = TEXT.Get["Legend_menses"];
            this.label6.Text = TEXT.Get["Legend_conceive_girl_day"];
            this.label5.Text = TEXT.Get["Legend_conceive_boy_day"];
            this.label4.Text = TEXT.Get["Legend_had_sex"];
            this.label3.Text = TEXT.Get["Legend_note"];
            this.label2.Text = TEXT.Get["Legend_selected_day"];
            this.label1.Text = TEXT.Get["Ledend_today_day"];

            this.xWoman.CaptionText = TEXT.Get["About_woman"];
            this.btnChangeWoman.Text = TEXT.Get["Change_woman"];
            this.chbAskPassword.Text = TEXT.Get["Always_ask_my_pwd"];
            this.chbDefaultWoman.Text = TEXT.Get["Set_as_default_woman"];
            this.lblMyCycle.Text = TEXT.Get["Special_cycle"];
            this.lblAverageCycle.Text = GenerateWomanInformation();
            this.xDay.CaptionText = TEXT.Get["Day_description"];
            this.Text = TEXT.Get["Ovulyashki"];

            var textLegend = TEXT.Get["Click_to_edit_color"];
            this.toolTipLegend.SetToolTip(this.dayLegendMenstruations, textLegend);
            this.toolTipLegend.SetToolTip(this.dayCellControl1, textLegend);
            this.toolTipLegend.SetToolTip(this.dayCellControl2, textLegend);
            this.toolTipLegend.SetToolTip(this.dayCellControl3, textLegend);
            this.toolTipLegend.SetToolTip(this.dayCellControl4, textLegend);
            this.toolTipLegend.SetToolTip(this.dayCellControl5, textLegend);
            this.toolTipLegend.SetToolTip(this.dayCellControl6, textLegend);
            this.toolTipLegend.SetToolTip(this.dayCellControl7, textLegend);
            this.toolTipLegend.SetToolTip(this.dayCellControl8, textLegend);
            this.toolTipLegend.SetToolTip(this.dayCellControl9, textLegend);
            this.toolTipLegend.SetToolTip(this.dayCellControl10, textLegend);
            this.toolTipLegend.SetToolTip(this.dayCellControl11, textLegend);
        }

        #endregion

        public void UpdateDayInformation(DateTime date)
        {
            lblDayDescription.Text = Program.CurrentWoman.GenerateDayInfo(date);
            xDay.Height = lblDayDescription.Height + 32;
        }

        public void UpdateDayInformationIfFocused(DateTime date)
        {
            if (monthControl.FocusDate == date)
            {
                UpdateDayInformation(date);
            }
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
            chbAskPassword.Checked = Program.CurrentWoman.AllwaysAskPassword;
        }

        public string GenerateWomanInformation()
        {
            StringBuilder sb = new StringBuilder();

            int usedDays = Program.CurrentWoman.AveragePeriodLength == 0 ? 28 : Program.CurrentWoman.AveragePeriodLength;
            sb.Append(TEXT.Get["My_auto_cycle"]);
            sb.Append(usedDays);
            sb.Append(' ');
            sb.Append(TEXT.GetDaysString(usedDays));

            return sb.ToString();
        }


        private void SetNumMenstrulationPriod(int length)
        {
            numMenstruationPeriod.Minimum = (length == 0) ? 0 : 10;
            numMenstruationPeriod.Value = length;
            numMenstruationPeriod.Enabled = !rbAuto.Checked;
        }

        public void RedrawCalendar()
        {
            monthControl.Redraw();
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
                chbDefaultWoman.Checked = (Program.Settings.DefaultWomanPath == Program.CurrentWoman.AssociatedFile);
                monthControl.Redraw();
            }
        }

        private void monthControl_FocusDateChanged(object sender, FocusDateChangedEventArgs e)
        {
            UpdateDayInformation(e.NewDate);
            dateTimePicker1.Value = e.NewDate;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !Program.AskAndSaveCurrentWoman();
            if (!e.Cancel)
            {

                // Beginning the part where we collect all application settings
                if (chbDefaultWoman.Checked)
                {
                    Program.Settings.DefaultWomanPath = Program.CurrentWoman.AssociatedFile;
                }
                // End of the settings collectioning part.

                /*Program.Settings.DefaultWindowIsMaximized = (WindowState == FormWindowState.Maximized);
                if (!Program.Settings.DefaultWindowIsMaximized)
                {
                    Program.Settings.DefaultWindowPosition = Location;
                    Program.Settings.DefaultWindowSize = Size;
                }*/
                Program.SaveSettings();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            monthControl.CreateAndAdjustMonthsAmount(true);
            //monthControl.Redraw();

            monthControl.FocusDate = DateTime.Today;
            chbDefaultWoman.Checked = !string.IsNullOrEmpty(Program.Settings.DefaultWomanPath);

            UpdateWomanInformation();
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
                if (MessageBox.Show(this,
                    TEXT.Get.Format("Cycle_more_than", MenstruationPeriod.NormalMaximalPeriod),
                    TEXT.Get["What_a_cycle"],
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    numMenstruationPeriod.Value = Program.CurrentWoman.ManualPeriodLength;
                    lblMyCycle2.Text = TEXT.GetDaysString(Program.CurrentWoman.ManualPeriodLength);
                    return;
                }
            }

            Program.CurrentWoman.ManualPeriodLength = newValue;
            lblMyCycle2.Text = TEXT.GetDaysString(newValue);
            monthControl.Redraw();
        }

        private void MainForm_MouseLeave(object sender, EventArgs e)
        {
            monthControl.CellPopupControl.Visible = false;
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

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            Program.Settings.DefaultWindowIsMaximized = (WindowState == FormWindowState.Maximized);
            if (WindowState == FormWindowState.Normal)
            {
                Program.Settings.DefaultWindowPosition = Location;
                Program.Settings.DefaultWindowSize = Size;
            }
        }

        private void btnChangeWoman_Click(object sender, EventArgs e)
        {
            if (Program.EditWoman())
            {
                monthControl.Redraw();
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog(this);
        }

        private void exportToExcel_Click(object sender, EventArgs e)
        {
            Program.ExportWoman();
        }

        public void SetWomanName(string name)
        {
            string text;
            if (string.IsNullOrEmpty(name))
            {
                text = TEXT.Get["Ovulyashki"];
            }
            else
            {
                text = TEXT.Get.Format("Ovulyashki_of", name);
            }
            if (!string.IsNullOrEmpty(Program.CurrentWoman.AssociatedFile))
            {
                text += " - " + Path.GetFileNameWithoutExtension(Program.CurrentWoman.AssociatedFile);
            }
            this.Text = text;
        }

        private void languageButton_Click(object sender, EventArgs e)
        {
            this.languageButton.DropDownItems.Clear();
            foreach (var langFilePair in TEXT.FindAllLangFiles())
            {
                CultureInfo cult;
                try
                {
                    cult = CultureInfo.GetCultureInfo(langFilePair.Key);
                }
                catch
                {
                    continue;
                }
                var menuItem = new ToolStripMenuItem();
                menuItem.Name = cult.EnglishName;
                menuItem.Text = cult.NativeName;
                menuItem.Tag = langFilePair;
                menuItem.Click += languageButtonMenuItem_Click;
                menuItem.Enabled = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName != langFilePair.Key;
                this.languageButton.DropDownItems.Add(menuItem);
            }
        }

        private void languageButtonMenuItem_Click(object sender, EventArgs e)
        {
            var pair = (KeyValuePair<string, string>)((sender as ToolStripMenuItem).Tag);
            if (TEXT.ApplyLanguageFile(pair.Key, pair.Value))
            {
                this.ReReadTranslations();
                UpdateDayInformationIfFocused(monthControl.FocusDate);
                monthControl.ReReadTranslations();
                monthControl.Redraw();
            }
        }

        private void toolUpdate_Click(object sender, EventArgs e)
        {
            AppUpdater.TryUpdate();
        }

        private void dayLegendMenstruations_Click(object sender, EventArgs e)
        {
            var dialog = new ColorDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var colorID = (sender as DayCellControl).BackColorIdAppearance;
                Program.Settings.DayCellAppearance.SetColor(colorID, dialog.Color);
                xLegend.Collapse();
                RedrawCalendar();
                xLegend.Expand();
            }
        }

        public void DisableUpdate()
        {
            toolUpdate.Enabled = false;
        }

        public void EnableUpdate()
        {
            toolUpdate.Enabled = true;
        }
    }
}
