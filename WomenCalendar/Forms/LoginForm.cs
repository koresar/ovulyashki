using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    /// <summary>
    /// The dialog to enter woman credentials.
    /// </summary>
    public partial class LoginForm : ModalBaseForm, ITranslatable
    {
        /// <summary>
        /// The dialog default constructor.
        /// </summary>
        public LoginForm()
        {
            this.InitializeComponent();
            if (TEXT.Get != null)
            {
                this.ReReadTranslations();
            }
        }

        /// <summary>
        /// The text entered by user.
        /// </summary>
        public string Password
        {
            get
            {
                return this.txtPassword.Text;
            }
        }

        #region ITranslatable interface impementation

        /// <summary>
        /// Refresh the localizations strings.
        /// </summary>
        public new void ReReadTranslations()
        {
            this.lblPass.Text = TEXT.Get["Please_enter_password"];
            this.Text = TEXT.Get["Please_enter_password"];
        }

        #endregion

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            this.txtPassword.Focus();
        }
    }
}
