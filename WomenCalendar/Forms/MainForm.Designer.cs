namespace WomenCalendar
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.prevStripButton = new System.Windows.Forms.ToolStripButton();
            this.nextStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.xPanderList1 = new XPanderControl.XPanderList();
            this.xLegend = new XPanderControl.XPander();
            this.xWoman = new XPanderControl.XPander();
            this.chbAskPassword = new System.Windows.Forms.CheckBox();
            this.chbDefaultWoman = new System.Windows.Forms.CheckBox();
            this.numMenstruationLength = new System.Windows.Forms.NumericUpDown();
            this.lblMyCycle2 = new System.Windows.Forms.Label();
            this.lblMenstruationLength2 = new System.Windows.Forms.Label();
            this.lblMenstruationLength1 = new System.Windows.Forms.Label();
            this.numMenstruationPeriod = new System.Windows.Forms.NumericUpDown();
            this.lblMyCycle = new System.Windows.Forms.Label();
            this.rbManual = new System.Windows.Forms.RadioButton();
            this.rbAuto = new System.Windows.Forms.RadioButton();
            this.lblAverageCycle = new System.Windows.Forms.Label();
            this.xDay = new XPanderControl.XPander();
            this.lblDayDescription = new System.Windows.Forms.Label();
            this.monthControl = new WomenCalendar.MonthsControl();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.xPanderList1.SuspendLayout();
            this.xWoman.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMenstruationLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMenstruationPeriod)).BeginInit();
            this.xDay.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.prevStripButton,
            this.nextStripButton,
            this.toolStripSeparator1,
            this.helpToolStripButton,
            this.toolStripSeparator2,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(788, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "Создать новую женщину";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "Открыть женщину";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Сохранить женщину";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // prevStripButton
            // 
            this.prevStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.prevStripButton.Image = ((System.Drawing.Image)(resources.GetObject("prevStripButton.Image")));
            this.prevStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevStripButton.Name = "prevStripButton";
            this.prevStripButton.Size = new System.Drawing.Size(23, 22);
            this.prevStripButton.Text = "Сдвинуть на месяц назад";
            this.prevStripButton.Click += new System.EventHandler(this.prevStripButton_Click);
            // 
            // nextStripButton
            // 
            this.nextStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextStripButton.Image = ((System.Drawing.Image)(resources.GetObject("nextStripButton.Image")));
            this.nextStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextStripButton.Name = "nextStripButton";
            this.nextStripButton.Size = new System.Drawing.Size(23, 22);
            this.nextStripButton.Text = "Сдвинуть на месяц вперёд";
            this.nextStripButton.Click += new System.EventHandler(this.nextStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "Помощь";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(61, 22);
            this.toolStripLabel1.Text = "Прыгнуть:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(312, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(139, 20);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.RoyalBlue;
            this.splitContainer1.CausesValidation = false;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.xPanderList1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.monthControl);
            this.splitContainer1.Size = new System.Drawing.Size(788, 507);
            this.splitContainer1.SplitterDistance = 232;
            this.splitContainer1.TabIndex = 5;
            // 
            // xPanderList1
            // 
            this.xPanderList1.AutoScroll = true;
            this.xPanderList1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(117)))), ((int)(((byte)(222)))));
            this.xPanderList1.Controls.Add(this.xLegend);
            this.xPanderList1.Controls.Add(this.xWoman);
            this.xPanderList1.Controls.Add(this.xDay);
            this.xPanderList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanderList1.Location = new System.Drawing.Point(0, 0);
            this.xPanderList1.Margin = new System.Windows.Forms.Padding(0);
            this.xPanderList1.Name = "xPanderList1";
            this.xPanderList1.Size = new System.Drawing.Size(232, 507);
            this.xPanderList1.TabIndex = 0;
            // 
            // xLegend
            // 
            this.xLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xLegend.Animated = true;
            this.xLegend.AnimationTime = 10;
            this.xLegend.BackColor = System.Drawing.Color.Transparent;
            this.xLegend.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.xLegend.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xLegend.CaptionFormatFlag = XPanderControl.XPander.FormatFlag.NoWrap;
            this.xLegend.CaptionStyle = XPanderControl.XPander.CaptionStyleEnum.Normal;
            this.xLegend.CaptionText = "Описание обозначений";
            this.xLegend.CaptionTextAlign = XPanderControl.XPander.CaptionTextAlignment.Left;
            this.xLegend.ChevronStyle = XPanderControl.XPander.ChevronStyleEnum.Image;
            this.xLegend.CollapsedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("xLegend.CollapsedHighlightImage")));
            this.xLegend.CollapsedImage = ((System.Drawing.Bitmap)(resources.GetObject("xLegend.CollapsedImage")));
            this.xLegend.ExpandedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("xLegend.ExpandedHighlightImage")));
            this.xLegend.ExpandedImage = ((System.Drawing.Bitmap)(resources.GetObject("xLegend.ExpandedImage")));
            this.xLegend.Location = new System.Drawing.Point(8, 10);
            this.xLegend.Name = "xLegend";
            this.xLegend.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.xLegend.Size = new System.Drawing.Size(216, 136);
            this.xLegend.TabIndex = 2;
            this.xLegend.Tag = 18;
            this.xLegend.TooltipText = null;
            // 
            // xWoman
            // 
            this.xWoman.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xWoman.Animated = true;
            this.xWoman.AnimationTime = 10;
            this.xWoman.BackColor = System.Drawing.Color.Transparent;
            this.xWoman.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.xWoman.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xWoman.CaptionFormatFlag = XPanderControl.XPander.FormatFlag.NoWrap;
            this.xWoman.CaptionStyle = XPanderControl.XPander.CaptionStyleEnum.Normal;
            this.xWoman.CaptionText = "О женщине";
            this.xWoman.CaptionTextAlign = XPanderControl.XPander.CaptionTextAlignment.Left;
            this.xWoman.ChevronStyle = XPanderControl.XPander.ChevronStyleEnum.Image;
            this.xWoman.CollapsedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("xWoman.CollapsedHighlightImage")));
            this.xWoman.CollapsedImage = ((System.Drawing.Bitmap)(resources.GetObject("xWoman.CollapsedImage")));
            this.xWoman.Controls.Add(this.chbAskPassword);
            this.xWoman.Controls.Add(this.chbDefaultWoman);
            this.xWoman.Controls.Add(this.numMenstruationLength);
            this.xWoman.Controls.Add(this.lblMyCycle2);
            this.xWoman.Controls.Add(this.lblMenstruationLength2);
            this.xWoman.Controls.Add(this.lblMenstruationLength1);
            this.xWoman.Controls.Add(this.numMenstruationPeriod);
            this.xWoman.Controls.Add(this.lblMyCycle);
            this.xWoman.Controls.Add(this.rbManual);
            this.xWoman.Controls.Add(this.rbAuto);
            this.xWoman.Controls.Add(this.lblAverageCycle);
            this.xWoman.ExpandedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("xWoman.ExpandedHighlightImage")));
            this.xWoman.ExpandedImage = ((System.Drawing.Bitmap)(resources.GetObject("xWoman.ExpandedImage")));
            this.xWoman.Location = new System.Drawing.Point(8, 160);
            this.xWoman.Name = "xWoman";
            this.xWoman.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.xWoman.Size = new System.Drawing.Size(216, 164);
            this.xWoman.TabIndex = 0;
            this.xWoman.Tag = 19;
            this.xWoman.TooltipText = null;
            // 
            // chbAskPassword
            // 
            this.chbAskPassword.AutoSize = true;
            this.chbAskPassword.Location = new System.Drawing.Point(12, 95);
            this.chbAskPassword.Name = "chbAskPassword";
            this.chbAskPassword.Size = new System.Drawing.Size(188, 17);
            this.chbAskPassword.TabIndex = 8;
            this.chbAskPassword.Text = "Всегда спрашивать мой пароль";
            this.chbAskPassword.UseVisualStyleBackColor = true;
            this.chbAskPassword.CheckedChanged += new System.EventHandler(this.chbAskPassword_CheckedChanged);
            // 
            // chbDefaultWoman
            // 
            this.chbDefaultWoman.Location = new System.Drawing.Point(12, 109);
            this.chbDefaultWoman.Name = "chbDefaultWoman";
            this.chbDefaultWoman.Size = new System.Drawing.Size(185, 43);
            this.chbDefaultWoman.TabIndex = 7;
            this.chbDefaultWoman.Text = "При запуске программы открывать меня по умолчанию";
            this.chbDefaultWoman.UseVisualStyleBackColor = true;
            this.chbDefaultWoman.CheckedChanged += new System.EventHandler(this.chbDefaultWoman_CheckedChanged);
            // 
            // numMenstruationLength
            // 
            this.numMenstruationLength.Location = new System.Drawing.Point(134, 69);
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
            this.numMenstruationLength.Size = new System.Drawing.Size(27, 20);
            this.numMenstruationLength.TabIndex = 4;
            this.numMenstruationLength.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numMenstruationLength.ValueChanged += new System.EventHandler(this.numMenstruationLength_ValueChanged);
            // 
            // lblMyCycle2
            // 
            this.lblMyCycle2.AutoSize = true;
            this.lblMyCycle2.Location = new System.Drawing.Point(129, 50);
            this.lblMyCycle2.Name = "lblMyCycle2";
            this.lblMyCycle2.Size = new System.Drawing.Size(25, 13);
            this.lblMyCycle2.TabIndex = 6;
            this.lblMyCycle2.Text = "дня";
            // 
            // lblMenstruationLength2
            // 
            this.lblMenstruationLength2.AutoSize = true;
            this.lblMenstruationLength2.Location = new System.Drawing.Point(163, 71);
            this.lblMenstruationLength2.Name = "lblMenstruationLength2";
            this.lblMenstruationLength2.Size = new System.Drawing.Size(25, 13);
            this.lblMenstruationLength2.TabIndex = 6;
            this.lblMenstruationLength2.Text = "дня";
            // 
            // lblMenstruationLength1
            // 
            this.lblMenstruationLength1.AutoSize = true;
            this.lblMenstruationLength1.Location = new System.Drawing.Point(9, 71);
            this.lblMenstruationLength1.Name = "lblMenstruationLength1";
            this.lblMenstruationLength1.Size = new System.Drawing.Size(123, 13);
            this.lblMenstruationLength1.TabIndex = 5;
            this.lblMenstruationLength1.Text = "Обычно овуляшки идут";
            // 
            // numMenstruationPeriod
            // 
            this.numMenstruationPeriod.Location = new System.Drawing.Point(89, 48);
            this.numMenstruationPeriod.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numMenstruationPeriod.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numMenstruationPeriod.Name = "numMenstruationPeriod";
            this.numMenstruationPeriod.Size = new System.Drawing.Size(37, 20);
            this.numMenstruationPeriod.TabIndex = 4;
            this.numMenstruationPeriod.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numMenstruationPeriod.ValueChanged += new System.EventHandler(this.numMenstruationPeriod_ValueChanged);
            // 
            // lblMyCycle
            // 
            this.lblMyCycle.AutoSize = true;
            this.lblMyCycle.Location = new System.Drawing.Point(31, 50);
            this.lblMyCycle.Name = "lblMyCycle";
            this.lblMyCycle.Size = new System.Drawing.Size(55, 13);
            this.lblMyCycle.TabIndex = 3;
            this.lblMyCycle.Text = "Мой цикл";
            // 
            // rbManual
            // 
            this.rbManual.AutoSize = true;
            this.rbManual.Location = new System.Drawing.Point(12, 50);
            this.rbManual.Name = "rbManual";
            this.rbManual.Size = new System.Drawing.Size(14, 13);
            this.rbManual.TabIndex = 2;
            this.rbManual.TabStop = true;
            this.rbManual.UseVisualStyleBackColor = true;
            // 
            // rbAuto
            // 
            this.rbAuto.AutoSize = true;
            this.rbAuto.Location = new System.Drawing.Point(12, 28);
            this.rbAuto.Name = "rbAuto";
            this.rbAuto.Size = new System.Drawing.Size(14, 13);
            this.rbAuto.TabIndex = 1;
            this.rbAuto.TabStop = true;
            this.rbAuto.UseVisualStyleBackColor = true;
            this.rbAuto.CheckedChanged += new System.EventHandler(this.rbAuto_CheckedChanged);
            // 
            // lblAverageCycle
            // 
            this.lblAverageCycle.AutoEllipsis = true;
            this.lblAverageCycle.AutoSize = true;
            this.lblAverageCycle.Location = new System.Drawing.Point(31, 29);
            this.lblAverageCycle.Name = "lblAverageCycle";
            this.lblAverageCycle.Size = new System.Drawing.Size(80, 13);
            this.lblAverageCycle.TabIndex = 0;
            this.lblAverageCycle.Text = "Средний цикл:";
            // 
            // xDay
            // 
            this.xDay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xDay.Animated = true;
            this.xDay.AnimationTime = 10;
            this.xDay.BackColor = System.Drawing.Color.Transparent;
            this.xDay.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.xDay.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.xDay.CaptionFormatFlag = XPanderControl.XPander.FormatFlag.NoWrap;
            this.xDay.CaptionStyle = XPanderControl.XPander.CaptionStyleEnum.Normal;
            this.xDay.CaptionText = "Описание дня";
            this.xDay.CaptionTextAlign = XPanderControl.XPander.CaptionTextAlignment.Left;
            this.xDay.ChevronStyle = XPanderControl.XPander.ChevronStyleEnum.Image;
            this.xDay.CollapsedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("xDay.CollapsedHighlightImage")));
            this.xDay.CollapsedImage = ((System.Drawing.Bitmap)(resources.GetObject("xDay.CollapsedImage")));
            this.xDay.Controls.Add(this.lblDayDescription);
            this.xDay.ExpandedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("xDay.ExpandedHighlightImage")));
            this.xDay.ExpandedImage = ((System.Drawing.Bitmap)(resources.GetObject("xDay.ExpandedImage")));
            this.xDay.Location = new System.Drawing.Point(8, 338);
            this.xDay.Name = "xDay";
            this.xDay.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.xDay.Size = new System.Drawing.Size(216, 76);
            this.xDay.TabIndex = 1;
            this.xDay.Tag = 20;
            this.xDay.TooltipText = null;
            // 
            // lblDayDescription
            // 
            this.lblDayDescription.AutoEllipsis = true;
            this.lblDayDescription.AutoSize = true;
            this.lblDayDescription.Location = new System.Drawing.Point(5, 29);
            this.lblDayDescription.Name = "lblDayDescription";
            this.lblDayDescription.Size = new System.Drawing.Size(42, 13);
            this.lblDayDescription.TabIndex = 0;
            this.lblDayDescription.Text = "...day...";
            // 
            // monthControl
            // 
            this.monthControl.BackColor = System.Drawing.Color.White;
            this.monthControl.CausesValidation = false;
            this.monthControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthControl.FocusDate = new System.DateTime(((long)(0)));
            this.monthControl.FocusDay = null;
            this.monthControl.FocusMonth = null;
            this.monthControl.Location = new System.Drawing.Point(0, 0);
            this.monthControl.MonthsMarginX = 10;
            this.monthControl.MonthsMarginY = 10;
            this.monthControl.Name = "monthControl";
            this.monthControl.Size = new System.Drawing.Size(552, 507);
            this.monthControl.StartMonth = new System.DateTime(2008, 2, 1, 0, 0, 0, 0);
            this.monthControl.TabIndex = 0;
            this.monthControl.FocusDateChanged += new WomenCalendar.MonthsControl.FocusDateChangedDelegate(this.monthControl_FocusDateChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 532);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Овуляшки";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseLeave += new System.EventHandler(this.MainForm_MouseLeave);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.xPanderList1.ResumeLayout(false);
            this.xWoman.ResumeLayout(false);
            this.xWoman.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMenstruationLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMenstruationPeriod)).EndInit();
            this.xDay.ResumeLayout(false);
            this.xDay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton prevStripButton;
        private System.Windows.Forms.ToolStripButton nextStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MonthsControl monthControl;
        private XPanderControl.XPanderList xPanderList1;
        private XPanderControl.XPander xLegend;
        private XPanderControl.XPander xWoman;
        private XPanderControl.XPander xDay;
        private System.Windows.Forms.Label lblDayDescription;
        private System.Windows.Forms.RadioButton rbManual;
        private System.Windows.Forms.RadioButton rbAuto;
        private System.Windows.Forms.NumericUpDown numMenstruationPeriod;
        private System.Windows.Forms.NumericUpDown numMenstruationLength;
        private System.Windows.Forms.Label lblMenstruationLength2;
        private System.Windows.Forms.Label lblMenstruationLength1;
        private System.Windows.Forms.CheckBox chbDefaultWoman;
        private System.Windows.Forms.CheckBox chbAskPassword;
        private System.Windows.Forms.Label lblMyCycle2;
        private System.Windows.Forms.Label lblMyCycle;
        private System.Windows.Forms.Label lblAverageCycle;


    }
}

