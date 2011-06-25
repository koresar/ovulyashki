using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using WomenCalendar.Properties;

namespace WomenCalendar
{
    /// <summary>
    /// The part of calendar representing one day.
    /// </summary>
    public partial class DayCellControl : UserControl
    {
        public const int DefaultCellWidth = 32;
        public const int DefaultCellHeight = 32;

        private static Font fontNormal;
        private static Font fontBold;

        private static Dictionary<string, Image> imageCache = new Dictionary<string, Image>();
        private static Dictionary<Color, Brush> brushesCache = new Dictionary<Color, Brush>();

        private PathGradientBrush backBrush;
        private Color backColor = Color.White;

        private DateTime date;

        private OneMonthControl ownerOneMonthControl;

        /// <summary>
        /// Create the default day with nothing inside.
        /// </summary>
        public DayCellControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Create the control on the calenday under the provided month.
        /// </summary>
        /// <param name="parent">The month to belong to.</param>
        public DayCellControl(OneMonthControl parent)
        {
            this.OwnerOneMonthControl = parent;
            this.InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);
        }

        /// <summary>
        /// The event handler signature.
        /// </summary>
        /// <param name="sender">The event invoker.</param>
        /// <param name="e">The event data.</param>
        public delegate void DayCellClick(object sender, DayCellClickEventArgs e);

        /// <summary>
        /// Fired when cell is clicked somewhere somehow.
        /// </summary>
        public event DayCellClick CellClick;

        /// <summary>
        /// The control which owns (contains) this control.
        /// </summary>
        public OneMonthControl OwnerOneMonthControl
        {
            get { return this.ownerOneMonthControl; }
            set { this.ownerOneMonthControl = value; }
        }

        /// <summary>
        /// Say associated with the control.
        /// </summary>
        public DateTime Date
        {
            get
            {
                return this.date;
            }

            set
            {
                if (this.date == value)
                {
                    return;
                }

                this.date = value;
                this.Enabled = this.OwnerOneMonthControl.Date.Month == this.Date.Month;

                if (fontNormal == null)
                {
                    fontNormal = new Font(Font, FontStyle.Regular);
                }

                if (fontBold == null)
                {
                    fontBold = new Font(fontNormal, FontStyle.Bold);
                }

                this.Invalidate();
            }
        }

        /// <summary>
        /// The unique ID of the color (the Id is being serialized to the file) of the control.
        /// </summary>
        public string BackColorIdAppearance { get; set; }

        /// <summary>
        /// When true indicates that we should not automaticaly 
        /// calculate pregnancy, menstruation, etc. parameters.
        /// </summary>
        public bool ManualDrawOptions { get; set; }

        /// <summary>
        /// The amount of woman egesta that day.
        /// </summary>
        public int Egesta { get; set; }

        /// <summary>
        /// The number of pregnancy week.
        /// </summary>
        public int PregnancyWeek { get; set; }

        /// <summary>
        /// Indicates the user clicked that day to be the focused one.
        /// </summary>
        public bool IsFocusDay { get; set; }
        
        /// <summary>
        /// Indicates the day is today.
        /// </summary>
        public bool IsTodayDay { get; set; }

        /// <summary>
        /// That day the woman is preignant.
        /// </summary>
        public bool IsPregnancyDay { get; set; }

        /// <summary>
        /// User set that day as the red tides day.
        /// </summary>
        public bool IsMenstruationDay { get; set; }

        /// <summary>
        /// Predicted by complex logic as ovulation day.
        /// </summary>
        public bool IsPredictedAsOvulationDay { get; set; }

        /// <summary>
        /// Preidcted as day without conceiving.
        /// </summary>
        public bool IsPredictedAsSafeSexDay { get; set; }

        /// <summary>
        /// Use left some notes for the day.
        /// </summary>
        public bool IsHaveNote { get; set; }

        /// <summary>
        /// Looks like good day to conceive a boy.
        /// </summary>
        public bool IsPredictedAsBoyDay { get; set; }

        /// <summary>
        /// Looks like good day to conceive a girl.
        /// </summary>
        public bool IsPredictedAsGirlDay { get; set; }

        /// <summary>
        /// Predicted by the application as red tides day.
        /// </summary>
        public bool IsPredictedAsMenstruationDay { get; set; }

        /// <summary>
        /// Indicates woman got preignant that day.
        /// </summary>
        public bool IsConceptionDay { get; set; }

        /// <summary>
        /// Indicates sex mark is set.
        /// </summary>
        public bool IsHadSex { get; set; }

        /// <summary>
        /// Has ny schdule fired that day.
        /// </summary>
        public bool IsAScheduleFired { get; set; }

        /// <summary>
        /// Reaction on mouse click on the control.
        /// </summary>
        /// <param name="e">Mouse event arguments.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.Enabled && this.CellClick != null)
            {
                this.CellClick(this, new DayCellClickEventArgs(e, this.Date));
            }
        }

        /// <summary>
        /// Draw the control.
        /// </summary>
        /// <param name="pe">Draing event arguments.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            this.Font = this.Enabled ? fontBold : fontNormal;

            if (!this.Enabled)
            {
                this.DrawDisabled(pe);
            }
            else
            {
                if (!this.ManualDrawOptions)
                {
                    Woman w = Program.CurrentWoman;
                    this.Egesta = w.Menstruations.GetEgestaAmount(this.Date);
                    this.IsFocusDay = this.OwnerOneMonthControl != null && this == this.OwnerOneMonthControl.FocusDay;
                    this.IsTodayDay = this.Date == DateTime.Today;
                    this.IsPregnancyDay = w.IsPregnancyDay(this.Date);
                    this.PregnancyWeek = w.Conceptions.GetPregnancyWeekNumberWhenFirstWeekDay(this.Date);
                    this.IsMenstruationDay = !this.IsPregnancyDay && w.Menstruations.IsMenstruationDay(this.Date);
                    this.IsPredictedAsOvulationDay = !this.IsPregnancyDay && w.IsPredictedAsOvulationDay(this.Date);
                    this.IsPredictedAsSafeSexDay = !this.IsPregnancyDay && w.IsPredictedAsSafeSexDay(this.Date);
                    this.IsHaveNote = w.Notes.ContainsKey(this.Date);
                    this.IsPredictedAsBoyDay = !this.IsPregnancyDay && w.IsPredictedAsBoyDay(this.Date);
                    this.IsPredictedAsGirlDay = !this.IsPregnancyDay && w.IsPredictedAsGirlDay(this.Date);
                    this.IsPredictedAsMenstruationDay = !this.IsPregnancyDay && w.IsPredictedAsMenstruationDay(this.Date);
                    this.IsConceptionDay = w.IsConceptionDay(this.Date);
                    this.IsHadSex = w.HadSexList.ContainsKey(this.Date);
                    this.IsAScheduleFired = w.Schedules.HasAFiredSchedule(this.Date);
                }

                this.DrawEnabled(pe);
            }
        }

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

        private void DayCellControl_MouseEnter(object sender, EventArgs e)
        {
            if (this.OwnerOneMonthControl != null)
            {
                this.OwnerOneMonthControl.OwnerMonthsControl.CellPopupControl.ShowAbove(this);
            }
        }

        private Brush GetMainBrush(Color color)
        {
            if (color == Color.White)
            {
                return Brushes.White;
            }

            Brush brush;
            if (brushesCache.TryGetValue(color, out brush))
            {
                return brush;
            }

            if (this.backBrush == null || color != this.backColor)
            {
                Rectangle rectBrush = new Rectangle(0, 0, 32, 32);
                PointF[] rect = 
                    { 
                        new PointF(rectBrush.Left, rectBrush.Top), 
                        new PointF(rectBrush.Left, rectBrush.Height),
                        new PointF(rectBrush.Width, rectBrush.Height),
                        new PointF(rectBrush.Width, rectBrush.Top), 
                        new PointF(rectBrush.Left, rectBrush.Top) 
                    };
                this.backBrush = new PathGradientBrush(rect);
                this.backBrush.CenterColor = ControlPaint.LightLight(color);
                this.backBrush.CenterPoint = new PointF(rectBrush.Left, rectBrush.Top);
                this.backBrush.SurroundColors = new Color[1] { color };
            }

            brushesCache[color] = this.backBrush;
            return this.backBrush;
        }

        private void DrawDisabled(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(Brushes.White, 0, 0, Size.Width - 1, Size.Height - 1);
            pe.Graphics.DrawRectangle(DayCellAppearance.EdgePen, 0, 0, Size.Width - 1, Size.Height - 1);
            pe.Graphics.DrawString(this.Date.Day.ToString(), this.Font, Brushes.Gray, 0, 0);
        }

        private void DrawEnabled(PaintEventArgs pe)
        {
            var appearance = Program.Settings == null || Program.Settings.DayCellAppearance == null ?
                new DayCellAppearance() : Program.Settings.DayCellAppearance;
            this.BackColor =
                this.IsConceptionDay ? appearance.BackConceptionDay :
                this.IsPregnancyDay ? appearance.BackPregnancyDay :
                this.IsMenstruationDay ? appearance.BackMenstruationDay :
                this.IsPredictedAsMenstruationDay ? appearance.BackPredictedMenstruationDay :
                this.IsPredictedAsOvulationDay ? appearance.BackOvulationDay :
                this.IsPredictedAsSafeSexDay ? appearance.BackSafeSex :
                this.IsHadSex ? appearance.BackHadSex :
                this.IsHaveNote ? appearance.BackHaveNote :
                this.IsPredictedAsBoyDay ? appearance.BackBoyDay :
                this.IsPredictedAsGirlDay ? appearance.BackGirlDay :
                appearance.BackEmpty;

            pe.Graphics.FillRectangle(this.GetMainBrush(this.BackColor), 0, 0, this.Size.Width - 1, this.Size.Height - 1);

            if (this.IsFocusDay)
            {
                pe.Graphics.DrawRectangle(DayCellAppearance.FocusEdgePen, 1, 1, Size.Width - 2, Size.Height - 2);
            }
            else if (this.IsTodayDay)
            {
                pe.Graphics.DrawRectangle(DayCellAppearance.TodayEdgePen, 1, 1, Size.Width - 2, Size.Height - 2);
            }
            else
            {
                pe.Graphics.DrawRectangle(DayCellAppearance.EdgePen, 0, 0, Size.Width - 1, Size.Height - 1);
            }

            if (this.IsMenstruationDay)
            {
                Image image = GetImageFromCache("egestaDrop" + this.Egesta);
                pe.Graphics.DrawImage(image, new Rectangle(2, 18, 10, 13), 0, 0, 13, 17, GraphicsUnit.Pixel);
            }

            if (this.IsConceptionDay)
            {
                Image image = GetImageFromCache("baby_Image");
                pe.Graphics.DrawImage(image, 1, 20, image.Width, image.Height);
            }
            else if (this.IsPregnancyDay)
            {
                if (this.PregnancyWeek > 0)
                {
                    pe.Graphics.DrawString(this.PregnancyWeek.ToString(), this.Font, DayCellAppearance.PregnancyWeekNumberBrush, 0, 19);
                }
            }
            else if (this.IsPredictedAsGirlDay)
            {
                Image image = GetImageFromCache("girl_Image");
                pe.Graphics.DrawImage(image, 1, 20, image.Width, image.Height);
            }
            else if (this.IsPredictedAsBoyDay)
            {
                Image image = GetImageFromCache("boy_Image");
                pe.Graphics.DrawImage(image, 1, 20, image.Width, image.Height);
            }

            if (this.IsHaveNote)
            {
                pe.Graphics.DrawImage(GetImageFromCache("note_Image"), 23, 2);
            }

            if (this.IsPredictedAsMenstruationDay)
            {
                pe.Graphics.DrawString("?", Font, DayCellAppearance.MenstruationPredictionBrush, 0, 18);
            }

            this.lblHadSex.Visible = this.IsHadSex;

            if (this.IsAScheduleFired)
            {
                Image image = GetImageFromCache("alarm");
                pe.Graphics.DrawImage(image, 12, 12, 8, 8);
            }

            pe.Graphics.DrawString(this.Date.Day.ToString(), Font, DayCellAppearance.DayNumberBrush, 0, 0);
        }

        /// <summary>
        /// Invoke the control redraw.
        /// </summary>
        private void Redraw()
        {
            this.Invalidate(true);
            this.Update();
        }
    }
}
