namespace WomenCalendar
{
    partial class SchedulesEditForm
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
            this.btnAddSchedule = new System.Windows.Forms.Button();
            this.scheduleControl1 = new WomenCalendar.ScheduleEditControl();
            this.schedulesControl1 = new WomenCalendar.Controls.SchedulesControl();
            this.btnDeleteSchedule = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddSchedule
            // 
            this.btnAddSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSchedule.Location = new System.Drawing.Point(438, 19);
            this.btnAddSchedule.Name = "btnAddSchedule";
            this.btnAddSchedule.Size = new System.Drawing.Size(177, 23);
            this.btnAddSchedule.TabIndex = 10002;
            this.btnAddSchedule.Text = "Добавить это расписание";
            this.btnAddSchedule.UseVisualStyleBackColor = true;
            this.btnAddSchedule.Click += new System.EventHandler(this.btnAddSchedule_Click);
            // 
            // scheduleScheduleControl1
            // 
            this.scheduleControl1.DefaultStartDate = new System.DateTime(((long)(0)));
            this.scheduleControl1.Location = new System.Drawing.Point(13, 12);
            this.scheduleControl1.Name = "scheduleScheduleControl1";
            this.scheduleControl1.Size = new System.Drawing.Size(396, 94);
            this.scheduleControl1.TabIndex = 10006;
            // 
            // scheduleSchedulesControl1
            // 
            this.schedulesControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.schedulesControl1.DefaultStartDate = new System.DateTime(((long)(0)));
            this.schedulesControl1.Location = new System.Drawing.Point(12, 112);
            this.schedulesControl1.Name = "scheduleSchedulesControl1";
            this.schedulesControl1.Size = new System.Drawing.Size(618, 164);
            this.schedulesControl1.TabIndex = 10007;
            // 
            // btnDeleteSchedule
            // 
            this.btnDeleteSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSchedule.Location = new System.Drawing.Point(438, 48);
            this.btnDeleteSchedule.Name = "btnDeleteSchedule";
            this.btnDeleteSchedule.Size = new System.Drawing.Size(177, 23);
            this.btnDeleteSchedule.TabIndex = 10002;
            this.btnDeleteSchedule.Text = "Удалить это расписание";
            this.btnDeleteSchedule.UseVisualStyleBackColor = true;
            this.btnDeleteSchedule.Click += new System.EventHandler(this.btnDeleteSchedule_Click);
            // 
            // SchedulesEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 316);
            this.Controls.Add(this.schedulesControl1);
            this.Controls.Add(this.scheduleControl1);
            this.Controls.Add(this.btnDeleteSchedule);
            this.Controls.Add(this.btnAddSchedule);
            this.Name = "SchedulesEditForm";
            this.Text = "SchedulesEditForm";
            this.Load += new System.EventHandler(this.ScxhedulesEditForm_Load);
            this.Controls.SetChildIndex(this.btnAddSchedule, 0);
            this.Controls.SetChildIndex(this.btnDeleteSchedule, 0);
            this.Controls.SetChildIndex(this.scheduleControl1, 0);
            this.Controls.SetChildIndex(this.schedulesControl1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddSchedule;
        private ScheduleEditControl scheduleControl1;
        private WomenCalendar.Controls.SchedulesControl schedulesControl1;
        private System.Windows.Forms.Button btnDeleteSchedule;
    }
}