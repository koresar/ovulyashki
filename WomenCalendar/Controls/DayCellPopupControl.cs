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
        public static string[] EgestasNames = {"День без менструашек", "Мало менструашек", "Посредственные менструашки", 
            "Средняя интенсивность менструашек", "Много менструашек" };

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
            new DayEditForm(DayCell, focus).ShowDialog(this);
        }

        private void HideTooltip()
        {
            toolTip.Hide(this);
        }

        private void ShowEgestaTooltip()
        {
            ShowTooltip("Количество менструашек", EgestasNames[EgestaSliderValue]);
        }

        private void ShowHasSexToolTip()
        {
            ShowTooltip("Секс", "А в этот день у меня был секс.");
        }

        private void ShowNoteToolTip()
        {
            ShowTooltip("Заметка", Program.CurrentWoman.Notes[DayCell.Date]);
        }

        private void ShowHealthTooltip()
        {
            ShowTooltip("Самочувствие", sliderHealth.Value.ToString());
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

        private void sliderEgestaAmount_Scroll(object sender, ScrollEventArgs e)
        {
            if (!initializing)
            {
                Program.CurrentWoman.Menstruations.SetEgesta(DayCell.Date, EgestaSliderValue);
                ShowEgestaTooltip();
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
            else if (e.Button == MouseButtons.Left)
            {
                bool hadSex = Program.CurrentWoman.HadSexList[DayCell.Date];
                Program.CurrentWoman.HadSexList[DayCell.Date] = !hadSex;
                PaintHadSex();
            }
        }

        private void lblBBT_MouseClick(object sender, MouseEventArgs e)
        {
            OwnerMonthsControl.FocusDate = DayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                OwnerMonthsControl.ShowDayContextMenu();
            }
            else if (e.Button == MouseButtons.Left)
            {
                ShowDayEditForm(DayEditFocus.BBT);
            }
        }

        public void Redraw()
        {
            Invalidate(true);
            Update();
        }

        private void sliderHealth_Scroll(object sender, ScrollEventArgs e)
        {
            if (!initializing)
            {
                Program.CurrentWoman.Health[DayCell.Date] = sliderHealth.Value;
                ShowHealthTooltip();
            }
        }

        private void sliderHealth_MouseClick(object sender, MouseEventArgs e)
        {
            OwnerMonthsControl.FocusDate = DayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                OwnerMonthsControl.ShowDayContextMenu();
            }
        }
    }
}
