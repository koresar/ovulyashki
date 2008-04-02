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
            Woman w = Program.CurrentWoman;

            bool menstruationDay = w.Menstruations.IsMenstruationDay(Date);
            bool predictedAsOvulationDay = w.IsPredictedAsOvulationDay(Date);
            bool predictedAsSafeSexDay = w.IsPredictedAsSafeSexDay(Date);
            bool predictedAsBoyDay = w.IsPredictedAsBoyDay(Date);
            bool predictedAsGirlDay = w.IsPredictedAsGirlDay(Date);

            pe.Graphics.FillRectangle(
                (menstruationDay && Date < DateTime.Today) ? Brushes.LightPink :
                (menstruationDay && Date >= DateTime.Today) ? Brushes.LightGreen :
                predictedAsOvulationDay ? Brushes.Yellow :
                predictedAsSafeSexDay ? Brushes.LightGreen :
                Brushes.White, 
                0, 0, Size.Width - 1, Size.Height - 1);

            if (this == OwnerOneMonthControl.FocusDay)
            {
                pe.Graphics.DrawRectangle(Program.DayCellAppearance.FocusEdgePen, 1, 1, Size.Width - 2, Size.Height - 2);
            }
            else if (Date == DateTime.Today)
            {
                pe.Graphics.DrawRectangle(Program.DayCellAppearance.TodayEdgePen, 1, 1, Size.Width - 2, Size.Height - 2);
            }
            else
            {
                pe.Graphics.DrawRectangle(Program.DayCellAppearance.EdgePen, 0, 0, Size.Width - 1, Size.Height - 1);
            }

            if (menstruationDay)
            {
                int egesta = w.Menstruations.GetEgestaAmount(Date);
                if (egesta > 0)
                {
                    Image image = (Image)Program.IconResource.GetObject("drop_Image");
                    ImageAttributes attr = new ImageAttributes();
                    ColorMatrix cMatrix = new ColorMatrix();
                    // alpha
                    cMatrix.Matrix33 = ((float)egesta) / (EgestasCollection.MaximumEgestaValue);
                    attr.SetColorMatrix(cMatrix);
                    pe.Graphics.DrawImage(image, new Rectangle(2, 20, 10, 10), 0, 0, 14, 14, GraphicsUnit.Pixel, attr);
                }
                else if (egesta == 0)
                {
                    pe.Graphics.DrawEllipse(Pens.Red, 3, 14, 5, 5);
                }
            }

            if (predictedAsGirlDay)
            {
                Image image = (Image)Program.IconResource.GetObject("girl_Image");
                pe.Graphics.DrawImage(image, new Rectangle(13, 20, 10, 10), 0, 0, 48, 48, GraphicsUnit.Pixel);
            }

            if (predictedAsBoyDay)
            {
                Image image = (Image)Program.IconResource.GetObject("boy_Image");
                pe.Graphics.DrawImage(image, new Rectangle(13, 20, 10, 10), 0, 0, 48, 48, GraphicsUnit.Pixel);
            }

            if (w.Notes.ContainsKey(Date))
            {
                pe.Graphics.DrawImage((Image)Program.IconResource.GetObject("note_Image"), 23, 2);
            }

            if (w.IsPredictedAsMenstruationDay(Date))
            {
                pe.Graphics.DrawString("?", Font, Brushes.Red, 0, 18);
            }

            if (w.HadSexList.ContainsKey(Date))
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
