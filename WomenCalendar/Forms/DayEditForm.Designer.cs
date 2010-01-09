namespace WomenCalendar
{
    partial class DayEditForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rbtCF3 = new System.Windows.Forms.RadioButton();
            this.rbtCF2 = new System.Windows.Forms.RadioButton();
            this.rbtCF1 = new System.Windows.Forms.RadioButton();
            this.sliderEgestaAmount = new MB.Controls.ColorSlider();
            this.txtBBT = new System.Windows.Forms.TextBox();
            this.lblBBT = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.numMenstruationLength = new System.Windows.Forms.NumericUpDown();
            this.btnPrevDay = new System.Windows.Forms.Button();
            this.btnNextDay = new System.Windows.Forms.Button();
            this.chkHadSex = new System.Windows.Forms.CheckBox();
            this.sliderHealth = new MB.Controls.ColorSlider();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grpMenstr = new System.Windows.Forms.GroupBox();
            this.pnlSurroundMentsLength = new System.Windows.Forms.Panel();
            this.lblMenstruationLength = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.verticalLabel1 = new WomenCalendar.VerticalLabel();
            this.grpNote = new System.Windows.Forms.GroupBox();
            this.grpBT = new System.Windows.Forms.GroupBox();
            this.grpHealth = new System.Windows.Forms.GroupBox();
            this.chkMentrustions = new System.Windows.Forms.CheckBox();
            this.grpCF = new System.Windows.Forms.GroupBox();
            this.toolTipCF = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numMenstruationLength)).BeginInit();
            this.grpMenstr.SuspendLayout();
            this.pnlSurroundMentsLength.SuspendLayout();
            this.grpNote.SuspendLayout();
            this.grpBT.SuspendLayout();
            this.grpHealth.SuspendLayout();
            this.grpCF.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtCF3
            // 
            this.rbtCF3.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtCF3.Location = new System.Drawing.Point(71, 13);
            this.rbtCF3.Name = "rbtCF3";
            this.rbtCF3.Size = new System.Drawing.Size(24, 24);
            this.rbtCF3.TabIndex = 10014;
            this.rbtCF3.TabStop = true;
            this.rbtCF3.Tag = "";
            this.rbtCF3.Text = "W";
            this.rbtCF3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTipCF.SetToolTip(this.rbtCF3, "Water");
            this.rbtCF3.UseVisualStyleBackColor = true;
            this.rbtCF3.Click += new System.EventHandler(this.rbtCF_Click);
            // 
            // rbtCF2
            // 
            this.rbtCF2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtCF2.Location = new System.Drawing.Point(39, 13);
            this.rbtCF2.Name = "rbtCF2";
            this.rbtCF2.Size = new System.Drawing.Size(24, 24);
            this.rbtCF2.TabIndex = 10014;
            this.rbtCF2.TabStop = true;
            this.rbtCF2.Tag = "";
            this.rbtCF2.Text = "S";
            this.rbtCF2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTipCF.SetToolTip(this.rbtCF2, "Stretchy");
            this.rbtCF2.UseVisualStyleBackColor = true;
            this.rbtCF2.Click += new System.EventHandler(this.rbtCF_Click);
            // 
            // rbtCF1
            // 
            this.rbtCF1.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtCF1.Location = new System.Drawing.Point(7, 13);
            this.rbtCF1.Name = "rbtCF1";
            this.rbtCF1.Size = new System.Drawing.Size(24, 24);
            this.rbtCF1.TabIndex = 10014;
            this.rbtCF1.TabStop = true;
            this.rbtCF1.Tag = "";
            this.rbtCF1.Text = "T";
            this.rbtCF1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTipCF.SetToolTip(this.rbtCF1, "Tacky");
            this.rbtCF1.UseVisualStyleBackColor = true;
            this.rbtCF1.Click += new System.EventHandler(this.rbtCF_Click);
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
            this.sliderEgestaAmount.MouseLeave += new System.EventHandler(this.sliderEgestaAmount_MouseLeave);
            this.sliderEgestaAmount.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sliderEgestaAmount_Scroll);
            this.sliderEgestaAmount.MouseEnter += new System.EventHandler(this.sliderEgestaAmount_MouseEnter);
            // 
            // txtBBT
            // 
            this.txtBBT.CausesValidation = false;
            this.txtBBT.Location = new System.Drawing.Point(11, 17);
            this.txtBBT.MaxLength = 5;
            this.txtBBT.Name = "txtBBT";
            this.txtBBT.Size = new System.Drawing.Size(48, 20);
            this.txtBBT.TabIndex = 0;
            this.txtBBT.Leave += new System.EventHandler(this.txtBBT_Leave);
            // 
            // lblBBT
            // 
            this.lblBBT.AutoSize = true;
            this.lblBBT.Location = new System.Drawing.Point(10, 260);
            this.lblBBT.Name = "lblBBT";
            this.lblBBT.Size = new System.Drawing.Size(0, 13);
            this.lblBBT.TabIndex = 0;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(8, 23);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(267, 120);
            this.txtNote.TabIndex = 0;
            this.txtNote.Leave += new System.EventHandler(this.txtNote_Leave);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 0;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.UseAnimation = false;
            this.toolTip.UseFading = false;
            // 
            // numMenstruationLength
            // 
            this.numMenstruationLength.Location = new System.Drawing.Point(3, 25);
            this.numMenstruationLength.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numMenstruationLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMenstruationLength.Name = "numMenstruationLength";
            this.numMenstruationLength.Size = new System.Drawing.Size(32, 20);
            this.numMenstruationLength.TabIndex = 1;
            this.numMenstruationLength.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMenstruationLength.ValueChanged += new System.EventHandler(this.numMenstruationLength_ValueChanged);
            // 
            // btnPrevDay
            // 
            this.btnPrevDay.Location = new System.Drawing.Point(5, 13);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(124, 23);
            this.btnPrevDay.TabIndex = 0;
            this.btnPrevDay.Text = "<< Previous day";
            this.btnPrevDay.UseVisualStyleBackColor = true;
            this.btnPrevDay.Click += new System.EventHandler(this.btnPrevDay_Click);
            // 
            // btnNextDay
            // 
            this.btnNextDay.Location = new System.Drawing.Point(164, 13);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(124, 23);
            this.btnNextDay.TabIndex = 1;
            this.btnNextDay.Text = "Next day >>";
            this.btnNextDay.UseVisualStyleBackColor = true;
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // chkHadSex
            // 
            this.chkHadSex.AutoSize = true;
            this.chkHadSex.Location = new System.Drawing.Point(12, 202);
            this.chkHadSex.Name = "chkHadSex";
            this.chkHadSex.Size = new System.Drawing.Size(113, 17);
            this.chkHadSex.TabIndex = 0;
            this.chkHadSex.Text = "I had sex that day!";
            this.chkHadSex.UseVisualStyleBackColor = true;
            // 
            // sliderHealth
            // 
            this.sliderHealth.BackColor = System.Drawing.Color.Transparent;
            this.sliderHealth.BarInnerColor = System.Drawing.Color.AliceBlue;
            this.sliderHealth.BarOuterColor = System.Drawing.Color.LightCyan;
            this.sliderHealth.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderHealth.DrawFocusRectangle = false;
            this.sliderHealth.ElapsedInnerColor = System.Drawing.Color.AliceBlue;
            this.sliderHealth.ElapsedOuterColor = System.Drawing.Color.LightCyan;
            this.sliderHealth.LargeChange = ((uint)(5u));
            this.sliderHealth.Location = new System.Drawing.Point(54, 10);
            this.sliderHealth.Maximum = 10;
            this.sliderHealth.Name = "sliderHealth";
            this.sliderHealth.Size = new System.Drawing.Size(152, 30);
            this.sliderHealth.SmallChange = ((uint)(1u));
            this.sliderHealth.TabIndex = 0;
            this.sliderHealth.Text = "colorSlider1";
            this.sliderHealth.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderHealth.Value = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 23);
            this.label2.TabIndex = 10007;
            this.label2.Text = "Bad";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(209, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 23);
            this.label3.TabIndex = 10007;
            this.label3.Text = "Good";
            // 
            // grpMenstr
            // 
            this.grpMenstr.Controls.Add(this.pnlSurroundMentsLength);
            this.grpMenstr.Controls.Add(this.verticalLabel1);
            this.grpMenstr.Controls.Add(this.sliderEgestaAmount);
            this.grpMenstr.Location = new System.Drawing.Point(327, 42);
            this.grpMenstr.Name = "grpMenstr";
            this.grpMenstr.Size = new System.Drawing.Size(90, 243);
            this.grpMenstr.TabIndex = 10008;
            this.grpMenstr.TabStop = false;
            this.grpMenstr.Text = "Menses";
            // 
            // pnlSurroundMentsLength
            // 
            this.pnlSurroundMentsLength.BackColor = System.Drawing.Color.Transparent;
            this.pnlSurroundMentsLength.Controls.Add(this.lblMenstruationLength);
            this.pnlSurroundMentsLength.Controls.Add(this.numMenstruationLength);
            this.pnlSurroundMentsLength.Controls.Add(this.label1);
            this.pnlSurroundMentsLength.Location = new System.Drawing.Point(2, 192);
            this.pnlSurroundMentsLength.Name = "pnlSurroundMentsLength";
            this.pnlSurroundMentsLength.Size = new System.Drawing.Size(86, 48);
            this.pnlSurroundMentsLength.TabIndex = 10014;
            this.pnlSurroundMentsLength.MouseLeave += new System.EventHandler(this.pnlSurroundMentsLength_MouseLeave);
            this.pnlSurroundMentsLength.MouseEnter += new System.EventHandler(this.pnlSurroundMentsLength_MouseEnter);
            // 
            // lblMenstruationLength
            // 
            this.lblMenstruationLength.AutoSize = true;
            this.lblMenstruationLength.BackColor = System.Drawing.Color.Transparent;
            this.lblMenstruationLength.Location = new System.Drawing.Point(41, 27);
            this.lblMenstruationLength.Name = "lblMenstruationLength";
            this.lblMenstruationLength.Size = new System.Drawing.Size(29, 13);
            this.lblMenstruationLength.TabIndex = 10011;
            this.lblMenstruationLength.Text = "days";
            this.lblMenstruationLength.MouseLeave += new System.EventHandler(this.lblMenstruationLength_MouseLeave);
            this.lblMenstruationLength.MouseEnter += new System.EventHandler(this.lblMenstruationLength_MouseEnter);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(2, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 10009;
            this.label1.Text = "Length";
            this.label1.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            this.label1.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
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
            // grpNote
            // 
            this.grpNote.Controls.Add(this.txtNote);
            this.grpNote.Location = new System.Drawing.Point(5, 42);
            this.grpNote.Name = "grpNote";
            this.grpNote.Size = new System.Drawing.Size(283, 149);
            this.grpNote.TabIndex = 10009;
            this.grpNote.TabStop = false;
            this.grpNote.Text = "Note for this day";
            // 
            // grpBT
            // 
            this.grpBT.Controls.Add(this.txtBBT);
            this.grpBT.Location = new System.Drawing.Point(5, 277);
            this.grpBT.Name = "grpBT";
            this.grpBT.Size = new System.Drawing.Size(170, 43);
            this.grpBT.TabIndex = 10010;
            this.grpBT.TabStop = false;
            this.grpBT.Text = "Basal Body Temperature";
            // 
            // grpHealth
            // 
            this.grpHealth.BackColor = System.Drawing.Color.Transparent;
            this.grpHealth.Controls.Add(this.sliderHealth);
            this.grpHealth.Controls.Add(this.label2);
            this.grpHealth.Controls.Add(this.label3);
            this.grpHealth.Location = new System.Drawing.Point(5, 225);
            this.grpHealth.Name = "grpHealth";
            this.grpHealth.Size = new System.Drawing.Size(267, 46);
            this.grpHealth.TabIndex = 10011;
            this.grpHealth.TabStop = false;
            this.grpHealth.Text = "Wellbeing";
            // 
            // chkMentrustions
            // 
            this.chkMentrustions.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkMentrustions.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chkMentrustions.Image = global::WomenCalendar.Properties.Resources.drop_Image;
            this.chkMentrustions.Location = new System.Drawing.Point(294, 126);
            this.chkMentrustions.Name = "chkMentrustions";
            this.chkMentrustions.Size = new System.Drawing.Size(27, 65);
            this.chkMentrustions.TabIndex = 10013;
            this.chkMentrustions.Text = ">>          >>";
            this.chkMentrustions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMentrustions.UseVisualStyleBackColor = true;
            this.chkMentrustions.MouseLeave += new System.EventHandler(this.chkMentrustions_MouseLeave);
            this.chkMentrustions.MouseEnter += new System.EventHandler(this.chkMentrustions_MouseEnter);
            this.chkMentrustions.CheckedChanged += new System.EventHandler(this.chkMentrustions_CheckedChanged);
            // 
            // grpCF
            // 
            this.grpCF.Controls.Add(this.rbtCF3);
            this.grpCF.Controls.Add(this.rbtCF2);
            this.grpCF.Controls.Add(this.rbtCF1);
            this.grpCF.Location = new System.Drawing.Point(181, 277);
            this.grpCF.Name = "grpCF";
            this.grpCF.Size = new System.Drawing.Size(102, 43);
            this.grpCF.TabIndex = 10010;
            this.grpCF.TabStop = false;
            this.grpCF.Text = "CF";
            // 
            // toolTipCF
            // 
            this.toolTipCF.ToolTipTitle = "Cervial fuild";
            // 
            // DayEditForm
            // 
            this.AcceptButton = null;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(427, 360);
            this.Controls.Add(this.chkMentrustions);
            this.Controls.Add(this.btnNextDay);
            this.Controls.Add(this.chkHadSex);
            this.Controls.Add(this.btnPrevDay);
            this.Controls.Add(this.lblBBT);
            this.Controls.Add(this.grpMenstr);
            this.Controls.Add(this.grpNote);
            this.Controls.Add(this.grpCF);
            this.Controls.Add(this.grpBT);
            this.Controls.Add(this.grpHealth);
            this.Name = "DayEditForm";
            this.Text = "Edit this day";
            this.Load += new System.EventHandler(this.DayEditForm_Load);
            this.Shown += new System.EventHandler(this.DayEditForm_Shown);
            this.Controls.SetChildIndex(this.grpHealth, 0);
            this.Controls.SetChildIndex(this.grpBT, 0);
            this.Controls.SetChildIndex(this.grpCF, 0);
            this.Controls.SetChildIndex(this.grpNote, 0);
            this.Controls.SetChildIndex(this.grpMenstr, 0);
            this.Controls.SetChildIndex(this.lblBBT, 0);
            this.Controls.SetChildIndex(this.btnPrevDay, 0);
            this.Controls.SetChildIndex(this.chkHadSex, 0);
            this.Controls.SetChildIndex(this.btnNextDay, 0);
            this.Controls.SetChildIndex(this.chkMentrustions, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numMenstruationLength)).EndInit();
            this.grpMenstr.ResumeLayout(false);
            this.pnlSurroundMentsLength.ResumeLayout(false);
            this.pnlSurroundMentsLength.PerformLayout();
            this.grpNote.ResumeLayout(false);
            this.grpNote.PerformLayout();
            this.grpBT.ResumeLayout(false);
            this.grpBT.PerformLayout();
            this.grpHealth.ResumeLayout(false);
            this.grpCF.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MB.Controls.ColorSlider sliderEgestaAmount;
        private System.Windows.Forms.TextBox txtBBT;
        private System.Windows.Forms.Label lblBBT;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnPrevDay;
        private System.Windows.Forms.Button btnNextDay;
        private System.Windows.Forms.CheckBox chkHadSex;
        private MB.Controls.ColorSlider sliderHealth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpMenstr;
        private System.Windows.Forms.GroupBox grpNote;
        private System.Windows.Forms.GroupBox grpBT;
        private System.Windows.Forms.GroupBox grpHealth;
        private VerticalLabel verticalLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numMenstruationLength;
        private System.Windows.Forms.Label lblMenstruationLength;
        private System.Windows.Forms.CheckBox chkMentrustions;
        private System.Windows.Forms.Panel pnlSurroundMentsLength;
        private System.Windows.Forms.GroupBox grpCF;
        private System.Windows.Forms.ToolTip toolTipCF;
        private System.Windows.Forms.RadioButton rbtCF3;
        private System.Windows.Forms.RadioButton rbtCF2;
        private System.Windows.Forms.RadioButton rbtCF1;

    }
}