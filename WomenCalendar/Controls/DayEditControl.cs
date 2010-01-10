using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class DayEditControl : UserControl, ITranslatable
    {
        public DayEditFocus LastFocus { get; set; }

        private CervicalFluid currentCF;
        public CervicalFluid CurrentCF
        {
            get { return currentCF; }
            set
            {
                currentCF = value;
                rbtCF1.Checked = currentCF == CervicalFluid.Tacky;
                rbtCF2.Checked = currentCF == CervicalFluid.Stretchy;
                rbtCF3.Checked = currentCF == CervicalFluid.Water;
            }
        }

        private string currentBBT = string.Empty;
        public string BBT
        {
            get { return currentBBT; }
            set { txtBBT.Text = currentBBT = value; }
        }

        public string Note
        {
            get { return txtNote.Text; }
            set { txtNote.Text = value; }
        }

        public bool HadSex
        {
            get { return chkHadSex.Checked; }
            set { chkHadSex.Checked = value; }
        }

        public int Health
        {
            get { return sliderHealth.Value; }
            set { sliderHealth.Value = value; }
        }

        public DayEditControl()
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();
            rbtCF1.Tag = CervicalFluid.Tacky;
            rbtCF2.Tag = CervicalFluid.Stretchy;
            rbtCF3.Tag = CervicalFluid.Water;
        }

        public void SetFocusTo(DayEditFocus focusTo)
        {
            switch (focusTo)
            {
                case DayEditFocus.Note:
                    txtNote.Focus();
                    break;
                case DayEditFocus.BBT:
                    txtBBT.Focus();
                    break;
            }
        }

        private void txtNote_Leave(object sender, EventArgs e)
        {
            LastFocus = DayEditFocus.Note;
        }

        private void txtBBT_Leave(object sender, EventArgs e)
        {
            LastFocus = DayEditFocus.BBT;
        }

        private void rbtCF_Click(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            CervicalFluid newCF = (CervicalFluid)rb.Tag;
            if (CurrentCF == newCF)
            {
                rb.Checked = false;
                currentCF = CervicalFluid.Undefined;
            }
            else
            {
                currentCF = newCF;
            }
        }

        private void txtBBT_TextChanged(object sender, EventArgs e)
        {
            ValidateBBT();
        }

        public bool ValidateBBT()
        {
            ttBBT.Hide(this.txtBBT);
            if (string.IsNullOrEmpty(txtBBT.Text)) { return true; }

            double res;
            string bbt = txtBBT.Text.Trim();
            if (!double.TryParse(bbt, out res))
            {
                bbt = txtBBT.Text.Trim().Replace(',', '.');
                if (!double.TryParse(bbt, out res))
                {
                    bbt = txtBBT.Text.Trim().Replace('.', ',');
                    if (!double.TryParse(bbt, out res))
                    {
                        ttBBT.Show(TEXT.Get["Wrong_temperature_entered"], txtBBT, txtBBT.Width, txtBBT.Height);
                        currentBBT = string.Empty;
                        return false;
                    }
                }
            }

            if (!BBTCollection.IsBBTInCorrectRange(res))
            {
                ttBBT.Show(TEXT.Get["Wrong_temperature"], txtBBT, txtBBT.Width, txtBBT.Height);
                currentBBT = string.Empty;
                return false;
            }

            currentBBT = res.ToString();

            return true;
        }

        #region ITranslatable Members

        public void ReReadTranslations()
        {
            this.chkHadSex.Text = TEXT.Get["Sex_was"];
            this.lblBadWellbeing.Text = TEXT.Get["Bad_wellbeing"];
            this.lblGoodWellbeing.Text = TEXT.Get["Good_wellbeing"];
            this.grpNote.Text = TEXT.Get["Note_for_day"];
            this.grpBT.Text = TEXT.Get["BBT_full"];
            this.grpHealth.Text = TEXT.Get["Wellbeing"];
            this.grpCF.Text = TEXT.Get["CF_full"];
            this.rbtCF1.Text = TEXT.Get["CF_tacky"];
            this.rbtCF2.Text = TEXT.Get["CF_stretchy"];
            this.rbtCF3.Text = TEXT.Get["CF_water"];
            this.ttCF.ToolTipTitle = TEXT.Get["CF_full"];
            this.ttCF.SetToolTip(this.rbtCF1, TEXT.Get["CF_full_tacky"]);
            this.ttCF.SetToolTip(this.rbtCF2, TEXT.Get["CF_full_stretchy"]);
            this.ttCF.SetToolTip(this.rbtCF3, TEXT.Get["CF_full_water"]);
            this.ttBBT.ToolTipTitle = TEXT.Get["BBT_full"];
        }

        #endregion

    }
}
