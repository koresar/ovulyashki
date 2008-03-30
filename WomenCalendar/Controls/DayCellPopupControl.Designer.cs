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
            this.lblDay = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureNote = new System.Windows.Forms.PictureBox();
            this.lblBBT = new System.Windows.Forms.Label();
            this.lblHadSex = new System.Windows.Forms.Label();
            this.sliderEgestaAmount = new MB.Controls.ColorSlider();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNote)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDay
            // 
            this.lblDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDay.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDay.Location = new System.Drawing.Point(0, 0);
            this.lblDay.Margin = new System.Windows.Forms.Padding(0);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(29, 20);
            this.lblDay.TabIndex = 1;
            this.lblDay.Text = "32";
            this.lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.pictureNote.Location = new System.Drawing.Point(31, 1);
            this.pictureNote.Name = "pictureNote";
            this.pictureNote.Size = new System.Drawing.Size(14, 14);
            this.pictureNote.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureNote.TabIndex = 2;
            this.pictureNote.TabStop = false;
            this.pictureNote.MouseLeave += new System.EventHandler(this.pictureNote_MouseLeave);
            this.pictureNote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureNote_MouseClick);
            this.pictureNote.MouseEnter += new System.EventHandler(this.pictureNote_MouseEnter);
            // 
            // lblBBT
            // 
            this.lblBBT.Location = new System.Drawing.Point(0, 49);
            this.lblBBT.Name = "lblBBT";
            this.lblBBT.Size = new System.Drawing.Size(45, 14);
            this.lblBBT.TabIndex = 4;
            this.lblBBT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblBBT_MouseClick);
            // 
            // lblHadSex
            // 
            this.lblHadSex.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblHadSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHadSex.ForeColor = System.Drawing.Color.Red;
            this.lblHadSex.Location = new System.Drawing.Point(30, 18);
            this.lblHadSex.Name = "lblHadSex";
            this.lblHadSex.Size = new System.Drawing.Size(13, 11);
            this.lblHadSex.TabIndex = 5;
            this.lblHadSex.Text = "S";
            this.lblHadSex.MouseLeave += new System.EventHandler(this.lblHadSex_MouseLeave);
            this.lblHadSex.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblHadSex_MouseClick);
            this.lblHadSex.MouseEnter += new System.EventHandler(this.lblHadSex_MouseEnter);
            // 
            // sliderEgestaAmount
            // 
            this.sliderEgestaAmount.BackColor = System.Drawing.Color.Transparent;
            this.sliderEgestaAmount.BarInnerColor = System.Drawing.Color.Pink;
            this.sliderEgestaAmount.BarOuterColor = System.Drawing.Color.DeepPink;
            this.sliderEgestaAmount.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderEgestaAmount.DrawFocusRectangle = false;
            this.sliderEgestaAmount.ElapsedInnerColor = System.Drawing.Color.Pink;
            this.sliderEgestaAmount.ElapsedOuterColor = System.Drawing.Color.DeepPink;
            this.sliderEgestaAmount.LargeChange = ((uint)(1u));
            this.sliderEgestaAmount.Location = new System.Drawing.Point(43, 1);
            this.sliderEgestaAmount.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEgestaAmount.Maximum = 4;
            this.sliderEgestaAmount.Name = "sliderEgestaAmount";
            this.sliderEgestaAmount.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderEgestaAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sliderEgestaAmount.Size = new System.Drawing.Size(20, 60);
            this.sliderEgestaAmount.SmallChange = ((uint)(1u));
            this.sliderEgestaAmount.TabIndex = 3;
            this.sliderEgestaAmount.Text = "colorSlider1";
            this.sliderEgestaAmount.ThumbInnerColor = System.Drawing.Color.WhiteSmoke;
            this.sliderEgestaAmount.ThumbOuterColor = System.Drawing.Color.Pink;
            this.sliderEgestaAmount.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderEgestaAmount.Value = 0;
            this.sliderEgestaAmount.MouseLeave += new System.EventHandler(this.sliderEgestaAmount_MouseLeave);
            this.sliderEgestaAmount.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sliderEgestaAmount_Scroll);
            this.sliderEgestaAmount.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sliderEgestaAmount_MouseDown);
            this.sliderEgestaAmount.MouseEnter += new System.EventHandler(this.sliderEgestaAmount_MouseEnter);
            // 
            // DayCellPopupControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.lblHadSex);
            this.Controls.Add(this.lblBBT);
            this.Controls.Add(this.sliderEgestaAmount);
            this.Controls.Add(this.pictureNote);
            this.Controls.Add(this.lblDay);
            this.DoubleBuffered = true;
            this.Name = "DayCellPopupControl";
            this.Size = new System.Drawing.Size(64, 64);
            this.DoubleClick += new System.EventHandler(this.DayCellPopupControl_DoubleClick);
            this.MouseLeave += new System.EventHandler(this.DayCellPopupControl_MouseLeave);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DayCellPopupControl_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureNote)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox pictureNote;
        private MB.Controls.ColorSlider sliderEgestaAmount;
        private System.Windows.Forms.Label lblBBT;
        private System.Windows.Forms.Label lblHadSex;
    }
}
