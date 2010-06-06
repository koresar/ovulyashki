using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MB.Controls
{
    /// <summary>
    /// Encapsulates control that visualy displays certain integer value and allows user to change it within desired range. It imitates <see cref="System.Windows.Forms.TrackBar"/> as far as mouse usage is concerned.
    /// </summary>
    [ToolboxBitmap(typeof(TrackBar))]
    [DefaultEvent("Scroll"), DefaultProperty("BarInnerColor")]
    public partial class ColorSlider : Control
    {
        #region Events

        /// <summary>
        /// Fires when Slider position has changed
        /// </summary>
        [Description("Event fires when the Value property changes")]
        [Category("Action")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// Fires when user scrolls the Slider
        /// </summary>
        [Description("Event fires when the Slider position is changed")]
        [Category("Behavior")]
        public event ScrollEventHandler Scroll;

        #endregion

        #region Properties

        /// <summary>
        /// Bounding rectangle of thumb area.
        /// </summary>
        private Rectangle thumbRect;

        /// <summary>
        /// Gets the thumb rect. Usefull to determine bounding rectangle when creating custom thumb shape.
        /// </summary>
        /// <value>The thumb rect.</value>
        [Browsable(false)]
        public Rectangle ThumbRect
        {
            get { return thumbRect; }
        }

        private Rectangle barRect; //bounding rectangle of bar area
        private Rectangle barHalfRect;
        private Rectangle thumbHalfRect;
        private Rectangle elapsedRect; //bounding rectangle of elapsed area

        private int thumbSize = 15;

        /// <summary>
        /// Gets or sets the size of the thumb.
        /// </summary>
        /// <value>The size of the thumb.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// Exception thrown when value is lower than zero or grather than half of appropiate dimension
        /// </exception>
        [Description("Set Slider thumb size")]
        [Category("ColorSlider")]
        [DefaultValue(15)]
        public int ThumbSize
        {
            get
            {
                return this.thumbSize;
            }

            set
            {
                if (value > 0 &
                    value < (barOrientation == Orientation.Horizontal ? ClientRectangle.Width : ClientRectangle.Height))
                {
                    this.thumbSize = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        "TrackSize has to be greather than zero and lower than half of Slider width");
                }

                this.Invalidate();
            }
        }

        private GraphicsPath thumbCustomShape = null;

        /// <summary>
        /// Gets or sets the thumb custom shape. Use ThumbRect property to determine bounding rectangle.
        /// </summary>
        /// <value>The thumb custom shape. null means default shape</value>
        [Description("Set Slider's thumb's custom shape")]
        [Category("ColorSlider")]
        [Browsable(false)]
        [DefaultValue(typeof(GraphicsPath), "null")]
        public GraphicsPath ThumbCustomShape
        {
            get
            {
                return this.thumbCustomShape;
            }

            set
            {
                this.thumbCustomShape = value;
                this.thumbSize = (int)(barOrientation == Orientation.Horizontal ? value.GetBounds().Width : value.GetBounds().Height) + 1;
                this.Invalidate();
            }
        }

        private Size thumbRoundRectSize = new Size(8, 8);

        /// <summary>
        /// Gets or sets the size of the thumb round rectangle edges.
        /// </summary>
        /// <value>The size of the thumb round rectangle edges.</value>
        [Description("Set Slider's thumb round rect size")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Size), "8; 8")]
        public Size ThumbRoundRectSize
        {
            get
            {
                return this.thumbRoundRectSize;
            }

            set
            {
                int h = value.Height, w = value.Width;
                if (h <= 0)
                {
                    h = 1;
                }

                if (w <= 0)
                {
                    w = 1;
                }

                this.thumbRoundRectSize = new Size(w, h);
                this.Invalidate();
            }
        }

        private Size borderRoundRectSize = new Size(8, 8);

        /// <summary>
        /// Gets or sets the size of the border round rect.
        /// </summary>
        /// <value>The size of the border round rect.</value>
        [Description("Set Slider's border round rect size")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Size), "8; 8")]
        public Size BorderRoundRectSize
        {
            get
            {
                return this.borderRoundRectSize;
            }

            set
            {
                int h = value.Height, w = value.Width;
                if (h <= 0)
                {
                    h = 1;
                }

                if (w <= 0)
                {
                    w = 1;
                }

                this.borderRoundRectSize = new Size(w, h);
                this.Invalidate();
            }
        }

        private Orientation barOrientation = Orientation.Horizontal;

        /// <summary>
        /// Gets or sets the orientation of Slider.
        /// </summary>
        /// <value>The orientation.</value>
        [Description("Set Slider orientation")]
        [Category("ColorSlider")]
        [DefaultValue(Orientation.Horizontal)]
        public Orientation Orientation
        {
            get
            {
                return this.barOrientation;
            }

            set
            {
                if (this.barOrientation != value)
                {
                    this.barOrientation = value;
                    int temp = this.Width;
                    this.Width = this.Height;
                    this.Height = temp;
                    if (this.thumbCustomShape != null)
                    {
                        this.thumbSize = (int)(this.barOrientation == Orientation.Horizontal ?
                            this.thumbCustomShape.GetBounds().Width :
                            this.thumbCustomShape.GetBounds().Height) + 1;
                    }

                    this.Invalidate();
                }
            }
        }

        private int trackerValue = 50;

        /// <summary>
        /// Gets or sets the value of Slider.
        /// </summary>
        /// <value>The value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value is outside appropriate range (min, max)</exception>
        [Description("Set Slider value")]
        [Category("ColorSlider")]
        [DefaultValue(50)]
        public int Value
        {
            get
            {
                return this.trackerValue;
            }

            set
            {
                if (value >= barMinimum & value <= barMaximum)
                {
                    int prevValue = this.trackerValue;
                    if (prevValue != value)
                    {
                        this.trackerValue = value;
                        if (this.ValueChanged != null)
                        {
                            this.ValueChanged(this, new EventArgs());
                        }

                        this.Invalidate();
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Value is outside appropriate range (min, max)");
                }
            }
        }

        private int barMinimum = 0;

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when minimal value is greather than maximal one</exception>
        [Description("Set Slider minimal point")]
        [Category("ColorSlider")]
        [DefaultValue(0)]
        public int Minimum
        {
            get
            {
                return this.barMinimum;
            }

            set
            {
                if (value < this.barMaximum)
                {
                    this.barMinimum = value;
                    if (this.Value < this.barMinimum)
                    {
                        this.Value = this.barMinimum;
                    }

                    this.Invalidate();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Minimal value is greather than maximal one");
                }
            }
        }

        private int barMaximum = 100;

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when maximal value is lower than minimal one</exception>
        [Description("Set Slider maximal point")]
        [Category("ColorSlider")]
        [DefaultValue(100)]
        public int Maximum
        {
            get
            {
                return this.barMaximum;
            }

            set
            {
                if (value > this.barMinimum)
                {
                    this.barMaximum = value;
                    if (this.trackerValue > this.barMaximum)
                    {
                        this.Value = this.barMaximum;
                    }

                    this.Invalidate();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Maximal value is lower than minimal one");
                }
            }
        }

        private uint smallChange = 1;

        /// <summary>
        /// Gets or sets trackbar's small change. It affects how to behave when directional keys are pressed
        /// </summary>
        /// <value>The small change value.</value>
        [Description("Set trackbar's small change")]
        [Category("ColorSlider")]
        [DefaultValue(1)]
        public uint SmallChange
        {
            get { return this.smallChange; }
            set { this.smallChange = value; }
        }

        private uint largeChange = 5;

        /// <summary>
        /// Gets or sets trackbar's large change. It affects how to behave when PageUp/PageDown keys are pressed
        /// </summary>
        /// <value>The large change value.</value>
        [Description("Set trackbar's large change")]
        [Category("ColorSlider")]
        [DefaultValue(5)]
        public uint LargeChange
        {
            get { return this.largeChange; }
            set { this.largeChange = value; }
        }

        private bool drawFocusRectangle = true;

        /// <summary>
        /// Gets or sets a value indicating whether to draw focus rectangle.
        /// </summary>
        /// <value><c>true</c> if focus rectangle should be drawn; otherwise, <c>false</c>.</value>
        [Description("Set whether to draw focus rectangle")]
        [Category("ColorSlider")]
        [DefaultValue(true)]
        public bool DrawFocusRectangle
        {
            get
            {
                return this.drawFocusRectangle;
            }

            set
            {
                this.drawFocusRectangle = value;
                this.Invalidate();
            }
        }

        private bool darkenBarIfLess = true;

        /// <summary>
        /// Gets or sets a value indicating whether to darken bar when the value is lower.
        /// </summary>
        /// <value><c>true</c> if darken bar should be drawn; otherwise, <c>false</c>.</value>
        [Description("Set whether to darken bar when the value is lower")]
        [Category("ColorSlider")]
        [DefaultValue(true)]
        public bool DarkenBarIfLess
        {
            get
            {
                return this.darkenBarIfLess;
            }

            set
            {
                this.darkenBarIfLess = value;
                this.Invalidate();
            }
        }

        private bool drawSemitransparentThumb = true;

        /// <summary>
        /// Gets or sets a value indicating whether to draw semitransparent thumb.
        /// </summary>
        /// <value><c>true</c> if semitransparent thumb should be drawn; otherwise, <c>false</c>.</value>
        [Description("Set whether to draw semitransparent thumb")]
        [Category("ColorSlider")]
        [DefaultValue(true)]
        public bool DrawSemitransparentThumb
        {
            get
            {
                return this.drawSemitransparentThumb;
            }

            set
            {
                this.drawSemitransparentThumb = value;
                this.Invalidate();
            }
        }

        private bool mouseEffects = true;

        /// <summary>
        /// Gets or sets whether mouse entry and exit actions have impact on how control look.
        /// </summary>
        /// <value><c>true</c> if mouse entry and exit actions have impact on how control look; otherwise, <c>false</c>.</value>
        [Description("Set whether mouse entry and exit actions have impact on how control look")]
        [Category("ColorSlider")]
        [DefaultValue(true)]
        public bool MouseEffects
        {
            get
            {
                return this.mouseEffects;
            }

            set
            {
                this.mouseEffects = value;
                this.Invalidate();
            }
        }

        private int mouseWheelBarPartitions = 10;

        /// <summary>
        /// Gets or sets the mouse wheel bar partitions.
        /// </summary>
        /// <value>The mouse wheel bar partitions.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value isn't greather than zero</exception>
        [Description("Set to how many parts is bar divided when using mouse wheel")]
        [Category("ColorSlider")]
        [DefaultValue(10)]
        public int MouseWheelBarPartitions
        {
            get
            {
                return this.mouseWheelBarPartitions;
            }

            set
            {
                if (value > 0)
                {
                    this.mouseWheelBarPartitions = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("MouseWheelBarPartitions has to be greather than zero");
                }
            }
        }

        private Color thumbOuterColor = Color.White;

        /// <summary>
        /// Gets or sets the thumb outer color .
        /// </summary>
        /// <value>The thumb outer color.</value>
        [Description("Set Slider thumb outer color")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Color), "White")]
        public Color ThumbOuterColor
        {
            get
            {
                return this.thumbOuterColor;
            }

            set
            {
                this.thumbOuterColor = value;
                this.Invalidate();
            }
        }

        private Color thumbInnerColor = Color.Gainsboro;

        /// <summary>
        /// Gets or sets the inner color of the thumb.
        /// </summary>
        /// <value>The inner color of the thumb.</value>
        [Description("Set Slider thumb inner color")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Color), "Gainsboro")]
        public Color ThumbInnerColor
        {
            get
            {
                return this.thumbInnerColor;
            }

            set
            {
                this.thumbInnerColor = value;
                this.Invalidate();
            }
        }
        
        private Color thumbPenColor = Color.Silver;

        /// <summary>
        /// Gets or sets the color of the thumb pen.
        /// </summary>
        /// <value>The color of the thumb pen.</value>
        [Description("Set Slider thumb pen color")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Color), "Silver")]
        public Color ThumbPenColor
        {
            get
            {
                return this.thumbPenColor;
            }

            set
            {
                this.thumbPenColor = value;
                this.Invalidate();
            }
        }
        
        private Color barOuterColor = Color.SkyBlue;

        /// <summary>
        /// Gets or sets the outer color of the bar.
        /// </summary>
        /// <value>The outer color of the bar.</value>
        [Description("Set Slider bar outer color")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Color), "SkyBlue")]
        public Color BarOuterColor
        {
            get
            {
                return this.barOuterColor;
            }

            set
            {
                this.barOuterColor = value;
                this.Invalidate();
            }
        }
        
        private Color barInnerColor = Color.DarkSlateBlue;

        /// <summary>
        /// Gets or sets the inner color of the bar.
        /// </summary>
        /// <value>The inner color of the bar.</value>
        [Description("Set Slider bar inner color")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Color), "DarkSlateBlue")]
        public Color BarInnerColor
        {
            get
            {
                return this.barInnerColor;
            }

            set
            {
                this.barInnerColor = value;
                this.Invalidate();
            }
        }
        
        private Color barPenColor = Color.Gainsboro;

        /// <summary>
        /// Gets or sets the color of the bar pen.
        /// </summary>
        /// <value>The color of the bar pen.</value>
        [Description("Set Slider bar pen color")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Color), "Gainsboro")]
        public Color BarPenColor
        {
            get
            {
                return this.barPenColor;
            }

            set
            {
                this.barPenColor = value;
                this.Invalidate();
            }
        }

        private Color elapsedOuterColor = Color.DarkGreen;

        /// <summary>
        /// Gets or sets the outer color of the elapsed.
        /// </summary>
        /// <value>The outer color of the elapsed.</value>
        [Description("Set Slider's elapsed part outer color")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Color), "DarkGreen")]
        public Color ElapsedOuterColor
        {
            get
            {
                return this.elapsedOuterColor;
            }

            set
            {
                this.elapsedOuterColor = value;
                this.Invalidate();
            }
        }

        private Color elapsedInnerColor = Color.Chartreuse;

        /// <summary>
        /// Gets or sets the inner color of the elapsed.
        /// </summary>
        /// <value>The inner color of the elapsed.</value>
        [Description("Set Slider's elapsed part inner color")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Color), "Chartreuse")]
        public Color ElapsedInnerColor
        {
            get
            {
                return this.elapsedInnerColor;
            }

            set
            {
                this.elapsedInnerColor = value;
                this.Invalidate();
            }
        }

        #endregion

        #region Color schemas

        //define own color schemas
        private Color[,] predefinedColorSchemas = new Color[,]
            {
                {
                    Color.White,
                    Color.Gainsboro,
                    Color.Silver, 
                    Color.SkyBlue, 
                    Color.DarkSlateBlue, 
                    Color.Gainsboro,
                    Color.DarkGreen, 
                    Color.Chartreuse
                },
                {
                    Color.White, 
                    Color.Gainsboro, 
                    Color.Silver, 
                    Color.Red, 
                    Color.DarkRed, 
                    Color.Gainsboro, 
                    Color.Coral,
                    Color.LightCoral
                },
                {
                    Color.White, 
                    Color.Gainsboro, 
                    Color.Silver, 
                    Color.GreenYellow, 
                    Color.Yellow, 
                    Color.Gold, 
                    Color.Orange,
                    Color.OrangeRed
                },
                {
                    Color.White, 
                    Color.Gainsboro, 
                    Color.Silver, 
                    Color.Red, 
                    Color.Crimson, 
                    Color.Gainsboro, 
                    Color.DarkViolet, 
                    Color.Violet
                }
            };

        /// <summary>
        /// Predefined coloring.
        /// </summary>
        public enum ColorSchemas
        {
            /// <summary>
            /// Perl Blue Green schema
            /// </summary>
            PerlBlueGreen,

            /// <summary>
            /// Perl Red Coral schema
            /// </summary>
            PerlRedCoral,

            /// <summary>
            /// Perl Gold schema
            /// </summary>
            PerlGold,

            /// <summary>
            /// Perl Royal Colors schema
            /// </summary>
            PerlRoyalColors
        }

        private ColorSchemas colorSchema = ColorSchemas.PerlBlueGreen;

        /// <summary>
        /// Sets color schema. Color generalization / fast color changing. Has no effect when slider colors are changed manually after schema was applied. 
        /// </summary>
        /// <value>New color schema value</value>
        [Description("Set Slider color schema. Has no effect when slider colors are changed manually after schema was applied.")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(ColorSchemas), "PerlBlueGreen")]
        public ColorSchemas ColorSchema
        {
            get
            {
                return this.colorSchema;
            }

            set
            {
                this.colorSchema = value;
                byte sn = (byte)value;
                this.thumbOuterColor = this.predefinedColorSchemas[sn, 0];
                this.thumbInnerColor = this.predefinedColorSchemas[sn, 1];
                this.thumbPenColor = this.predefinedColorSchemas[sn, 2];
                this.barOuterColor = this.predefinedColorSchemas[sn, 3];
                this.barInnerColor = this.predefinedColorSchemas[sn, 4];
                this.barPenColor = this.predefinedColorSchemas[sn, 5];
                this.elapsedOuterColor = this.predefinedColorSchemas[sn, 6];
                this.elapsedInnerColor = this.predefinedColorSchemas[sn, 7];

                this.Invalidate();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorSlider"/> class.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="value">The current value.</param>
        public ColorSlider(int min, int max, int value)
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.Selectable |
                     ControlStyles.SupportsTransparentBackColor | ControlStyles.UserMouse |
                     ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;

            this.Minimum = min;
            this.Maximum = max;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorSlider"/> class.
        /// </summary>
        public ColorSlider()
            : this(0, 100, 50)
        {
        }

        #endregion

        #region Paint

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Color[] usedColors = new Color[]
            { 
                thumbOuterColor, thumbInnerColor, thumbPenColor,
                barOuterColor, barInnerColor, barPenColor,
                elapsedOuterColor, elapsedInnerColor 
            };

            if (DarkenBarIfLess)
            {
                usedColors = DarkenColors(usedColors);
            }

            if (!Enabled)
            {
                Color[] desaturatedColors = DesaturateColors(usedColors);
                DrawColorSlider(e, desaturatedColors[0], desaturatedColors[1], desaturatedColors[2],
                                desaturatedColors[3],
                                desaturatedColors[4], desaturatedColors[5], desaturatedColors[6], desaturatedColors[7]);
            }
            else
            {
                if (mouseEffects && mouseInRegion)
                {
                    Color[] lightenedColors = LightenColors(usedColors);
                    DrawColorSlider(e, lightenedColors[0], lightenedColors[1], lightenedColors[2], lightenedColors[3],
                                    lightenedColors[4], lightenedColors[5], lightenedColors[6], lightenedColors[7]);
                }
                else
                {
                    DrawColorSlider(e, usedColors[0], usedColors[1], usedColors[2], usedColors[3],
                                    usedColors[4], usedColors[5], usedColors[6], usedColors[7]);
                }
            }
        }

        /// <summary>
        /// Draws the colorslider control using passed colors.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        /// <param name="thumbOuterColorPaint">The thumb outer color paint.</param>
        /// <param name="thumbInnerColorPaint">The thumb inner color paint.</param>
        /// <param name="thumbPenColorPaint">The thumb pen color paint.</param>
        /// <param name="barOuterColorPaint">The bar outer color paint.</param>
        /// <param name="barInnerColorPaint">The bar inner color paint.</param>
        /// <param name="barPenColorPaint">The bar pen color paint.</param>
        /// <param name="elapsedOuterColorPaint">The elapsed outer color paint.</param>
        /// <param name="elapsedInnerColorPaint">The elapsed inner color paint.</param>
        private void DrawColorSlider(
            PaintEventArgs e, 
            Color thumbOuterColorPaint, 
            Color thumbInnerColorPaint,
            Color thumbPenColorPaint, 
            Color barOuterColorPaint, 
            Color barInnerColorPaint,
            Color barPenColorPaint, 
            Color elapsedOuterColorPaint, 
            Color elapsedInnerColorPaint)
        {
            try
            {
                //set up thumbRect aproprietly
                if (barOrientation == Orientation.Horizontal)
                {
                    int trackX = ((this.Value - this.barMinimum) * (this.ClientRectangle.Width - this.thumbSize)) / (this.barMaximum - this.barMinimum);
                    this.thumbRect = new Rectangle(trackX, 1, this.thumbSize - 1, this.ClientRectangle.Height - 3);
                }
                else
                {
                    int trackY = ((this.Value - this.barMinimum) * (this.ClientRectangle.Height - this.thumbSize)) / (this.barMaximum - this.barMinimum);
                    this.thumbRect = new Rectangle(1, trackY, this.ClientRectangle.Width - 3, this.thumbSize - 1);
                }

                // adjust drawing rects
                this.barRect = this.ClientRectangle;
                this.thumbHalfRect = this.thumbRect;
                LinearGradientMode gradientOrientation;
                if (this.barOrientation == Orientation.Horizontal)
                {
                    this.barRect.Inflate(-1, -barRect.Height / 3);
                    this.barHalfRect = barRect;
                    this.barHalfRect.Height /= 2;
                    gradientOrientation = LinearGradientMode.Vertical;
                    this.thumbHalfRect.Height /= 2;
                    this.elapsedRect = barRect;
                    this.elapsedRect.Width = thumbRect.Left + (thumbSize / 2);
                }
                else
                {
                    this.barRect.Inflate(-barRect.Width / 3, -1);
                    this.barHalfRect = barRect;
                    this.barHalfRect.Width /= 2;
                    gradientOrientation = LinearGradientMode.Horizontal;
                    this.thumbHalfRect.Width /= 2;
                    this.elapsedRect = barRect;
                    this.elapsedRect.Height = thumbRect.Top + (thumbSize / 2);
                }

                // get thumb shape path 
                GraphicsPath thumbPath;
                if (this.thumbCustomShape == null)
                {
                    thumbPath = CreateRoundRectPath(thumbRect, thumbRoundRectSize);
                }
                else
                {
                    thumbPath = this.thumbCustomShape;
                    Matrix m = new Matrix();
                    m.Translate(this.thumbRect.Left - thumbPath.GetBounds().Left, this.thumbRect.Top - thumbPath.GetBounds().Top);
                    thumbPath.Transform(m);
                }

                // draw bar
                using (LinearGradientBrush lgbBar =
                    new LinearGradientBrush(barHalfRect, barOuterColorPaint, barInnerColorPaint, gradientOrientation))
                {
                    lgbBar.WrapMode = WrapMode.TileFlipXY;
                    e.Graphics.FillRectangle(lgbBar, barRect);

                    // draw elapsed bar
                    using (LinearGradientBrush lgbElapsed =
                        new LinearGradientBrush(barHalfRect, elapsedOuterColorPaint, elapsedInnerColorPaint, gradientOrientation))
                    {
                        lgbElapsed.WrapMode = WrapMode.TileFlipXY;
                        if (Capture && drawSemitransparentThumb)
                        {
                            Region elapsedReg = new Region(elapsedRect);
                            elapsedReg.Exclude(thumbPath);
                            e.Graphics.FillRegion(lgbElapsed, elapsedReg);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(lgbElapsed, elapsedRect);
                        }
                    }

                    // draw bar band                    
                    using (Pen barPen = new Pen(barPenColorPaint, 0.5f))
                    {
                        e.Graphics.DrawRectangle(barPen, barRect);
                    }
                }

                // draw thumb
                Color newthumbOuterColorPaint = thumbOuterColorPaint, newthumbInnerColorPaint = thumbInnerColorPaint;
                if (Capture && drawSemitransparentThumb)
                {
                    newthumbOuterColorPaint = Color.FromArgb(175, thumbOuterColorPaint);
                    newthumbInnerColorPaint = Color.FromArgb(175, thumbInnerColorPaint);
                }

                using (LinearGradientBrush lgbThumb =
                    new LinearGradientBrush(thumbHalfRect, newthumbOuterColorPaint, newthumbInnerColorPaint, gradientOrientation))
                {
                    lgbThumb.WrapMode = WrapMode.TileFlipXY;
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(lgbThumb, thumbPath);

                    // draw thumb band
                    Color newThumbPenColor = thumbPenColorPaint;
                    if (mouseEffects && (Capture || mouseInThumbRegion))
                    {
                        newThumbPenColor = ControlPaint.Dark(newThumbPenColor);
                    }

                    using (Pen thumbPen = new Pen(newThumbPenColor))
                    {
                        e.Graphics.DrawPath(thumbPen, thumbPath);
                    }

                    // gp.Dispose();                    
                    /* if (Capture || mouseInThumbRegion)
                        using (LinearGradientBrush lgbThumb2 = new LinearGradientBrush(thumbHalfRect, Color.FromArgb(150, Color.Blue), Color.Transparent, gradientOrientation))
                        {
                            lgbThumb2.WrapMode = WrapMode.TileFlipXY;
                            e.Graphics.FillPath(lgbThumb2, gp);
                        }*/
                }

                // draw focusing rectangle
                if (Focused & drawFocusRectangle)
                {
                    using (Pen p = new Pen(Color.FromArgb(200, barPenColorPaint)))
                    {
                        p.DashStyle = DashStyle.Dot;
                        Rectangle r = ClientRectangle;
                        r.Width -= 2;
                        r.Height--;
                        r.X++;

                        // ControlPaint.DrawFocusRectangle(e.Graphics, r);                        
                        using (GraphicsPath graphPathBorder = CreateRoundRectPath(r, borderRoundRectSize))
                        {
                            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            e.Graphics.DrawPath(p, graphPathBorder);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DrawBackGround Error in " + this.Name + ":" + ex.Message);
            }
        }

        #endregion

        #region Overided events

        private bool mouseInRegion = false;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.mouseInRegion = true;
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.mouseInRegion = false;
            this.mouseInThumbRegion = false;
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = true;
                if (this.Scroll != null)
                {
                    this.Scroll(this, new ScrollEventArgs(ScrollEventType.ThumbTrack, this.Value));
                }

                this.OnMouseMove(e);
            }
        }

        private bool mouseInThumbRegion = false;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.mouseInThumbRegion = IsPointInRect(e.Location, thumbRect);
            if (this.Capture & e.Button == MouseButtons.Left)
            {
                ScrollEventType set = ScrollEventType.ThumbPosition;
                Point pt = e.Location;
                int p = barOrientation == Orientation.Horizontal ? pt.X : pt.Y;
                int margin = thumbSize >> 1;
                p -= margin;
                float coef = (float)(barMaximum - barMinimum) /
                             (float)((barOrientation == Orientation.Horizontal ? ClientSize.Width : ClientSize.Height) - (2 * margin));
                int tmpTrackerValue = (int)((p * coef) + barMinimum + 0.5);

                if (tmpTrackerValue <= barMinimum)
                {
                    this.Value = barMinimum;
                    set = ScrollEventType.First;
                }
                else if (tmpTrackerValue >= barMaximum)
                {
                    this.Value = barMaximum;
                    set = ScrollEventType.Last;
                }
                else
                {
                    this.Value = tmpTrackerValue;
                }

                if (Scroll != null)
                {
                    this.Scroll(this, new ScrollEventArgs(set, Value));
                }
            }

            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.Capture = false;
            this.mouseInThumbRegion = IsPointInRect(e.Location, thumbRect);
            if (this.Scroll != null)
            {
                this.Scroll(this, new ScrollEventArgs(ScrollEventType.EndScroll, this.Value));
            }

            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int v = e.Delta / 120 * (this.barMaximum - this.barMinimum) / this.mouseWheelBarPartitions;
            this.SetProperValue(Value + v);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.KeyUp"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data.</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Left:
                    this.SetProperValue(this.Value - (int)this.smallChange);
                    if (this.Scroll != null)
                    {
                        this.Scroll(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, this.Value));
                    }

                    break;
                case Keys.Up:
                case Keys.Right:
                    this.SetProperValue(this.Value + (int)this.smallChange);
                    if (this.Scroll != null)
                    {
                        this.Scroll(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, this.Value));
                    }

                    break;
                case Keys.Home:
                    this.Value = this.barMinimum;
                    break;
                case Keys.End:
                    this.Value = this.barMaximum;
                    break;
                case Keys.PageDown:
                    this.SetProperValue(this.Value - (int)this.largeChange);
                    if (this.Scroll != null)
                    {
                        Scroll(this, new ScrollEventArgs(ScrollEventType.LargeDecrement, this.Value));
                    }

                    break;
                case Keys.PageUp:
                    this.SetProperValue(this.Value + (int)this.largeChange);
                    if (this.Scroll != null)
                    {
                        this.Scroll(this, new ScrollEventArgs(ScrollEventType.LargeIncrement, this.Value));
                    }

                    break;
            }

            if (this.Scroll != null && this.Value == this.barMinimum)
            {
                this.Scroll(this, new ScrollEventArgs(ScrollEventType.First, this.Value));
            }

            if (this.Scroll != null && this.Value == this.barMaximum)
            {
                this.Scroll(this, new ScrollEventArgs(ScrollEventType.Last, this.Value));
            }

            Point pt = this.PointToClient(Cursor.Position);
            this.OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, pt.X, pt.Y, 0));
        }

        /// <summary>
        /// Processes a dialog key.
        /// </summary>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"></see> values that represents the key to process.</param>
        /// <returns>
        /// true if the key was processed by the control; otherwise, false.
        /// </returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Tab | ModifierKeys == Keys.Shift)
            {
                return base.ProcessDialogKey(keyData);
            }
            else
            {
                this.OnKeyDown(new KeyEventArgs(keyData));
                return true;
            }
        }

        #endregion

        #region Help routines

        /// <summary>
        /// Creates the round rect path.
        /// </summary>
        /// <param name="rect">The rectangle on which graphics path will be spanned.</param>
        /// <param name="size">The size of rounded rectangle edges.</param>
        /// <returns>The path for rounded rectangle.</returns>
        public static GraphicsPath CreateRoundRectPath(Rectangle rect, Size size)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(rect.Left + (size.Width / 2), rect.Top, rect.Right - (size.Width / 2), rect.Top);
            gp.AddArc(rect.Right - size.Width, rect.Top, size.Width, size.Height, 270, 90);

            gp.AddLine(rect.Right, rect.Top + (size.Height / 2), rect.Right, rect.Bottom - (size.Width / 2));
            gp.AddArc(rect.Right - size.Width, rect.Bottom - size.Height, size.Width, size.Height, 0, 90);

            gp.AddLine(rect.Right - (size.Width / 2), rect.Bottom, rect.Left + (size.Width / 2), rect.Bottom);
            gp.AddArc(rect.Left, rect.Bottom - size.Height, size.Width, size.Height, 90, 90);

            gp.AddLine(rect.Left, rect.Bottom - (size.Height / 2), rect.Left, rect.Top + (size.Height / 2));
            gp.AddArc(rect.Left, rect.Top, size.Width, size.Height, 180, 90);
            return gp;
        }

        /// <summary>
        /// Desaturates colors from given array.
        /// </summary>
        /// <param name="colorsToDesaturate">The colors to be desaturated.</param>
        /// <returns>The list of same colors but desaturated.</returns>
        public static Color[] DesaturateColors(params Color[] colorsToDesaturate)
        {
            Color[] colorsToReturn = new Color[colorsToDesaturate.Length];
            for (int i = 0; i < colorsToDesaturate.Length; i++)
            {
                // use NTSC weighted avarage
                int gray = (int)((colorsToDesaturate[i].R * 0.3) + (colorsToDesaturate[i].G * 0.6) + (colorsToDesaturate[i].B * 0.1));
                colorsToReturn[i] = Color.FromArgb((-0x010101 * (255 - gray)) - 1);
            }

            return colorsToReturn;
        }

        /// <summary>
        /// Lightens colors from given array.
        /// </summary>
        /// <param name="colorsToLighten">The colors to lighten.</param>
        /// <returns>The list of same color but lighten.</returns>
        public static Color[] LightenColors(params Color[] colorsToLighten)
        {
            Color[] colorsToReturn = new Color[colorsToLighten.Length];
            for (int i = 0; i < colorsToLighten.Length; i++)
            {
                colorsToReturn[i] = ControlPaint.Light(colorsToLighten[i]);
            }

            return colorsToReturn;
        }

        /// <summary>
        /// Darken colors from given array.
        /// </summary>
        /// <param name="colorsToDarken">The colors to darken.</param>
        /// <returns>The list of same colors but darken.</returns>
        public Color[] DarkenColors(params Color[] colorsToDarken)
        {
            Color[] colorsToReturn = new Color[colorsToDarken.Length];
            for (int i = 0; i < colorsToDarken.Length; i++)
            {
                colorsToReturn[i] = ControlPaint.Dark(
                    colorsToDarken[i],
                    1 - (float)Math.Log(((float)Value - Minimum + 2) / 2, Maximum / 2));
            }

            return colorsToReturn;
        }

        /// <summary>
        /// Sets the trackbar value so that it wont exceed allowed range.
        /// </summary>
        /// <param name="val">The value.</param>
        private void SetProperValue(int val)
        {
            if (val < this.barMinimum)
            {
                this.Value = this.barMinimum;
            }
            else if (val > this.barMaximum)
            {
                this.Value = this.barMaximum;
            }
            else
            {
                this.Value = val;
            }
        }

        /// <summary>
        /// Determines whether rectangle contains given point.
        /// </summary>
        /// <param name="pt">The point to test.</param>
        /// <param name="rect">The base rectangle.</param>
        /// <returns>
        /// <c>true</c> if rectangle contains given point; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsPointInRect(Point pt, Rectangle rect)
        {
            return pt.X > rect.Left & pt.X < rect.Right & pt.Y > rect.Top & pt.Y < rect.Bottom;
        }

        #endregion
    }
}