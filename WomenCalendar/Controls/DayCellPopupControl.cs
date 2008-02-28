﻿using System;
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
        private static string[] EgestasNames = {"День без овуляшек", "Мало овуляшек", "Посредственные овуляшки", 
            "Средняя интенсивность овуляшек", "Много овуляшек" };

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
                trackEgestaAmount.Value = egesta;
                trackEgestaAmount.Visible = true;
            }
            else
            {
                trackEgestaAmount.Visible = false;
            }

            pictureNote.Visible = w.Notes.ContainsKey(dayCell.Date);

            lblDay.Text = dayCell.Date.Day.ToString();
            BackColor = dayCell.BackColor;
            trackEgestaAmount.BackColor = dayCell.BackColor;
            Point newLocation = dayCell.Location;
            newLocation.Offset(-dayCell.Size.Width/2, -dayCell.Size.Height/2);
            Location = OwnerMonthsControl.PointToClient(dayCell.OwnerOneMonthControl.PointToScreen(newLocation));

            if (Visible == false)
            {
                Visible = true;
            }

            initializing = false;
        }

        private void ShowDayEditForm()
        {
            new DayEditForm().ShowDialog(this);
        }

        private void HideTooltip()
        {
            toolTip.Hide(this);
        }

        private void ShowEgestaTooltip()
        {
            ShowTooltip("Количество выделений", EgestasNames[trackEgestaAmount.Value]);
        }

        private void ShowNoteEditForm()
        {
            NoteEditForm form = new NoteEditForm(Program.CurrentWoman.Notes[DayCell.Date]);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Program.CurrentWoman.AddNote(DayCell.Date, form.NoteText);
            }
        }

        private void ShowNoteToolTip()
        {
            ShowTooltip("Заметка", Program.CurrentWoman.Notes[DayCell.Date]);
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
                OwnerMonthsControl.ShowPopupMenu();
            }
        }

        private void DayCellPopupControl_DoubleClick(object sender, EventArgs e)
        {
            ShowDayEditForm();
        }

        private void lblDay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OwnerMonthsControl.FocusDate = DayCell.Date;
                ShowDayEditForm();
            }
            else
            {
                DayCellPopupControl_MouseClick(sender, e);
            }
        }

        private void trackEgestaAmount_ValueChanged(object sender, EventArgs e)
        {
            if (!initializing)
            {
                Program.CurrentWoman.Menstruations.SetEgesta(DayCell.Date, trackEgestaAmount.Value);
                ShowEgestaTooltip();
            }
        }

        private void trackEgestaAmount_MouseEnter(object sender, EventArgs e)
        {
            ShowEgestaTooltip();
        }

        private void trackEgestaAmount_MouseLeave(object sender, EventArgs e)
        {
            HideTooltip();
        }

        private void pictureNote_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OwnerMonthsControl.FocusDate = DayCell.Date;
                ShowNoteEditForm();
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

        private void trackEgestaAmount_MouseDown(object sender, MouseEventArgs e)
        {
            OwnerMonthsControl.FocusDate = DayCell.Date;
            if (e.Button == MouseButtons.Right)
            {
                DayCellPopupControl_MouseClick(sender, e);
            }
        }

        private void DayCellPopupControl_MouseLeave(object sender, EventArgs e)
        {
            if (!ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                Visible = false;
            }
        }
    }
}