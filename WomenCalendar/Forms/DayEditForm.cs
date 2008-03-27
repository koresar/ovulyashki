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
                if (value < sliderEgestaAmount.Minimum || value > sliderEgestaAmount.Maximum) return;
                sliderEgestaAmount.Value = 4 - value;
            }
        }

        public override void AcceptAction()
        {
            if (!ValidateData()) return;
            SaveData();
            DialogResult = DialogResult.OK;
        }

        private void SaveData()
        {
            Woman w = Program.CurrentWoman;
            w.BBT.SetBBT(date, txtBBT.Text);

            if (sliderEgestaAmount.Visible)
            {
                w.Menstruations.SetEgesta(date, EgestaSliderValue);
            }

            w.Notes[date] = txtNote.Text;

            w.HadSex[date] = chkHadSex.Checked;

            w.Health[date] = sliderHealth.Value;
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

        private void LoadForm()
        {
            Text = date.ToLongDateString();

            Woman w = Program.CurrentWoman;

            int egesta = w.Menstruations.GetEgestaAmount(date);
            EgestaSliderValue = egesta;
            sliderEgestaAmount.Visible = egesta >= 0;

            txtBBT.Text = w.BBT.GetBBTString(date);

            string note = w.Notes[date];
            txtNote.Text = (!note.Contains("\r\n")) ? note.Replace("\n", "\r\n") : note;

            chkHadSex.Checked = w.HadSex[date];

            sliderHealth.Value = w.Health[date];
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

        private void Rotate(int days)
        {
            if (!ValidateData()) return;
            SaveData();

            date = date.AddDays(days);
            LoadForm();
        }

        private void DayEditForm_Load(object sender, EventArgs e)
        {
            LoadForm();
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

        private void btnPrevDay_Click(object sender, EventArgs e)
        {
            Rotate(-1);
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            Rotate(1);
        }
    }
}
