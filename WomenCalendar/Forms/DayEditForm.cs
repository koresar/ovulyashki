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

            public override bool Equals(object obj)
            {
                DayData d = obj as DayData;
                return d.BBT == BBT && d.HadSex == HadSex && d.Health == Health && d.Note == Note && 
                    d.HasMenstr == HasMenstr && d.MenstrLength == MenstrLength && d.Egesta == Egesta;
            }
        }

        private DayData initialData;
        private DayCellControl DayCell;
        private DateTime date;
        private DayEditFocus defaultFocus;
        private DayEditFocus lastFocus;

        public DayEditForm(DayCellControl dayCell)
            : this(dayCell, DayEditFocus.Note)
        {
        }

        public DayEditForm(DayCellControl dayCell, DayEditFocus focus)
        {
            InitializeComponent();
            this.date = dayCell.Date;
            DayCell = dayCell;
            defaultFocus = focus;
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

        private void ShowEgestaTooltip()
        {
            ShowTooltip("Количество выделений", DayCellPopupControl.EgestasNames[EgestaSliderValue], sliderEgestaAmount);
        }

        private void ShowMenstButtonToolTip(bool isMenstr)
        {
            ShowTooltip("Кнопка включения/выключения менструашек", 
                !isMenstr ? "Установить этот день как начало менструашек" : "Отменить эти менструашки", chkMentrustions);
        }

        private void ShowLengthTooltip()
        {
            ShowTooltip("Длительность менструашек", "Укажи сколько дней они шли!", numMenstruationLength);
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
            };
        }

        private bool ValidateData()
        {
            HideTooltip(txtBBT);
            if (!string.IsNullOrEmpty(txtBBT.Text))
            {
                double res;
                string bbt = txtBBT.Text.Trim().Replace(',', '.');
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

                if (double.TryParse(txtBBT.Text, out res))
                {
                    if (!BBTCollection.IsBBTInCorrectRange(res))
                    {
                        ShowTooltip("Базальная температура тела", "У человека не может быть такой температуры. Или ты ящерица?", txtBBT);
                        return false;
                    }
                }
                else
                {
                    ShowTooltip("Базальная температура тела", "Что это за фигню ты сюда ввела? Это не температура!", txtBBT);
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
            lblMenstruationLength.Text = Woman.GetDaysString((int)numMenstruationLength.Value);
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
                this.grpOv.Visible = true;
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
                this.grpOv.Visible = false;
            }
        }

        private void chkMentrustions_MouseEnter(object sender, EventArgs e)
        {
            ShowMenstButtonToolTip(chkMentrustions.Checked);
        }

        private void chkMentrustions_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip(chkMentrustions);
        }

        private bool DataChanged
        {
            get
            {
                return !initialData.Equals(CollectDayData());
            }
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

        private void txtNote_Leave(object sender, EventArgs e)
        {
            lastFocus = DayEditFocus.Note;
        }
    }
}
