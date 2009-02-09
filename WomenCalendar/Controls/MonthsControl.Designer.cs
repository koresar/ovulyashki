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
            this.removeNote = new System.Windows.Forms.ToolStripMenuItem();
            this.editNote = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.setAsConceptionDay = new System.Windows.Forms.ToolStripMenuItem();
            this.removeConceptionDay = new System.Windows.Forms.ToolStripMenuItem();
            this.calendarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.редактироватьДеньToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripBBTGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.построитьГрафикСамочувствияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneMonthControl = new WomenCalendar.OneMonthControl();
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
            this.removeNote,
            this.editNote,
            this.toolStripSeparator2,
            this.setAsConceptionDay,
            this.removeConceptionDay,
            this.calendarMenu,
            this.toolStripSeparator3,
            this.редактироватьДеньToolStripMenuItem});
            this.dayContextMenu.Name = "contextMenu";
            this.dayContextMenu.Size = new System.Drawing.Size(321, 220);
            // 
            // setAsMenstruationDay
            // 
            this.setAsMenstruationDay.Name = "setAsMenstruationDay";
            this.setAsMenstruationDay.Size = new System.Drawing.Size(320, 22);
            this.setAsMenstruationDay.Text = "Установить начало менструашек";
            this.setAsMenstruationDay.Click += new System.EventHandler(this.setAsMenstruationDay_Click);
            // 
            // removeMenstruationDay
            // 
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
            // removeNote
            // 
            this.removeNote.Name = "removeNote";
            this.removeNote.Size = new System.Drawing.Size(320, 22);
            this.removeNote.Text = "Удалить заметку";
            this.removeNote.Click += new System.EventHandler(this.removeNote_Click);
            // 
            // editNote
            // 
            this.editNote.Name = "editNote";
            this.editNote.Size = new System.Drawing.Size(320, 22);
            this.editNote.Text = "Редактировать заметку";
            this.editNote.Click += new System.EventHandler(this.editNote_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(317, 6);
            // 
            // setAsConceptionDay
            // 
            this.setAsConceptionDay.Name = "setAsConceptionDay";
            this.setAsConceptionDay.Size = new System.Drawing.Size(320, 22);
            this.setAsConceptionDay.Text = "Установить беременность";
            this.setAsConceptionDay.Click += new System.EventHandler(this.setAsConceptionDay_Click);
            // 
            // removeConceptionDay
            // 
            this.removeConceptionDay.Name = "removeConceptionDay";
            this.removeConceptionDay.Size = new System.Drawing.Size(320, 22);
            this.removeConceptionDay.Text = "Отменить беременность";
            this.removeConceptionDay.Click += new System.EventHandler(this.removeConceptionDay_Click);
            // 
            // calendarMenu
            // 
            this.calendarMenu.Name = "calendarMenu";
            this.calendarMenu.Size = new System.Drawing.Size(320, 22);
            this.calendarMenu.Text = "Подсказка беременным на $ неделе с сайта...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(317, 6);
            // 
            // редактироватьДеньToolStripMenuItem
            // 
            this.редактироватьДеньToolStripMenuItem.Name = "редактироватьДеньToolStripMenuItem";
            this.редактироватьДеньToolStripMenuItem.Size = new System.Drawing.Size(320, 22);
            this.редактироватьДеньToolStripMenuItem.Text = "Редактировать день";
            this.редактироватьДеньToolStripMenuItem.Click += new System.EventHandler(this.editDay_Click);
            // 
            // monthMenu
            // 
            this.monthMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripBBTGraph,
            this.построитьГрафикСамочувствияToolStripMenuItem});
            this.monthMenu.Name = "monthMenu";
            this.monthMenu.Size = new System.Drawing.Size(334, 48);
            // 
            // ToolStripBBTGraph
            // 
            this.ToolStripBBTGraph.Name = "ToolStripBBTGraph";
            this.ToolStripBBTGraph.Size = new System.Drawing.Size(333, 22);
            this.ToolStripBBTGraph.Text = "Построить график Базальной Температуры Тела";
            this.ToolStripBBTGraph.Click += new System.EventHandler(this.ToolStripBBTGraph_Click);
            // 
            // построитьГрафикСамочувствияToolStripMenuItem
            // 
            this.построитьГрафикСамочувствияToolStripMenuItem.Name = "построитьГрафикСамочувствияToolStripMenuItem";
            this.построитьГрафикСамочувствияToolStripMenuItem.Size = new System.Drawing.Size(333, 22);
            this.построитьГрафикСамочувствияToolStripMenuItem.Text = "Построить график Самочувствия";
            this.построитьГрафикСамочувствияToolStripMenuItem.Click += new System.EventHandler(this.ToolStripHealthesGraph_Click);
            // 
            // oneMonthControl
            // 
            this.oneMonthControl.Date = new System.DateTime(2008, 2, 1, 0, 0, 0, 0);
            this.oneMonthControl.FocusDate = new System.DateTime(((long)(0)));
            this.oneMonthControl.FocusDay = null;
            this.oneMonthControl.ForeColor = System.Drawing.Color.Transparent;
            this.oneMonthControl.Location = new System.Drawing.Point(0, 0);
            this.oneMonthControl.Margin = new System.Windows.Forms.Padding(0);
            this.oneMonthControl.Name = "oneMonthControl";
            this.oneMonthControl.OwnerMonthsControl = null;
            this.oneMonthControl.Size = new System.Drawing.Size(230, 230);
            this.oneMonthControl.TabIndex = 0;
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
        private System.Windows.Forms.ToolStripMenuItem removeNote;
        private System.Windows.Forms.ToolStripMenuItem editNote;
        private System.Windows.Forms.ContextMenuStrip monthMenu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripBBTGraph;
        private System.Windows.Forms.ToolStripMenuItem построитьГрафикСамочувствияToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem setAsConceptionDay;
        private System.Windows.Forms.ToolStripMenuItem removeConceptionDay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem редактироватьДеньToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calendarMenu;

    }
}
