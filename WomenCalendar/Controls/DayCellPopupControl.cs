using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class DayCellPopupControl : UserControl
    {
        private bool initializing;

        private DayCellControl DayCell;
        public MonthsControl OwnerMonthsControl;

        public new bool Visible
        {
            set
            {
                base.Visible = value;
                HideTooltip();
            }
            get
            {
                return base.Visible;
            }
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

        public bool Forbidden { get; set; }

        public DayCellPopupControl()
        {
            InitializeComponent();
            Visible = false;
            BorderStyle = BorderStyle.FixedSingle;
        }

        public DayCellPopupControl(MonthsControl ownerMonthsControl) : this()
        {
            OwnerMonthsControl = ownerMonthsControl;
            Parent = OwnerMonthsControl;
        }

        public void ShowAbove(DayCellControl dayCell)
        {
            if (Forbidden) return;

            initializing = true;

            HideTooltip();
            DayCell = dayCell;
            BringToFront();

            Woman w = Program.CurrentWoman;
            int egesta = w.Menstruations.GetEgestaAmount(dayCell.Date);
            if (egesta >= 0)
            {
                EgestaSliderValue = egesta;
                sliderEgestaAmount.Visible = true;
            }
            else
            {
                sliderEgestaAmount.Visible = false;
            }

            sliderHealth.Value = Program.CurrentWoman.Health[DayCell.Date];

            pictureNote.Visible = w.Notes.ContainsKey(dayCell.Date);

            lblDay.Text = dayCell.Date.Day.ToString();
            BackColor = dayCell.BackColor;
            Point newLocation = dayCell.Location;
            newLocation.Offset(-dayCell.Size.Width/2, -dayCell.Size.Height/2);
            Location = OwnerMonthsControl.PointToClient(dayCell.OwnerOneMonthControl.PointToScreen(newLocation));

            double bbt = w.BBT.GetBBT(dayCell.Date);
            lblBBT.Visible = true;
            if (bbt != 0)
            {
                lblBBT.ForeColor = SystemColors.ControlText;
                lblBBT.Text = "t°" + bbt.ToString("##.##");
            }
            else
            {
                Color c = Color.FromArgb((int)(BackColor.R * 0.9), (int)(BackColor.G * 0.9), (int)(BackColor.B * 0.9));
                lblBBT.ForeColor = c;
                lblBBT.Text = "t°36.6";
            }

            PaintHadSex();

            if (Visible == false)
            {
                Visible = true;
            }

            initializing = false;
        }

        private void PaintHadSex()
        {
            if (Program.CurrentWoman.HadSexList[DayCell.Date])
            {
                lblHadSex.ForeColor = Color.Red;
            }
            else
            {
                Color c = Color.FromArgb((int)(BackColor.R * 0.9), (int)(BackColor.G * 0.9), (int)(BackColor.B * 0.9));
                lblHadSex.ForeColor = c;
            }
        }

        private void ShowDayEditForm(DayEditFocus focus)
        {
            Visible = false;
            new DayEditForm(DayCell.Date, focus).ShowDialog(this);
        }

        private void HideTooltip()
        {
            toolTip.Hide(this);
        }

        private void ShowEgestaTooltip()
        {
            ShowTooltip(TEXT.Get["Amount_of_bleeding"], EgestasCollection.EgestasNames[EgestaSliderValue]);
        }

        private void ShowHasSexToolTip()
        {
            ShowTooltip(TEXT.Get["Sex"], Program.CurrentWoman.HadSexList[DayCell.Date] ? 
                TEXT.Get["Sex_was"] : 
                TEXT.Get["Sex_was_not"]);
        }

        private void ShowNoteToolTip()
        {
            ShowTooltip(TEXT.Get["Note"], Program.CurrentWoman.Notes[DayCell.Date]);
        }

        private void ShowHealthTooltip()
        {
            ShowTooltip(TEXT.Get["Wellbeing"], TEXT.Get.Format("N_of_10", sliderHealth.Value.ToString()));
        }

        private void ShowBBTTooltip()
        {
            var bbt = Program.CurrentWoman.BBT.GetBBT(DayCell.Date);
            ShowTooltip(TEXT.Get["BBT_full"], bbt == 0 ? TEXT.Get["Not_set_f"] : bbt.ToString());
        }

        private void ShowDayInfoTooltip()
        {
            var info = Program.CurrentWoman.GenerateDayInfo(DayCell.Date);
            ShowTooltip(TEXT.Get["Click_to_edit"], info);
        }        

        private void ShowTooltip(string caption, string text)
        {
            toolTip.ToolTipTitle = caption;
            toolTip.Show(text, this, Width, Height);
        }

        private void DayCellPopupControl_MouseClick(object sender, MouseEventArgs e)
        {
            OwnerMonthsControl.FocusDate = DayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                OwnerMonthsControl.ShowDayContextMenu();
            }
        }

        private void DayCellPopupControl_DoubleClick(object sender, EventArgs e)
        {
            ShowDayEditForm(DayEditFocus.Note);
        }

        private void lblDay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OwnerMonthsControl.FocusDate = DayCell.Date;
                ShowDayEditForm(DayEditFocus.Note);
            }
            else
            {
                DayCellPopupControl_MouseClick(sender, e);
            }
        }

        private void pictureNote_MouseClick(object sender, MouseEventArgs e)
        {
            DayCellPopupControl_MouseClick(sender, e);
        }

        private void pictureNote_MouseEnter(object sender, EventArgs e)
        {
            ShowNoteToolTip();
        }

        private void pictureNote_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip();
        }

        private void DayCellPopupControl_MouseLeave(object sender, EventArgs e)
        {
            if (!ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                Visible = false;
            }
        }

        private void sliderEgestaAmount_MouseEnter(object sender, EventArgs e)
        {
            ShowEgestaTooltip();
        }

        private void sliderEgestaAmount_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip();
        }

        private void sliderHealth_MouseEnter(object sender, EventArgs e)
        {
            ShowHealthTooltip();
        }

        private void sliderHealth_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip();
        }

        private void sliderEgestaAmount_MouseDown(object sender, MouseEventArgs e)
        {
            OwnerMonthsControl.FocusDate = DayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                DayCellPopupControl_MouseClick(sender, e);
            }
        }

        private void lblHadSex_MouseEnter(object sender, EventArgs e)
        {
            ShowHasSexToolTip();
        }

        private void lblHadSex_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip();
        }

        private void lblHadSex_MouseClick(object sender, MouseEventArgs e)
        {
            OwnerMonthsControl.FocusDate = DayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                OwnerMonthsControl.ShowDayContextMenu();
            }
        }

        private void lblBBT_MouseClick(object sender, MouseEventArgs e)
        {
            OwnerMonthsControl.FocusDate = DayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                OwnerMonthsControl.ShowDayContextMenu();
            }
        }

        public void Redraw()
        {
            Invalidate(true);
            Update();
        }

        private void sliderHealth_MouseClick(object sender, MouseEventArgs e)
        {
            OwnerMonthsControl.FocusDate = DayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                OwnerMonthsControl.ShowDayContextMenu();
            }
        }

        private void lblBBT_DoubleClick(object sender, EventArgs e)
        {
            ShowDayEditForm(DayEditFocus.BBT);
        }

        private void lblHadSex_DoubleClick(object sender, EventArgs e)
        {
            ShowDayEditForm(DayEditFocus.Note);
        }

        private void lblBBT_MouseEnter(object sender, EventArgs e)
        {
            ShowBBTTooltip();
        }

        private void lblBBT_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip();
        }

        private void pictureNote_DoubleClick(object sender, EventArgs e)
        {
            ShowDayEditForm(DayEditFocus.Note);
        }

        private void lblDay_MouseEnter(object sender, EventArgs e)
        {
            ShowDayInfoTooltip();
        }

        private void lblDay_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip();
        }

        private void sliderEgestaAmount_ValueChanged(object sender, EventArgs e)
        {
            if (!initializing)
            {
                Program.CurrentWoman.Menstruations.SetEgesta(DayCell.Date, EgestaSliderValue);
                ShowEgestaTooltip();
            }
        }

        private void sliderHealth_ValueChanged(object sender, EventArgs e)
        {
            if (!initializing)
            {
                Program.CurrentWoman.Health[DayCell.Date] = sliderHealth.Value;
                ShowHealthTooltip();
            }
        }
    }
}
