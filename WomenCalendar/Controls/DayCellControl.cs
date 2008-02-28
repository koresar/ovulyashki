using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

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

                if (FontNormal == null) FontNormal = Font;
                if (FontBold == null) FontBold = new Font(FontNormal, FontStyle.Bold);

                Font = (Enabled) ? FontBold : FontNormal;
                BackColor = Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday ?
                    Color.FromArgb(255, 255, 128) : Color.LightGreen;
                BackBrush = new SolidBrush(BackColor);
                FontBrush = Enabled ? (_date == DateTime.Today ? Brushes.Blue : Brushes.Black) : Brushes.Gray;
                Invalidate();
            }
        }

        public DayCellControl(OneMonthControl parent)
        {
            OwnerOneMonthControl = parent;
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Woman w = Program.CurrentWoman;
            pe.Graphics.FillRectangle(BackBrush, 0, 0, Size.Width - 1, Size.Height - 1);
            if (this == OwnerOneMonthControl.FocusDay)
            {
                pe.Graphics.DrawRectangle(Program.DayCellAppearance.FocusEdgePen, 1, 1, Size.Width - 2, Size.Height - 2);
            }
            else
            {
                pe.Graphics.DrawRectangle(Program.DayCellAppearance.EdgePen, 0, 0, Size.Width - 1, Size.Height - 1);
            }

            if (w.Menstruations.IsMenstruationDay(Date))
            {
                int egesta = w.Menstruations.GetEgestaAmount(Date);
                if (egesta > 0)
                {
                    Image image = (Image) Program.IconResource.GetObject("drop_Image");
                    ImageAttributes attr = new ImageAttributes();
                    ColorMatrix cMatrix = new ColorMatrix();
                    // alpha
                    cMatrix.Matrix33 = ((float)egesta) / (EgestasCollection.MaximumEgestaValue);
                    attr.SetColorMatrix(cMatrix);
                    pe.Graphics.DrawImage(image, new Rectangle(3, 14, 14, 14), 0, 0, 14, 14, GraphicsUnit.Pixel, attr);
                }
                else if (egesta == 0)
                {
                    pe.Graphics.DrawEllipse(Pens.Red, 3, 14, 5, 5);
                }
            }

            if (w.Notes.ContainsKey(Date))
            {
                pe.Graphics.DrawImage((Image)Program.IconResource.GetObject("note_Image"), 23, 2);
            }

            if (w.IsPredictedAsMenstruationDay(Date))
            {
                pe.Graphics.DrawString("?", Font, OwnerOneMonthControl.Date.Month == Date.Month ? Brushes.Red : Brushes.Brown, 3, 14);
            }

            pe.Graphics.DrawString(Date.Day.ToString(), Font, FontBrush, 0, 0);
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
