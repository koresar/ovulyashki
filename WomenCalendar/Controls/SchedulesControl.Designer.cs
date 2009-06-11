namespace WomenCalendar.Controls
{
    partial class SchedulesControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.gridSchedules = new System.Windows.Forms.DataGridView();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pause = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridSchedules)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 10007;
            this.label1.Text = "Расписание приёма";
            // 
            // gridSchedules
            // 
            this.gridSchedules.AllowUserToAddRows = false;
            this.gridSchedules.AllowUserToDeleteRows = false;
            this.gridSchedules.AllowUserToOrderColumns = true;
            this.gridSchedules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSchedules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Description,
            this.Start,
            this.Stop,
            this.Length,
            this.Pause});
            this.gridSchedules.Location = new System.Drawing.Point(0, 21);
            this.gridSchedules.MultiSelect = false;
            this.gridSchedules.Name = "gridSchedules";
            this.gridSchedules.ReadOnly = true;
            this.gridSchedules.RowHeadersVisible = false;
            this.gridSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSchedules.Size = new System.Drawing.Size(618, 111);
            this.gridSchedules.TabIndex = 10006;
            this.gridSchedules.SelectionChanged += new System.EventHandler(this.gridSchedules_SelectionChanged);
            // 
            // Description
            // 
            this.Description.FillWeight = 130F;
            this.Description.HeaderText = "Описание";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 130;
            // 
            // Start
            // 
            this.Start.FillWeight = 110F;
            this.Start.HeaderText = "Начало";
            this.Start.Name = "Start";
            this.Start.ReadOnly = true;
            this.Start.Width = 110;
            // 
            // Stop
            // 
            this.Stop.FillWeight = 110F;
            this.Stop.HeaderText = "Конец";
            this.Stop.Name = "Stop";
            this.Stop.ReadOnly = true;
            this.Stop.Width = 110;
            // 
            // Length
            // 
            this.Length.FillWeight = 110F;
            this.Length.HeaderText = "Дней делать";
            this.Length.Name = "Length";
            this.Length.ReadOnly = true;
            this.Length.Width = 110;
            // 
            // Pause
            // 
            this.Pause.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Pause.FillWeight = 120F;
            this.Pause.HeaderText = "Пауза перед повтором";
            this.Pause.Name = "Pause";
            this.Pause.ReadOnly = true;
            // 
            // SchedulesControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridSchedules);
            this.Name = "SchedulesControl";
            this.Size = new System.Drawing.Size(618, 132);
            ((System.ComponentModel.ISupportInitialize)(this.gridSchedules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridSchedules;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stop;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pause;
    }
}
