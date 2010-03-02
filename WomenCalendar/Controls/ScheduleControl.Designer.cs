namespace WomenCalendar.Controls
{
    partial class ScheduleControl
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
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numTake = new System.Windows.Forms.NumericUpDown();
            this.numPause = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numTake)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPause)).BeginInit();
            this.SuspendLayout();
            // 
            // dateStart
            // 
            this.dateStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateStart.Location = new System.Drawing.Point(63, 4);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(109, 20);
            this.dateStart.TabIndex = 0;
            this.dateStart.ValueChanged += new System.EventHandler(this.controls_ValueChanged);
            // 
            // dateEnd
            // 
            this.dateEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEnd.Location = new System.Drawing.Point(241, 4);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(109, 20);
            this.dateEnd.TabIndex = 0;
            this.dateEnd.ValueChanged += new System.EventHandler(this.controls_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(181, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "End:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Start:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Take days:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(181, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Pause days:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numTake
            // 
            this.numTake.Location = new System.Drawing.Point(105, 36);
            this.numTake.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTake.Name = "numTake";
            this.numTake.Size = new System.Drawing.Size(67, 20);
            this.numTake.TabIndex = 2;
            this.numTake.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTake.ValueChanged += new System.EventHandler(this.controls_ValueChanged);
            // 
            // numPause
            // 
            this.numPause.Location = new System.Drawing.Point(283, 36);
            this.numPause.Name = "numPause";
            this.numPause.Size = new System.Drawing.Size(67, 20);
            this.numPause.TabIndex = 2;
            this.numPause.ValueChanged += new System.EventHandler(this.controls_ValueChanged);
            // 
            // ScheduleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numPause);
            this.Controls.Add(this.numTake);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.dateStart);
            this.Name = "ScheduleControl";
            this.Size = new System.Drawing.Size(353, 62);
            this.Load += new System.EventHandler(this.ScheduleControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTake)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPause)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateStart;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numTake;
        private System.Windows.Forms.NumericUpDown numPause;
    }
}
