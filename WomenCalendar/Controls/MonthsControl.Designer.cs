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
            this.ediDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripBBTGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripHealthGraph = new System.Windows.Forms.ToolStripMenuItem();
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
            this.ediDayToolStripMenuItem});
            this.dayContextMenu.Name = "contextMenu";
            this.dayContextMenu.Size = new System.Drawing.Size(283, 214);
            this.dayContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.dayContextMenu_Opening);
            // 
            // setAsMenstruationDay
            // 
            this.setAsMenstruationDay.Image = global::WomenCalendar.Properties.Resources.drop_Image;
            this.setAsMenstruationDay.Name = "setAsMenstruationDay";
            this.setAsMenstruationDay.Size = new System.Drawing.Size(282, 22);
            this.setAsMenstruationDay.Text = "Set menses start";
            this.setAsMenstruationDay.Click += new System.EventHandler(this.setAsMenstruationDay_Click);
            // 
            // removeMenstruationDay
            // 
            this.removeMenstruationDay.Image = global::WomenCalendar.Properties.Resources.dropNot_Image;
            this.removeMenstruationDay.Name = "removeMenstruationDay";
            this.removeMenstruationDay.Size = new System.Drawing.Size(282, 22);
            this.removeMenstruationDay.Text = "Cancel menses";
            this.removeMenstruationDay.Click += new System.EventHandler(this.removeMenstruationDay_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(279, 6);
            // 
            // setLastPregnancyDay
            // 
            this.setLastPregnancyDay.Image = global::WomenCalendar.Properties.Resources.baby_Image;
            this.setLastPregnancyDay.Name = "setLastPregnancyDay";
            this.setLastPregnancyDay.Size = new System.Drawing.Size(282, 22);
            this.setLastPregnancyDay.Text = "Set last pregnancy day";
            this.setLastPregnancyDay.Click += new System.EventHandler(this.setLastPregnancyDay_Click);
            // 
            // setAsConceptionDay
            // 
            this.setAsConceptionDay.Image = global::WomenCalendar.Properties.Resources.baby_Image;
            this.setAsConceptionDay.Name = "setAsConceptionDay";
            this.setAsConceptionDay.Size = new System.Drawing.Size(282, 22);
            this.setAsConceptionDay.Text = "Set pregnancy";
            this.setAsConceptionDay.Click += new System.EventHandler(this.setAsConceptionDay_Click);
            // 
            // removeConceptionDay
            // 
            this.removeConceptionDay.Image = global::WomenCalendar.Properties.Resources.babyNot_Image;
            this.removeConceptionDay.Name = "removeConceptionDay";
            this.removeConceptionDay.Size = new System.Drawing.Size(282, 22);
            this.removeConceptionDay.Text = "Cancel pregnancy";
            this.removeConceptionDay.Click += new System.EventHandler(this.removeConceptionDay_Click);
            // 
            // calendarMenu
            // 
            this.calendarMenu.Image = global::WomenCalendar.Properties.Resources.babyQuestion_Image;
            this.calendarMenu.Name = "calendarMenu";
            this.calendarMenu.Size = new System.Drawing.Size(282, 22);
            this.calendarMenu.Text = "Help for {0} week pregnancy from site...";
            // 
            // showBirthDate
            // 
            this.showBirthDate.Image = global::WomenCalendar.Properties.Resources.babyFace_Image;
            this.showBirthDate.Name = "showBirthDate";
            this.showBirthDate.Size = new System.Drawing.Size(282, 22);
            this.showBirthDate.Text = "Show forecasted childbirth day";
            this.showBirthDate.Click += new System.EventHandler(this.showBirthDate_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(279, 6);
            // 
            // ediDayToolStripMenuItem
            // 
            this.ediDayToolStripMenuItem.Image = global::WomenCalendar.Properties.Resources.calendarEdit_Image;
            this.ediDayToolStripMenuItem.Name = "ediDayToolStripMenuItem";
            this.ediDayToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.ediDayToolStripMenuItem.Text = "Edit day";
            this.ediDayToolStripMenuItem.Click += new System.EventHandler(this.editDay_Click);
            // 
            // monthMenu
            // 
            this.monthMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripBBTGraph,
            this.ToolStripHealthGraph,
            this.ToolStripCycleLengthGraph});
            this.monthMenu.Name = "monthMenu";
            this.monthMenu.Size = new System.Drawing.Size(269, 70);
            // 
            // ToolStripBBTGraph
            // 
            this.ToolStripBBTGraph.Name = "ToolStripBBTGraph";
            this.ToolStripBBTGraph.Size = new System.Drawing.Size(268, 22);
            this.ToolStripBBTGraph.Text = "Show Basal Body Temperature graph";
            this.ToolStripBBTGraph.Click += new System.EventHandler(this.ToolStripBBTGraph_Click);
            // 
            // ToolStripHealthGraph
            // 
            this.ToolStripHealthGraph.Name = "ToolStripHealthGraph";
            this.ToolStripHealthGraph.Size = new System.Drawing.Size(268, 22);
            this.ToolStripHealthGraph.Text = "Show wellbeing graph";
            this.ToolStripHealthGraph.Click += new System.EventHandler(this.ToolStripHealthesGraph_Click);
            // 
            // ToolStripCycleLengthGraph
            // 
            this.ToolStripCycleLengthGraph.Name = "ToolStripCycleLengthGraph";
            this.ToolStripCycleLengthGraph.Size = new System.Drawing.Size(268, 22);
            this.ToolStripCycleLengthGraph.Text = "Show cycle length graph";
            this.ToolStripCycleLengthGraph.Click += new System.EventHandler(this.ToolStripCycleLengthGraph_Click);
            // 
            // MonthsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.DoubleBuffered = true;
            this.Name = "MonthsControl";
            this.Size = new System.Drawing.Size(501, 440);
            this.SizeChanged += new System.EventHandler(this.MonthControl_SizeChanged);
            this.MouseEnter += new System.EventHandler(this.MonthsControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MonthsControl_MouseLeave);
            this.dayContextMenu.ResumeLayout(false);
            this.monthMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.ToolStripMenuItem ediDayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calendarMenu;
        private System.Windows.Forms.ToolStripMenuItem setLastPregnancyDay;
        private System.Windows.Forms.ToolStripMenuItem showBirthDate;
        private System.Windows.Forms.ToolStripMenuItem ToolStripCycleLengthGraph;

    }
}
