using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class DateRangeForm : BaseForm, ITranslatable
    {
        public DateRangeForm()
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();
        }

        #region ITranslatable interface impementation

        public void ReReadTranslations()
        {
            this.label1.Text = TEXT.Get["From_dates"];
            this.label2.Text = TEXT.Get["To_dates"];
            this.button1.Text = TEXT.Get["Export_this"];
            this.button2.Text = TEXT.Get["Cancel_this"];
            this.Text = TEXT.Get["Enter_dates"];
        }

        #endregion

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
