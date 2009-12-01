using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar.Forms
{
    public partial class ErrorForm : Form
    {
        public ErrorForm()
        {
            InitializeComponent();
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            this.txtError.SelectAll();
            this.txtError.Copy();
            this.txtError.DeselectAll();
        }

        public static void Show(Exception ex)
        {
            var form = new ErrorForm();
            form.txtError.Text = ex.Message + Environment.NewLine + ex.StackTrace;
            form.ShowDialog();
        }
    }
}
