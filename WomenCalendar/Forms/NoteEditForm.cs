using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class NoteEditForm : BaseForm
    {
        public NoteEditForm()
        {
            InitializeComponent();
        }

        public NoteEditForm(string editText)
            : this()
        {
            txtNote.Text = (!editText.Contains("\r\n")) ? editText.Replace("\n", "\r\n") : editText;
        }

        public string NoteText
        {
            get
            {
                return txtNote.Text;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
