using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public enum DayEditFocus { Note, BBT, Length };

    public partial class DayEditForm : ModalBaseForm
    {
        class DayData
        {
            public string BBT;
            public bool HadSex;
            public int Health;
            public string Note;
            public bool HasMenstr;
            public decimal MenstrLength;
            public int Egesta;
            public int CF;

            public override bool Equals(object obj)
            {
                DayData d = obj as DayData;
                return d.BBT == BBT && d.HadSex == HadSex && d.Health == Health && d.Note == Note && 
                    d.HasMenstr == HasMenstr && d.MenstrLength == MenstrLength && d.Egesta == Egesta && d.CF == CF;
            }

            public override int GetHashCode()
            {
                return BBT.GetHashCode() ^ HadSex.GetHashCode() ^ Health.GetHashCode() ^ Note.GetHashCode() ^
                    HasMenstr.GetHashCode() ^ MenstrLength.GetHashCode() ^ Egesta.GetHashCode() ^ CF.GetHashCode();
            }
        }

        private DayData initialData;
        private DayCellControl DayCell;
        private DateTime date;
        private DayEditFocus defaultFocus;
        private DayEditFocus lastFocus;
        private int currentCF;

        public DayEditForm(DayCellControl dayCell)
            : this(dayCell, DayEditFocus.Note)
        {
        }

        public DayEditForm(DayCellControl dayCell, DayEditFocus focus)
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();
            this.date = dayCell.Date;
            DayCell = dayCell;
            defaultFocus = focus;
        }

        #region ITranslatable interface impementation

        public new void ReReadTranslations()
        {
            base.ReReadTranslations();
            this.btnPrevDay.Text = "<< " + TEXT.Get["Previous_day"];
            this.btnNextDay.Text = TEXT.Get["Next_day"] + " >>";
            this.chkHadSex.Text = TEXT.Get["Sex_was"];
            this.label2.Text = TEXT.Get["Bad_wellbeing"];
            this.label3.Text = TEXT.Get["Good_wellbeing"];
            this.grpMenstr.Text = TEXT.Get["Menses"];
            this.label1.Text = TEXT.Get["Length_cycle"];
            this.verticalLabel1.Text = TEXT.Get["Intensity"];
            this.grpNote.Text = TEXT.Get["Note_for_day"];
            this.grpBT.Text = TEXT.Get["BBT_full"];
            this.grpHealth.Text = TEXT.Get["Wellbeing"];
            this.Text = TEXT.Get["Change_day"];
        }

        #endregion

        private int EgestaSliderValue
        {
            get
            {
                return 4 - sliderEgestaAmount.Value;
            }
            set
            {
                if (value < sliderEgestaAmount.Minimum || value > sliderEgestaAmount.Maximum) return;
                sliderEgestaAmount.Value = 4 - value;
            }
        }

        public override void AcceptAction()
        {
            if (DataChanged)
            {
                if (!ValidateData()) return;
                SaveData();
                Program.ApplicationForm.UpdateDayInformationIfFocused(date);
            }

            DialogResult = DialogResult.OK;
        }

        private void SaveData()
        {
            Woman w = Program.CurrentWoman;
            w.BBT.SetBBT(date, txtBBT.Text);

            if (chkMentrustions.Checked)
            {
                MenstruationPeriod period = w.Menstruations.GetPeriodByDate(date);
                if (period != null)
                {
                    w.Menstruations.SetPeriodLength(period, (int)numMenstruationLength.Value);
                    period.Egestas[date] = EgestaSliderValue;
                }
            }
            else
            {
                w.Menstruations.Remove(date);
            }

            w.Notes[date] = txtNote.Text;

            w.HadSexList[date] = chkHadSex.Checked;

            w.Health[date] = sliderHealth.Value;

            DayCell.OwnerOneMonthControl.OwnerMonthsControl.Redraw(); // redraw whole calendar
        }

        #region Tooltip functions

        private void ShowEgestaTooltip()
        {
            ShowTooltip(TEXT.Get["Excreta_amount"], DayCellPopupControl.EgestasNames[EgestaSliderValue], sliderEgestaAmount);
        }

        private void ShowMenstButtonToolTip(bool isMenstr)
        {
            ShowTooltip(TEXT.Get["On_off_menses_button"], 
                !isMenstr ? TEXT.Get["Set_day_as_menses_start"] : TEXT.Get["Cancel_these_menses"], chkMentrustions);
        }

        private void ShowLengthTooltip()
        {
            ShowTooltip(TEXT.Get["Menses_length"], TEXT.Get["Please_set_menses_length"], numMenstruationLength);
        }

        private void ShowEditSchedulesTooltip()
        {
            ShowTooltip("Кнопка редактирования расписаний", 
                "Здесь можно отредактировать список расписаний", 
                btnSchedulesEdit);
        }

        private void ShowPlannedSchedulesTooltip()
        {
            ShowTooltip("Это те лекарства, которые надо принять", txtSchedulesPlanned.Text, txtSchedulesPlanned);
        }

        private void ShowTakenSchedulesTooltip()
        {
            ShowTooltip("Это лекарства, которые ты приняла в этот день", txtSchedulesComplete.Text, txtSchedulesComplete);
        }

        private void ShowEditTakenSchedulesListTooltip()
        {
            ShowTooltip("Кнопка редактирования списка принятых лекарств", 
                "Нажми эту кнопку, чтобы добавить или удалить лекарство из списка.",
                txtSchedulesComplete);
        }

        private void HideTooltip(IWin32Window control)
        {
            toolTip.Hide(control);
        }

        private void ShowTooltip(string caption, string text, Control control)
        {
            toolTip.ToolTipTitle = caption;
            toolTip.Show(text, control, control.Width, control.Height);
        }

        #endregion

        private void LoadForm()
        {
            Text = date.ToLongDateString();

            Woman w = Program.CurrentWoman;

            MenstruationPeriod period = w.Menstruations.GetPeriodByDate(date);
            if (period != null)
            {
                numMenstruationLength.Value = period.Length;
                int egesta = period.Egestas[date];
                EgestaSliderValue = egesta;
                ShowOv(true);
                chkMentrustions.Enabled = period.StartDay == date;
            }
            else
            {
                ShowOv(false);
                chkMentrustions.Enabled = true;
            }

            txtBBT.Text = w.BBT.GetBBTString(date);

            string note = w.Notes[date];
            txtNote.Text = (!note.Contains("\r\n")) ? note.Replace("\n", "\r\n") : note;

            chkHadSex.Checked = w.HadSexList[date];

            sliderHealth.Value = w.Health[date];

            initialData = CollectDayData();

            UpdatePlannedSchedulesList();
            UpdateCompleteSchedulesList();
        }

        private DayData CollectDayData()
        {
            return new DayData()
            {
                BBT = txtBBT.Text,
                HadSex = chkHadSex.Checked,
                Health = sliderHealth.Value,
                Note = txtNote.Text,
                HasMenstr = chkMentrustions.Checked,
                MenstrLength = numMenstruationLength.Value,
                Egesta = EgestaSliderValue,
                CF = currentCF,
            };
        }

        private bool ValidateData()
        {
            HideTooltip(txtBBT);
            if (!string.IsNullOrEmpty(txtBBT.Text))
            {
                double res;
                string bbt = txtBBT.Text.Trim();
                if (double.TryParse(bbt, out res))
                {
                    txtBBT.Text = bbt;
                }
                else
                {
                    bbt = txtBBT.Text.Trim().Replace(',', '.');
                    if (double.TryParse(bbt, out res))
                    {
                        txtBBT.Text = bbt;
                    }
                    else
                    {
                        bbt = txtBBT.Text.Trim().Replace('.', ',');
                        if (double.TryParse(bbt, out res))
                        {
                            txtBBT.Text = bbt;
                        }
                    }
                }

                if (double.TryParse(txtBBT.Text, out res))
                {
                    if (!BBTCollection.IsBBTInCorrectRange(res))
                    {
                        ShowTooltip(TEXT.Get["BBT_full"], TEXT.Get["Wrong_temperature"], txtBBT);
                        return false;
                    }
                }
                else
                {
                    ShowTooltip(TEXT.Get["BBT_full"], TEXT.Get["Wrong_temperature_entered"], txtBBT);
                    return false;
                }
            }

            if (chkMentrustions.Checked)
            {
                MenstruationPeriod period = Program.CurrentWoman.Menstruations.GetPeriodByDate(date);
                if (period == null)
                { // this is new period user want to add
                    if (!Program.CurrentWoman.Menstruations.Add(date, (int)numMenstruationLength.Value))
                    {
                        return false;
                    }
                    Program.CurrentWoman.Menstruations.SetEgesta(date, EgestaSliderValue);
                }
            }

            return true;
        }

        /// <summary>
        /// Shift active day.
        /// </summary>
        /// <param name="days">Amount of days from current to shift. Can be either negative or positive.</param>
        private void Rotate(int days)
        {
            if (DataChanged)
            {
                if (!ValidateData()) return;
                SaveData();
                Program.ApplicationForm.UpdateDayInformationIfFocused(date);
            }

            date = date.AddDays(days);
            LoadForm();
            SetFocusTo(lastFocus);
            lastFocus = txtBBT.Focused ? DayEditFocus.BBT : DayEditFocus.Note;
        }

        private void DayEditForm_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void sliderEgestaAmount_Scroll(object sender, ScrollEventArgs e)
        {
            ShowEgestaTooltip();
        }

        private void txtBBT_Leave(object sender, EventArgs e)
        {
            HideTooltip(txtBBT);
            lastFocus = DayEditFocus.BBT;
        }

        private void btnPrevDay_Click(object sender, EventArgs e)
        {
            Rotate(-1);
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            Rotate(1);
        }

        private void numMenstruationLength_ValueChanged(object sender, EventArgs e)
        {
            lblMenstruationLength.Text = TEXT.GetDaysString((int)numMenstruationLength.Value);
        }

        private void DayEditForm_Shown(object sender, EventArgs e)
        {
            SetFocusTo(defaultFocus);
        }

        private void SetFocusTo(DayEditFocus focusTo)
        {
            switch (focusTo)
            {
                case DayEditFocus.Note:
                    txtNote.Focus();
                    pnlSurroundMentsLength.BackColor = Color.Transparent;
                    break;
                case DayEditFocus.BBT:
                    txtBBT.Focus();
                    pnlSurroundMentsLength.BackColor = Color.Transparent;
                    break;
                case DayEditFocus.Length:
                    numMenstruationLength.Focus();
                    pnlSurroundMentsLength.BackColor = Color.Red;
                    ShowLengthTooltip();
                    break;
            }
        }

        private void chkMentrustions_CheckedChanged(object sender, EventArgs e)
        {
            HideTooltip(chkMentrustions);
            ShowOv(chkMentrustions.Checked);
        }

        private void ShowOv(bool show)
        {
            if (chkMentrustions.Checked != show)
            {
                chkMentrustions.Checked = show;
            }

            if (show)
            {
                this.Width = 424;
                chkMentrustions.Image = global::WomenCalendar.Properties.Resources.dropNot_Image;
                chkMentrustions.Text = "<<          <<";
                this.grpMenstr.Visible = true;
                if (initialData != null && initialData.HasMenstr != true)
                { // this is first time we expand the dialog, this means user want to add new menstr. day.
                    SetFocusTo(DayEditFocus.Length);
                }
            }
            else
            {
                this.Width = 330;
                chkMentrustions.Image = global::WomenCalendar.Properties.Resources.drop_Image;
                chkMentrustions.Text = ">>          >>";
                this.grpMenstr.Visible = false;
            }
        }

        private void txtNote_Leave(object sender, EventArgs e)
        {
            lastFocus = DayEditFocus.Note;
        }

        private bool DataChanged
        {
            get
            {
                return !initialData.Equals(CollectDayData());
            }
        }

        #region MouseEnter and MouseLeave event handlers

        private void sliderEgestaAmount_MouseEnter(object sender, EventArgs e)
        {
            ShowEgestaTooltip();
        }

        private void sliderEgestaAmount_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(sliderEgestaAmount);
        }

        private void chkMentrustions_MouseEnter(object sender, EventArgs e)
        {
            ShowMenstButtonToolTip(chkMentrustions.Checked);
        }

        private void chkMentrustions_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(chkMentrustions);
        }

        private void lblMenstruationLength_MouseEnter(object sender, EventArgs e)
        {
            ShowLengthTooltip();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            ShowLengthTooltip();
        }

        private void pnlSurroundMentsLength_MouseEnter(object sender, EventArgs e)
        {
            ShowLengthTooltip();
        }

        private void lblMenstruationLength_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(numMenstruationLength);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(numMenstruationLength);
        }

        private void pnlSurroundMentsLength_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(numMenstruationLength);
        }

        private void btnSchedulesEdit_MouseEnter(object sender, EventArgs e)
        {
            ShowEditSchedulesTooltip();
        }

        private void btnSchedulesEdit_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(btnSchedulesEdit);
        }

        private void txtSchedulesPlanned_MouseEnter(object sender, EventArgs e)
        {
            ShowPlannedSchedulesTooltip();
        }

        private void txtSchedulesPlanned_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(txtSchedulesPlanned);
        }

        private void txtSchedulesComplete_MouseEnter(object sender, EventArgs e)
        {
            ShowTakenSchedulesTooltip();
        }

        private void txtSchedulesComplete_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(txtSchedulesComplete);
        }

        private void btnEditSchedulesCompleteList_MouseEnter(object sender, EventArgs e)
        {
            ShowEditTakenSchedulesListTooltip();
        }

        private void btnEditSchedulesCompleteList_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(btnEditCompleteSchedulesList);
        }

        #endregion

        private void btnSchedulesEdit_Click(object sender, EventArgs e)
        {
            if (new SchedulesEditForm(date).ShowDialog() == DialogResult.OK)
            {
                UpdatePlannedSchedulesList();
            }
        }

        private void btnEditCompleteSchedulesList_Click(object sender, EventArgs e)
        {
            schedulesContextMenu.Items.Clear();
            //foreach ()
            {
                //string itemText = drug.Name;
                //bool isTaken = takenList.Contains(drug);
                //bool isPlanned = plannedList.Contains(drug);
                //if (isTaken && !isPlanned)
                //{
                //    itemText = itemText + "\nЭто лекарство: Принято";
                //}
                //else if (isPlanned && !isTaken)
                //{
                //    itemText = itemText + "\nЭто лекарство: Надо бы принять";
                //}
                //else if (isPlanned && isTaken)
                //{
                //    itemText = itemText + "\nЭто лекарство: Запланировано и принято";
                //}

                //var item = schedulesContextMenu.Items.Add(itemText, null, schedulesMenu_Click);
                //item.Tag = drug;
                //if (!string.IsNullOrEmpty(drug.Description)) item.ToolTipText = drug.Description;
            }

            schedulesContextMenu.Show(this, this.PointToClient(MousePosition));
        }

        private void schedulesMenu_Click(object sender, EventArgs e)
        {
            //Program.CurrentWoman.TakenDrugs.Switch(date, (sender as ToolStripItem).Tag as Drug);
            UpdateCompleteSchedulesList();
        }

        private void UpdateCompleteSchedulesList()
        {
            //var list = Program.CurrentWoman.TakenDrugs.At(date);
            //if (list.Count == 0) return;
            //var sb = new StringBuilder();
            //foreach (var drug in list) sb.Append((sb.Length == 0 ? string.Empty : ", ") + drug.Name);
            //txtDrugsTaken.Text = sb.ToString();
        }

        private void UpdatePlannedSchedulesList()
        {
            //var list = Program.CurrentWoman.PlannedDrugs.At(date);
            //if (list.Count == 0) return;
            //var sb = new StringBuilder();
            //foreach (var drug in list) sb.Append(drug.Name + (sb.Length == 0 ? string.Empty : ", "));
            //txtDrugsPlanned.Text = sb.ToString();
        }

        private void rbtCF_Click(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            int newCF = int.Parse(rb.Tag as string);
            if (currentCF == newCF)
            {
                rb.Checked = false;
                currentCF = 0;
            }
            else
            {
                currentCF = newCF;
            }
        }
    }
}
