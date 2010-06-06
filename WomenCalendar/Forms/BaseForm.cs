using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    /// <summary>
    /// The common things of any application window is placed here. For example the icon.
    /// </summary>
    public class BaseForm : Form
    {
        /// <summary>
        /// The default window constructor.
        /// </summary>
        public BaseForm()
        {
            this.InitializeComponent();

            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            this.Icon = (Icon)resources.GetObject("$this.Icon");
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.SuspendLayout();

            this.ClientSize = new System.Drawing.Size(292, 266);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            this.ResumeLayout(false);
        }
    }
}
