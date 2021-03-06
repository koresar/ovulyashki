﻿namespace WomenCalendar
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
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnThanks = new System.Windows.Forms.Button();
            this.txtThanks = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSite
            // 
            this.btnSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSite.Location = new System.Drawing.Point(13, 121);
            this.btnSite.Name = "btnSite";
            this.btnSite.Size = new System.Drawing.Size(267, 23);
            this.btnSite.TabIndex = 2;
            this.btnSite.Text = "Visit Ovulyashki site";
            this.btnSite.UseVisualStyleBackColor = true;
            this.btnSite.Click += new System.EventHandler(this.Site_Click);
            // 
            // btnFeedback
            // 
            this.btnFeedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFeedback.Location = new System.Drawing.Point(13, 151);
            this.btnFeedback.Name = "btnFeedback";
            this.btnFeedback.Size = new System.Drawing.Size(267, 23);
            this.btnFeedback.TabIndex = 3;
            this.btnFeedback.Text = "Leave some thanks words";
            this.btnFeedback.UseVisualStyleBackColor = true;
            this.btnFeedback.Click += new System.EventHandler(this.Feedback_Click);
            // 
            // btnBug
            // 
            this.btnBug.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBug.Location = new System.Drawing.Point(13, 181);
            this.btnBug.Name = "btnBug";
            this.btnBug.Size = new System.Drawing.Size(267, 23);
            this.btnBug.TabIndex = 4;
            this.btnBug.Text = "Tell us about a bug";
            this.btnBug.UseVisualStyleBackColor = true;
            this.btnBug.Click += new System.EventHandler(this.Bug_Click);
            // 
            // btnNewFeature
            // 
            this.btnNewFeature.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewFeature.Location = new System.Drawing.Point(13, 211);
            this.btnNewFeature.Name = "btnNewFeature";
            this.btnNewFeature.Size = new System.Drawing.Size(267, 23);
            this.btnNewFeature.TabIndex = 5;
            this.btnNewFeature.Text = "Request a feature";
            this.btnNewFeature.UseVisualStyleBackColor = true;
            this.btnNewFeature.Click += new System.EventHandler(this.NewFeature_Click);
            // 
            // btnAskQuestion
            // 
            this.btnAskQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAskQuestion.Location = new System.Drawing.Point(13, 241);
            this.btnAskQuestion.Name = "btnAskQuestion";
            this.btnAskQuestion.Size = new System.Drawing.Size(267, 23);
            this.btnAskQuestion.TabIndex = 6;
            this.btnAskQuestion.Text = "Ask us question";
            this.btnAskQuestion.UseVisualStyleBackColor = true;
            this.btnAskQuestion.Click += new System.EventHandler(this.AskQuestion_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(13, 67);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(85, 13);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "Ovulyashki X.X.X";
            // 
            // btnThanks
            // 
            this.btnThanks.Location = new System.Drawing.Point(13, 92);
            this.btnThanks.Name = "btnThanks";
            this.btnThanks.Size = new System.Drawing.Size(267, 23);
            this.btnThanks.TabIndex = 1;
            this.btnThanks.Text = "Thanks to...";
            this.btnThanks.UseVisualStyleBackColor = true;
            this.btnThanks.Click += new System.EventHandler(this.Thanks_Click);
            // 
            // txtThanks
            // 
            this.txtThanks.Location = new System.Drawing.Point(301, 67);
            this.txtThanks.Multiline = true;
            this.txtThanks.Name = "txtThanks";
            this.txtThanks.ReadOnly = true;
            this.txtThanks.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtThanks.Size = new System.Drawing.Size(321, 196);
            this.txtThanks.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WomenCalendar.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(267, 44);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 276);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtThanks);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnAskQuestion);
            this.Controls.Add(this.btnNewFeature);
            this.Controls.Add(this.btnBug);
            this.Controls.Add(this.btnFeedback);
            this.Controls.Add(this.btnThanks);
            this.Controls.Add(this.btnSite);
            this.Name = "AboutForm";
            this.Text = "About application";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSite;
        private System.Windows.Forms.Button btnFeedback;
        private System.Windows.Forms.Button btnBug;
        private System.Windows.Forms.Button btnNewFeature;
        private System.Windows.Forms.Button btnAskQuestion;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnThanks;
        private System.Windows.Forms.TextBox txtThanks;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}