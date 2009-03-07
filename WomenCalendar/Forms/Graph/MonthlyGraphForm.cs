using System;
using System.Collections.Generic;
using System.Text;
using ZedGraph;

namespace WomenCalendar
{
    public class MonthlyGraphForm : GraphForm
    {
        public class PeriodDisplayItem
        {
            public int Number { get; set; }
            public DateTime Start { get; set; }
            public DateTime Stop { get; set; }
            public int Length { get; set; }
            public PeriodDisplayItem(int num, DateTime start, DateTime stop, int len)
            {
                Number = num;
                Start = start;
                Stop = stop;
                Length = len;
            }

            public override string ToString()
            {
                return string.Format("{0}: {1} - {2} ({3} {4})",
                    Number, Start.ToString("d"), Stop.ToString("d"), Length, Woman.GetDaysString(Length));
            }
        }

        protected System.Windows.Forms.Button btnNextPeriod;
        protected System.Windows.Forms.ComboBox cmbPeriods;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Button btnPrevPeriod;
    
        public MonthlyGraphForm(DateTime month)
            : base(month)
        {
            InitializeComponent();

            InitializePeriodList();
        }

        public MonthlyGraphForm()
        {
            InitializeComponent();
        }
        
        protected void InitializePeriodList()
        {
            MenstruationPeriod selectMe = null;
            var current = Program.CurrentWoman.Menstruations.GetPeriodByDate(initialMonth);
            if (current != null)
            {
                selectMe = current;
            }
            else
            {
                var before = Program.CurrentWoman.Menstruations.GetClosestPeriodBeforeDay(initialMonth);
                var after = Program.CurrentWoman.Menstruations.GetClosestPeriodAfterDay(initialMonth);
                int beforeDays = before == null ? int.MaxValue : (initialMonth - before.StartDay).Days;
                int afterDays = after == null ? int.MaxValue : (after.StartDay - initialMonth).Days;
                selectMe = (afterDays <= beforeDays) ? after : before;
            }

            int selectIndex = 0;
            for (int i = 0; i < Program.CurrentWoman.Menstruations.Count; i++)
            {
                var p1 = Program.CurrentWoman.Menstruations[i];
                var p2 = i == Program.CurrentWoman.Menstruations.Count - 1 ? null : Program.CurrentWoman.Menstruations[i + 1];
                int days = p2 == null ? Program.CurrentWoman.ManualPeriodLength : ((p2.StartDay - p1.StartDay).Days - 1);
                var start = p1.StartDay;
                var stop = p2 == null ? p1.StartDay.AddDays(Program.CurrentWoman.ManualPeriodLength) : p2.StartDay.AddDays(-1);
                int index = cmbPeriods.Items.Add(new PeriodDisplayItem(i + 1, start, stop, days));
                if (p1 == selectMe) selectIndex = index; else if (p2 == selectMe) selectIndex = index;
            }
            if (cmbPeriods.Items.Count > 0) cmbPeriods.SelectedIndex = selectIndex;
        }

        private void InitializeComponent()
        {
            this.btnPrevPeriod = new System.Windows.Forms.Button();
            this.btnNextPeriod = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPeriods = new System.Windows.Forms.ComboBox();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.label1);
            this.splitContainer.Panel1.Controls.Add(this.btnPrevPeriod);
            this.splitContainer.Panel1.Controls.Add(this.cmbPeriods);
            this.splitContainer.Panel1.Controls.Add(this.btnNextPeriod);
            this.splitContainer.SplitterDistance = 90;
            // 
            // zgc
            // 
            this.zgc.Size = new System.Drawing.Size(638, 354);
            // 
            // btnPrevPeriod
            // 
            this.btnPrevPeriod.Location = new System.Drawing.Point(146, 63);
            this.btnPrevPeriod.Name = "btnPrevPeriod";
            this.btnPrevPeriod.Size = new System.Drawing.Size(23, 23);
            this.btnPrevPeriod.TabIndex = 4;
            this.btnPrevPeriod.Text = "<";
            this.btnPrevPeriod.UseVisualStyleBackColor = true;
            this.btnPrevPeriod.Click += new System.EventHandler(this.btnPrevPeriod_Click);
            // 
            // btnNextPeriod
            // 
            this.btnNextPeriod.Location = new System.Drawing.Point(422, 63);
            this.btnNextPeriod.Name = "btnNextPeriod";
            this.btnNextPeriod.Size = new System.Drawing.Size(23, 23);
            this.btnNextPeriod.TabIndex = 5;
            this.btnNextPeriod.Text = ">";
            this.btnNextPeriod.UseVisualStyleBackColor = true;
            this.btnNextPeriod.Click += new System.EventHandler(this.btnNextPeriod_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Показать цикл №";
            // 
            // cmbPeriods
            // 
            this.cmbPeriods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriods.FormattingEnabled = true;
            this.cmbPeriods.Location = new System.Drawing.Point(175, 64);
            this.cmbPeriods.Name = "cmbPeriods";
            this.cmbPeriods.Size = new System.Drawing.Size(241, 21);
            this.cmbPeriods.TabIndex = 7;
            this.cmbPeriods.SelectedIndexChanged += new System.EventHandler(this.cmbPeriods_SelectedIndexChanged);
            // 
            // MonthlyGraphForm
            // 
            this.ClientSize = new System.Drawing.Size(638, 448);
            this.Name = "MonthlyGraphForm";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void btnPrevPeriod_Click(object sender, EventArgs e)
        {
            cmbPeriods.SelectedIndex = cmbPeriods.SelectedIndex - 1;
        }

        private void btnNextPeriod_Click(object sender, EventArgs e)
        {
            cmbPeriods.SelectedIndex = cmbPeriods.SelectedIndex + 1;
        }

        private void cmbPeriods_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dateFrom.ValueChanged -= new System.EventHandler(this.dateFrom_ValueChanged);
            this.dateTo.ValueChanged -= new System.EventHandler(this.dateTo_ValueChanged);

            btnPrevPeriod.Enabled = !(cmbPeriods.SelectedIndex <= 0);
            btnNextPeriod.Enabled = !(cmbPeriods.SelectedIndex >= cmbPeriods.Items.Count - 1);
            if (cmbPeriods.SelectedIndex >= 0)
            {
                var item = (cmbPeriods.SelectedItem as PeriodDisplayItem);
                if (item != null)
                {
                    dateFrom.Value = item.Start;
                    dateTo.Value = item.Stop;
                    RedrawGraph();
                }
            }

            this.dateFrom.ValueChanged += new System.EventHandler(this.dateFrom_ValueChanged);
            this.dateTo.ValueChanged += new System.EventHandler(this.dateTo_ValueChanged);
        }
    }
}
