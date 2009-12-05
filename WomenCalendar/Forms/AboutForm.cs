using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace WomenCalendar
{
    public partial class AboutForm : BaseForm, ITranslatable
    {
        private const int minWidth = 300;
        private const int maxWidth = 640;

        public AboutForm()
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();

            this.Width = minWidth;
            txtThanks.Text = TEXT.Get["Thanks_to_components"];
            int verLen = 4;
            var ver = Assembly.GetEntryAssembly().GetName().Version;
            var verText = ver.ToString(verLen);
            while (verText.EndsWith(".0")) verText = ver.ToString(--verLen);
            lblVersion.Text = TEXT.Get["Ovulyashki"] + " " + verText;
        }

        #region ITranslatable interface impementation

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

        private void btnBug_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://ovulyashki.dp.ua/bugtrack/bugs/add/");
        }

        private void btnNewFeature_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://ovulyashki.dp.ua/bugtrack/features/add/");
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://ovulyashki.dp.ua/feedback/");
        }

        private void btnAskQuestion_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://ovulyashki.dp.ua/questions/");
        }

        private void btnSite_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://ovulyashki.dp.ua");
        }

        private void btnThanks_Click(object sender, EventArgs e)
        {
            this.Width = this.Width == maxWidth ? minWidth : maxWidth;
        }
    }
}
