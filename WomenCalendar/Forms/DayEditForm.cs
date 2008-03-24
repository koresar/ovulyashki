﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

        public override void AcceptAction()
        {
            if (!ValidateData()) return;

            Woman w = Program.CurrentWoman;
            if (!string.IsNullOrEmpty(txtBBT.Text))
            {
                w.BBT[date] = Convert.ToDouble(txtBBT.Text);
            }

            if (sliderEgestaAmount.Visible)
            {
                w.Menstruations.SetEgesta(date, EgestaSliderValue);                
            }

            if (!string.IsNullOrEmpty(txtNote.Text))
            {
                w.Notes[date] = txtNote.Text;
            }

            DialogResult = DialogResult.OK;
        }

        private void ShowEgestaTooltip()
        {
            ShowTooltip("Количество выделений", 
                DayCellPopupControl.EgestasNames[EgestaSliderValue], sliderEgestaAmount);
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

        private void DayEditForm_Load(object sender, EventArgs e)
        {
            Text = date.ToLongDateString();

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

            txtBBT.Text = w.BBT.ContainsKey(date) ? w.BBT[date].ToString() : string.Empty;

            if (w.Notes.ContainsKey(date))
            {
                string note = w.Notes[date];
                txtNote.Text = (!note.Contains("\r\n")) ? note.Replace("\n", "\r\n") : note;
            }
        }

        private bool ValidateData()
        {
            HideTooltip(txtBBT);
            if (string.IsNullOrEmpty(txtBBT.Text)) return true;

            double res;
            string bbt = txtBBT.Text.Trim().Replace('.', ',');
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
            }

            if (double.TryParse(txtBBT.Text, out res))
            {
                if (BBTCollection.IsBBTInCorrectRange(res))
                {
                    return true;
                }
                else
                {
                    ShowTooltip("Базальная температура тела", "У человека не может быть такой температуры. Или ты ящерица?", txtBBT);
                }
            }
            else
            {
                ShowTooltip("Базальная температура тела", "Что это за фигню ты сюда ввела? Это не температура!", txtBBT);
            }

            txtBBT.Focus();
            return false;
        }

        private void sliderEgestaAmount_MouseEnter(object sender, EventArgs e)
        {
            ShowEgestaTooltip();
        }

        private void sliderEgestaAmount_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(sliderEgestaAmount);
        }

        private void sliderEgestaAmount_Scroll(object sender, ScrollEventArgs e)
        {
            ShowEgestaTooltip();
        }

        private void txtBBT_Leave(object sender, EventArgs e)
        {
            HideTooltip(txtBBT);
        }
    }
}
