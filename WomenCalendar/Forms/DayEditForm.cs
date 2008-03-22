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
            Text = date.ToLongDateString();

            Woman w = Program.CurrentWoman;
            int egesta = w.Menstruations.GetEgestaAmount(date);
            if (egesta >= 0)
            {
                EgestaSliderValue = egesta;
                sliderEgestaAmount.Visible = true; // TODO: add popup!!!
            }
            else
            {
                sliderEgestaAmount.Visible = false;
            }

            txtBBT.Text = w.BBT.ContainsKey(date) ? w.BBT[date].ToString() : string.Empty;

            if (w.Notes.ContainsKey(date))
            {
                string note = w.Notes[date];
                txtNote.Text = (!note.Contains("\r\n")) ? note.Replace("\n", "\r\n") : note;
            }
        }

        public override void AcceptAction()
        {
            Woman w = Program.CurrentWoman;
            if (!string.IsNullOrEmpty(txtBBT.Text))
            {
                w.BBT[date] = GetDouble(txtBBT.Text);
            }

            if (sliderEgestaAmount.Visible)
            {
                w.Menstruations.SetEgesta(date, EgestaSliderValue);                
            }

            w.Notes[date] = txtNote.Text;

            DialogResult = DialogResult.OK;
        }

        private double GetDouble(string bbt)
        {
            return Convert.ToDouble(txtBBT.Text);            
        }
    }
}
