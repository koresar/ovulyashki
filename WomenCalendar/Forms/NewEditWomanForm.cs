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
    /// The form to create or edit the woman.
    /// </summary>
    public partial class NewEditWomanForm : ModalBaseForm, ITranslatable
    {
        /// <summary>
        /// Create the dialog.
        /// </summary>
        public NewEditWomanForm()
        {
            this.InitializeComponent();
            if (TEXT.Get != null)
            {
                this.ReReadTranslations();
            }

            this.txtName.Text = Environment.UserName;
        }

        /// <summary>
        /// Get or set a woman login/name.
        /// </summary>
        public string WomanName
        {
            get { return this.txtName.Text; }
            set { this.txtName.Text = value; }
        }

        /// <summary>
        /// The password of the file.
        /// </summary>
        public string WomanPassword
        {
            get { return this.txtPassword.Text; }
            set { this.txtPassword.Text = value; }
        }

        #region ITranslatable interface impementation

        /// <summary>
        /// Refresh the localizations strings.
        /// </summary>
        public new void ReReadTranslations()
        {
            base.ReReadTranslations();
            this.lblName.Text = TEXT.Get["What_is_your_name"];
            this.lblPassword.Text = TEXT.Get["Enter_your_pwd"];
            this.Text = TEXT.Get["Creating_new_woman"];
        }

        #endregion        
    }
}
