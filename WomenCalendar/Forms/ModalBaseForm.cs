using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class ModalBaseForm : BaseForm
    {
        public ModalBaseForm()
        {
            InitializeComponent();
        }
        
        public virtual void AcceptAction()
        {
            DialogResult = DialogResult.OK;
        }

        public virtual void CancelAction()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AcceptAction();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelAction();
        }
    }
}
