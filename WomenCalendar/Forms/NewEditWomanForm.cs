using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class NewEditWomanForm : ModalBaseForm, ITranslatable
    {
        public NewEditWomanForm()
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();
            txtName.Text = Environment.UserName;
        }


        #region ITranslatable interface impementation

        public new void ReReadTranslations()
        {
            base.ReReadTranslations();
            this.lblName.Text = TEXT.Get["What_is_your_name"];
            this.lblPassword.Text = TEXT.Get["Enter_your_pwd"];
            this.Text = TEXT.Get["Creating_new_woman"];
        }

        #endregion
        
        public string WomanName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public string WomanPassword
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }
    }
}
