using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class DayEditForm : ModalBaseForm
    {
        private DateTime date;

        public DayEditForm(DateTime date)
        {
            InitializeComponent();
            this.date = date;
        }

        private int EgestaSliderValue
        {
            get
            {
                return 4 - sliderEgestaAmount.Value;
            }
            set
            {
                sliderEgestaAmount.Value = 4 - value;
            }
        }

        private void DayEditForm_Load(object sender, EventArgs e)
        {
            Woman w = Program.CurrentWoman;
            int egesta = w.Menstruations.GetEgestaAmount(date);
            if (egesta >= 0)
            {
                EgestaSliderValue = egesta;
                sliderEgestaAmount.Visible = true;
            }
            else
            {
                sliderEgestaAmount.Visible = false;
            }
        }

    }
}
