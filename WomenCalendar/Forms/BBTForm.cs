using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace WomenCalendar
{
    public class BBTForm : Form
    {
        private IContainer components;
        private DateTime initialMonth;
        private int valuesCount;
        private SplitContainer splitContainer;
        private ZedGraphControl zgc;
        private System.Windows.Forms.Label lbl1;
        private DateTimePicker dateTo;
        private DateTimePicker dateFrom;
        private System.Windows.Forms.Label lbl2;

        private static Dictionary<string, string> _labels;
        private static Dictionary<string, string> Labels
        {
            get
            {
                if (_labels == null)
                {
                    _labels = new Dictionary<string, string>();
                    _labels["copy"] = "����������� � �����";
                    _labels["page_setup"] = "��������� ������...";
                    _labels["print"] = "��������...";
                    _labels["save_as"] = "��������� �������� ���...";
                    _labels["set_default"] = "������������ ������ � ���������� ���";
                    _labels["show_val"] = "���������� �������� ��������";
                    _labels["undo_all"] = "�������� �� �������� �����������/��������";
                    _labels["unzoom"] = "�������� ��������� ��������";
                }
                return _labels;
            }
        }

        public BBTForm(DateTime month, int valuesCount)
        {
            InitializeComponent();
            initialMonth = new DateTime(month.Year, month.Month, 1);
            this.valuesCount = valuesCount;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.zgc = new ZedGraph.ZedGraphControl();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lbl2);
            this.splitContainer.Panel1.Controls.Add(this.lbl1);
            this.splitContainer.Panel1.Controls.Add(this.dateTo);
            this.splitContainer.Panel1.Controls.Add(this.dateFrom);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.zgc);
            this.splitContainer.Size = new System.Drawing.Size(638, 448);
            this.splitContainer.SplitterDistance = 32;
            this.splitContainer.TabIndex = 1;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(225, 13);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(19, 13);
            this.lbl2.TabIndex = 2;
            this.lbl2.Text = "��";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(12, 13);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(59, 13);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "������� �";
            // 
            // dateTo
            // 
            this.dateTo.Location = new System.Drawing.Point(267, 9);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(142, 20);
            this.dateTo.TabIndex = 0;
            // 
            // dateFrom
            // 
            this.dateFrom.Location = new System.Drawing.Point(77, 9);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(142, 20);
            this.dateFrom.TabIndex = 0;
            // 
            // zgc
            // 
            this.zgc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgc.EditModifierKeys = System.Windows.Forms.Keys.None;
            this.zgc.Location = new System.Drawing.Point(0, 0);
            this.zgc.Name = "zgc";
            this.zgc.ScrollGrace = 0;
            this.zgc.ScrollMaxX = 0;
            this.zgc.ScrollMaxY = 0;
            this.zgc.ScrollMaxY2 = 0;
            this.zgc.ScrollMinX = 0;
            this.zgc.ScrollMinY = 0;
            this.zgc.ScrollMinY2 = 0;
            this.zgc.Size = new System.Drawing.Size(638, 412);
            this.zgc.TabIndex = 1;
            this.zgc.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zgc_ContextMenuBuilder);
            // 
            // BBTForm
            // 
            this.ClientSize = new System.Drawing.Size(638, 448);
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(427, 307);
            this.Name = "BBTForm";
            this.ShowIcon = false;
            this.Text = "������ ��������� T���������� T���";
            this.Load += new System.EventHandler(this.BBTForm_Load);
            this.Resize += new System.EventHandler(this.BBTForm_Resize);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void BBTForm_Load(object sender, EventArgs e)
        {
            dateFrom.Value = initialMonth;
            dateTo.Value = initialMonth.AddDays(valuesCount);
            CreateChart();
            SetGraphSize();

            this.dateFrom.ValueChanged += new System.EventHandler(this.dateFrom_ValueChanged);
            this.dateTo.ValueChanged += new System.EventHandler(this.dateTo_ValueChanged);
        }

        public void CreateChart()
        {
            if (valuesCount <= 0)
            {
                zgc.Visible = false;
                return;
            }
            zgc.Visible = true;

            GraphPane myPane = zgc.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = "������ ��������� ����������� ����";
            myPane.XAxis.Title.IsVisible = false;//.Text = "Time, Days\n(Since Plant Construction Startup)";
            myPane.YAxis.Title.IsVisible = false;// Text = "Widget Production\n(units/hour)";
            myPane.Legend.IsVisible = false;
            myPane.XAxis.Type = AxisType.Date;

            LineItem curve;

            // Set up curve "Larry"
            double[] y = Program.CurrentWoman.BBT.GetTemperaturesSince(initialMonth, valuesCount);
                //DateTime.DaysInMonth(initialMonth.Year, initialMonth.Month));
                //{ 36.6, 36.4, 36.2, 36.9, 36.8, 36.4, 36.8, 36.6, 36.8, 36.5 };
            //double[] x = new double[10];
            PointPairList list = new PointPairList();
            Symbol emptyCircle = new Symbol(SymbolType.Circle, Color.Blue);
            emptyCircle.Size = 10;
            emptyCircle.Fill = new Fill(Color.White);
            Symbol filledCircle = new Symbol(SymbolType.Circle, Color.Blue);
            filledCircle.Size = 10;
            filledCircle.Fill = new Fill(Color.Blue);

            double maxBBT = double.MinValue;
            double minBBT = double.MaxValue;
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i] == 0) continue;
                DateTime d = initialMonth.AddDays(i);
                PointPair point = new PointPair((double)new XDate(d.Year, d.Month, d.Day), y[i]);
                point.Symbol = filledCircle;
                point.DashStyle = (i+1 < y.Length && y[i + 1] == 0) ? DashStyle.Dash : DashStyle.Solid;
                list.Add(point);
                if (y[i] > maxBBT) maxBBT = y[i];
                if (y[i] < minBBT) minBBT = y[i];
            }

            curve = myPane.AddCurve("���", list, Color.Blue, SymbolType.Circle);
            curve.Line.Width = 2.0F;

            // Fill the symbols
            //Fill fill = new Fill(Color.White);//Color.Red, Color.Blue);
/*            fill.Type = FillType.GradientByZ;
            fill.RangeMin = 1;
            fill.RangeMax = 2;*/
            //curve.Symbol.Fill.SecondaryValueGradientColor = Color.Red;
            //curve.Symbol.Fill = fill;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.DashOff = 0;
            myPane.XAxis.MajorGrid.DashOn = 0;
            myPane.YAxis.MajorGrid.DashOff = 0;
            myPane.YAxis.MajorGrid.DashOn = 0;

            myPane.XAxis.Scale.MajorStep = 1;
            myPane.XAxis.Scale.MinorStep = 1;
            myPane.XAxis.Scale.MaxAuto = false;
            DateTime d1 = initialMonth;
            myPane.XAxis.Scale.Min = (double)new XDate(d1.Year, d1.Month, d1.Day);
            d1 = d1.AddDays(y.Length - 1);
            myPane.XAxis.Scale.Max = (double)new XDate(d1.Year, d1.Month, d1.Day);

            myPane.YAxis.Scale.MajorStep = 0.1;
            myPane.YAxis.Scale.MinorStep = 0.1;
            myPane.YAxis.Scale.MaxAuto = false;
            myPane.YAxis.Scale.Max = ((int)(maxBBT*10 + 0.5) + 2) / 10.0;
            myPane.YAxis.Scale.Min = ((int)(minBBT*10 - 0.5) - 2) / 10.0;

            myPane.XAxis.MinorGrid.IsVisible = false;
            myPane.YAxis.MinorGrid.IsVisible = false;

            myPane.XAxis.Scale.FontSpec.Angle = 90;

            myPane.XAxis.MajorTic.IsAllTics = false;
            myPane.YAxis.MajorTic.IsAllTics = false;

            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
        }

        private void SetGraphSize()
        {
            zgc.Location = new Point(0, 0);
            // Leave a small margin around the outside of the control
            zgc.Size = ClientRectangle.Size; //new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        }

        private void BBTForm_Resize(object sender, EventArgs e)
        {
            SetGraphSize();
        }

        private void zgc_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            foreach (ToolStripItem item in menuStrip.Items)
            {
                item.Text = Labels[item.Tag.ToString()];
            }
        }

        private void dateFrom_ValueChanged(object sender, EventArgs e)
        {
            valuesCount = (dateTo.Value - dateFrom.Value).Days;
            CreateChart();
        }

        private void dateTo_ValueChanged(object sender, EventArgs e)
        {
            valuesCount = (dateTo.Value - dateFrom.Value).Days;
            CreateChart();
        }
    }
}