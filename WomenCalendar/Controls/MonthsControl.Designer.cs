namespace WomenCalendar
{
    partial class MonthsControl
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
            this.dayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAsMenstruationDay = new System.Windows.Forms.ToolStripMenuItem();
            this.removeMenstruationDay = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.setLastPregnancyDay = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsConceptionDay = new System.Windows.Forms.ToolStripMenuItem();
            this.removeConceptionDay = new System.Windows.Forms.ToolStripMenuItem();
            this.calendarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showBirthDate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.редактироватьДеньToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripBBTGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripHealthGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.oneMonthControl = new WomenCalendar.OneMonthControl();
            this.ToolStripCycleLengthGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.dayContextMenu.SuspendLayout();
            this.monthMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dayContextMenu
            // 
            this.dayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAsMenstruationDay,
            this.removeMenstruationDay,
            this.toolStripSeparator1,
            this.setLastPregnancyDay,
            this.setAsConceptionDay,
            this.removeConceptionDay,
            this.calendarMenu,
            this.showBirthDate,
            this.toolStripSeparator3,
            this.редактироватьДеньToolStripMenuItem});
            this.dayContextMenu.Name = "contextMenu";
            this.dayContextMenu.Size = new System.Drawing.Size(321, 192);
            // 
            // setAsMenstruationDay
            // 
            this.setAsMenstruationDay.Image = global::WomenCalendar.Properties.Resources.drop_Image;
            this.setAsMenstruationDay.Name = "setAsMenstruationDay";
            this.setAsMenstruationDay.Size = new System.Drawing.Size(320, 22);
            this.setAsMenstruationDay.Text = "Установить начало менструашек";
            this.setAsMenstruationDay.Click += new System.EventHandler(this.setAsMenstruationDay_Click);
            // 
            // removeMenstruationDay
            // 
            this.removeMenstruationDay.Image = global::WomenCalendar.Properties.Resources.dropNot_Image;
            this.removeMenstruationDay.Name = "removeMenstruationDay";
            this.removeMenstruationDay.Size = new System.Drawing.Size(320, 22);
            this.removeMenstruationDay.Text = "Отменить менструашки";
            this.removeMenstruationDay.Click += new System.EventHandler(this.removeMenstruationDay_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(317, 6);
            // 
            // setLastPregnancyDay
            // 
            this.setLastPregnancyDay.Image = global::WomenCalendar.Properties.Resources.baby_Image;
            this.setLastPregnancyDay.Name = "setLastPregnancyDay";
            this.setLastPregnancyDay.Size = new System.Drawing.Size(320, 22);
            this.setLastPregnancyDay.Text = "Установить последний день беременности";
            this.setLastPregnancyDay.Click += new System.EventHandler(this.setLastPregnancyDay_Click);
            // 
            // setAsConceptionDay
            // 
            this.setAsConceptionDay.Image = global::WomenCalendar.Properties.Resources.baby_Image;
            this.setAsConceptionDay.Name = "setAsConceptionDay";
            this.setAsConceptionDay.Size = new System.Drawing.Size(320, 22);
            this.setAsConceptionDay.Text = "Установить беременность";
            this.setAsConceptionDay.Click += new System.EventHandler(this.setAsConceptionDay_Click);
            // 
            // removeConceptionDay
            // 
            this.removeConceptionDay.Image = global::WomenCalendar.Properties.Resources.babyNot_Image;
            this.removeConceptionDay.Name = "removeConceptionDay";
            this.removeConceptionDay.Size = new System.Drawing.Size(320, 22);
            this.removeConceptionDay.Text = "Отменить беременность";
            this.removeConceptionDay.Click += new System.EventHandler(this.removeConceptionDay_Click);
            // 
            // calendarMenu
            // 
            this.calendarMenu.Image = global::WomenCalendar.Properties.Resources.babyQuestion_Image;
            this.calendarMenu.Name = "calendarMenu";
            this.calendarMenu.Size = new System.Drawing.Size(320, 22);
            this.calendarMenu.Text = "Подсказка беременным на $ неделе с сайта...";
            // 
            // showBirthDate
            // 
            this.showBirthDate.Image = global::WomenCalendar.Properties.Resources.babyFace_Image;
            this.showBirthDate.Name = "showBirthDate";
            this.showBirthDate.Size = new System.Drawing.Size(320, 22);
            this.showBirthDate.Text = "Показать предполагаемый день родов";
            this.showBirthDate.Click += new System.EventHandler(this.showBirthDate_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(317, 6);
            // 
            // редактироватьДеньToolStripMenuItem
            // 
            this.редактироватьДеньToolStripMenuItem.Image = global::WomenCalendar.Properties.Resources.calendarEdit_Image;
            this.редактироватьДеньToolStripMenuItem.Name = "редактироватьДеньToolStripMenuItem";
            this.редактироватьДеньToolStripMenuItem.Size = new System.Drawing.Size(320, 22);
            this.редактироватьДеньToolStripMenuItem.Text = "Редактировать день";
            this.редактироватьДеньToolStripMenuItem.Click += new System.EventHandler(this.editDay_Click);
            // 
            // monthMenu
            // 
            this.monthMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripBBTGraph,
            this.ToolStripHealthGraph,
            this.ToolStripCycleLengthGraph});
            this.monthMenu.Name = "monthMenu";
            this.monthMenu.Size = new System.Drawing.Size(334, 92);
            // 
            // ToolStripBBTGraph
            // 
            this.ToolStripBBTGraph.Name = "ToolStripBBTGraph";
            this.ToolStripBBTGraph.Size = new System.Drawing.Size(333, 22);
            this.ToolStripBBTGraph.Text = "Построить график Базальной Температуры Тела";
            this.ToolStripBBTGraph.Click += new System.EventHandler(this.ToolStripBBTGraph_Click);
            // 
            // ToolStripHealthGraph
            // 
            this.ToolStripHealthGraph.Name = "ToolStripHealthGraph";
            this.ToolStripHealthGraph.Size = new System.Drawing.Size(333, 22);
            this.ToolStripHealthGraph.Text = "Построить график Самочувствия";
            this.ToolStripHealthGraph.Click += new System.EventHandler(this.ToolStripHealthesGraph_Click);
            // 
            // oneMonthControl
            // 
            this.oneMonthControl.BackColor = System.Drawing.Color.White;
            this.oneMonthControl.Date = new System.DateTime(2008, 2, 1, 0, 0, 0, 0);
            this.oneMonthControl.FocusDate = new System.DateTime(((long)(0)));
            this.oneMonthControl.FocusDay = null;
            this.oneMonthControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.oneMonthControl.ForeColor = System.Drawing.Color.Transparent;
            this.oneMonthControl.Location = new System.Drawing.Point(0, 0);
            this.oneMonthControl.Margin = new System.Windows.Forms.Padding(0);
            this.oneMonthControl.Name = "oneMonthControl";
            this.oneMonthControl.OwnerMonthsControl = null;
            this.oneMonthControl.Size = new System.Drawing.Size(230, 230);
            this.oneMonthControl.TabIndex = 0;
            // 
            // ToolStripCycleLengthGraph
            // 
            this.ToolStripCycleLengthGraph.Name = "ToolStripCycleLengthGraph";
            this.ToolStripCycleLengthGraph.Size = new System.Drawing.Size(333, 22);
            this.ToolStripCycleLengthGraph.Text = "Построить график Длины Циклов";
            this.ToolStripCycleLengthGraph.Click += new System.EventHandler(this.ToolStripCycleLengthGraph_Click);
            // 
            // MonthsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.Controls.Add(this.oneMonthControl);
            this.DoubleBuffered = true;
            this.Name = "MonthsControl";
            this.Size = new System.Drawing.Size(501, 440);
            this.MouseLeave += new System.EventHandler(this.MonthsControl_MouseLeave);
            this.SizeChanged += new System.EventHandler(this.MonthControl_SizeChanged);
            this.MouseEnter += new System.EventHandler(this.MonthsControl_MouseEnter);
            this.dayContextMenu.ResumeLayout(false);
            this.monthMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OneMonthControl oneMonthControl;
        private System.Windows.Forms.ContextMenuStrip dayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem setAsMenstruationDay;
        private System.Windows.Forms.ToolStripMenuItem removeMenstruationDay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip monthMenu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripBBTGraph;
        private System.Windows.Forms.ToolStripMenuItem ToolStripHealthGraph;
        private System.Windows.Forms.ToolStripMenuItem setAsConceptionDay;
        private System.Windows.Forms.ToolStripMenuItem removeConceptionDay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem редактироватьДеньToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calendarMenu;
        private System.Windows.Forms.ToolStripMenuItem setLastPregnancyDay;
        private System.Windows.Forms.ToolStripMenuItem showBirthDate;
        private System.Windows.Forms.ToolStripMenuItem ToolStripCycleLengthGraph;

    }
}
