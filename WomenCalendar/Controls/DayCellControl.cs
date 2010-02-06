using System;
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

        private static Dictionary<string, Image> imageCache = new Dictionary<string, Image>();
        private static Image GetImageFromCache(string id)
        {
            Image image;
            if (!imageCache.TryGetValue(id, out image))
            {
                image = Resources.ResourceManager.GetObject(id) as Image;
                imageCache[id] = image;
            }
            return image;
        }

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

                if (FontNormal == null) FontNormal = new Font(Font, FontStyle.Regular);
                if (FontBold == null) FontBold = new Font(FontNormal, FontStyle.Bold);

                Invalidate();
            }
        }

        public string BackColorIdAppearance { get; set; }

        /// <summary>
        /// When true indicates that we should not automaticaly 
        /// calculate pregnancy, menstruation, etc. parameters.
        /// </summary>
        public bool ManualDrawOptions { get; set; }

        public int Egesta { get; set; }
        public int PregnancyWeek { get; set; }
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

        private PathGradientBrush _backBrush;
        private Color _backColor = Color.White;
        private static Dictionary<Color, Brush> brushesCache = new Dictionary<Color, Brush>();
        private Brush GetMainBrush(Color color)
        {
            if (color == Color.White) return Brushes.White;
            Brush brush;
            if (brushesCache.TryGetValue(color, out brush))
            {
                return brush;
            }
            if (_backBrush == null || color != _backColor)
            {
                Rectangle rectBrush = new Rectangle(0, 0, 32, 32);
                PointF[] rect = { new PointF(rectBrush.Left, rectBrush.Top), 
                     new PointF(rectBrush.Left, rectBrush.Height),
                     new PointF(rectBrush.Width, rectBrush.Height),
                     new PointF(rectBrush.Width, rectBrush.Top), 
                     new PointF(rectBrush.Left, rectBrush.Top) };
                _backBrush = new PathGradientBrush(rect);
                _backBrush.CenterColor = ControlPaint.LightLight(color);
                _backBrush.CenterPoint = new PointF(rectBrush.Left, rectBrush.Top);
                _backBrush.SurroundColors = new Color[1] { color };
            }
            brushesCache[color] = _backBrush;
            return _backBrush;
        }

        private void DrawDisabled(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(Brushes.White, 0, 0, Size.Width - 1, Size.Height - 1);
            pe.Graphics.DrawRectangle(DayCellAppearance.EdgePen, 0, 0, Size.Width - 1, Size.Height - 1);
            pe.Graphics.DrawString(Date.Day.ToString(), Font, Brushes.Gray, 0, 0);
        }

        private void DrawEnabled(PaintEventArgs pe)
        {
            var appearance = Program.Settings == null || Program.Settings.DayCellAppearance == null ? 
                new DayCellAppearance() : Program.Settings.DayCellAppearance;
            BackColor = 
                IsHadSex ? appearance.BackHadSex :
                IsHaveNote ? appearance.BackHaveNote :
                IsPredictedAsBoyDay ? appearance.BackBoyDay :
                IsPredictedAsGirlDay ? appearance.BackGirlDay :
                IsConceptionDay ? appearance.BackConceptionDay :
                IsPregnancyDay ? appearance.BackPregnancyDay :
                IsMenstruationDay ? appearance.BackMenstruationDay :
                IsPredictedAsMenstruationDay ? appearance.BackPredictedMenstruationDay :
                IsPredictedAsOvulationDay ? appearance.BackOvulationDay :
                IsPredictedAsSafeSexDay ? appearance.BackSafeSex :
                appearance.BackEmpty;

            pe.Graphics.FillRectangle(GetMainBrush(BackColor), 0, 0, Size.Width - 1, Size.Height - 1);

            if (IsFocusDay)
            {
                pe.Graphics.DrawRectangle(DayCellAppearance.FocusEdgePen, 1, 1, Size.Width - 2, Size.Height - 2);
            }
            else if (IsTodayDay)
            {
                pe.Graphics.DrawRectangle(DayCellAppearance.TodayEdgePen, 1, 1, Size.Width - 2, Size.Height - 2);
            }
            else
            {
                pe.Graphics.DrawRectangle(DayCellAppearance.EdgePen, 0, 0, Size.Width - 1, Size.Height - 1);
            }

            if (IsMenstruationDay)
            {
                Image image = GetImageFromCache("egestaDrop" + Egesta);
                pe.Graphics.DrawImage(image, new Rectangle(2, 18, 10, 13), 0, 0, 13, 17, GraphicsUnit.Pixel);
            }

            if (IsConceptionDay)
            {
                Image image = GetImageFromCache("baby_Image");
                pe.Graphics.DrawImage(image, 1, 20, image.Width, image.Height);
            }
            else if (IsPregnancyDay)
            {
                if (PregnancyWeek > 0)
                {
                    pe.Graphics.DrawString(PregnancyWeek.ToString(), Font, DayCellAppearance.PregnancyWeekNumberBrush, 0, 19);
                }
            }
            else if (IsPredictedAsGirlDay)
            {
                Image image = GetImageFromCache("girl_Image");
                pe.Graphics.DrawImage(image, 1, 20, image.Width, image.Height);
            }
            else if (IsPredictedAsBoyDay)
            {
                Image image = GetImageFromCache("boy_Image");
                pe.Graphics.DrawImage(image, 1, 20, image.Width, image.Height);
            }

            if (IsHaveNote)
            {
                pe.Graphics.DrawImage(GetImageFromCache("note_Image"), 23, 2);
            }

            if (IsPredictedAsMenstruationDay)
            {
                pe.Graphics.DrawString("?", Font, DayCellAppearance.MenstruationPredictionBrush, 0, 18);
            }

            if (IsHadSex)
            {
                pe.Graphics.DrawString("S", Font, DayCellAppearance.HadSexBrush, 22, 19);
            }

            pe.Graphics.DrawString(Date.Day.ToString(), Font, DayCellAppearance.DayNumberBrush, 0, 0);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Font = (Enabled) ? FontBold : FontNormal;

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
                    PregnancyWeek = w.Conceptions.GetPregnancyWeekNumberWhenFirstWeekDay(Date);
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
