using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WomenCalendar
{
    public partial class AboutForm : BaseForm
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void btnBug_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://sourceforge.net/tracker/?func=add&group_id=219686&atid=1048403");
        }

        private void btnNewFeature_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://sourceforge.net/tracker/?func=add&group_id=219686&atid=1048406");
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://sourceforge.net/forum/forum.php?forum_id=791627");
        }

        private void btnAskQuestion_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://sourceforge.net/forum/forum.php?forum_id=791628");
        }

        private void btnSite_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://sourceforge.net/projects/ovulyashki/");
        }
    }
}
