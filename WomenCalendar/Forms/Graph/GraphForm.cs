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
    public class GraphForm : Form, ITranslatable
    {
        private IContainer components;
        protected SplitContainer splitContainer;
        protected ZedGraphControl zgc;
        protected System.Windows.Forms.Label lbl1;
        protected DateTimePicker dateTo;
        protected DateTimePicker dateFrom;
        protected System.Windows.Forms.Label lbl2;

        protected DateTime initialMonth;
        protected double MinYValue = double.MaxValue;
        protected double MaxYValue = double.MinValue;
        protected DateTime MaxXValue = DateTime.MinValue;
        protected DateTime MinXValue = DateTime.MaxValue;
        protected ToolStrip toolStrip1;
        protected ToolStripButton toolStripButton1;
        protected ToolStripButton toolStripButton2;
        protected static Dictionary<string, string> _labels;
        protected static Dictionary<string, string> Labels
        {
            get
            {
                if (_labels == null)
                {
                    _labels = new Dictionary<string, string>();
                    _labels["copy"] = TEXT.Get["Copy_to_clipboard"];
                    _labels["page_setup"] = TEXT.Get["Print_setup"];
                    _labels["print"] = TEXT.Get["Print_dialog"];
                    _labels["save_as"] = TEXT.Get["Save_picture_as"];
                    _labels["set_default"] = TEXT.Get["Set_scale_to_default"];
                    _labels["show_val"] = TEXT.Get["Show_point_values"];
                    _labels["undo_all"] = TEXT.Get["Undo_all_zoom"];
                    _labels["unzoom"] = TEXT.Get["Undo_last_zoom"];
                }
                return _labels;
            }
        }

        #region ITranslatable interface impementation

        public void ReReadTranslations()
        {
            this.toolStripButton2.Text = TEXT.Get["Print_preview"];
            this.toolStripButton1.Text = TEXT.Get["Print_graph"];
            this.lbl2.Text = TEXT.Get["To_dates"];
            this.lbl1.Text = TEXT.Get["Show_from"];
            this.Text = TEXT.Get["Graph"];
        }

        #endregion

        public GraphForm()
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();
        }

        public GraphForm(DateTime month) : this()
        {
            initialMonth = new DateTime(month.Year, month.Month, 1);

            if (initialMonth != DateTime.MinValue)
            {
                dateFrom.Value = initialMonth;
                int valuesCount = DateTime.DaysInMonth(initialMonth.Year, initialMonth.Month);
                dateTo.Value =  initialMonth.AddDays(valuesCount - 1);
            }
            RedrawGraph();
            SetGraphSize();

            this.dateFrom.ValueChanged += new System.EventHandler(this.dateFrom_ValueChanged);
            this.dateTo.ValueChanged += new System.EventHandler(this.dateTo_ValueChanged);
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.zgc = new ZedGraph.ZedGraphControl();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.splitContainer.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer.Panel1.Controls.Add(this.lbl2);
            this.splitContainer.Panel1.Controls.Add(this.lbl1);
            this.splitContainer.Panel1.Controls.Add(this.dateTo);
            this.splitContainer.Panel1.Controls.Add(this.dateFrom);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.zgc);
            this.splitContainer.Size = new System.Drawing.Size(638, 448);
            this.splitContainer.SplitterDistance = 60;
            this.splitContainer.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(638, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::WomenCalendar.Properties.Resources.printPreviewGreen;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(175, 22);
            this.toolStripButton2.Text = "Print preview";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::WomenCalendar.Properties.Resources.printGreen;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(158, 22);
            this.toolStripButton1.Text = "Print graph";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(370, 38);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(19, 13);
            this.lbl2.TabIndex = 2;
            this.lbl2.Text = "to";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(31, 38);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(109, 13);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "Show from";
            // 
            // dateTo
            // 
            this.dateTo.Location = new System.Drawing.Point(412, 34);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(206, 20);
            this.dateTo.TabIndex = 0;
            // 
            // dateFrom
            // 
            this.dateFrom.Location = new System.Drawing.Point(146, 34);
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
            this.zgc.Size = new System.Drawing.Size(638, 384);
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
            this.Text = "Graph";
            this.Resize += new System.EventHandler(this.GraphForm_Resize);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        public void RedrawGraph()
        {
            zgc.Invalidate();
            CreateChart();
        }

        protected void CreateChart()
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
                myPane.XAxis.Scale.FormatAdditional = new Dictionary<double, string>();
                for (int i = 0; i < y.Length; i++)
                {
                    if (y[i] == 0) continue;
                    DateTime d = dateFrom.Value.AddDays(i);
                    PointPair point = new PointPair((double)new XDate(d.Year, d.Month, d.Day), y[i]);
                    point.Symbol = filledCircle;
                    point.DashStyle = (i + 1 < y.Length && y[i + 1] == 0) ? DashStyle.Dash : DashStyle.Solid;
                    list.Add(point);
                    if (y[i] > MaxYValue) MaxYValue = y[i];
                    if (y[i] < MinYValue) MinYValue = y[i];
                    myPane.XAxis.Scale.FormatAdditional[point.X] = " - " + (i + 1).ToString();
                }
                var curve = myPane.AddCurve("Main", list, Color.Blue, SymbolType.Circle);
                curve.Line.Width = 2.0F;
            }
            else
            {
                if (x.Length != y.Length) throw new Exception("Number of X values must match number of Y values.");
                filledCircle.Size = 5;
                for (int i = 0; i < y.Length; i++)
                {
                    if (y[i] == 0) continue;
                    PointPair point = new PointPair(x[i], y[i]);
                    list.Add(point);

                    var txt = new TextObj((i + 1).ToString(), point.X, point.Y - 1, CoordType.AxisXYScale, AlignH.Center, AlignV.Top);
                    txt.ZOrder = ZOrder.A_InFront;
                    txt.FontSpec.Border.IsVisible = false;
                    txt.FontSpec.Fill.IsVisible = false;
                    labels.Add(txt);

                    var txt1 = new TextObj(y[i].ToString(), point.X, point.Y + 1, CoordType.AxisXYScale, AlignH.Center, AlignV.Bottom);
                    txt1.ZOrder = ZOrder.A_InFront;
                    txt1.FontSpec.Border.IsVisible = false;
                    if (y[i] < 21 || y[i] > 35)
                    {
                        txt1.FontSpec.Fill.IsVisible = true;
                        txt1.FontSpec.IsBold = true;
                        txt1.FontSpec.Fill.Color = Color.LightCoral;
                        txt1.FontSpec.Fill.Brush = Brushes.LightCoral;
                    }
                    else
                    {
                        txt1.FontSpec.Fill.IsVisible = false;
                    }
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

                myPane.BarSettings.ClusterScaleWidthAuto = true;
                myPane.BarSettings.MinClusterGap = 0.0f;

                var bar = myPane.AddBar("Main", list, Color.Blue);
                bar.Bar.Fill = new Fill(Color.LightSkyBlue, Color.White, Color.LightSkyBlue);
            }

            myPane.GraphObjList.AddRange(labels);

            SetupGraph();

            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
        }

        protected void SetGraphSize()
        {
            zgc.Location = new Point(0, 0);
            // Leave a small margin around the outside of the control
            zgc.Size = ClientRectangle.Size; //new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        }

        protected void GraphForm_Resize(object sender, EventArgs e)
        {
            SetGraphSize();
        }

        protected void zgc_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            foreach (ToolStripItem item in menuStrip.Items)
            {
                item.Text = Labels[item.Tag.ToString()];
            }
        }

        protected void dateFrom_ValueChanged(object sender, EventArgs e)
        {
            initialMonth = dateFrom.Value;
            RedrawGraph();
        }

        protected void dateTo_ValueChanged(object sender, EventArgs e)
        {
            RedrawGraph();
        }

        protected void toolStripButton1_Click(object sender, EventArgs e)
        {
            zgc.DoPrint();
        }

        protected void toolStripButton2_Click(object sender, EventArgs e)
        {
            zgc.DoPrintPreview();
        }
    }
}