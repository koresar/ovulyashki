using System;
using System.Collections.Generic;
using System.Text;
using ZedGraph;

namespace WomenCalendar
{
    public class HealthForm : MonthlyGraphForm
    {
        public HealthForm(DateTime month)
            : base(month)
        {
            InitializeComponent();
        }

        public HealthForm()
        {
            InitializeComponent();
        }

        protected override double[] GetYValues()
        {
            return Program.CurrentWoman.Health.GetHealthesSince(dateFrom.Value, (dateTo.Value - dateFrom.Value).Days + 1);
        }

        protected override void SetupGraph()
        {
            GraphPane myPane = zgc.GraphPane;

            myPane.Title.Text = TEXT.Get["Wellbeing_graph"];

            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.DashOff = 0;
            myPane.XAxis.MajorGrid.DashOn = 0;
            myPane.YAxis.MajorGrid.DashOff = 0;
            myPane.YAxis.MajorGrid.DashOn = 0;

            myPane.XAxis.Scale.MajorStep = 1;
            myPane.XAxis.Scale.MinorStep = 1;
            myPane.XAxis.Scale.MaxAuto = false;
            DateTime d1 = dateFrom.Value;
            myPane.XAxis.Scale.Min = (double)new XDate(d1.Year, d1.Month, d1.Day);
            d1 = dateTo.Value;
            myPane.XAxis.Scale.Max = (double)new XDate(d1.Year, d1.Month, d1.Day);

            myPane.YAxis.Scale.MajorStep = 1;
            myPane.YAxis.Scale.MinorStep = 1;
            myPane.YAxis.Scale.MaxAuto = false;
            myPane.YAxis.Scale.Max = 10.0;
            myPane.YAxis.Scale.Min = 0;
            //myPane.YAxis.Scale.IsSkipFirstLabel = true;
            myPane.YAxis.Scale.MaxAuto = false;
            myPane.YAxis.Scale.MinAuto = false;
            myPane.YAxis.Scale.IsVisible = true;

            myPane.XAxis.MinorGrid.IsVisible = false;
            myPane.YAxis.MinorGrid.IsVisible = false;

            myPane.XAxis.Scale.FontSpec.Angle = 90;

            myPane.XAxis.MajorTic.IsAllTics = false;
            myPane.YAxis.MajorTic.IsAllTics = false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HealthForm
            // 
            this.ClientSize = new System.Drawing.Size(638, 448);
            this.Name = "HealthForm";
            this.ResumeLayout(false);
        }
    }
}
