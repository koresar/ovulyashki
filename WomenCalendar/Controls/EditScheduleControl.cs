using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar.Controls
{
    public partial class EditScheduleControl : UserControl
    {
        public DateTime initialDate;
        public DateTime InitialDate
        {
            get { return initialDate; }
            set
            {
                initialDate = value;
                coloredSchedulerCalendarControl1.StartMonth = initialDate;
            }
        }

        public EditScheduleControl()
        {
            InitializeComponent();
        }
    }
}
