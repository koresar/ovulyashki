namespace WomenCalendar
{
    partial class DayCellPopupControl
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
            this.trackEgestaAmount = new System.Windows.Forms.TrackBar();
            this.lblDay = new System.Windows.Forms.LinkLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureNote = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackEgestaAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNote)).BeginInit();
            this.SuspendLayout();
            // 
            // trackEgestaAmount
            // 
            this.trackEgestaAmount.AutoSize = false;
            this.trackEgestaAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.trackEgestaAmount.LargeChange = 1;
            this.trackEgestaAmount.Location = new System.Drawing.Point(43, -3);
            this.trackEgestaAmount.Margin = new System.Windows.Forms.Padding(0);
            this.trackEgestaAmount.Maximum = 4;
            this.trackEgestaAmount.Name = "trackEgestaAmount";
            this.trackEgestaAmount.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackEgestaAmount.Size = new System.Drawing.Size(20, 70);
            this.trackEgestaAmount.TabIndex = 0;
            this.trackEgestaAmount.TabStop = false;
            this.trackEgestaAmount.Value = 2;
            this.trackEgestaAmount.MouseLeave += new System.EventHandler(this.trackEgestaAmount_MouseLeave);
            this.trackEgestaAmount.ValueChanged += new System.EventHandler(this.trackEgestaAmount_ValueChanged);
            this.trackEgestaAmount.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackEgestaAmount_MouseDown);
            this.trackEgestaAmount.MouseEnter += new System.EventHandler(this.trackEgestaAmount_MouseEnter);
            // 
            // lblDay
            // 
            this.lblDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDay.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDay.LinkColor = System.Drawing.Color.Blue;
            this.lblDay.Location = new System.Drawing.Point(0, 0);
            this.lblDay.Margin = new System.Windows.Forms.Padding(0);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(29, 20);
            this.lblDay.TabIndex = 1;
            this.lblDay.TabStop = true;
            this.lblDay.Text = "32";
            this.lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDay.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lblDay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblDay_MouseClick);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 0;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.UseAnimation = false;
            this.toolTip.UseFading = false;
            // 
            // pictureNote
            // 
            this.pictureNote.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pictureNote.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureNote.Image = global::WomenCalendar.Properties.Resources.note_Image;
            this.pictureNote.Location = new System.Drawing.Point(30, 3);
            this.pictureNote.Name = "pictureNote";
            this.pictureNote.Size = new System.Drawing.Size(14, 14);
            this.pictureNote.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureNote.TabIndex = 2;
            this.pictureNote.TabStop = false;
            this.pictureNote.MouseLeave += new System.EventHandler(this.pictureNote_MouseLeave);
            this.pictureNote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureNote_MouseClick);
            this.pictureNote.MouseEnter += new System.EventHandler(this.pictureNote_MouseEnter);
            // 
            // DayCellPopupControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.pictureNote);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.trackEgestaAmount);
            this.DoubleBuffered = true;
            this.Name = "DayCellPopupControl";
            this.Size = new System.Drawing.Size(64, 64);
            this.DoubleClick += new System.EventHandler(this.DayCellPopupControl_DoubleClick);
            this.MouseLeave += new System.EventHandler(this.DayCellPopupControl_MouseLeave);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DayCellPopupControl_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.trackEgestaAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNote)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackEgestaAmount;
        private System.Windows.Forms.LinkLabel lblDay;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox pictureNote;
    }
}
