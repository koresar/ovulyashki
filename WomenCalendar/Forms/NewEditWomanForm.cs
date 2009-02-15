using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class NewEditWomanForm : ModalBaseForm
    {
        public NewEditWomanForm()
        {
            InitializeComponent();
            txtName.Text = Environment.UserName;
        }
        
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
