using System;
using System.Collections.Generic;
using System.Text;
using ZedGraph;
using System.Drawing;

namespace WomenCalendar
{
    public class CycleLengthForm : GraphForm
    {
        public CycleLengthForm()
        {
            InitializeComponent();

            var ms = Program.CurrentWoman.Menstruations;
            if (ms != null && ms.Count > 0)
            {
                dateFrom.Value = ms.First.StartDay;
                if (ms.Count > 1)
                {
                    dateTo.Value = ms[ms.Count - 2].StartDay;
                }
                else
                {
                    dateTo.Value = dateFrom.Value;
                }
            }

            this.dateFrom.ValueChanged += new System.EventHandler(this.dateFrom_ValueChanged);
            this.dateTo.ValueChanged += new System.EventHandler(this.dateTo_ValueChanged);
        }

        private bool InSelectedRange(DateTime d)
        {
            return d >= dateFrom.Value && d <= dateTo.Value;
        }

        protected override double[] GetYValues()
        {
            var ms = Program.CurrentWoman.Menstruations;
            if (ms.Count < 2) return new double[0];
            var res = new List<double>();
            for (int i = 1; i < ms.Count; i++)
            {
                var m = ms[i - 1];
                if (InSelectedRange(m.StartDay))
                {
                    res.Add((ms[i].StartDay - m.StartDay).Days);
                }
            }
            return res.ToArray();
        }

        protected override double[] GetXValues()
        {
            var ms = Program.CurrentWoman.Menstruations;
            if (ms.Count < 2) return new double[0];
            var res = new List<double>();
            MinXValue = dateFrom.Value;
            MaxXValue = dateTo.Value;
            for (int i = 1; i < ms.Count; i++)
            {
                var d = ms[i - 1].StartDay;
                if (InSelectedRange(d))
                {
                    if (d > MaxXValue) MaxXValue = d;
                    if (d < MinXValue) MinXValue = d;
                    res.Add((double)new XDate(d.Year, d.Month, d.Day));
                }
            }
            return res.ToArray();
        }

        protected override void SetupGraph()
        {
            /*
            GraphPane myPane = zgc.GraphPane;

            // Set the title and axis labels
            myPane.Title.Text = "Vertical Bars with Value Labels Above Each Bar";
            myPane.XAxis.Title.Text = "Position Number";
            myPane.YAxis.Title.Text = "Some Random Thing";

            PointPairList list = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            Random rand = new Random();

            // Generate random data for three curves
            for (int i = 0; i < 5; i++)
            {
                double x = (double)i;
                double y = rand.NextDouble() * 1000;
                double y2 = rand.NextDouble() * 1000;
                double y3 = rand.NextDouble() * 1000;
                list.Add(x, y);
                list2.Add(x, y2);
                list3.Add(x, y3);
            }

            // create the curves
            BarItem myCurve = myPane.AddBar("curve 1", list, Color.Blue);
            BarItem myCurve2 = myPane.AddBar("curve 2", list2, Color.Red);
            BarItem myCurve3 = myPane.AddBar("curve 3", list3, Color.Green);

            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White,
               Color.FromArgb(255, 255, 166), 45.0F);

            zgc.AxisChange();

            // expand the range of the Y axis slightly to accommodate the labels
            myPane.YAxis.Scale.Max += myPane.YAxis.Scale.MajorStep;

            // Create TextObj's to provide labels for each bar
            BarItem.CreateBarLabels(myPane, false, "f0");
            */
            GraphPane myPane = zgc.GraphPane;

            myPane.Title.Text = TEXT.Get["Cycle_length_graph"];

            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.DashOff = 0;
            myPane.XAxis.MajorGrid.DashOn = 0;
            myPane.YAxis.MajorGrid.DashOff = 0;
            myPane.YAxis.MajorGrid.DashOn = 0;

            myPane.XAxis.Scale.MajorStep = 1;
            myPane.XAxis.Scale.MinorStep = 1;
            myPane.XAxis.Scale.MajorUnit = DateUnit.Year;
            myPane.XAxis.Scale.MinorUnit = DateUnit.Month;
            myPane.XAxis.Scale.MaxAuto = false;
            myPane.XAxis.Scale.Min = (double)new XDate(MinXValue.AddMonths(-1));
            myPane.XAxis.Scale.Max = (double)new XDate(MaxXValue.AddMonths(1));
            myPane.XAxis.Scale.IsSkipFirstLabel = myPane.XAxis.Scale.IsSkipLastLabel = false;

            myPane.YAxis.Scale.MajorStep = 5;
            myPane.YAxis.Scale.MinorStep = 1;
            myPane.YAxis.Scale.MinGrace = 1;
            myPane.YAxis.Scale.MaxAuto = false;
            myPane.YAxis.Scale.Max = MaxYValue + 5;
            myPane.YAxis.Scale.Min = 0;

            myPane.XAxis.MinorGrid.IsVisible = false;
            myPane.YAxis.MinorGrid.IsVisible = false;

            myPane.XAxis.Scale.FontSpec.Angle = 90;

            myPane.XAxis.MajorTic.IsAllTics = false;
            myPane.YAxis.MajorTic.IsAllTics = false;            
        }

        private void InitializeComponent()
        {
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            // 
            // CycleLengthForm
            // 
            this.ClientSize = new System.Drawing.Size(638, 448);
            this.Name = "CycleLengthForm";
            this.Load += new System.EventHandler(this.CycleLengthForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void CycleLengthForm_Load(object sender, EventArgs e)
        {
            RedrawGraph();
        }
    }
}
