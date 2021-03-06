using System;
using System.ComponentModel;
using System.Drawing;

namespace WomenCalendar
{
    /// <summary>
    /// A custom windows control to display text vertically
    /// </summary>
    public class VerticalLabel : System.Windows.Forms.Control
    {
        private string labelText;
        private DrawMode drawMode = DrawMode.BottomUp;

        private System.ComponentModel.Container components = new System.ComponentModel.Container();

        /// <summary>
        /// VerticalLabel constructor
        /// </summary>
        public VerticalLabel()
        {
            // Original code had that line, but is cores under Mono.
            // Looks redundant so I decided to commented it out. Still works fine.
            // base.CreateControl();
            this.InitializeComponent();
        }

        /// <summary>
        /// Text Drawing Mode
        /// </summary>
        public enum DrawMode
        {
            /// <summary>
            /// Text is drawn from bottom - up
            /// </summary>
            BottomUp = 1,

            /// <summary>
            /// Text is drawn from top to bottom
            /// </summary>
            TopBottom
        }

        /// <summary>
        /// The text to be displayed in the control
        /// </summary>
        [Category("VerticalLabel"), Description("Text is displayed vertically in container.")]
        public override string Text
        {
            get
            {
                return this.labelText;
            }

            set
            {
                this.labelText = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Whether the text will be drawn from Bottom or from Top.
        /// </summary>
        [Category("Properties"), Description("Whether the text will be drawn from Bottom or from Top.")]
        public DrawMode TextDrawMode
        {
            get { return this.drawMode; }
            set { this.drawMode = value; }
        }

        /// <summary>
        /// Dispose override method
        /// </summary>
        /// <param name="disposing">boolean parameter</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// OnPaint override. This is where the text is rendered vertically.
        /// </summary>
        /// <param name="e">Paint arguments.</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            float vlblControlWidth;
            float vlblControlHeight;
            float vlblTransformX;
            float vlblTransformY;
            Color controlBackColor = BackColor;
            Pen labelBorderPen = new Pen(controlBackColor, 0);
            SolidBrush labelBackColorBrush = new SolidBrush(controlBackColor);
            SolidBrush labelForeColorBrush = new SolidBrush(this.ForeColor);
            base.OnPaint(e);
            vlblControlWidth = this.Size.Width;
            vlblControlHeight = this.Size.Height;
            e.Graphics.DrawRectangle(labelBorderPen, 0, 0, vlblControlWidth, vlblControlHeight);
            e.Graphics.FillRectangle(labelBackColorBrush, 0, 0, vlblControlWidth, vlblControlHeight);
            
            if (this.TextDrawMode == DrawMode.BottomUp)
            {
                vlblTransformX = 0;
                vlblTransformY = vlblControlHeight;
                e.Graphics.TranslateTransform(vlblTransformX, vlblTransformY);
                e.Graphics.RotateTransform(270);
                e.Graphics.DrawString(this.labelText, this.Font, labelForeColorBrush, 0, 0);
            }
            else
            {
                // vlblTransformX = vlblControlWidth;
                // vlblTransformY = vlblControlHeight;
                e.Graphics.TranslateTransform(vlblControlWidth, 0);
                e.Graphics.RotateTransform(90);
                e.Graphics.DrawString(this.labelText, Font, labelForeColorBrush, 0, 0, StringFormat.GenericTypographic);
            }
        }

        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(24, 100);
        }

        private void VerticalTextBox_Resize(object sender, System.EventArgs e)
        {
            this.Invalidate();
        }
    }
}
