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
            public CervicalFluid CF;

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
        private CervicalFluid currentCF;

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
            rbtCF1.Tag = CervicalFluid.Tacky;
            rbtCF2.Tag = CervicalFluid.Stretchy;
            rbtCF3.Tag = CervicalFluid.Water;
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
            this.grpNote.Text = TEXT.Get["Note_for_day"];
            this.grpBT.Text = TEXT.Get["BBT_full"];
            this.grpHealth.Text = TEXT.Get["Wellbeing"];
            this.Text = TEXT.Get["Change_day"];
            this.grpCF.Text = TEXT.Get["CF"];
            this.rbtCF1.Text = TEXT.Get["CF_tacky"];
            this.rbtCF2.Text = TEXT.Get["CF_stretchy"];
            this.rbtCF3.Text = TEXT.Get["CF_water"];
            this.toolTipCF.ToolTipTitle = TEXT.Get["CF_full"];
            this.toolTipCF.SetToolTip(this.rbtCF1, TEXT.Get["CF_full_tacky"]);
            this.toolTipCF.SetToolTip(this.rbtCF2, TEXT.Get["CF_full_stretchy"]);
            this.toolTipCF.SetToolTip(this.rbtCF3, TEXT.Get["CF_full_water"]);
            this.mensesEditControl.ReReadTranslations();
        }

        #endregion

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
                    w.Menstruations.SetPeriodLength(period, mensesEditControl.Length);
                    period.Egestas[date] = mensesEditControl.EgestaSliderValue;
                }
            }
            else
            {
                w.Menstruations.Remove(date);
            }

            w.Notes[date] = txtNote.Text;

            w.HadSexList[date] = chkHadSex.Checked;

            w.Health[date] = sliderHealth.Value;

            w.CFs[date] = currentCF;

            w.Menstruations.ResetOvulyationsDates();

            DayCell.OwnerOneMonthControl.OwnerMonthsControl.Redraw(); // redraw whole calendar
        }

        #region Tooltip functions

        private void ShowMenstButtonToolTip(bool isMenstr)
        {
            ShowTooltip(TEXT.Get["On_off_menses_button"], 
                !isMenstr ? TEXT.Get["Set_day_as_menses_start"] : TEXT.Get["Cancel_these_menses"], chkMentrustions);
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
                mensesEditControl.Length = period.Length;
                int egesta = period.Egestas[date];
                mensesEditControl.EgestaSliderValue = egesta;
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

            currentCF = w.CFs[date];
            rbtCF1.Checked = currentCF == CervicalFluid.Tacky;
            rbtCF2.Checked = currentCF == CervicalFluid.Stretchy;
            rbtCF3.Checked = currentCF == CervicalFluid.Water;

            initialData = CollectDayData();
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
                MenstrLength = mensesEditControl.Length,
                Egesta = mensesEditControl.EgestaSliderValue,
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
                    if (!Program.CurrentWoman.Menstruations.Add(date, mensesEditControl.Length))
                    {
                        return false;
                    }
                    Program.CurrentWoman.Menstruations.SetEgesta(date, mensesEditControl.EgestaSliderValue);
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

        private void rbtCF_Click(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            CervicalFluid newCF = (CervicalFluid)rb.Tag;
            if (currentCF == newCF)
            {
                rb.Checked = false;
                currentCF = CervicalFluid.Undefined;
            }
            else
            {
                currentCF = newCF;
            }
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
                    mensesEditControl.UnHighlight();
                    break;
                case DayEditFocus.BBT:
                    txtBBT.Focus();
                    mensesEditControl.UnHighlight();
                    break;
                case DayEditFocus.Length:
                    mensesEditControl.Highlight();
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
                this.Width = mensesEditControl.Location.X + mensesEditControl.Size.Width + 10;
                chkMentrustions.Image = global::WomenCalendar.Properties.Resources.dropNot_Image;
                chkMentrustions.Text = "<<          <<";
                this.mensesEditControl.Visible = true;
                if (initialData != null && initialData.HasMenstr != true)
                { // this is first time we expand the dialog, this means user want to add new menstr. day.
                    SetFocusTo(DayEditFocus.Length);
                }
            }
            else
            {
                this.Width = chkMentrustions.Location.X + chkMentrustions.Size.Width + 10;
                chkMentrustions.Image = global::WomenCalendar.Properties.Resources.drop_Image;
                chkMentrustions.Text = ">>          >>";
                this.mensesEditControl.Visible = false;
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

        private void chkMentrustions_MouseEnter(object sender, EventArgs e)
        {
            ShowMenstButtonToolTip(chkMentrustions.Checked);
        }

        private void chkMentrustions_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(chkMentrustions);
        }

        #endregion
    }
}
