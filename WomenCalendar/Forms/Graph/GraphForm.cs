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
    public class GraphForm : Form
    {
        private IContainer components;
        private SplitContainer splitContainer;
        protected ZedGraphControl zgc;
        private System.Windows.Forms.Label lbl1;
        protected DateTimePicker dateTo;
        protected DateTimePicker dateFrom;
        private System.Windows.Forms.Label lbl2;

        protected DateTime initialMonth;
        protected int valuesCount;
        protected double MinYValue = double.MaxValue;
        protected double MaxYValue = double.MinValue;
        protected DateTime MaxXValue = DateTime.MinValue;
        protected DateTime MinXValue = DateTime.MaxValue;
        private static Dictionary<string, string> _labels;
        private static Dictionary<string, string> Labels
        {
            get
            {
                if (_labels == null)
                {
                    _labels = new Dictionary<string, string>();
                    _labels["copy"] = "Скопировать в буфер";
                    _labels["page_setup"] = "Настройка печати...";
                    _labels["print"] = "Печатать...";
                    _labels["save_as"] = "Сохранить картинку как...";
                    _labels["set_default"] = "Восстановить график в нормальный вид";
                    _labels["show_val"] = "Показывать точечные значения";
                    _labels["undo_all"] = "Отменить всё сделаное приближение/вращение";
                    _labels["unzoom"] = "Отменить последнее действие";
                }
                return _labels;
            }
        }

        public GraphForm()
        {
            InitializeComponent();
        }

        public GraphForm(DateTime month, int valuesCount) : this()
        {
            initialMonth = new DateTime(month.Year, month.Month, 1);
            this.valuesCount = valuesCount;
        }

        protected virtual double[] GetYValues()
        {
            return new double[10] {1, 2, 3, 4, 5, 4, 3, 2, 1, 2};
        }

        protected virtual double[] GetXValues()
        {
            return null;
        }

        protected virtual void SetupGraph()
        {
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
            this.lbl2.Location = new System.Drawing.Point(301, 13);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(19, 13);
            this.lbl2.TabIndex = 2;
            this.lbl2.Text = "по";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(12, 13);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(59, 13);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "Начиная с";
            // 
            // dateTo
            // 
            this.dateTo.Location = new System.Drawing.Point(343, 9);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(206, 20);
            this.dateTo.TabIndex = 0;
            // 
            // dateFrom
            // 
            this.dateFrom.Location = new System.Drawing.Point(77, 9);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(206, 20);
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
            // GraphForm
            // 
            this.ClientSize = new System.Drawing.Size(638, 448);
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(427, 307);
            this.Name = "GraphForm";
            this.ShowIcon = false;
            this.Text = "График";
            this.Load += new System.EventHandler(this.GraphForm_Load);
            this.Resize += new System.EventHandler(this.GraphForm_Resize);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void GraphForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (initialMonth != DateTime.MinValue)
                {
                    dateFrom.Value = initialMonth;
                    dateTo.Value = initialMonth.AddDays(valuesCount - 1);
                }
                CreateChart();
                SetGraphSize();

                dateFrom.MaxDate = dateTo.Value;
                dateTo.MinDate = dateFrom.Value;

                this.dateFrom.ValueChanged += new System.EventHandler(this.dateFrom_ValueChanged);
                this.dateTo.ValueChanged += new System.EventHandler(this.dateTo_ValueChanged);
            }
        }

        public void CreateChart()
        {
            zgc.Visible = true;

            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();

            // Set the titles and axis labels
            myPane.Title.Text = this.Text;
            myPane.XAxis.Title.IsVisible = false;//.Text = "Time, Days\n(Since Plant Construction Startup)";
            myPane.YAxis.Title.IsVisible = false;// Text = "Widget Production\n(units/hour)";
            myPane.Legend.IsVisible = false;
            myPane.XAxis.Type = AxisType.Date;

            var y = GetYValues();
                //Program.CurrentWoman.BBT.GetTemperaturesSince(initialMonth, valuesCount);
                //DateTime.DaysInMonth(initialMonth.Year, initialMonth.Month));
                //{ 36.6, 36.4, 36.2, 36.9, 36.8, 36.4, 36.8, 36.6, 36.8, 36.5 };
            var x = GetXValues();
            var labels = new List<GraphObj>();

            PointPairList list = new PointPairList();
            Symbol emptyCircle = new Symbol(SymbolType.Circle, Color.Blue);
            emptyCircle.Size = 10;
            emptyCircle.Fill = new Fill(Color.White);
            Symbol filledCircle = new Symbol(SymbolType.Circle, Color.Blue);
            filledCircle.Size = 10;
            filledCircle.Fill = new Fill(Color.Blue);

            myPane.GraphObjList.Clear();

            if (x == null)
            {
                for (int i = 0; i < y.Length; i++)
                {
                    if (y[i] == 0) continue;
                    DateTime d = initialMonth.AddDays(i);
                    PointPair point = new PointPair((double)new XDate(d.Year, d.Month, d.Day), y[i]);
                    point.Symbol = filledCircle;
                    point.DashStyle = (i + 1 < y.Length && y[i + 1] == 0) ? DashStyle.Dash : DashStyle.Solid;
                    list.Add(point);
                    if (y[i] > MaxYValue) MaxYValue = y[i];
                    if (y[i] < MinYValue) MinYValue = y[i];
                }
                var curve = myPane.AddCurve("Main", list, Color.Blue, SymbolType.Circle);
                curve.Line.Width = 2.0F;
            }
            else
            {
                if (x.Length != y.Length) throw new Exception("Number of X values must mach number of Y values.");
                filledCircle.Size = 5;
                for (int i = 0; i < y.Length; i++)
                {
                    if (y[i] == 0) continue;
                    PointPair point = new PointPair(x[i], y[i]);
                    point.Symbol = filledCircle;
                    point.DashStyle = DashStyle.Solid;
                    list.Add(point);

                    var txt = new TextObj((i + 1).ToString(), point.X, point.Y - 1, CoordType.AxisXYScale, AlignH.Center, AlignV.Top);
                    txt.ZOrder = ZOrder.A_InFront;
                    txt.FontSpec.Border.IsVisible = false;
                    txt.FontSpec.Fill.IsVisible = false;
                    labels.Add(txt);

                    var txt1 = new TextObj(y[i].ToString(), point.X, point.Y, CoordType.AxisXYScale, AlignH.Center, AlignV.Bottom);
                    txt1.ZOrder = ZOrder.A_InFront;
                    txt1.FontSpec.Border.IsVisible = false;
                    txt1.FontSpec.Fill.IsVisible = false;
                    labels.Add(txt1);

                    var s = ((XDate)x[i]).ToString("d");
                    var txt2 = new TextObj(s, point.X, 0, CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                    txt2.ZOrder = ZOrder.A_InFront;
                    txt2.FontSpec.Border.IsVisible = false;
                    txt2.FontSpec.Fill.IsVisible = false;
                    txt2.FontSpec.Angle = 90;

                    labels.Add(txt2);

                    if (y[i] > MaxYValue) MaxYValue = y[i];
                    if (y[i] < MinYValue) MinYValue = y[i];
                }

                var bar = myPane.AddBar("Main", list, Color.Blue);
                bar.Bar.Fill = new Fill(Color.LightSkyBlue, Color.White, Color.LightSkyBlue);
                myPane.BarSettings.ClusterScaleWidthAuto = true;
                myPane.BarSettings.MinClusterGap = 0.0f;
            }

            myPane.GraphObjList.AddRange(labels);

            SetupGraph();

            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
        }

        private void SetGraphSize()
        {
            zgc.Location = new Point(0, 0);
            // Leave a small margin around the outside of the control
            zgc.Size = ClientRectangle.Size; //new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        }

        private void GraphForm_Resize(object sender, EventArgs e)
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
            dateTo.MinDate = dateFrom.Value;
            valuesCount = (dateTo.Value - dateFrom.Value).Days + 1;
            initialMonth = dateFrom.Value;
            zgc.Invalidate();
            CreateChart();
        }

        private void dateTo_ValueChanged(object sender, EventArgs e)
        {
            dateFrom.MaxDate = dateTo.Value;
            valuesCount = (dateTo.Value - dateFrom.Value).Days + 1;
            zgc.Invalidate();
            CreateChart();
        }
    }
}