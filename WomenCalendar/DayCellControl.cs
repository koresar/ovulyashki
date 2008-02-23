using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class DayCellControl : UserControl
    {
        public static int DefaultCellWidth = 32;
        public static int DefaultCellHeight = 32;

        private void InitializeFonts()
        {
            if (FontNormal == null) FontNormal = Font;
            if (FontBold == null) FontBold = new Font(FontNormal, FontStyle.Bold);
        }

        private static Font FontNormal;
        private static Font FontBold;


        private OneMonthControl _ownerOneMonthControl;
        public OneMonthControl OwnerOneMonthControl
        {
            get { return _ownerOneMonthControl; }
            set { _ownerOneMonthControl = value; }
        }

        private Brush _backBrush = Brushes.Aqua;
        public Brush BackBrush
        {
            get { return _backBrush; }
            set { _backBrush = value; }
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

                InitializeFonts();

                Font = (Enabled) ? FontBold : FontNormal;
                BackColor = Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday ?
                    Color.LightPink : Color.LightSkyBlue;
                BackBrush = new SolidBrush(BackColor);
                FontBrush = Enabled ? (_date == DateTime.Today ? Brushes.GreenYellow : Brushes.Black) : Brushes.Gray;
                Invalidate();
            }
        }

        public DayCellControl(OneMonthControl parent)
        {
            OwnerOneMonthControl = parent;
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(BackBrush, 0, 0, Size.Width - 1, Size.Height - 1);
            if (this == OwnerOneMonthControl.FocusDay)
            {
                pe.Graphics.DrawRectangle(Program.DayCellAppearance.FocusEdgePen, 1, 1, Size.Width - 2, Size.Height - 2);
            }
            else
            {
                pe.Graphics.DrawRectangle(Program.DayCellAppearance.EdgePen, 0, 0, Size.Width - 1, Size.Height - 1);
            }

            if (Program.CurrentWoman.Menstruations.IsMenstruationDay(Date))
            {
                pe.Graphics.DrawImage((Image) Program.IconResource.GetObject("drop_Image"), 3, 14);
            }

            if (Program.CurrentWoman.Notes.ContainsKey(Date))
            {
                pe.Graphics.DrawImage((Image)Program.IconResource.GetObject("note_Image"), 23, 2);
            }

            if (Program.CurrentWoman.IsPredictedAsMenstruationDay(Date))
            {
                pe.Graphics.DrawEllipse(Pens.Red, 3, 14, 5, 5);
            }

            pe.Graphics.DrawString(Date.Day.ToString(), Font, FontBrush, 0, 0);
        }

        public delegate void DayCellClick(object sender, DayCellClickEventArgs e);
        public event DayCellClick CellClick;

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

    public class DayCellClickEventArgs : MouseEventArgs
    {
        public DateTime NewDate;

        public DayCellClickEventArgs(MouseEventArgs e, DateTime newDate) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            NewDate = newDate;
        }
    }
}
