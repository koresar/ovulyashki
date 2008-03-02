namespace WomenCalendar
{
    partial class OneMonthControl
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
            this.btnDropDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDropDown
            // 
            this.btnDropDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDropDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDropDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDropDown.FlatAppearance.BorderSize = 0;
            this.btnDropDown.Image = global::WomenCalendar.Properties.Resources.month_dropdown;
            this.btnDropDown.Location = new System.Drawing.Point(211, 3);
            this.btnDropDown.Name = "btnDropDown";
            this.btnDropDown.Size = new System.Drawing.Size(16, 16);
            this.btnDropDown.TabIndex = 0;
            this.btnDropDown.TabStop = false;
            this.btnDropDown.UseVisualStyleBackColor = false;
            this.btnDropDown.Click += new System.EventHandler(this.btnDropDown_Click);
            // 
            // OneMonthControl
            // 
            this.Controls.Add(this.btnDropDown);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "OneMonthControl";
            this.Size = new System.Drawing.Size(230, 230);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDropDown;

    }
}
