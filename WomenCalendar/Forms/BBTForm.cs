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
        private ZedGraph.ZedGraphControl zgc;
        private IContainer components;


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

        public BBTForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.zgc = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // graph
            // 
            this.zgc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgc.EditModifierKeys = System.Windows.Forms.Keys.None;
            this.zgc.Location = new System.Drawing.Point(0, 0);
            this.zgc.Name = "graph";
            this.zgc.ScrollGrace = 0;
            this.zgc.ScrollMaxX = 0;
            this.zgc.ScrollMaxY = 0;
            this.zgc.ScrollMaxY2 = 0;
            this.zgc.ScrollMinX = 0;
            this.zgc.ScrollMinY = 0;
            this.zgc.ScrollMinY2 = 0;
            this.zgc.Size = new System.Drawing.Size(625, 492);
            this.zgc.TabIndex = 0;
            this.zgc.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.graph_ContextMenuBuilder);
            // 
            // BBTForm
            // 
            this.ClientSize = new System.Drawing.Size(625, 492);
            this.Controls.Add(this.zgc);
            this.Name = "BBTForm";
            this.ShowIcon = false;
            this.Text = "График Базальной Tемпературы Tела";
            this.Load += new System.EventHandler(this.BBTForm_Load);
            this.Resize += new System.EventHandler(this.BBTForm_Resize);
            this.ResumeLayout(false);

        }

        private void BBTForm_Load(object sender, EventArgs e)
        {
            CreateChart();
            SetGraphSize();
        }

        public void CreateChart()
        {
            GraphPane myPane = zgc.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = "График базальной температуры тела";
            myPane.XAxis.Title.IsVisible = false;//.Text = "Time, Days\n(Since Plant Construction Startup)";
            myPane.YAxis.Title.IsVisible = false;// Text = "Widget Production\n(units/hour)";
            myPane.Legend.IsVisible = false;
            myPane.XAxis.Type = AxisType.Date;

            LineItem curve;

            // Set up curve "Larry"
            double[] y = { 36.6, 36.4, 36.2, 36.9, 36.8, 36.4, 36.8, 36.6, 36.8, 36.5 };
            //double[] x = new double[10];
            PointPairList list = new PointPairList();
            Symbol emptyCircle = new Symbol(SymbolType.Circle, Color.Blue);
            emptyCircle.Size = 10;
            emptyCircle.Fill = new Fill(Color.White);
            Symbol filledCircle = new Symbol(SymbolType.Circle, Color.Blue);
            filledCircle.Size = 10;
            filledCircle.Fill = new Fill(Color.Blue);
            for (int i = 0; i < 10; i++)
            {
                DateTime d = DateTime.Today.AddDays(i);
                PointPair point = new PointPair((double) new XDate(d.Year, d.Month, d.Day), y[i]);
                point.Symbol = i%2 == 0 ? emptyCircle : filledCircle;
                point.DashStyle = i % 2 == 0 ? DashStyle.Solid : DashStyle.Dash;
                list.Add(point);
            }

            // Use green, with circle symbols
            curve = myPane.AddCurve("БТТ", list, Color.Blue, SymbolType.Circle);
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
            myPane.YAxis.Scale.MajorStep = 0.1;
            myPane.XAxis.Scale.MinorStep = 1;
            myPane.YAxis.Scale.MinorStep = 0.1;

            myPane.XAxis.MinorGrid.IsVisible = false;
            myPane.YAxis.MinorGrid.IsVisible = false;

            myPane.XAxis.Scale.FontSpec.Angle = 90;

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

        private void graph_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            foreach (ToolStripItem item in menuStrip.Items)
            {
                item.Text = Labels[item.Tag.ToString()];
            }
        }
    }
}