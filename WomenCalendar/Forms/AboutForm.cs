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
        private const int minWidth = 300;
        private const int maxWidth = 640;

        public AboutForm()
        {
            InitializeComponent();

            this.Width = minWidth;
            string s = "Спасибо Michal Brylka за ColorSlider: http://www.codeproject.com/KB/selection/ColorSlider.aspx" + Environment.NewLine + Environment.NewLine +
                "Спасибо randz за VerticalLabel: http://www.codeproject.com/KB/miscctrl/Vertical_Label_Control.aspx" + Environment.NewLine + Environment.NewLine +
                "Спасибо BobK, Darren Martz, John Champion, Jerry Vos, Chris Champoin, Brian Chappell, Ronan O Sullivan, Benjamin Mayrargue за ZegGraph: https://sourceforge.net/projects/zedgraph/ , http://zedgraph.org" + Environment.NewLine + Environment.NewLine +
                "Спасибо mkg за XPanderControl: http://www.codeproject.com/KB/cpp/XPander.aspx" + Environment.NewLine + Environment.NewLine + 
                "Спасибо Carlos Aguilar Mares за ExcelXmlWriter: http://www.carlosag.net/Tools/ExcelXmlWriter/"  + Environment.NewLine + Environment.NewLine + 
                "Спасибо Mike Krueger, John Reilly за SharpZipLib: http://icsharpcode.net/" + Environment.NewLine + Environment.NewLine
                ;
            txtThanks.Text = s;
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

        private void btnThanks_Click(object sender, EventArgs e)
        {
            this.Width = this.Width == maxWidth ? minWidth : maxWidth;
        }
    }
}
