namespace WomenCalendar
{
    partial class DayCellControl
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
            this.lblHadSex = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHadSex
            // 
            this.lblHadSex.BackColor = System.Drawing.Color.Transparent;
            this.lblHadSex.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblHadSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHadSex.ForeColor = System.Drawing.Color.Red;
            this.lblHadSex.Location = new System.Drawing.Point(19, 19);
            this.lblHadSex.Name = "lblHadSex";
            this.lblHadSex.Size = new System.Drawing.Size(13, 11);
            this.lblHadSex.TabIndex = 6;
            this.lblHadSex.Text = "♥";
            this.lblHadSex.Visible = false;
            // 
            // DayCellControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Controls.Add(this.lblHadSex);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "DayCellControl";
            this.Size = new System.Drawing.Size(32, 32);
            this.MouseEnter += new System.EventHandler(this.DayCellControl_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHadSex;
    }
}
