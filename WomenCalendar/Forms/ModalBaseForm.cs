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
    /// The base class for modal windows. Adds OK and Cancel buttons.
    /// </summary>
    public partial class ModalBaseForm : BaseForm, ITranslatable
    {
        /// <summary>
        /// The default contructor for the dialog.
        /// </summary>
        public ModalBaseForm()
        {
            this.InitializeComponent();
            if (TEXT.Get != null)
            {
                this.ReReadTranslations();
            }
        }

        #region ITranslatable interface impementation

        /// <summary>
        /// Refresh the localizations strings.
        /// </summary>
        public void ReReadTranslations()
        {
            this.btnCancel.Text = TEXT.Get["Cancel_this"];
            this.btnOK.Text = TEXT.Get["OK_this"];
        }

        #endregion

        /// <summary>
        /// The function which executed on OK click.
        /// </summary>
        public virtual void AcceptAction()
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// The function which executed on Cancel click.
        /// </summary>
        public virtual void CancelAction()
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.AcceptAction();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.CancelAction();
        }
    }
}
