using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar.Forms
{
    /// <summary>
    /// Shows exception - the critical error.
    /// </summary>
    public partial class ErrorForm : Form, ITranslatable
    {
        /// <summary>
        /// DEfault window constructor.
        /// </summary>
        public ErrorForm()
        {
            this.InitializeComponent();
            if (TEXT.Get != null)
            {
                this.ReReadTranslations();
            }
        }

        /// <summary>
        /// The function to use by the code.
        /// </summary>
        /// <param name="ex">The exception to show.</param>
        public static void Show(Exception ex)
        {
            var form = new ErrorForm();
            form.txtError.Text = Assembly.GetEntryAssembly().GetName().Version.ToString() + Environment.NewLine + 
                ex.Message + Environment.NewLine + 
                ex.StackTrace + Environment.NewLine + 
                (ex.InnerException == null ? string.Empty : ex.InnerException.Message);
            form.ShowDialog();
        }

        #region ITranslatable interface impementation

        /// <summary>
        /// Refresh localizations strings.
        /// </summary>
        public void ReReadTranslations()
        {
            try
            {
                this.btnCopyToClipboard.Text = TEXT.Get["Copy_to_clipboard"];
                this.Text = TEXT.Get["FFFUUUUUUUUUUUUUU"];
            }
            catch
            {
                // The window may be shown in case translations are unavailable, thus the code won't work and throw exception.
                // We ignore exception and show default (English) non translated text.
            }
        }

        #endregion

        private void CopyToClipboard_Click(object sender, EventArgs e)
        {
            this.txtError.SelectAll();
            this.txtError.Copy();
            this.txtError.DeselectAll();
        }
    }
}
