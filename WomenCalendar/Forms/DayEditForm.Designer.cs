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
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.editScheduleControl = new WomenCalendar.Controls.EditScheduleControl();
            this.chkSchedules = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pnlOkCancel = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlRotation = new System.Windows.Forms.Panel();
            this.flowLayoutPanel.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.pnlOkCancel.SuspendLayout();
            this.pnlRotation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBBT
            // 
            this.lblBBT.AutoSize = true;
            this.lblBBT.Location = new System.Drawing.Point(13, 260);
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
            this.btnPrevDay.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrevDay.Location = new System.Drawing.Point(0, 0);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(142, 26);
            this.btnPrevDay.TabIndex = 0;
            this.btnPrevDay.Text = "<< Previous day";
            this.btnPrevDay.UseVisualStyleBackColor = true;
            this.btnPrevDay.Click += new System.EventHandler(this.btnPrevDay_Click);
            // 
            // btnNextDay
            // 
            this.btnNextDay.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextDay.Location = new System.Drawing.Point(146, 0);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(142, 26);
            this.btnNextDay.TabIndex = 1;
            this.btnNextDay.Text = "Next day >>";
            this.btnNextDay.UseVisualStyleBackColor = true;
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // chkMentrustions
            // 
            this.chkMentrustions.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkMentrustions.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chkMentrustions.Checked = true;
            this.chkMentrustions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMentrustions.Image = global::WomenCalendar.Properties.Resources.drop_Image;
            this.chkMentrustions.Location = new System.Drawing.Point(633, 100);
            this.chkMentrustions.Margin = new System.Windows.Forms.Padding(3, 100, 3, 0);
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
            this.mensesEditControl.Location = new System.Drawing.Point(663, 0);
            this.mensesEditControl.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
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
            this.dayEditControl.Location = new System.Drawing.Point(386, 0);
            this.dayEditControl.Margin = new System.Windows.Forms.Padding(0);
            this.dayEditControl.Name = "dayEditControl";
            this.dayEditControl.Note = "";
            this.dayEditControl.Size = new System.Drawing.Size(244, 330);
            this.dayEditControl.TabIndex = 10015;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel.Controls.Add(this.editScheduleControl);
            this.flowLayoutPanel.Controls.Add(this.chkSchedules);
            this.flowLayoutPanel.Controls.Add(this.dayEditControl);
            this.flowLayoutPanel.Controls.Add(this.chkMentrustions);
            this.flowLayoutPanel.Controls.Add(this.mensesEditControl);
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 32);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(762, 379);
            this.flowLayoutPanel.TabIndex = 10016;
            // 
            // editScheduleControl
            // 
            this.editScheduleControl.InitialDate = new System.DateTime(((long)(0)));
            this.editScheduleControl.Location = new System.Drawing.Point(0, 0);
            this.editScheduleControl.Margin = new System.Windows.Forms.Padding(0);
            this.editScheduleControl.Name = "editScheduleControl";
            this.editScheduleControl.Size = new System.Drawing.Size(353, 379);
            this.editScheduleControl.TabIndex = 10016;
            // 
            // chkSchedules
            // 
            this.chkSchedules.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSchedules.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chkSchedules.Checked = true;
            this.chkSchedules.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSchedules.Image = global::WomenCalendar.Properties.Resources.drop_Image;
            this.chkSchedules.Location = new System.Drawing.Point(356, 100);
            this.chkSchedules.Margin = new System.Windows.Forms.Padding(3, 100, 3, 0);
            this.chkSchedules.Name = "chkSchedules";
            this.chkSchedules.Size = new System.Drawing.Size(27, 65);
            this.chkSchedules.TabIndex = 10013;
            this.chkSchedules.Text = ">>          >>";
            this.chkSchedules.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSchedules.UseVisualStyleBackColor = true;
            this.chkSchedules.Visible = false;
            this.chkSchedules.CheckedChanged += new System.EventHandler(this.chkSchedules_CheckedChanged);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.pnlOkCancel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.pnlRotation, 0, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(762, 443);
            this.tableLayoutPanel.TabIndex = 10017;
            // 
            // pnlOkCancel
            // 
            this.pnlOkCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pnlOkCancel.Controls.Add(this.btnCancel);
            this.pnlOkCancel.Controls.Add(this.btnOK);
            this.pnlOkCancel.Location = new System.Drawing.Point(259, 414);
            this.pnlOkCancel.Name = "pnlOkCancel";
            this.pnlOkCancel.Size = new System.Drawing.Size(244, 26);
            this.pnlOkCancel.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(126, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(118, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOK.Location = new System.Drawing.Point(0, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(118, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlRotation
            // 
            this.pnlRotation.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlRotation.Controls.Add(this.btnPrevDay);
            this.pnlRotation.Controls.Add(this.btnNextDay);
            this.pnlRotation.Location = new System.Drawing.Point(237, 3);
            this.pnlRotation.Name = "pnlRotation";
            this.pnlRotation.Size = new System.Drawing.Size(288, 26);
            this.pnlRotation.TabIndex = 2;
            // 
            // DayEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(762, 444);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.lblBBT);
            this.Name = "DayEditForm";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Text = "Edit this day";
            this.Load += new System.EventHandler(this.DayEditForm_Load);
            this.Shown += new System.EventHandler(this.DayEditForm_Shown);
            this.flowLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.pnlOkCancel.ResumeLayout(false);
            this.pnlRotation.ResumeLayout(false);
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel pnlRotation;
        private System.Windows.Forms.Panel pnlOkCancel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkSchedules;
        private WomenCalendar.Controls.EditScheduleControl editScheduleControl;

    }
}