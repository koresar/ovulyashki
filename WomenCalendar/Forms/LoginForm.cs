using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class LoginForm : ModalBaseForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public string Password
        {
            get
            {
                return txtPassword.Text;
            }
        }
    }
}
