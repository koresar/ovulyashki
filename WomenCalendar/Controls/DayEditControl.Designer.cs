namespace WomenCalendar
{
    partial class DayEditControl
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
            this.chkHadSex = new System.Windows.Forms.CheckBox();
            this.lblBBT = new System.Windows.Forms.Label();
            this.grpNote = new System.Windows.Forms.GroupBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.grpCF = new System.Windows.Forms.GroupBox();
            this.rbtCF3 = new System.Windows.Forms.RadioButton();
            this.rbtCF2 = new System.Windows.Forms.RadioButton();
            this.rbtCF1 = new System.Windows.Forms.RadioButton();
            this.grpBT = new System.Windows.Forms.GroupBox();
            this.txtBBT = new System.Windows.Forms.TextBox();
            this.grpHealth = new System.Windows.Forms.GroupBox();
            this.lblBadWellbeing = new System.Windows.Forms.Label();
            this.lblGoodWellbeing = new System.Windows.Forms.Label();
            this.ttBBT = new System.Windows.Forms.ToolTip(this.components);
            this.ttCF = new System.Windows.Forms.ToolTip(this.components);
            this.sliderHealth = new MB.Controls.ColorSlider();
            this.grpNote.SuspendLayout();
            this.grpCF.SuspendLayout();
            this.grpBT.SuspendLayout();
            this.grpHealth.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkHadSex
            // 
            this.chkHadSex.AutoSize = true;
            this.chkHadSex.Location = new System.Drawing.Point(9, 163);
            this.chkHadSex.Name = "chkHadSex";
            this.chkHadSex.Size = new System.Drawing.Size(113, 17);
            this.chkHadSex.TabIndex = 10013;
            this.chkHadSex.Text = "I had sex that day!";
            this.chkHadSex.UseVisualStyleBackColor = true;
            // 
            // lblBBT
            // 
            this.lblBBT.AutoSize = true;
            this.lblBBT.Location = new System.Drawing.Point(7, 221);
            this.lblBBT.Name = "lblBBT";
            this.lblBBT.Size = new System.Drawing.Size(0, 13);
            this.lblBBT.TabIndex = 10012;
            // 
            // grpNote
            // 
            this.grpNote.Controls.Add(this.txtNote);
            this.grpNote.Location = new System.Drawing.Point(2, 3);
            this.grpNote.Name = "grpNote";
            this.grpNote.Size = new System.Drawing.Size(238, 149);
            this.grpNote.TabIndex = 10014;
            this.grpNote.TabStop = false;
            this.grpNote.Text = "Note for this day";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(8, 23);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(224, 120);
            this.txtNote.TabIndex = 0;
            this.txtNote.Leave += new System.EventHandler(this.txtNote_Leave);
            // 
            // grpCF
            // 
            this.grpCF.Controls.Add(this.rbtCF3);
            this.grpCF.Controls.Add(this.rbtCF2);
            this.grpCF.Controls.Add(this.rbtCF1);
            this.grpCF.Location = new System.Drawing.Point(2, 287);
            this.grpCF.Name = "grpCF";
            this.grpCF.Size = new System.Drawing.Size(238, 43);
            this.grpCF.TabIndex = 10016;
            this.grpCF.TabStop = false;
            this.grpCF.Text = "Cerfical Fluid";
            // 
            // rbtCF3
            // 
            this.rbtCF3.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtCF3.Location = new System.Drawing.Point(75, 13);
            this.rbtCF3.Name = "rbtCF3";
            this.rbtCF3.Size = new System.Drawing.Size(24, 24);
            this.rbtCF3.TabIndex = 10014;
            this.rbtCF3.TabStop = true;
            this.rbtCF3.Tag = "";
            this.rbtCF3.Text = "W";
            this.rbtCF3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtCF3.UseVisualStyleBackColor = true;
            this.rbtCF3.Click += new System.EventHandler(this.rbtCF_Click);
            // 
            // rbtCF2
            // 
            this.rbtCF2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtCF2.Location = new System.Drawing.Point(41, 13);
            this.rbtCF2.Name = "rbtCF2";
            this.rbtCF2.Size = new System.Drawing.Size(24, 24);
            this.rbtCF2.TabIndex = 10014;
            this.rbtCF2.TabStop = true;
            this.rbtCF2.Tag = "";
            this.rbtCF2.Text = "S";
            this.rbtCF2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.rbtCF1.UseVisualStyleBackColor = true;
            this.rbtCF1.Click += new System.EventHandler(this.rbtCF_Click);
            // 
            // grpBT
            // 
            this.grpBT.Controls.Add(this.txtBBT);
            this.grpBT.Location = new System.Drawing.Point(2, 238);
            this.grpBT.Name = "grpBT";
            this.grpBT.Size = new System.Drawing.Size(238, 43);
            this.grpBT.TabIndex = 10015;
            this.grpBT.TabStop = false;
            this.grpBT.Text = "Basal Body Temperature";
            // 
            // txtBBT
            // 
            this.txtBBT.Location = new System.Drawing.Point(11, 17);
            this.txtBBT.MaxLength = 5;
            this.txtBBT.Name = "txtBBT";
            this.txtBBT.Size = new System.Drawing.Size(48, 20);
            this.txtBBT.TabIndex = 0;
            this.txtBBT.TextChanged += new System.EventHandler(this.txtBBT_TextChanged);
            this.txtBBT.Leave += new System.EventHandler(this.txtBBT_Leave);
            // 
            // grpHealth
            // 
            this.grpHealth.BackColor = System.Drawing.Color.Transparent;
            this.grpHealth.Controls.Add(this.sliderHealth);
            this.grpHealth.Controls.Add(this.lblBadWellbeing);
            this.grpHealth.Controls.Add(this.lblGoodWellbeing);
            this.grpHealth.Location = new System.Drawing.Point(2, 186);
            this.grpHealth.Name = "grpHealth";
            this.grpHealth.Size = new System.Drawing.Size(238, 46);
            this.grpHealth.TabIndex = 10017;
            this.grpHealth.TabStop = false;
            this.grpHealth.Text = "Wellbeing";
            // 
            // lblBadWellbeing
            // 
            this.lblBadWellbeing.Location = new System.Drawing.Point(6, 18);
            this.lblBadWellbeing.Name = "lblBadWellbeing";
            this.lblBadWellbeing.Size = new System.Drawing.Size(48, 23);
            this.lblBadWellbeing.TabIndex = 10007;
            this.lblBadWellbeing.Text = "Bad";
            this.lblBadWellbeing.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblGoodWellbeing
            // 
            this.lblGoodWellbeing.Location = new System.Drawing.Point(180, 18);
            this.lblGoodWellbeing.Name = "lblGoodWellbeing";
            this.lblGoodWellbeing.Size = new System.Drawing.Size(52, 23);
            this.lblGoodWellbeing.TabIndex = 10007;
            this.lblGoodWellbeing.Text = "Good";
            // 
            // ttBBT
            // 
            this.ttBBT.AutomaticDelay = 1;
            this.ttBBT.AutoPopDelay = 5000;
            this.ttBBT.InitialDelay = 1;
            this.ttBBT.ReshowDelay = 1;
            this.ttBBT.ShowAlways = true;
            this.ttBBT.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            this.ttBBT.ToolTipTitle = "Basal Body Temperature";
            this.ttBBT.UseAnimation = false;
            this.ttBBT.UseFading = false;
            // 
            // ttCF
            // 
            this.ttCF.AutomaticDelay = 1;
            this.ttCF.AutoPopDelay = 5000;
            this.ttCF.InitialDelay = 1;
            this.ttCF.ReshowDelay = 1;
            this.ttCF.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttCF.UseAnimation = false;
            this.ttCF.UseFading = false;
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
            this.sliderHealth.Size = new System.Drawing.Size(120, 30);
            this.sliderHealth.SmallChange = ((uint)(1u));
            this.sliderHealth.TabIndex = 0;
            this.sliderHealth.Text = "colorSlider1";
            this.sliderHealth.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderHealth.Value = 5;
            // 
            // DayEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkHadSex);
            this.Controls.Add(this.lblBBT);
            this.Controls.Add(this.grpNote);
            this.Controls.Add(this.grpCF);
            this.Controls.Add(this.grpBT);
            this.Controls.Add(this.grpHealth);
            this.Name = "DayEditControl";
            this.Size = new System.Drawing.Size(242, 332);
            this.grpNote.ResumeLayout(false);
            this.grpNote.PerformLayout();
            this.grpCF.ResumeLayout(false);
            this.grpBT.ResumeLayout(false);
            this.grpBT.PerformLayout();
            this.grpHealth.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkHadSex;
        private System.Windows.Forms.Label lblBBT;
        private System.Windows.Forms.GroupBox grpNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.GroupBox grpCF;
        private System.Windows.Forms.RadioButton rbtCF3;
        private System.Windows.Forms.RadioButton rbtCF2;
        private System.Windows.Forms.RadioButton rbtCF1;
        private System.Windows.Forms.GroupBox grpBT;
        private System.Windows.Forms.TextBox txtBBT;
        private System.Windows.Forms.GroupBox grpHealth;
        private MB.Controls.ColorSlider sliderHealth;
        private System.Windows.Forms.Label lblBadWellbeing;
        private System.Windows.Forms.Label lblGoodWellbeing;
        private System.Windows.Forms.ToolTip ttBBT;
        private System.Windows.Forms.ToolTip ttCF;
    }
}
