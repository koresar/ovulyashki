namespace WomenCalendar.Controls
{
    partial class MensesEditControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpMenstr = new System.Windows.Forms.GroupBox();
            this.pnlSurround = new System.Windows.Forms.Panel();
            this.lblDays = new System.Windows.Forms.Label();
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.lblLength = new System.Windows.Forms.Label();
            this.ttExcreta = new System.Windows.Forms.ToolTip(this.components);
            this.ttLength = new System.Windows.Forms.ToolTip(this.components);
            this.verticalLabel1 = new WomenCalendar.VerticalLabel();
            this.sliderEgestaAmount = new MB.Controls.ColorSlider();
            this.grpMenstr.SuspendLayout();
            this.pnlSurround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMenstr
            // 
            this.grpMenstr.BackColor = System.Drawing.Color.Transparent;
            this.grpMenstr.Controls.Add(this.pnlSurround);
            this.grpMenstr.Controls.Add(this.verticalLabel1);
            this.grpMenstr.Controls.Add(this.sliderEgestaAmount);
            this.grpMenstr.Location = new System.Drawing.Point(3, 3);
            this.grpMenstr.Name = "grpMenstr";
            this.grpMenstr.Size = new System.Drawing.Size(90, 243);
            this.grpMenstr.TabIndex = 10009;
            this.grpMenstr.TabStop = false;
            this.grpMenstr.Text = "Menses";
            // 
            // pnlSurround
            // 
            this.pnlSurround.BackColor = System.Drawing.Color.Transparent;
            this.pnlSurround.Controls.Add(this.lblDays);
            this.pnlSurround.Controls.Add(this.numLength);
            this.pnlSurround.Controls.Add(this.lblLength);
            this.pnlSurround.Location = new System.Drawing.Point(2, 192);
            this.pnlSurround.Name = "pnlSurround";
            this.pnlSurround.Size = new System.Drawing.Size(86, 48);
            this.pnlSurround.TabIndex = 10014;
            // 
            // lblDays
            // 
            this.lblDays.AutoSize = true;
            this.lblDays.BackColor = System.Drawing.Color.Transparent;
            this.lblDays.Location = new System.Drawing.Point(41, 27);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(29, 13);
            this.lblDays.TabIndex = 10011;
            this.lblDays.Text = "days";
            // 
            // numLength
            // 
            this.numLength.Location = new System.Drawing.Point(3, 25);
            this.numLength.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLength.Name = "numLength";
            this.numLength.Size = new System.Drawing.Size(32, 20);
            this.numLength.TabIndex = 1;
            this.numLength.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numLength.ValueChanged += new System.EventHandler(this.numLength_ValueChanged);
            // 
            // lblLength
            // 
            this.lblLength.BackColor = System.Drawing.Color.Transparent;
            this.lblLength.Location = new System.Drawing.Point(2, 5);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(82, 17);
            this.lblLength.TabIndex = 10009;
            this.lblLength.Text = "Length";
            // 
            // ttExcreta
            // 
            this.ttExcreta.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttExcreta.ToolTipTitle = "Excreta amount";
            this.ttExcreta.UseAnimation = false;
            this.ttExcreta.UseFading = false;
            // 
            // ttLength
            // 
            this.ttLength.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttLength.ToolTipTitle = "Please set menses length!";
            this.ttLength.UseAnimation = false;
            this.ttLength.UseFading = false;
            // 
            // verticalLabel1
            // 
            this.verticalLabel1.Location = new System.Drawing.Point(7, 61);
            this.verticalLabel1.Name = "verticalLabel1";
            this.verticalLabel1.Size = new System.Drawing.Size(16, 83);
            this.verticalLabel1.TabIndex = 10008;
            this.verticalLabel1.Text = "Intensity";
            this.verticalLabel1.TextDrawMode = WomenCalendar.DrawMode.BottomUp;
            // 
            // sliderEgestaAmount
            // 
            this.sliderEgestaAmount.BackColor = System.Drawing.Color.Transparent;
            this.sliderEgestaAmount.BarInnerColor = System.Drawing.Color.Pink;
            this.sliderEgestaAmount.BarOuterColor = System.Drawing.Color.DeepPink;
            this.sliderEgestaAmount.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderEgestaAmount.DarkenBarIfLess = false;
            this.sliderEgestaAmount.ElapsedInnerColor = System.Drawing.Color.Pink;
            this.sliderEgestaAmount.ElapsedOuterColor = System.Drawing.Color.DeepPink;
            this.sliderEgestaAmount.LargeChange = ((uint)(5u));
            this.sliderEgestaAmount.Location = new System.Drawing.Point(32, 19);
            this.sliderEgestaAmount.Maximum = 4;
            this.sliderEgestaAmount.Name = "sliderEgestaAmount";
            this.sliderEgestaAmount.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderEgestaAmount.Size = new System.Drawing.Size(30, 167);
            this.sliderEgestaAmount.SmallChange = ((uint)(1u));
            this.sliderEgestaAmount.TabIndex = 0;
            this.sliderEgestaAmount.Text = "colorSlider1";
            this.sliderEgestaAmount.ThumbInnerColor = System.Drawing.Color.White;
            this.sliderEgestaAmount.ThumbOuterColor = System.Drawing.Color.Pink;
            this.sliderEgestaAmount.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderEgestaAmount.Value = 0;
            this.sliderEgestaAmount.ValueChanged += new System.EventHandler(this.sliderEgestaAmount_ValueChanged);
            // 
            // MensesEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMenstr);
            this.Name = "MensesEditControl";
            this.Size = new System.Drawing.Size(96, 249);
            this.VisibleChanged += new System.EventHandler(this.MensesEditControl_VisibleChanged);
            this.grpMenstr.ResumeLayout(false);
            this.pnlSurround.ResumeLayout(false);
            this.pnlSurround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMenstr;
        private System.Windows.Forms.Panel pnlSurround;
        private System.Windows.Forms.Label lblDays;
        private System.Windows.Forms.NumericUpDown numLength;
        private System.Windows.Forms.Label lblLength;
        private VerticalLabel verticalLabel1;
        private MB.Controls.ColorSlider sliderEgestaAmount;
        private System.Windows.Forms.ToolTip ttExcreta;
        private System.Windows.Forms.ToolTip ttLength;
    }
}
