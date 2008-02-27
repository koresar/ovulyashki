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
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAsMenstruationDay = new System.Windows.Forms.ToolStripMenuItem();
            this.removeMenstruationDay = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addNote = new System.Windows.Forms.ToolStripMenuItem();
            this.removeNote = new System.Windows.Forms.ToolStripMenuItem();
            this.editNote = new System.Windows.Forms.ToolStripMenuItem();
            this.oneMonthControl = new WomenCalendar.OneMonthControl();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAsMenstruationDay,
            this.removeMenstruationDay,
            this.toolStripSeparator1,
            this.addNote,
            this.removeNote,
            this.editNote});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(238, 142);
            // 
            // setAsMenstruationDay
            // 
            this.setAsMenstruationDay.Name = "setAsMenstruationDay";
            this.setAsMenstruationDay.Size = new System.Drawing.Size(237, 22);
            this.setAsMenstruationDay.Text = "Установить начало овуляшек";
            this.setAsMenstruationDay.Click += new System.EventHandler(this.setAsMenstruationDay_Click);
            // 
            // removeMenstruationDay
            // 
            this.removeMenstruationDay.Name = "removeMenstruationDay";
            this.removeMenstruationDay.Size = new System.Drawing.Size(237, 22);
            this.removeMenstruationDay.Text = "Отменить овуляшки";
            this.removeMenstruationDay.Click += new System.EventHandler(this.removeMenstruationDay_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(234, 6);
            // 
            // addNote
            // 
            this.addNote.Name = "addNote";
            this.addNote.Size = new System.Drawing.Size(237, 22);
            this.addNote.Text = "Добавить заметку";
            this.addNote.Click += new System.EventHandler(this.addNote_Click);
            // 
            // removeNote
            // 
            this.removeNote.Name = "removeNote";
            this.removeNote.Size = new System.Drawing.Size(237, 22);
            this.removeNote.Text = "Удалить заметку";
            this.removeNote.Click += new System.EventHandler(this.removeNote_Click);
            // 
            // editNote
            // 
            this.editNote.Name = "editNote";
            this.editNote.Size = new System.Drawing.Size(237, 22);
            this.editNote.Text = "Редактировать заметку";
            this.editNote.Click += new System.EventHandler(this.editNote_Click);
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
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OneMonthControl oneMonthControl;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem setAsMenstruationDay;
        private System.Windows.Forms.ToolStripMenuItem addNote;
        private System.Windows.Forms.ToolStripMenuItem removeMenstruationDay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem removeNote;
        private System.Windows.Forms.ToolStripMenuItem editNote;

    }
}
