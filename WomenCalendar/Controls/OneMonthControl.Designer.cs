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
            this.components = new System.ComponentModel.Container();
            this.btnDropDown = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnDropDown
            // 
            this.btnDropDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDropDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDropDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDropDown.FlatAppearance.BorderSize = 0;
            this.btnDropDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDropDown.ForeColor = System.Drawing.Color.Black;
            this.btnDropDown.Image = global::WomenCalendar.Properties.Resources.month_dropdown;
            this.btnDropDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDropDown.Location = new System.Drawing.Point(172, 3);
            this.btnDropDown.Margin = new System.Windows.Forms.Padding(0);
            this.btnDropDown.Name = "btnDropDown";
            this.btnDropDown.Size = new System.Drawing.Size(55, 16);
            this.btnDropDown.TabIndex = 0;
            this.btnDropDown.TabStop = false;
            this.btnDropDown.Text = "Graphs";
            this.btnDropDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDropDown.UseVisualStyleBackColor = false;
            this.btnDropDown.MouseLeave += new System.EventHandler(this.btnDropDown_MouseLeave);
            this.btnDropDown.Click += new System.EventHandler(this.btnDropDown_Click);
            this.btnDropDown.MouseEnter += new System.EventHandler(this.btnDropDown_MouseEnter);
            // 
            // OneMonthControl
            // 
            this.BackColor = System.Drawing.Color.White;
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
        private System.Windows.Forms.ToolTip toolTip;

    }
}
