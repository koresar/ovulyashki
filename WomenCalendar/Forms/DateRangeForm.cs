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
    /// The form to select a start and stop dates.
    /// </summary>
    public partial class DateRangeForm : BaseForm, ITranslatable
    {
        /// <summary>
        /// The defacult constructor of the window.
        /// </summary>
        public DateRangeForm()
        {
            this.InitializeComponent();
            if (TEXT.Get != null)
            {
                this.ReReadTranslations();
            }
        }

        /// <summary>
        /// The start date entered.
        /// </summary>
        public DateTime From
        {
            get { return this.dateFrom.Value; }
        }

        /// <summary>
        /// The end date entered.
        /// </summary>
        public DateTime To
        {
            get { return this.dateTo.Value; }
        }

        #region ITranslatable interface impementation

        /// <summary>
        /// Refresh localizations strings.
        /// </summary>
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
                this.dateFrom.Value = Program.CurrentWoman.Menstruations.First.StartDay;
                this.dateTo.Value = Program.CurrentWoman.Menstruations.Last.LastDay;
            }
        }
    }
}
