namespace WomenCalendar.Controls
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
            this.coloredSchedulerCalendarControl1 = new WomenCalendar.Controls.ColoredSchedulerCalendarControl();
            this.SuspendLayout();
            // 
            // coloredSchedulerCalendarControl1
            // 
            this.coloredSchedulerCalendarControl1.BackColor = System.Drawing.Color.White;
            this.coloredSchedulerCalendarControl1.Location = new System.Drawing.Point(1, 232);
            this.coloredSchedulerCalendarControl1.Name = "coloredSchedulerCalendarControl1";
            this.coloredSchedulerCalendarControl1.Size = new System.Drawing.Size(337, 135);
            this.coloredSchedulerCalendarControl1.StartMonth = new System.DateTime(((long)(0)));
            this.coloredSchedulerCalendarControl1.TabIndex = 10017;
            // 
            // EditScheduleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.coloredSchedulerCalendarControl1);
            this.Name = "EditScheduleControl";
            this.Size = new System.Drawing.Size(406, 379);
            this.ResumeLayout(false);

        }

        #endregion

        private ColoredSchedulerCalendarControl coloredSchedulerCalendarControl1;
    }
}
