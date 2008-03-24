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
            this.sliderEgestaAmount = new MB.Controls.ColorSlider();
            this.txtBBT = new System.Windows.Forms.TextBox();
            this.lblBBT = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnPrevDay = new System.Windows.Forms.Button();
            this.btnNextDay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sliderEgestaAmount
            // 
            this.sliderEgestaAmount.BackColor = System.Drawing.Color.Transparent;
            this.sliderEgestaAmount.BarInnerColor = System.Drawing.Color.Pink;
            this.sliderEgestaAmount.BarOuterColor = System.Drawing.Color.DeepPink;
            this.sliderEgestaAmount.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderEgestaAmount.ElapsedInnerColor = System.Drawing.Color.Pink;
            this.sliderEgestaAmount.ElapsedOuterColor = System.Drawing.Color.DeepPink;
            this.sliderEgestaAmount.LargeChange = ((uint)(5u));
            this.sliderEgestaAmount.Location = new System.Drawing.Point(250, 38);
            this.sliderEgestaAmount.Maximum = 4;
            this.sliderEgestaAmount.Name = "sliderEgestaAmount";
            this.sliderEgestaAmount.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderEgestaAmount.Size = new System.Drawing.Size(30, 200);
            this.sliderEgestaAmount.SmallChange = ((uint)(1u));
            this.sliderEgestaAmount.TabIndex = 2;
            this.sliderEgestaAmount.Text = "colorSlider1";
            this.sliderEgestaAmount.ThumbInnerColor = System.Drawing.Color.White;
            this.sliderEgestaAmount.ThumbOuterColor = System.Drawing.Color.Pink;
            this.sliderEgestaAmount.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderEgestaAmount.Value = 4;
            this.sliderEgestaAmount.MouseLeave += new System.EventHandler(this.sliderEgestaAmount_MouseLeave);
            this.sliderEgestaAmount.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sliderEgestaAmount_Scroll);
            this.sliderEgestaAmount.MouseEnter += new System.EventHandler(this.sliderEgestaAmount_MouseEnter);
            // 
            // txtBBT
            // 
            this.txtBBT.CausesValidation = false;
            this.txtBBT.Location = new System.Drawing.Point(172, 42);
            this.txtBBT.MaxLength = 5;
            this.txtBBT.Name = "txtBBT";
            this.txtBBT.Size = new System.Drawing.Size(48, 20);
            this.txtBBT.TabIndex = 1;
            this.txtBBT.Leave += new System.EventHandler(this.txtBBT_Leave);
            // 
            // lblBBT
            // 
            this.lblBBT.AutoSize = true;
            this.lblBBT.Location = new System.Drawing.Point(10, 45);
            this.lblBBT.Name = "lblBBT";
            this.lblBBT.Size = new System.Drawing.Size(159, 13);
            this.lblBBT.TabIndex = 0;
            this.lblBBT.Text = "Базальная температура тела:";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(13, 88);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(207, 150);
            this.txtNote.TabIndex = 10000;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(9, 69);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(119, 13);
            this.lblNote.TabIndex = 10001;
            this.lblNote.Text = "Заметка к этому дню:";
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 0;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.UseAnimation = false;
            this.toolTip.UseFading = false;
            // 
            // btnPrevDay
            // 
            this.btnPrevDay.Location = new System.Drawing.Point(16, 13);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(124, 23);
            this.btnPrevDay.TabIndex = 10002;
            this.btnPrevDay.Text = "<< Предыдущий день";
            this.btnPrevDay.UseVisualStyleBackColor = true;
            this.btnPrevDay.Click += new System.EventHandler(this.btnPrevDay_Click);
            // 
            // btnNextDay
            // 
            this.btnNextDay.Location = new System.Drawing.Point(152, 13);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(124, 23);
            this.btnNextDay.TabIndex = 10003;
            this.btnNextDay.Text = "Следующий день >>";
            this.btnNextDay.UseVisualStyleBackColor = true;
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // DayEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 284);
            this.Controls.Add(this.btnNextDay);
            this.Controls.Add(this.btnPrevDay);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.lblBBT);
            this.Controls.Add(this.txtBBT);
            this.Controls.Add(this.sliderEgestaAmount);
            this.Name = "DayEditForm";
            this.Text = "Изменить день";
            this.Load += new System.EventHandler(this.DayEditForm_Load);
            this.Controls.SetChildIndex(this.sliderEgestaAmount, 0);
            this.Controls.SetChildIndex(this.txtBBT, 0);
            this.Controls.SetChildIndex(this.lblBBT, 0);
            this.Controls.SetChildIndex(this.txtNote, 0);
            this.Controls.SetChildIndex(this.lblNote, 0);
            this.Controls.SetChildIndex(this.btnPrevDay, 0);
            this.Controls.SetChildIndex(this.btnNextDay, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MB.Controls.ColorSlider sliderEgestaAmount;
        private System.Windows.Forms.TextBox txtBBT;
        private System.Windows.Forms.Label lblBBT;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnPrevDay;
        private System.Windows.Forms.Button btnNextDay;

    }
}