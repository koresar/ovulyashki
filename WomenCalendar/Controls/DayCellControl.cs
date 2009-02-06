﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using WomenCalendar.Properties;

namespace WomenCalendar
{
    public partial class DayCellControl : UserControl
    {
        public static int DefaultCellWidth = 32;
        public static int DefaultCellHeight = 32;

        private static Font FontNormal;
        private static Font FontBold;

        public delegate void DayCellClick(object sender, DayCellClickEventArgs e);
        public event DayCellClick CellClick;

        private OneMonthControl _ownerOneMonthControl;
        public OneMonthControl OwnerOneMonthControl
        {
            get { return _ownerOneMonthControl; }
            set { _ownerOneMonthControl = value; }
        }

        private Brush _fontBrush = Brushes.Black;
        public Brush FontBrush
        {
            get { return _fontBrush; }
            set { _fontBrush = value; }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date == value) return;
                _date = value;
                Enabled = OwnerOneMonthControl.Date.Month == Date.Month;

                if (FontNormal == null) FontNormal = Font;
                if (FontBold == null) FontBold = new Font(FontNormal, FontStyle.Bold);

                Font = (Enabled) ? FontBold : FontNormal;

                Invalidate();
            }
        }

        /// <summary>
        /// When true indicates that we should not automaticaly 
        /// calculate pregnancy, menstruation, etc. parameters.
        /// </summary>
        public bool ManualDrawOptions { get; set; }

        public int Egesta { get; set; }
        public bool IsFocusDay { get; set; }
        public bool IsTodayDay { get; set; }
        public bool IsPregnancyDay { get; set; }
        public bool IsMenstruationDay { get; set; }
        public bool IsPredictedAsOvulationDay { get; set; }
        public bool IsPredictedAsSafeSexDay { get; set; }
        public bool IsHaveNote { get; set; }
        public bool IsPredictedAsBoyDay { get; set; }
        public bool IsPredictedAsGirlDay { get; set; }
        public bool IsPredictedAsMenstruationDay { get; set; }
        public bool IsConceptionDay { get; set; }
        public bool IsHadSex { get; set; }

        public DayCellControl()
        {
        }

        public DayCellControl(OneMonthControl parent)
        {
            OwnerOneMonthControl = parent;
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);
        }

        private void DrawDisabled(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(Brushes.White, 0, 0, Size.Width - 1, Size.Height - 1);
            pe.Graphics.DrawRectangle(Program.DayCellAppearance.EdgePen, 0, 0, Size.Width - 1, Size.Height - 1);
            pe.Graphics.DrawString(Date.Day.ToString(), Font, Brushes.Gray, 0, 0);
        }

        private void DrawEnabled(PaintEventArgs pe)
        {
            BackColor = IsPregnancyDay ? Color.LightBlue :
                (IsMenstruationDay && Date <= DateTime.Today) ? Color.LightPink :
                (IsMenstruationDay && Date > DateTime.Today) ? Color.LightGreen :
                IsPredictedAsOvulationDay ? Color.Yellow :
                IsPredictedAsSafeSexDay ? Color.LightGreen :
                Color.White;

            pe.Graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, Size.Width - 1, Size.Height - 1);

            if (IsFocusDay)
            {
                pe.Graphics.DrawRectangle(Program.DayCellAppearance.FocusEdgePen, 1, 1, Size.Width - 2, Size.Height - 2);
            }
            else if (IsTodayDay)
            {
                pe.Graphics.DrawRectangle(Program.DayCellAppearance.TodayEdgePen, 1, 1, Size.Width - 2, Size.Height - 2);
            }
            else
            {
                pe.Graphics.DrawRectangle(Program.DayCellAppearance.EdgePen, 0, 0, Size.Width - 1, Size.Height - 1);
            }

            if (IsMenstruationDay)
            {
                if (Egesta > 0)
                {
                    Image image = (Image)Resources.ResourceManager.GetObject("drop_Image");
                    ImageAttributes attr = new ImageAttributes();
                    ColorMatrix cMatrix = new ColorMatrix();
                    // alpha
                    cMatrix.Matrix33 = ((float)Egesta) / (EgestasCollection.MaximumEgestaValue);
                    attr.SetColorMatrix(cMatrix);
                    pe.Graphics.DrawImage(image, new Rectangle(2, 20, 10, 10), 0, 0, 14, 14, GraphicsUnit.Pixel, attr);
                }
                else if (Egesta == 0)
                {
                    pe.Graphics.DrawEllipse(Pens.Red, 3, 14, 5, 5);
                }
            }

            if (IsConceptionDay)
            {
                Image image = (Image)Resources.ResourceManager.GetObject("baby_Image");
                pe.Graphics.DrawImage(image, new Rectangle(2, 20, 10, 10), 0, 0, 48, 48, GraphicsUnit.Pixel);
            }
            else if (IsPredictedAsGirlDay)
            {
                Image image = (Image)Resources.ResourceManager.GetObject("girl_Image");
                pe.Graphics.DrawImage(image, new Rectangle(2, 20, 10, 10), 0, 0, 48, 48, GraphicsUnit.Pixel);
            }
            else if (IsPredictedAsBoyDay)
            {
                Image image = (Image)Resources.ResourceManager.GetObject("boy_Image");
                pe.Graphics.DrawImage(image, new Rectangle(2, 20, 10, 10), 0, 0, 48, 48, GraphicsUnit.Pixel);
            }

            if (IsHaveNote)
            {
                pe.Graphics.DrawImage((Image)Resources.ResourceManager.GetObject("note_Image"), 23, 2);
            }
            else
            {
                Image image = (Image)Resources.ResourceManager.GetObject("note_Image");
                ImageAttributes attr = new ImageAttributes();
                ColorMatrix cMatrix = new ColorMatrix();
                // alpha
                cMatrix.Matrix33 = 0.1F;
                attr.SetColorMatrix(cMatrix);
                pe.Graphics.DrawImage(image, new Rectangle(23, 2, 7, 7), 0, 0, 7, 7, GraphicsUnit.Pixel, attr);
            }

            if (IsPredictedAsMenstruationDay)
            {
                pe.Graphics.DrawString("?", Font, Brushes.Red, 0, 18);
            }

            if (IsHadSex)
            {
                pe.Graphics.DrawString("S", Font, Brushes.Red, 22, 19);
            }

            pe.Graphics.DrawString(Date.Day.ToString(), Font, Brushes.Black, 0, 0);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (!Enabled)
            {
                DrawDisabled(pe);
            }
            else
            {
                if (!ManualDrawOptions)
                {
                    Woman w = Program.CurrentWoman;
                    Egesta = w.Menstruations.GetEgestaAmount(Date);
                    IsFocusDay = OwnerOneMonthControl != null && this == OwnerOneMonthControl.FocusDay;
                    IsTodayDay = Date == DateTime.Today;
                    IsPregnancyDay = w.IsPregnancyDay(Date);
                    IsMenstruationDay = !IsPregnancyDay && w.Menstruations.IsMenstruationDay(Date);
                    IsPredictedAsOvulationDay = !IsPregnancyDay && w.IsPredictedAsOvulationDay(Date);
                    IsPredictedAsSafeSexDay = !IsPregnancyDay && w.IsPredictedAsSafeSexDay(Date);
                    IsHaveNote = w.Notes.ContainsKey(Date);
                    IsPredictedAsBoyDay = !IsPregnancyDay && w.IsPredictedAsBoyDay(Date);
                    IsPredictedAsGirlDay = !IsPregnancyDay && w.IsPredictedAsGirlDay(Date);
                    IsPredictedAsMenstruationDay = !IsPregnancyDay && w.IsPredictedAsMenstruationDay(Date);
                    IsConceptionDay = w.IsConceptionDay(Date);
                    IsHadSex = w.HadSexList.ContainsKey(Date);
                }

                DrawEnabled(pe);
            }
        }

        public void Redraw()
        {
            Invalidate(true);
            Update();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Enabled && CellClick != null)
            {
                CellClick(this, new DayCellClickEventArgs(e, Date));
            }
        }

        private void DayCellControl_MouseEnter(object sender, EventArgs e)
        {
            OwnerOneMonthControl.OwnerMonthsControl.CellPopupControl.ShowAbove(this);
        }
    }
}
