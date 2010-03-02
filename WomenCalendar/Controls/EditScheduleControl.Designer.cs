﻿namespace WomenCalendar.Controls
{
    partial class EditScheduleControl
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
            this.cmbScheduleType = new System.Windows.Forms.ComboBox();
            this.btnApplySchedule = new System.Windows.Forms.Button();
            this.btnAddSchedule = new System.Windows.Forms.Button();
            this.txtScheduleText = new System.Windows.Forms.TextBox();
            this.lblScheduleText = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.schedulesListControl1 = new WomenCalendar.Controls.SchedulesListControl();
            this.coloredSchedulerCalendarControl1 = new WomenCalendar.Controls.ColoredSchedulerCalendarControl();
            this.SuspendLayout();
            // 
            // cmbScheduleType
            // 
            this.cmbScheduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScheduleType.FormattingEnabled = true;
            this.cmbScheduleType.Location = new System.Drawing.Point(3, 152);
            this.cmbScheduleType.Name = "cmbScheduleType";
            this.cmbScheduleType.Size = new System.Drawing.Size(227, 21);
            this.cmbScheduleType.TabIndex = 10020;
            // 
            // btnApplySchedule
            // 
            this.btnApplySchedule.Location = new System.Drawing.Point(236, 151);
            this.btnApplySchedule.Name = "btnApplySchedule";
            this.btnApplySchedule.Size = new System.Drawing.Size(53, 23);
            this.btnApplySchedule.TabIndex = 10021;
            this.btnApplySchedule.Text = "Apply";
            this.btnApplySchedule.UseVisualStyleBackColor = true;
            this.btnApplySchedule.Visible = false;
            this.btnApplySchedule.Click += new System.EventHandler(this.btnApplySchedule_Click);
            // 
            // btnAddSchedule
            // 
            this.btnAddSchedule.Enabled = false;
            this.btnAddSchedule.Location = new System.Drawing.Point(15, 205);
            this.btnAddSchedule.Name = "btnAddSchedule";
            this.btnAddSchedule.Size = new System.Drawing.Size(323, 23);
            this.btnAddSchedule.TabIndex = 10022;
            this.btnAddSchedule.Text = "Add one more schedule";
            this.btnAddSchedule.UseVisualStyleBackColor = true;
            this.btnAddSchedule.Click += new System.EventHandler(this.btnAddSchedule_Click);
            // 
            // txtScheduleText
            // 
            this.txtScheduleText.Location = new System.Drawing.Point(133, 179);
            this.txtScheduleText.Name = "txtScheduleText";
            this.txtScheduleText.Size = new System.Drawing.Size(205, 20);
            this.txtScheduleText.TabIndex = 10023;
            this.txtScheduleText.TextChanged += new System.EventHandler(this.txtScheduleText_TextChanged);
            // 
            // lblScheduleText
            // 
            this.lblScheduleText.Location = new System.Drawing.Point(3, 179);
            this.lblScheduleText.Name = "lblScheduleText";
            this.lblScheduleText.Size = new System.Drawing.Size(124, 20);
            this.lblScheduleText.TabIndex = 10024;
            this.lblScheduleText.Text = "New schedule name";
            this.lblScheduleText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(295, 151);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(53, 23);
            this.btnCancel.TabIndex = 10021;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // schedulesListControl1
            // 
            this.schedulesListControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.schedulesListControl1.Location = new System.Drawing.Point(0, 0);
            this.schedulesListControl1.Name = "schedulesListControl1";
            this.schedulesListControl1.Size = new System.Drawing.Size(353, 151);
            this.schedulesListControl1.TabIndex = 10019;
            // 
            // coloredSchedulerCalendarControl1
            // 
            this.coloredSchedulerCalendarControl1.BackColor = System.Drawing.Color.Transparent;
            this.coloredSchedulerCalendarControl1.Location = new System.Drawing.Point(1, 243);
            this.coloredSchedulerCalendarControl1.Name = "coloredSchedulerCalendarControl1";
            this.coloredSchedulerCalendarControl1.Size = new System.Drawing.Size(337, 135);
            this.coloredSchedulerCalendarControl1.StartMonth = new System.DateTime(((long)(0)));
            this.coloredSchedulerCalendarControl1.TabIndex = 10017;
            // 
            // EditScheduleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblScheduleText);
            this.Controls.Add(this.txtScheduleText);
            this.Controls.Add(this.btnAddSchedule);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApplySchedule);
            this.Controls.Add(this.cmbScheduleType);
            this.Controls.Add(this.schedulesListControl1);
            this.Controls.Add(this.coloredSchedulerCalendarControl1);
            this.Name = "EditScheduleControl";
            this.Size = new System.Drawing.Size(353, 379);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColoredSchedulerCalendarControl coloredSchedulerCalendarControl1;
        private SchedulesListControl schedulesListControl1;
        private System.Windows.Forms.ComboBox cmbScheduleType;
        private System.Windows.Forms.Button btnApplySchedule;
        private System.Windows.Forms.Button btnAddSchedule;
        private System.Windows.Forms.TextBox txtScheduleText;
        private System.Windows.Forms.Label lblScheduleText;
        private System.Windows.Forms.Button btnCancel;
    }
}
