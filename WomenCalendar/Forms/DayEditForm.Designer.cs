namespace WomenCalendar
{
    partial class DayEditForm
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
            this.components = new System.ComponentModel.Container();
            this.lblBBT = new System.Windows.Forms.Label();
            this.ttButton = new System.Windows.Forms.ToolTip(this.components);
            this.btnPrevDay = new System.Windows.Forms.Button();
            this.btnNextDay = new System.Windows.Forms.Button();
            this.chkMentrustions = new System.Windows.Forms.CheckBox();
            this.mensesEditControl = new WomenCalendar.MensesEditControl();
            this.dayEditControl = new WomenCalendar.DayEditControl();
            this.SuspendLayout();
            // 
            // lblBBT
            // 
            this.lblBBT.AutoSize = true;
            this.lblBBT.Location = new System.Drawing.Point(10, 260);
            this.lblBBT.Name = "lblBBT";
            this.lblBBT.Size = new System.Drawing.Size(0, 13);
            this.lblBBT.TabIndex = 0;
            // 
            // ttButton
            // 
            this.ttButton.AutomaticDelay = 1;
            this.ttButton.AutoPopDelay = 5000;
            this.ttButton.InitialDelay = 1;
            this.ttButton.ReshowDelay = 1;
            this.ttButton.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttButton.ToolTipTitle = "On/Off menses button";
            this.ttButton.UseAnimation = false;
            this.ttButton.UseFading = false;
            // 
            // btnPrevDay
            // 
            this.btnPrevDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrevDay.Location = new System.Drawing.Point(5, 13);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(124, 23);
            this.btnPrevDay.TabIndex = 0;
            this.btnPrevDay.Text = "<< Previous day";
            this.btnPrevDay.UseVisualStyleBackColor = true;
            this.btnPrevDay.Click += new System.EventHandler(this.btnPrevDay_Click);
            // 
            // btnNextDay
            // 
            this.btnNextDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNextDay.Location = new System.Drawing.Point(128, 13);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(119, 23);
            this.btnNextDay.TabIndex = 1;
            this.btnNextDay.Text = "Next day >>";
            this.btnNextDay.UseVisualStyleBackColor = true;
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // chkMentrustions
            // 
            this.chkMentrustions.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkMentrustions.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chkMentrustions.Image = global::WomenCalendar.Properties.Resources.drop_Image;
            this.chkMentrustions.Location = new System.Drawing.Point(247, 126);
            this.chkMentrustions.Name = "chkMentrustions";
            this.chkMentrustions.Size = new System.Drawing.Size(27, 65);
            this.chkMentrustions.TabIndex = 10013;
            this.chkMentrustions.Text = ">>          >>";
            this.chkMentrustions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMentrustions.UseVisualStyleBackColor = true;
            this.chkMentrustions.CheckedChanged += new System.EventHandler(this.chkMentrustions_CheckedChanged);
            // 
            // mensesEditControl
            // 
            this.mensesEditControl.EgestaSliderValue = 4;
            this.mensesEditControl.Length = 5;
            this.mensesEditControl.Location = new System.Drawing.Point(275, 42);
            this.mensesEditControl.Name = "mensesEditControl";
            this.mensesEditControl.Size = new System.Drawing.Size(96, 249);
            this.mensesEditControl.TabIndex = 10014;
            // 
            // dayEditControl
            // 
            this.dayEditControl.BBT = "";
            this.dayEditControl.CurrentCF = WomenCalendar.CervicalFluid.Undefined;
            this.dayEditControl.HadSex = false;
            this.dayEditControl.Health = 5;
            this.dayEditControl.LastFocus = WomenCalendar.DayEditFocus.Note;
            this.dayEditControl.Location = new System.Drawing.Point(3, 42);
            this.dayEditControl.Name = "dayEditControl";
            this.dayEditControl.Note = "";
            this.dayEditControl.Size = new System.Drawing.Size(244, 333);
            this.dayEditControl.TabIndex = 10015;
            // 
            // DayEditForm
            // 
            this.AcceptButton = null;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(375, 409);
            this.Controls.Add(this.dayEditControl);
            this.Controls.Add(this.mensesEditControl);
            this.Controls.Add(this.chkMentrustions);
            this.Controls.Add(this.btnNextDay);
            this.Controls.Add(this.btnPrevDay);
            this.Controls.Add(this.lblBBT);
            this.Name = "DayEditForm";
            this.Text = "Edit this day";
            this.Load += new System.EventHandler(this.DayEditForm_Load);
            this.Shown += new System.EventHandler(this.DayEditForm_Shown);
            this.Controls.SetChildIndex(this.lblBBT, 0);
            this.Controls.SetChildIndex(this.btnPrevDay, 0);
            this.Controls.SetChildIndex(this.btnNextDay, 0);
            this.Controls.SetChildIndex(this.chkMentrustions, 0);
            this.Controls.SetChildIndex(this.mensesEditControl, 0);
            this.Controls.SetChildIndex(this.dayEditControl, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBBT;
        private System.Windows.Forms.ToolTip ttButton;
        private System.Windows.Forms.Button btnPrevDay;
        private System.Windows.Forms.Button btnNextDay;
        private System.Windows.Forms.CheckBox chkMentrustions;
        private WomenCalendar.MensesEditControl mensesEditControl;
        private DayEditControl dayEditControl;

    }
}