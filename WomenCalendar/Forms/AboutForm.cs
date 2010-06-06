using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    /// <summary>
    /// Shows information about the application.
    /// </summary>
    public partial class AboutForm : BaseForm, ITranslatable
    {
        private const int MinWidth = 300;
        private const int MaxWidth = 640;

        /// <summary>
        /// The default window constructor.
        /// </summary>
        public AboutForm()
        {
            this.InitializeComponent();
            if (TEXT.Get != null)
            {
                this.ReReadTranslations();
            }

            this.Width = MinWidth;
            this.txtThanks.Text = TEXT.Get["Thanks_to_components"];
            int verLen = 4;
            var ver = Assembly.GetEntryAssembly().GetName().Version;
            var verText = ver.ToString(verLen);
            while (verText.EndsWith(".0"))
            {
                verText = ver.ToString(--verLen);
            }

            this.lblVersion.Text = TEXT.Get["Ovulyashki"] + " " + verText;
        }

        #region ITranslatable interface impementation

        /// <summary>
        /// Refresh localizations strings.
        /// </summary>
        public void ReReadTranslations()
        {
            this.btnSite.Text = TEXT.Get["Visit_site"];
            this.btnFeedback.Text = TEXT.Get["Leave_thanks"];
            this.btnBug.Text = TEXT.Get["Tell_about_bug"];
            this.btnNewFeature.Text = TEXT.Get["Ask_feature_request"];
            this.btnAskQuestion.Text = TEXT.Get["Ask_question"];
            this.btnThanks.Text = TEXT.Get["Thanks_to"];
            this.Text = TEXT.Get["About_application"];
        }

        #endregion

        private void Bug_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://ovulyashki.dp.ua/bugtrack/bugs/add/");
        }

        private void NewFeature_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://ovulyashki.dp.ua/bugtrack/features/add/");
        }

        private void Feedback_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://ovulyashki.dp.ua/feedback/");
        }

        private void AskQuestion_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://ovulyashki.dp.ua/questions/");
        }

        private void Site_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://ovulyashki.dp.ua");
        }

        private void Thanks_Click(object sender, EventArgs e)
        {
            this.Width = this.Width == MaxWidth ? MinWidth : MaxWidth;
        }
    }
}
