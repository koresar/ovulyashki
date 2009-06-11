namespace WomenCalendar
{
    partial class ScheduleEditControl
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
            this.numTake = new System.Windows.Forms.NumericUpDown();
            this.numPause = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDays1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDays2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTill = new System.Windows.Forms.DateTimePicker();
            this.txtName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numTake)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPause)).BeginInit();
            this.SuspendLayout();
            // 
            // numTake
            // 
            this.numTake.Location = new System.Drawing.Point(71, 38);
            this.numTake.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTake.Name = "numTake";
            this.numTake.Size = new System.Drawing.Size(43, 20);
            this.numTake.TabIndex = 1;
            this.numTake.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTake.ValueChanged += new System.EventHandler(this.numTake_ValueChanged);
            // 
            // numPause
            // 
            this.numPause.Location = new System.Drawing.Point(308, 38);
            this.numPause.Name = "numPause";
            this.numPause.Size = new System.Drawing.Size(43, 20);
            this.numPause.TabIndex = 1;
            this.numPause.ValueChanged += new System.EventHandler(this.numPause_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Принимать";
            // 
            // lblDays1
            // 
            this.lblDays1.AutoSize = true;
            this.lblDays1.Location = new System.Drawing.Point(116, 41);
            this.lblDays1.Name = "lblDays1";
            this.lblDays1.Size = new System.Drawing.Size(31, 13);
            this.lblDays1.TabIndex = 2;
            this.lblDays1.Text = "дней";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Потом прерваться на";
            // 
            // lblDays2
            // 
            this.lblDays2.AutoSize = true;
            this.lblDays2.Location = new System.Drawing.Point(353, 41);
            this.lblDays2.Name = "lblDays2";
            this.lblDays2.Size = new System.Drawing.Size(31, 13);
            this.lblDays2.TabIndex = 2;
            this.lblDays2.Text = "дней";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Описание";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "И так повторять до";
            // 
            // dateTill
            // 
            this.dateTill.Location = new System.Drawing.Point(127, 67);
            this.dateTill.Name = "dateTill";
            this.dateTill.Size = new System.Drawing.Size(200, 20);
            this.dateTill.TabIndex = 6;
            this.dateTill.ValueChanged += new System.EventHandler(this.dateTill_ValueChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(80, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(313, 20);
            this.txtName.TabIndex = 7;
            // 
            // ScheduleEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.dateTill);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDays2);
            this.Controls.Add(this.lblDays1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numPause);
            this.Controls.Add(this.numTake);
            this.Name = "ScheduleEditControl";
            this.Size = new System.Drawing.Size(396, 94);
            this.Load += new System.EventHandler(this.ScheduleEditControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTake)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPause)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numTake;
        private System.Windows.Forms.NumericUpDown numPause;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDays1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDays2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTill;
        private System.Windows.Forms.TextBox txtName;
    }
}
