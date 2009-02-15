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
            this.exportToExcel = new System.Windows.Forms.ToolStripButton();
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
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dayCellControl4 = new WomenCalendar.DayCellControl();
            this.dayCellControl6 = new WomenCalendar.DayCellControl();
            this.dayCellControl8 = new WomenCalendar.DayCellControl();
            this.dayCellControl7 = new WomenCalendar.DayCellControl();
            this.dayCellControl12 = new WomenCalendar.DayCellControl();
            this.dayCellControl10 = new WomenCalendar.DayCellControl();
            this.dayCellControl9 = new WomenCalendar.DayCellControl();
            this.dayCellControl5 = new WomenCalendar.DayCellControl();
            this.dayCellControl3 = new WomenCalendar.DayCellControl();
            this.dayCellControl2 = new WomenCalendar.DayCellControl();
            this.dayCellControl11 = new WomenCalendar.DayCellControl();
            this.dayCellControl1 = new WomenCalendar.DayCellControl();
            this.dayLegendMenstruations = new WomenCalendar.DayCellControl();
            this.xWoman = new XPanderControl.XPander();
            this.btnChangeWoman = new System.Windows.Forms.Button();
            this.chbAskPassword = new System.Windows.Forms.CheckBox();
            this.chbDefaultWoman = new System.Windows.Forms.CheckBox();
            this.lblMyCycle2 = new System.Windows.Forms.Label();
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
            this.xLegend.SuspendLayout();
            this.xWoman.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMenstruationPeriod)).BeginInit();
            this.xDay.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.exportToExcel,
            this.toolStripSeparator,
            this.prevStripButton,
            this.nextStripButton,
            this.toolStripSeparator1,
            this.helpToolStripButton,
            this.toolStripSeparator2,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(747, 39);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = global::WomenCalendar.Properties.Resources.newGreen;
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.newToolStripButton.Text = "Создать новую женщину";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::WomenCalendar.Properties.Resources.openGreen;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.openToolStripButton.Text = "Открыть женщину";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::WomenCalendar.Properties.Resources.saveGreen;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.saveToolStripButton.Text = "Сохранить женщину";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // exportToExcel
            // 
            this.exportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportToExcel.Image = global::WomenCalendar.Properties.Resources.exportGreen;
            this.exportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportToExcel.Name = "exportToExcel";
            this.exportToExcel.Size = new System.Drawing.Size(36, 36);
            this.exportToExcel.Text = "Экспортировать женщину в Excel";
            this.exportToExcel.Click += new System.EventHandler(this.exportToExcel_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 39);
            // 
            // prevStripButton
            // 
            this.prevStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.prevStripButton.Image = global::WomenCalendar.Properties.Resources.Green_04;
            this.prevStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevStripButton.Name = "prevStripButton";
            this.prevStripButton.Size = new System.Drawing.Size(36, 36);
            this.prevStripButton.Text = "Сдвинуть на месяц назад";
            this.prevStripButton.Click += new System.EventHandler(this.prevStripButton_Click);
            // 
            // nextStripButton
            // 
            this.nextStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextStripButton.Image = global::WomenCalendar.Properties.Resources.Green_01;
            this.nextStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextStripButton.Name = "nextStripButton";
            this.nextStripButton.Size = new System.Drawing.Size(36, 36);
            this.nextStripButton.Text = "Сдвинуть на месяц вперёд";
            this.nextStripButton.Click += new System.EventHandler(this.nextStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = global::WomenCalendar.Properties.Resources.ovulashki_logo2;
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.helpToolStripButton.Text = "Помощь";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(61, 36);
            this.toolStripLabel1.Text = "Прыгнуть:";
            this.toolStripLabel1.ToolTipText = "Выбранная дата";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(312, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(203, 20);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.RoyalBlue;
            this.splitContainer1.CausesValidation = false;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.xPanderList1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.monthControl);
            this.splitContainer1.Size = new System.Drawing.Size(747, 803);
            this.splitContainer1.SplitterDistance = 250;
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
            this.xPanderList1.Size = new System.Drawing.Size(250, 803);
            this.xPanderList1.TabIndex = 0;
            // 
            // xLegend
            // 
            this.xLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xLegend.Animated = true;
            this.xLegend.AnimationTime = 1;
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
            this.xLegend.Controls.Add(this.label13);
            this.xLegend.Controls.Add(this.label12);
            this.xLegend.Controls.Add(this.label11);
            this.xLegend.Controls.Add(this.label10);
            this.xLegend.Controls.Add(this.label9);
            this.xLegend.Controls.Add(this.label8);
            this.xLegend.Controls.Add(this.label7);
            this.xLegend.Controls.Add(this.label6);
            this.xLegend.Controls.Add(this.label5);
            this.xLegend.Controls.Add(this.label4);
            this.xLegend.Controls.Add(this.label3);
            this.xLegend.Controls.Add(this.label2);
            this.xLegend.Controls.Add(this.label1);
            this.xLegend.Controls.Add(this.dayCellControl4);
            this.xLegend.Controls.Add(this.dayCellControl6);
            this.xLegend.Controls.Add(this.dayCellControl8);
            this.xLegend.Controls.Add(this.dayCellControl7);
            this.xLegend.Controls.Add(this.dayCellControl12);
            this.xLegend.Controls.Add(this.dayCellControl10);
            this.xLegend.Controls.Add(this.dayCellControl9);
            this.xLegend.Controls.Add(this.dayCellControl5);
            this.xLegend.Controls.Add(this.dayCellControl3);
            this.xLegend.Controls.Add(this.dayCellControl2);
            this.xLegend.Controls.Add(this.dayCellControl11);
            this.xLegend.Controls.Add(this.dayCellControl1);
            this.xLegend.Controls.Add(this.dayLegendMenstruations);
            this.xLegend.ExpandedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("xLegend.ExpandedHighlightImage")));
            this.xLegend.ExpandedImage = ((System.Drawing.Bitmap)(resources.GetObject("xLegend.ExpandedImage")));
            this.xLegend.Location = new System.Drawing.Point(0, 0);
            this.xLegend.Margin = new System.Windows.Forms.Padding(0);
            this.xLegend.Name = "xLegend";
            this.xLegend.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.xLegend.Size = new System.Drawing.Size(250, 520);
            this.xLegend.TabIndex = 2;
            this.xLegend.Tag = 0;
            this.xLegend.TooltipText = null;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.Location = new System.Drawing.Point(45, 485);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(202, 32);
            this.label13.TabIndex = 2;
            this.label13.Text = "Номер недели беременности";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.Location = new System.Drawing.Point(45, 447);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(202, 32);
            this.label12.TabIndex = 2;
            this.label12.Text = "День зачатия";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.Location = new System.Drawing.Point(46, 409);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(201, 32);
            this.label11.TabIndex = 2;
            this.label11.Text = "Беременность";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Location = new System.Drawing.Point(45, 371);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(202, 32);
            this.label10.TabIndex = 2;
            this.label10.Text = "День овуляции";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(46, 332);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(201, 32);
            this.label9.TabIndex = 2;
            this.label9.Text = "День будущих менструаций";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(46, 295);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(201, 32);
            this.label8.TabIndex = 2;
            this.label8.Text = "Дни с наименьшей вероятностью зачатия";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(46, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(201, 32);
            this.label7.TabIndex = 2;
            this.label7.Text = "Менструации";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(46, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(201, 32);
            this.label6.TabIndex = 2;
            this.label6.Text = "Вероятно зачатие девочки";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(46, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 32);
            this.label5.TabIndex = 2;
            this.label5.Text = "Вероятно зачатие мальчика";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(46, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 32);
            this.label4.TabIndex = 2;
            this.label4.Text = "День секса";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(46, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "Заметка";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(46, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Выбраный день (в фокусе)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(46, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Сегодняшний день";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dayCellControl4
            // 
            this.dayCellControl4.BackColor = System.Drawing.Color.LightGreen;
            this.dayCellControl4.Date = new System.DateTime(((long)(0)));
            this.dayCellControl4.Egesta = 5;
            this.dayCellControl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl4.IsConceptionDay = false;
            this.dayCellControl4.IsFocusDay = false;
            this.dayCellControl4.IsHadSex = false;
            this.dayCellControl4.IsHaveNote = false;
            this.dayCellControl4.IsMenstruationDay = false;
            this.dayCellControl4.IsPredictedAsBoyDay = false;
            this.dayCellControl4.IsPredictedAsGirlDay = false;
            this.dayCellControl4.IsPredictedAsMenstruationDay = true;
            this.dayCellControl4.IsPredictedAsOvulationDay = false;
            this.dayCellControl4.IsPredictedAsSafeSexDay = true;
            this.dayCellControl4.IsPregnancyDay = false;
            this.dayCellControl4.IsTodayDay = false;
            this.dayCellControl4.Location = new System.Drawing.Point(8, 332);
            this.dayCellControl4.ManualDrawOptions = true;
            this.dayCellControl4.Name = "dayCellControl4";
            this.dayCellControl4.OwnerOneMonthControl = null;
            this.dayCellControl4.PregnancyWeek = 0;
            this.dayCellControl4.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl4.TabIndex = 1;
            // 
            // dayCellControl6
            // 
            this.dayCellControl6.BackColor = System.Drawing.Color.White;
            this.dayCellControl6.Date = new System.DateTime(((long)(0)));
            this.dayCellControl6.Egesta = 5;
            this.dayCellControl6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl6.IsConceptionDay = false;
            this.dayCellControl6.IsFocusDay = false;
            this.dayCellControl6.IsHadSex = true;
            this.dayCellControl6.IsHaveNote = false;
            this.dayCellControl6.IsMenstruationDay = false;
            this.dayCellControl6.IsPredictedAsBoyDay = false;
            this.dayCellControl6.IsPredictedAsGirlDay = false;
            this.dayCellControl6.IsPredictedAsMenstruationDay = false;
            this.dayCellControl6.IsPredictedAsOvulationDay = false;
            this.dayCellControl6.IsPredictedAsSafeSexDay = false;
            this.dayCellControl6.IsPregnancyDay = false;
            this.dayCellControl6.IsTodayDay = false;
            this.dayCellControl6.Location = new System.Drawing.Point(8, 143);
            this.dayCellControl6.ManualDrawOptions = true;
            this.dayCellControl6.Name = "dayCellControl6";
            this.dayCellControl6.OwnerOneMonthControl = null;
            this.dayCellControl6.PregnancyWeek = 0;
            this.dayCellControl6.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl6.TabIndex = 1;
            // 
            // dayCellControl8
            // 
            this.dayCellControl8.BackColor = System.Drawing.Color.White;
            this.dayCellControl8.Date = new System.DateTime(((long)(0)));
            this.dayCellControl8.Egesta = 5;
            this.dayCellControl8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl8.IsConceptionDay = false;
            this.dayCellControl8.IsFocusDay = false;
            this.dayCellControl8.IsHadSex = false;
            this.dayCellControl8.IsHaveNote = false;
            this.dayCellControl8.IsMenstruationDay = false;
            this.dayCellControl8.IsPredictedAsBoyDay = false;
            this.dayCellControl8.IsPredictedAsGirlDay = true;
            this.dayCellControl8.IsPredictedAsMenstruationDay = false;
            this.dayCellControl8.IsPredictedAsOvulationDay = false;
            this.dayCellControl8.IsPredictedAsSafeSexDay = false;
            this.dayCellControl8.IsPregnancyDay = false;
            this.dayCellControl8.IsTodayDay = false;
            this.dayCellControl8.Location = new System.Drawing.Point(8, 219);
            this.dayCellControl8.ManualDrawOptions = true;
            this.dayCellControl8.Name = "dayCellControl8";
            this.dayCellControl8.OwnerOneMonthControl = null;
            this.dayCellControl8.PregnancyWeek = 0;
            this.dayCellControl8.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl8.TabIndex = 1;
            // 
            // dayCellControl7
            // 
            this.dayCellControl7.BackColor = System.Drawing.Color.White;
            this.dayCellControl7.Date = new System.DateTime(((long)(0)));
            this.dayCellControl7.Egesta = 5;
            this.dayCellControl7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl7.IsConceptionDay = false;
            this.dayCellControl7.IsFocusDay = false;
            this.dayCellControl7.IsHadSex = false;
            this.dayCellControl7.IsHaveNote = false;
            this.dayCellControl7.IsMenstruationDay = false;
            this.dayCellControl7.IsPredictedAsBoyDay = true;
            this.dayCellControl7.IsPredictedAsGirlDay = false;
            this.dayCellControl7.IsPredictedAsMenstruationDay = false;
            this.dayCellControl7.IsPredictedAsOvulationDay = false;
            this.dayCellControl7.IsPredictedAsSafeSexDay = false;
            this.dayCellControl7.IsPregnancyDay = false;
            this.dayCellControl7.IsTodayDay = false;
            this.dayCellControl7.Location = new System.Drawing.Point(8, 181);
            this.dayCellControl7.ManualDrawOptions = true;
            this.dayCellControl7.Name = "dayCellControl7";
            this.dayCellControl7.OwnerOneMonthControl = null;
            this.dayCellControl7.PregnancyWeek = 0;
            this.dayCellControl7.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl7.TabIndex = 1;
            // 
            // dayCellControl12
            // 
            this.dayCellControl12.BackColor = System.Drawing.Color.LightBlue;
            this.dayCellControl12.Date = new System.DateTime(((long)(0)));
            this.dayCellControl12.Egesta = 5;
            this.dayCellControl12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl12.IsConceptionDay = false;
            this.dayCellControl12.IsFocusDay = false;
            this.dayCellControl12.IsHadSex = false;
            this.dayCellControl12.IsHaveNote = false;
            this.dayCellControl12.IsMenstruationDay = false;
            this.dayCellControl12.IsPredictedAsBoyDay = false;
            this.dayCellControl12.IsPredictedAsGirlDay = false;
            this.dayCellControl12.IsPredictedAsMenstruationDay = false;
            this.dayCellControl12.IsPredictedAsOvulationDay = false;
            this.dayCellControl12.IsPredictedAsSafeSexDay = false;
            this.dayCellControl12.IsPregnancyDay = true;
            this.dayCellControl12.IsTodayDay = false;
            this.dayCellControl12.Location = new System.Drawing.Point(8, 485);
            this.dayCellControl12.ManualDrawOptions = true;
            this.dayCellControl12.Name = "dayCellControl12";
            this.dayCellControl12.OwnerOneMonthControl = null;
            this.dayCellControl12.PregnancyWeek = 35;
            this.dayCellControl12.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl12.TabIndex = 1;
            // 
            // dayCellControl10
            // 
            this.dayCellControl10.BackColor = System.Drawing.Color.LightBlue;
            this.dayCellControl10.Date = new System.DateTime(((long)(0)));
            this.dayCellControl10.Egesta = 5;
            this.dayCellControl10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl10.IsConceptionDay = true;
            this.dayCellControl10.IsFocusDay = false;
            this.dayCellControl10.IsHadSex = false;
            this.dayCellControl10.IsHaveNote = false;
            this.dayCellControl10.IsMenstruationDay = false;
            this.dayCellControl10.IsPredictedAsBoyDay = false;
            this.dayCellControl10.IsPredictedAsGirlDay = false;
            this.dayCellControl10.IsPredictedAsMenstruationDay = false;
            this.dayCellControl10.IsPredictedAsOvulationDay = false;
            this.dayCellControl10.IsPredictedAsSafeSexDay = false;
            this.dayCellControl10.IsPregnancyDay = true;
            this.dayCellControl10.IsTodayDay = false;
            this.dayCellControl10.Location = new System.Drawing.Point(8, 447);
            this.dayCellControl10.ManualDrawOptions = true;
            this.dayCellControl10.Name = "dayCellControl10";
            this.dayCellControl10.OwnerOneMonthControl = null;
            this.dayCellControl10.PregnancyWeek = 0;
            this.dayCellControl10.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl10.TabIndex = 1;
            // 
            // dayCellControl9
            // 
            this.dayCellControl9.BackColor = System.Drawing.Color.LightBlue;
            this.dayCellControl9.Date = new System.DateTime(((long)(0)));
            this.dayCellControl9.Egesta = 5;
            this.dayCellControl9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl9.IsConceptionDay = false;
            this.dayCellControl9.IsFocusDay = false;
            this.dayCellControl9.IsHadSex = false;
            this.dayCellControl9.IsHaveNote = false;
            this.dayCellControl9.IsMenstruationDay = false;
            this.dayCellControl9.IsPredictedAsBoyDay = false;
            this.dayCellControl9.IsPredictedAsGirlDay = false;
            this.dayCellControl9.IsPredictedAsMenstruationDay = false;
            this.dayCellControl9.IsPredictedAsOvulationDay = false;
            this.dayCellControl9.IsPredictedAsSafeSexDay = false;
            this.dayCellControl9.IsPregnancyDay = true;
            this.dayCellControl9.IsTodayDay = false;
            this.dayCellControl9.Location = new System.Drawing.Point(8, 409);
            this.dayCellControl9.ManualDrawOptions = true;
            this.dayCellControl9.Name = "dayCellControl9";
            this.dayCellControl9.OwnerOneMonthControl = null;
            this.dayCellControl9.PregnancyWeek = 0;
            this.dayCellControl9.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl9.TabIndex = 1;
            // 
            // dayCellControl5
            // 
            this.dayCellControl5.BackColor = System.Drawing.Color.LightGreen;
            this.dayCellControl5.Date = new System.DateTime(((long)(0)));
            this.dayCellControl5.Egesta = 5;
            this.dayCellControl5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl5.IsConceptionDay = false;
            this.dayCellControl5.IsFocusDay = false;
            this.dayCellControl5.IsHadSex = false;
            this.dayCellControl5.IsHaveNote = false;
            this.dayCellControl5.IsMenstruationDay = false;
            this.dayCellControl5.IsPredictedAsBoyDay = false;
            this.dayCellControl5.IsPredictedAsGirlDay = false;
            this.dayCellControl5.IsPredictedAsMenstruationDay = false;
            this.dayCellControl5.IsPredictedAsOvulationDay = false;
            this.dayCellControl5.IsPredictedAsSafeSexDay = true;
            this.dayCellControl5.IsPregnancyDay = false;
            this.dayCellControl5.IsTodayDay = false;
            this.dayCellControl5.Location = new System.Drawing.Point(8, 295);
            this.dayCellControl5.ManualDrawOptions = true;
            this.dayCellControl5.Name = "dayCellControl5";
            this.dayCellControl5.OwnerOneMonthControl = null;
            this.dayCellControl5.PregnancyWeek = 0;
            this.dayCellControl5.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl5.TabIndex = 1;
            // 
            // dayCellControl3
            // 
            this.dayCellControl3.BackColor = System.Drawing.Color.Yellow;
            this.dayCellControl3.Date = new System.DateTime(((long)(0)));
            this.dayCellControl3.Egesta = 5;
            this.dayCellControl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl3.IsConceptionDay = false;
            this.dayCellControl3.IsFocusDay = false;
            this.dayCellControl3.IsHadSex = false;
            this.dayCellControl3.IsHaveNote = false;
            this.dayCellControl3.IsMenstruationDay = false;
            this.dayCellControl3.IsPredictedAsBoyDay = false;
            this.dayCellControl3.IsPredictedAsGirlDay = false;
            this.dayCellControl3.IsPredictedAsMenstruationDay = false;
            this.dayCellControl3.IsPredictedAsOvulationDay = true;
            this.dayCellControl3.IsPredictedAsSafeSexDay = false;
            this.dayCellControl3.IsPregnancyDay = false;
            this.dayCellControl3.IsTodayDay = false;
            this.dayCellControl3.Location = new System.Drawing.Point(8, 371);
            this.dayCellControl3.ManualDrawOptions = true;
            this.dayCellControl3.Name = "dayCellControl3";
            this.dayCellControl3.OwnerOneMonthControl = null;
            this.dayCellControl3.PregnancyWeek = 0;
            this.dayCellControl3.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl3.TabIndex = 1;
            // 
            // dayCellControl2
            // 
            this.dayCellControl2.BackColor = System.Drawing.Color.LightPink;
            this.dayCellControl2.Date = new System.DateTime(((long)(0)));
            this.dayCellControl2.Egesta = 4;
            this.dayCellControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl2.IsConceptionDay = false;
            this.dayCellControl2.IsFocusDay = false;
            this.dayCellControl2.IsHadSex = false;
            this.dayCellControl2.IsHaveNote = false;
            this.dayCellControl2.IsMenstruationDay = true;
            this.dayCellControl2.IsPredictedAsBoyDay = false;
            this.dayCellControl2.IsPredictedAsGirlDay = false;
            this.dayCellControl2.IsPredictedAsMenstruationDay = false;
            this.dayCellControl2.IsPredictedAsOvulationDay = false;
            this.dayCellControl2.IsPredictedAsSafeSexDay = false;
            this.dayCellControl2.IsPregnancyDay = false;
            this.dayCellControl2.IsTodayDay = false;
            this.dayCellControl2.Location = new System.Drawing.Point(8, 257);
            this.dayCellControl2.ManualDrawOptions = true;
            this.dayCellControl2.Name = "dayCellControl2";
            this.dayCellControl2.OwnerOneMonthControl = null;
            this.dayCellControl2.PregnancyWeek = 0;
            this.dayCellControl2.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl2.TabIndex = 1;
            // 
            // dayCellControl11
            // 
            this.dayCellControl11.BackColor = System.Drawing.Color.White;
            this.dayCellControl11.Date = new System.DateTime(((long)(0)));
            this.dayCellControl11.Egesta = 5;
            this.dayCellControl11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl11.IsConceptionDay = false;
            this.dayCellControl11.IsFocusDay = true;
            this.dayCellControl11.IsHadSex = false;
            this.dayCellControl11.IsHaveNote = false;
            this.dayCellControl11.IsMenstruationDay = false;
            this.dayCellControl11.IsPredictedAsBoyDay = false;
            this.dayCellControl11.IsPredictedAsGirlDay = false;
            this.dayCellControl11.IsPredictedAsMenstruationDay = false;
            this.dayCellControl11.IsPredictedAsOvulationDay = false;
            this.dayCellControl11.IsPredictedAsSafeSexDay = false;
            this.dayCellControl11.IsPregnancyDay = false;
            this.dayCellControl11.IsTodayDay = false;
            this.dayCellControl11.Location = new System.Drawing.Point(8, 67);
            this.dayCellControl11.ManualDrawOptions = true;
            this.dayCellControl11.Name = "dayCellControl11";
            this.dayCellControl11.OwnerOneMonthControl = null;
            this.dayCellControl11.PregnancyWeek = 0;
            this.dayCellControl11.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl11.TabIndex = 1;
            // 
            // dayCellControl1
            // 
            this.dayCellControl1.BackColor = System.Drawing.Color.White;
            this.dayCellControl1.Date = new System.DateTime(((long)(0)));
            this.dayCellControl1.Egesta = 5;
            this.dayCellControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayCellControl1.IsConceptionDay = false;
            this.dayCellControl1.IsFocusDay = false;
            this.dayCellControl1.IsHadSex = false;
            this.dayCellControl1.IsHaveNote = true;
            this.dayCellControl1.IsMenstruationDay = false;
            this.dayCellControl1.IsPredictedAsBoyDay = false;
            this.dayCellControl1.IsPredictedAsGirlDay = false;
            this.dayCellControl1.IsPredictedAsMenstruationDay = false;
            this.dayCellControl1.IsPredictedAsOvulationDay = false;
            this.dayCellControl1.IsPredictedAsSafeSexDay = false;
            this.dayCellControl1.IsPregnancyDay = false;
            this.dayCellControl1.IsTodayDay = false;
            this.dayCellControl1.Location = new System.Drawing.Point(8, 105);
            this.dayCellControl1.ManualDrawOptions = true;
            this.dayCellControl1.Name = "dayCellControl1";
            this.dayCellControl1.OwnerOneMonthControl = null;
            this.dayCellControl1.PregnancyWeek = 0;
            this.dayCellControl1.Size = new System.Drawing.Size(32, 32);
            this.dayCellControl1.TabIndex = 1;
            // 
            // dayLegendMenstruations
            // 
            this.dayLegendMenstruations.BackColor = System.Drawing.Color.White;
            this.dayLegendMenstruations.Date = new System.DateTime(((long)(0)));
            this.dayLegendMenstruations.Egesta = 5;
            this.dayLegendMenstruations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dayLegendMenstruations.IsConceptionDay = false;
            this.dayLegendMenstruations.IsFocusDay = false;
            this.dayLegendMenstruations.IsHadSex = false;
            this.dayLegendMenstruations.IsHaveNote = false;
            this.dayLegendMenstruations.IsMenstruationDay = false;
            this.dayLegendMenstruations.IsPredictedAsBoyDay = false;
            this.dayLegendMenstruations.IsPredictedAsGirlDay = false;
            this.dayLegendMenstruations.IsPredictedAsMenstruationDay = false;
            this.dayLegendMenstruations.IsPredictedAsOvulationDay = false;
            this.dayLegendMenstruations.IsPredictedAsSafeSexDay = false;
            this.dayLegendMenstruations.IsPregnancyDay = false;
            this.dayLegendMenstruations.IsTodayDay = true;
            this.dayLegendMenstruations.Location = new System.Drawing.Point(8, 29);
            this.dayLegendMenstruations.ManualDrawOptions = true;
            this.dayLegendMenstruations.Name = "dayLegendMenstruations";
            this.dayLegendMenstruations.OwnerOneMonthControl = null;
            this.dayLegendMenstruations.PregnancyWeek = 0;
            this.dayLegendMenstruations.Size = new System.Drawing.Size(32, 32);
            this.dayLegendMenstruations.TabIndex = 0;
            // 
            // xWoman
            // 
            this.xWoman.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xWoman.Animated = true;
            this.xWoman.AnimationTime = 1;
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
            this.xWoman.Controls.Add(this.btnChangeWoman);
            this.xWoman.Controls.Add(this.chbAskPassword);
            this.xWoman.Controls.Add(this.chbDefaultWoman);
            this.xWoman.Controls.Add(this.lblMyCycle2);
            this.xWoman.Controls.Add(this.numMenstruationPeriod);
            this.xWoman.Controls.Add(this.lblMyCycle);
            this.xWoman.Controls.Add(this.rbManual);
            this.xWoman.Controls.Add(this.rbAuto);
            this.xWoman.Controls.Add(this.lblAverageCycle);
            this.xWoman.ExpandedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("xWoman.ExpandedHighlightImage")));
            this.xWoman.ExpandedImage = ((System.Drawing.Bitmap)(resources.GetObject("xWoman.ExpandedImage")));
            this.xWoman.Location = new System.Drawing.Point(0, 529);
            this.xWoman.Margin = new System.Windows.Forms.Padding(0);
            this.xWoman.Name = "xWoman";
            this.xWoman.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.xWoman.Size = new System.Drawing.Size(250, 155);
            this.xWoman.TabIndex = 0;
            this.xWoman.Tag = 1;
            this.xWoman.TooltipText = null;
            // 
            // btnChangeWoman
            // 
            this.btnChangeWoman.Location = new System.Drawing.Point(12, 124);
            this.btnChangeWoman.Name = "btnChangeWoman";
            this.btnChangeWoman.Size = new System.Drawing.Size(185, 23);
            this.btnChangeWoman.TabIndex = 9;
            this.btnChangeWoman.Text = "Изменить имя и пароль";
            this.btnChangeWoman.UseVisualStyleBackColor = true;
            this.btnChangeWoman.Click += new System.EventHandler(this.btnChangeWoman_Click);
            // 
            // chbAskPassword
            // 
            this.chbAskPassword.AutoSize = true;
            this.chbAskPassword.Location = new System.Drawing.Point(12, 72);
            this.chbAskPassword.Name = "chbAskPassword";
            this.chbAskPassword.Size = new System.Drawing.Size(188, 17);
            this.chbAskPassword.TabIndex = 8;
            this.chbAskPassword.Text = "Всегда спрашивать мой пароль";
            this.chbAskPassword.UseVisualStyleBackColor = true;
            this.chbAskPassword.CheckedChanged += new System.EventHandler(this.chbAskPassword_CheckedChanged);
            // 
            // chbDefaultWoman
            // 
            this.chbDefaultWoman.Location = new System.Drawing.Point(12, 86);
            this.chbDefaultWoman.Name = "chbDefaultWoman";
            this.chbDefaultWoman.Size = new System.Drawing.Size(185, 43);
            this.chbDefaultWoman.TabIndex = 7;
            this.chbDefaultWoman.Text = "При запуске программы открывать меня по умолчанию";
            this.chbDefaultWoman.UseVisualStyleBackColor = true;
            this.chbDefaultWoman.CheckedChanged += new System.EventHandler(this.chbDefaultWoman_CheckedChanged);
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
            this.rbAuto.Checked = true;
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
            this.xDay.AnimationTime = 1;
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
            this.xDay.Location = new System.Drawing.Point(0, 693);
            this.xDay.Margin = new System.Windows.Forms.Padding(0);
            this.xDay.Name = "xDay";
            this.xDay.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.xDay.Size = new System.Drawing.Size(251, 76);
            this.xDay.TabIndex = 1;
            this.xDay.Tag = 2;
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
            this.monthControl.Size = new System.Drawing.Size(493, 803);
            this.monthControl.StartMonth = new System.DateTime(2007, 2, 1, 0, 0, 0, 0);
            this.monthControl.TabIndex = 0;
            this.monthControl.FocusDateChanged += new WomenCalendar.MonthsControl.FocusDateChangedDelegate(this.monthControl_FocusDateChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 842);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(492, 308);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Овуляшки";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.MouseLeave += new System.EventHandler(this.MainForm_MouseLeave);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.xPanderList1.ResumeLayout(false);
            this.xLegend.ResumeLayout(false);
            this.xWoman.ResumeLayout(false);
            this.xWoman.PerformLayout();
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
        private System.Windows.Forms.CheckBox chbDefaultWoman;
        private System.Windows.Forms.CheckBox chbAskPassword;
        private System.Windows.Forms.Label lblMyCycle2;
        private System.Windows.Forms.Label lblMyCycle;
        private System.Windows.Forms.Label lblAverageCycle;
        private System.Windows.Forms.Button btnChangeWoman;
        private DayCellControl dayLegendMenstruations;
        private DayCellControl dayCellControl1;
        private DayCellControl dayCellControl2;
        private DayCellControl dayCellControl3;
        private DayCellControl dayCellControl4;
        private DayCellControl dayCellControl5;
        private DayCellControl dayCellControl6;
        private DayCellControl dayCellControl8;
        private DayCellControl dayCellControl7;
        private DayCellControl dayCellControl9;
        private DayCellControl dayCellControl10;
        private DayCellControl dayCellControl11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripButton exportToExcel;
        private System.Windows.Forms.Label label13;
        private DayCellControl dayCellControl12;


    }
}

