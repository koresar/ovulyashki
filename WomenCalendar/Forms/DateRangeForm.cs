using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class DateRangeForm : BaseForm
    {
        public DateRangeForm()
        {
            InitializeComponent();
        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
            if (Program.CurrentWoman.Menstruations.Count > 0)
            {
                dateFrom.Value = Program.CurrentWoman.Menstruations.First.StartDay;
                dateTo.Value = Program.CurrentWoman.Menstruations.Last.LastDay;
            }
        }

        public DateTime From { get { return dateFrom.Value; } }
        public DateTime To { get { return dateTo.Value; } }
    }
}
