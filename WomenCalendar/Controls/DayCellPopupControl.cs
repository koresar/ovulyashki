using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class DayCellPopupControl : UserControl
    {
        private MonthsControl ownerMonthsControl;

        private bool initializing;

        private DayCellControl dayCell;

        public DayCellPopupControl()
        {
            this.InitializeComponent();
            this.Visible = false;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        public DayCellPopupControl(MonthsControl ownerMonthsControl)
            : this()
        {
            this.ownerMonthsControl = ownerMonthsControl;
            this.Parent = this.ownerMonthsControl;
        }

        public new bool Visible
        {
            get
            {
                return base.Visible;
            }

            set
            {
                base.Visible = value;
                this.HideTooltip();
            }
        }

        public bool Forbidden { get; set; }

        private int EgestaSliderValue
        {
            get
            {
                return 4 - this.sliderEgestaAmount.Value;
            }

            set
            {
                this.sliderEgestaAmount.Value = 4 - value;
            }
        }

        /// <summary>
        /// Popup the control above the given control.
        /// </summary>
        /// <param name="dayCell">The control to popup above.</param>
        public void ShowAbove(DayCellControl dayCell)
        {
            if (this.Forbidden)
            {
                return;
            }

            this.initializing = true;

            this.HideTooltip();
            this.dayCell = dayCell;
            this.BringToFront();

            this.PaintEgesta();

            this.sliderHealth.Value = Program.CurrentWoman.Health[this.dayCell.Date];

            this.pictureNote.Visible = Program.CurrentWoman.Notes.ContainsKey(this.dayCell.Date);

            this.lblDay.Text = this.dayCell.Date.Day.ToString();

            this.BackColor = this.dayCell.BackColor;

            Point newLocation = this.dayCell.Location;
            newLocation.Offset(-this.dayCell.Size.Width / 2, -this.dayCell.Size.Height / 2);
            this.Location = this.ownerMonthsControl.PointToClient(this.dayCell.OwnerOneMonthControl.PointToScreen(newLocation));

            this.PaintBBT();

            this.PaintHadSex();

            this.pictureAlarm.Visible = Program.CurrentWoman.Schedules.HasAFiredSchedule(this.dayCell.Date);

            if (this.Visible == false)
            {
                this.Visible = true;
            }

            this.initializing = false;
        }

        private void PaintEgesta()
        {
            int egesta = Program.CurrentWoman.Menstruations.GetEgestaAmount(this.dayCell.Date);
            if (egesta >= 0)
            {
                this.EgestaSliderValue = egesta;
                this.sliderEgestaAmount.Visible = true;
            }
            else
            {
                this.sliderEgestaAmount.Visible = false;
            }
        }

        private void PaintBBT()
        {
            double bbt = Program.CurrentWoman.BBT.GetBBT(this.dayCell.Date);
            this.lblBBT.Visible = true;
            if (bbt != 0)
            {
                this.lblBBT.ForeColor = SystemColors.ControlText;
                this.lblBBT.Text = "t°" + bbt.ToString("##.##");
            }
            else
            {
                Color c = Color.FromArgb((int)(this.BackColor.R * 0.9), (int)(this.BackColor.G * 0.9), (int)(this.BackColor.B * 0.9));
                this.lblBBT.ForeColor = c;
                this.lblBBT.Text = "t°36.6";
            }
        }

        private void PaintHadSex()
        {
            if (Program.CurrentWoman.HadSexList[this.dayCell.Date])
            {
                this.lblHadSex.ForeColor = Color.Red;
            }
            else
            {
                Color c = Color.FromArgb((int)(this.BackColor.R * 0.9), (int)(this.BackColor.G * 0.9), (int)(this.BackColor.B * 0.9));
                this.lblHadSex.ForeColor = c;
            }
        }

        private void ShowDayEditForm(DayEditFocus focus)
        {
            this.Visible = false;
            new DayEditForm(this.dayCell.Date, focus).ShowDialog(this);
        }

        private void HideTooltip()
        {
            this.toolTip.Hide(this);
        }

        private void ShowEgestaTooltip()
        {
            this.ShowTooltip(TEXT.Get["Amount_of_bleeding"], EgestasCollection.EgestasNames[this.EgestaSliderValue]);
        }

        private void ShowHasSexToolTip()
        {
            this.ShowTooltip(
                TEXT.Get["Sex"],
                TEXT.Get[Program.CurrentWoman.HadSexList[this.dayCell.Date] ? "Sex_was" : "Sex_was_not"]);
        }

        private void ShowNoteToolTip()
        {
            this.ShowTooltip(TEXT.Get["Note"], Program.CurrentWoman.Notes[this.dayCell.Date]);
        }

        private void ShowHealthTooltip()
        {
            this.ShowTooltip(TEXT.Get["Wellbeing"], TEXT.Get.Format("N_of_10", this.sliderHealth.Value.ToString()));
        }

        private void ShowBBTTooltip()
        {
            var bbt = Program.CurrentWoman.BBT.GetBBT(this.dayCell.Date);
            this.ShowTooltip(TEXT.Get["BBT_full"], bbt == 0 ? TEXT.Get["Not_set_f"] : bbt.ToString());
        }

        private void ShowDayInfoTooltip()
        {
            var info = Program.CurrentWoman.GenerateDayInfo(this.dayCell.Date);
            this.ShowTooltip(TEXT.Get["Click_to_edit"], info);
        }        

        private void ShowTooltip(string caption, string text)
        {
            toolTip.ToolTipTitle = caption;
            toolTip.Show(text, this, Width, Height);
        }

        private void ShowSchedulesTooltip()
        {
            string text = Program.CurrentWoman.Schedules.GetFormattedSchedulesText(this.dayCell.Date);
            // TODO
        }

        private void DayCellPopupControl_MouseClick(object sender, MouseEventArgs e)
        {
            this.ownerMonthsControl.FocusDate = this.dayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                this.ownerMonthsControl.ShowDayContextMenu();
            }
        }

        private void DayCellPopupControl_DoubleClick(object sender, EventArgs e)
        {
            this.ShowDayEditForm(DayEditFocus.Note);
        }

        private void Day_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ownerMonthsControl.FocusDate = this.dayCell.Date;
                this.ShowDayEditForm(DayEditFocus.Note);
            }
            else
            {
                this.DayCellPopupControl_MouseClick(sender, e);
            }
        }

        private void PictureNote_MouseClick(object sender, MouseEventArgs e)
        {
            this.DayCellPopupControl_MouseClick(sender, e);
        }

        private void PictureNote_MouseEnter(object sender, EventArgs e)
        {
            this.ShowNoteToolTip();
        }

        private void PictureNote_MouseLeave(object sender, EventArgs e)
        {
            this.HideTooltip();
        }

        private void DayCellPopupControl_MouseLeave(object sender, EventArgs e)
        {
            if (!this.ClientRectangle.Contains(this.PointToClient(MousePosition)))
            {
                this.Visible = false;
            }
        }

        private void SliderEgestaAmount_MouseEnter(object sender, EventArgs e)
        {
            this.ShowEgestaTooltip();
        }

        private void SliderEgestaAmount_MouseLeave(object sender, EventArgs e)
        {
            this.HideTooltip();
        }

        private void SliderHealth_MouseEnter(object sender, EventArgs e)
        {
            this.ShowHealthTooltip();
        }

        private void SliderHealth_MouseLeave(object sender, EventArgs e)
        {
            this.HideTooltip();
        }

        private void SliderEgestaAmount_MouseDown(object sender, MouseEventArgs e)
        {
            this.ownerMonthsControl.FocusDate = this.dayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                this.DayCellPopupControl_MouseClick(sender, e);
            }
        }

        private void HadSex_MouseEnter(object sender, EventArgs e)
        {
            this.ShowHasSexToolTip();
        }

        private void HadSex_MouseLeave(object sender, EventArgs e)
        {
            this.HideTooltip();
        }

        private void HadSex_MouseClick(object sender, MouseEventArgs e)
        {
            this.ownerMonthsControl.FocusDate = this.dayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                this.ownerMonthsControl.ShowDayContextMenu();
            }
        }

        private void BBT_MouseClick(object sender, MouseEventArgs e)
        {
            this.ownerMonthsControl.FocusDate = this.dayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                this.ownerMonthsControl.ShowDayContextMenu();
            }
        }

        private void SliderHealth_MouseClick(object sender, MouseEventArgs e)
        {
            this.ownerMonthsControl.FocusDate = this.dayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                this.ownerMonthsControl.ShowDayContextMenu();
            }
        }

        private void BBT_DoubleClick(object sender, EventArgs e)
        {
            this.ShowDayEditForm(DayEditFocus.BBT);
        }

        private void HadSex_DoubleClick(object sender, EventArgs e)
        {
            this.ShowDayEditForm(DayEditFocus.Note);
        }

        private void BBT_MouseEnter(object sender, EventArgs e)
        {
            this.ShowBBTTooltip();
        }

        private void BBT_MouseLeave(object sender, EventArgs e)
        {
            this.HideTooltip();
        }

        private void PictureNote_DoubleClick(object sender, EventArgs e)
        {
            this.ShowDayEditForm(DayEditFocus.Note);
        }

        private void Day_MouseEnter(object sender, EventArgs e)
        {
            this.ShowDayInfoTooltip();
        }

        private void Day_MouseLeave(object sender, EventArgs e)
        {
            this.HideTooltip();
        }

        private void SliderEgestaAmount_ValueChanged(object sender, EventArgs e)
        {
            if (!this.initializing)
            {
                Program.CurrentWoman.Menstruations.SetEgesta(this.dayCell.Date, this.EgestaSliderValue);
                this.ShowEgestaTooltip();
            }
        }

        private void SliderHealth_ValueChanged(object sender, EventArgs e)
        {
            if (!this.initializing)
            {
                Program.CurrentWoman.Health[this.dayCell.Date] = this.sliderHealth.Value;
                this.ShowHealthTooltip();
            }
        }

        private void PictureAlarm_MouseClick(object sender, MouseEventArgs e)
        {
            this.DayCellPopupControl_MouseClick(sender, e);
        }

        private void PictureAlarm_DoubleClick(object sender, EventArgs e)
        {
            this.ShowDayEditForm(DayEditFocus.Schedules);
        }

        private void PictureAlarm_MouseEnter(object sender, EventArgs e)
        {
            this.ShowSchedulesTooltip();
        }

        private void PictureAlarm_MouseLeave(object sender, EventArgs e)
        {
            this.HideTooltip();
        }
    }
}
