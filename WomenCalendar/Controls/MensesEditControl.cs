using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class MensesEditControl : UserControl, ITranslatable
    {
        public MensesEditControl()
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();
        }

        public void Highlight()
        {
            this.numLength.Focus();
            this.pnlSurround.BackColor = Color.Red;
            ttLength.SetToolTip(this.numLength, TEXT.Get["Please_set_menses_length"]);
            ttLength.Show(TEXT.Get["Please_set_menses_length"], numLength, numLength.Width, numLength.Height, 5000);
        }

        public void UnHighlight()
        {
            this.pnlSurround.BackColor = Color.Transparent;
        }

        public int EgestaSliderValue
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

        public int Length
        {
            get
            {
                return (int)numLength.Value;
            }
            set
            {
                numLength.Value = value;
            }
        }

        private void sliderEgestaAmount_ValueChanged(object sender, EventArgs e)
        {
            ttExcreta.SetToolTip(this.sliderEgestaAmount, EgestasCollection.EgestasNames[EgestaSliderValue]);
            ttExcreta.Show(EgestasCollection.EgestasNames[EgestaSliderValue], sliderEgestaAmount, 
                sliderEgestaAmount.Width, sliderEgestaAmount.Height, 5000);
        }

        #region ITranslatable Members

        public void ReReadTranslations()
        {
            this.ttExcreta.ToolTipTitle = TEXT.Get["Excreta_amount"];
            this.ttExcreta.SetToolTip(this.sliderEgestaAmount, EgestasCollection.EgestasNames[EgestaSliderValue]);
            this.ttLength.ToolTipTitle = TEXT.Get["Menses_length"];
            var lengthMessage = TEXT.Get["Please_set_menses_length"];
            this.ttLength.SetToolTip(this.pnlSurround, lengthMessage);
            this.ttLength.SetToolTip(this.lblLength, lengthMessage);
            this.ttLength.SetToolTip(this.lblDays, lengthMessage);
            this.ttLength.SetToolTip(this.numLength, lengthMessage);
            this.grpMenstr.Text = TEXT.Get["Menses"];
            this.lblLength.Text = TEXT.Get["Length_cycle"];
            this.lblDays.Text = TEXT.GetDaysString(Length);
            this.verticalLabel1.Text = TEXT.Get["Intensity"];
        }

        #endregion

        private void numLength_ValueChanged(object sender, EventArgs e)
        {
            lblDays.Text = TEXT.GetDaysString(Length);
        }

        private void MensesEditControl_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                ttLength.Hide(this.numLength);
                ttExcreta.Hide(this.sliderEgestaAmount);
            }
        }
    }
}
