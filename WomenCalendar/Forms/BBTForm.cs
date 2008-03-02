using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace WomenCalendar
{
    public class BBTForm : Form
    {
        private ZedGraph.ZedGraphControl graph;
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
            this.graph = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // graph
            // 
            this.graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graph.EditModifierKeys = System.Windows.Forms.Keys.None;
            this.graph.Location = new System.Drawing.Point(0, 0);
            this.graph.Name = "graph";
            this.graph.ScrollGrace = 0;
            this.graph.ScrollMaxX = 0;
            this.graph.ScrollMaxY = 0;
            this.graph.ScrollMaxY2 = 0;
            this.graph.ScrollMinX = 0;
            this.graph.ScrollMinY = 0;
            this.graph.ScrollMinY2 = 0;
            this.graph.Size = new System.Drawing.Size(625, 492);
            this.graph.TabIndex = 0;
            this.graph.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.graph_ContextMenuBuilder);
            // 
            // BBTForm
            // 
            this.ClientSize = new System.Drawing.Size(625, 492);
            this.Controls.Add(this.graph);
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
            GraphPane myPane = graph.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = "SampleMultiPointList (IPointList) Demo";
            myPane.XAxis.Title.Text = "Time, seconds";
            myPane.YAxis.Title.Text = "Distance (m), or Velocity (m/s)";

            // Create a new SampleMultiPointList (see SampleMultiPointList.cs for details)
            SampleMultiPointList myList = new SampleMultiPointList();
            // For the first list, specify that the Y data to be plotted will be the distance
            myList.YData = PerfDataType.Distance;

            // note how it does not matter that we created the second list before actually
            // adding the data -- this is because the cloned list shares data with the
            // original
            SampleMultiPointList myList2 = new SampleMultiPointList(myList);
            // For the second list, specify that the Y data to be plotted will be the velocity
            myList2.YData = PerfDataType.Velocity;

            // Populate the dataset using some calculated values
            for (int i = 0; i < 20; i++)
            {
                double time = (double)i;
                double acceleration = 1.0;
                double velocity = acceleration * time;
                double distance = acceleration * time * time / 2.0;
                PerformanceData perfData = new PerformanceData(time, distance, velocity, acceleration);
                myList.Add(perfData);
            }

            // Add two curves to the graph
            myPane.AddCurve("Distance", myList, Color.Blue);
            myPane.AddCurve("Velocity", myList2, Color.Red);

            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White,
               Color.LightGoldenrodYellow, 45.0F);

            graph.AxisChange();
        }

        private void SetGraphSize()
        {
            graph.Location = new Point(0, 0);
            // Leave a small margin around the outside of the control
            graph.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
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