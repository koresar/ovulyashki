namespace WomenCalendar
{
    partial class AboutForm
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
            this.btnSite = new System.Windows.Forms.Button();
            this.btnFeedback = new System.Windows.Forms.Button();
            this.btnBug = new System.Windows.Forms.Button();
            this.btnNewFeature = new System.Windows.Forms.Button();
            this.btnAskQuestion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSite
            // 
            this.btnSite.Location = new System.Drawing.Point(13, 13);
            this.btnSite.Name = "btnSite";
            this.btnSite.Size = new System.Drawing.Size(267, 23);
            this.btnSite.TabIndex = 0;
            this.btnSite.Text = "Посетить сайт Овуляшек";
            this.btnSite.UseVisualStyleBackColor = true;
            // 
            // btnFeedback
            // 
            this.btnFeedback.Location = new System.Drawing.Point(13, 43);
            this.btnFeedback.Name = "btnFeedback";
            this.btnFeedback.Size = new System.Drawing.Size(267, 23);
            this.btnFeedback.TabIndex = 1;
            this.btnFeedback.Text = "Написать слова благодарности автору";
            this.btnFeedback.UseVisualStyleBackColor = true;
            // 
            // btnBug
            // 
            this.btnBug.Location = new System.Drawing.Point(13, 73);
            this.btnBug.Name = "btnBug";
            this.btnBug.Size = new System.Drawing.Size(267, 23);
            this.btnBug.TabIndex = 2;
            this.btnBug.Text = "Рассказать об ошибке в программе";
            this.btnBug.UseVisualStyleBackColor = true;
            // 
            // btnNewFeature
            // 
            this.btnNewFeature.Location = new System.Drawing.Point(13, 103);
            this.btnNewFeature.Name = "btnNewFeature";
            this.btnNewFeature.Size = new System.Drawing.Size(267, 23);
            this.btnNewFeature.TabIndex = 3;
            this.btnNewFeature.Text = "Попросить автора добавить новую фишку";
            this.btnNewFeature.UseVisualStyleBackColor = true;
            // 
            // btnAskQuestion
            // 
            this.btnAskQuestion.Location = new System.Drawing.Point(13, 133);
            this.btnAskQuestion.Name = "btnAskQuestion";
            this.btnAskQuestion.Size = new System.Drawing.Size(267, 23);
            this.btnAskQuestion.TabIndex = 4;
            this.btnAskQuestion.Text = "Задать вопрос";
            this.btnAskQuestion.UseVisualStyleBackColor = true;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 170);
            this.Controls.Add(this.btnAskQuestion);
            this.Controls.Add(this.btnNewFeature);
            this.Controls.Add(this.btnBug);
            this.Controls.Add(this.btnFeedback);
            this.Controls.Add(this.btnSite);
            this.Name = "AboutForm";
            this.Text = "Всяко разно о программе";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSite;
        private System.Windows.Forms.Button btnFeedback;
        private System.Windows.Forms.Button btnBug;
        private System.Windows.Forms.Button btnNewFeature;
        private System.Windows.Forms.Button btnAskQuestion;
    }
}