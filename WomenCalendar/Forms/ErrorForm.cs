using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar.Forms
{
    public partial class ErrorForm : Form, ITranslatable
    {
        public ErrorForm()
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();
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
            form.txtError.Text = ex.Message + Environment.NewLine + ex.StackTrace +
                Environment.NewLine + (ex.InnerException == null ? "" : ex.InnerException.Message);
            form.ShowDialog();
        }

        #region ITranslatable interface impementation

        public void ReReadTranslations()
        {
            try
            {
                this.btnCopyToClipboard.Text = TEXT.Get["Copy_to_clipboard"];
                this.Text = TEXT.Get["FFFUUUUUUUUUUUUUU"];
            }
            catch
            { }
        }

        #endregion
    }
}
