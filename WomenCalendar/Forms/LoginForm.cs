using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class LoginForm : ModalBaseForm, ITranslatable
    {
        public LoginForm()
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();
        }

        #region ITranslatable interface impementation

        public new void ReReadTranslations()
        {
            this.lblPass.Text = TEXT.Get["Please_enter_password"];
            this.Text = TEXT.Get["Please_enter_password"];
        }

        #endregion

        public string Password
        {
            get
            {
                return txtPassword.Text;
            }
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
    }
}
