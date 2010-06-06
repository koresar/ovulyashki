using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace WomenCalendar
{
    public enum DayEditFocus { Note, BBT, Length, Schedules };

    public partial class DayEditForm : BaseForm, ITranslatable
    {
        class DayData
        {
            public string BBT;
            public bool HadSex;
            public int Health;
            public string Note;
            public bool HasMenstr;
            public decimal MenstrLength;
            public int Egesta;
            public CervicalFluid CF;
            public List<Schedule> Schedules;

            public override bool Equals(object obj)
            {
                DayData d = obj as DayData;
                return d.BBT == BBT && d.HadSex == HadSex && d.Health == Health && d.Note == Note && 
                    d.HasMenstr == HasMenstr && d.MenstrLength == MenstrLength && d.Egesta == Egesta && d.CF == CF &&
                    SchedulesEqual(d.Schedules);
            }

            public override int GetHashCode()
            {
                return BBT.GetHashCode() ^ HadSex.GetHashCode() ^ Health.GetHashCode() ^ Note.GetHashCode() ^
                    HasMenstr.GetHashCode() ^ MenstrLength.GetHashCode() ^ Egesta.GetHashCode() ^ CF.GetHashCode();
            }

            private bool SchedulesEqual(List<Schedule> d)
            {
                return this.Schedules.SequenceEqual(d);
            }
        }

        private DayData initialData;
        private DateTime date;
        private DayEditFocus defaultFocus;

        public DayEditForm(DateTime day)
            : this(day, DayEditFocus.Note)
        {
        }

        public DayEditForm(DateTime day, DayEditFocus focus)
        {
            InitializeComponent();
            if (TEXT.Get != null) ReReadTranslations();
            this.date = day;
            defaultFocus = focus;
        }

        #region ITranslatable interface impementation

        public void ReReadTranslations()
        {
            this.Text = TEXT.Get["Change_day"];
            this.btnPrevDay.Text = "<< " + TEXT.Get["Previous_day"];
            this.btnNextDay.Text = TEXT.Get["Next_day"] + " >>";
            this.ttButton.ToolTipTitle = TEXT.Get["On_off_menses_button"];
            this.ttButton.SetToolTip(chkMentrustions,
                chkMentrustions.Checked ? TEXT.Get["Cancel_these_menses"] : TEXT.Get["Set_day_as_menses_start"]);
            this.dayEditControl.ReReadTranslations();
            this.mensesEditControl.ReReadTranslations();
            this.btnCancel.Text = TEXT.Get["Cancel_this"];
            this.btnOK.Text = TEXT.Get["OK_this"];
        }

        #endregion

        public void AcceptAction()
        {
            if (DataChanged)
            {
                if (!ValidateData()) return;
                if (!SaveData()) return;
            }

            DialogResult = DialogResult.OK;
        }

        private bool SaveData()
        {
            Woman w = Program.CurrentWoman;

            if (chkMentrustions.Checked)
            {
                MenstruationPeriod period = w.Menstruations.GetPeriodByDate(date);
                if (period == null)
                { // this is new period user want to add
                    if (!Program.CurrentWoman.Menstruations.Add(date, mensesEditControl.Length))
                    { // nothing was saved for now, let's quit.
                        return false;
                    }
                    Program.CurrentWoman.Menstruations.SetEgesta(date, mensesEditControl.EgestaSliderValue);
                }
                else
                {
                    w.Menstruations.SetPeriodLength(period, mensesEditControl.Length);
                    period.Egestas[date] = mensesEditControl.EgestaSliderValue;
                }
            }
            else
            {
                w.Menstruations.Remove(date);
            }

            w.BBT.SetBBT(date, dayEditControl.BBT);

            w.Notes[date] = dayEditControl.Note;

            w.HadSexList[date] = dayEditControl.HadSex;

            w.Health[date] = dayEditControl.Health;

            w.CFs[date] = dayEditControl.CurrentCF;

            w.Menstruations.ResetOvulyationsDates();

            w.Schedules.UpdateData(editScheduleControl.GetAllSchedules());

            Program.ApplicationForm.UpdateDayInformationIfFocused(date);
            Program.ApplicationForm.RedrawCalendar(); // redraw whole calendar

            return true;
        }

        private void LoadForm()
        {
            Text = date.ToLongDateString();

            Woman w = Program.CurrentWoman;

            MenstruationPeriod period = w.Menstruations.GetPeriodByDate(date);
            if (period != null)
            {
                mensesEditControl.Length = period.Length;
                int egesta = period.Egestas[date];
                mensesEditControl.EgestaSliderValue = egesta;
                chkMentrustions.Checked = true;
                chkMentrustions.Enabled = period.StartDay == date;
            }
            else
            {
                chkMentrustions.Checked = false;
                chkMentrustions.Enabled = true;
            }

            var schedules = w.Schedules.GetFiredSchedulesForDay(date);
            if (schedules.Count > 0)
            {
                chkSchedules.Checked = true;
                editScheduleControl.SetSchedules(schedules);
            }
            else
            {
                chkSchedules.Checked = false;
            }

            dayEditControl.BBT = w.BBT.GetBBTString(date);

            string note = w.Notes[date];
            dayEditControl.Note = (!note.Contains("\r\n")) ? note.Replace("\n", "\r\n") : note;

            dayEditControl.HadSex = w.HadSexList[date];

            dayEditControl.Health = w.Health[date];

            dayEditControl.CurrentCF = w.CFs[date];

            editScheduleControl.InitialDate = date;

            initialData = CollectDayData();
        }

        private DayData CollectDayData()
        {
            return new DayData()
            {
                BBT = dayEditControl.BBT,
                HadSex = dayEditControl.HadSex,
                Health = dayEditControl.Health,
                Note = dayEditControl.Note,
                CF = dayEditControl.CurrentCF,

                HasMenstr = chkMentrustions.Checked,
                MenstrLength = mensesEditControl.Length,
                Egesta = mensesEditControl.EgestaSliderValue,

                Schedules = editScheduleControl.GetAllSchedules(),
            };
        }

        private bool ValidateData()
        {
            return dayEditControl.ValidateBBT();
        }

        /// <summary>
        /// Shift active day.
        /// </summary>
        /// <param name="days">Amount of days from current to shift. Can be either negative or positive.</param>
        private void Rotate(int days)
        {
            if (DataChanged)
            {
                if (!ValidateData()) return;
                if (!SaveData()) return;
                Program.ApplicationForm.UpdateDayInformationIfFocused(date);
            }

            date = date.AddDays(days);
            LoadForm();
            dayEditControl.SetFocusTo(dayEditControl.LastFocus);
        }

        private void SetFocusTo(DayEditFocus focusTo)
        {
            switch (focusTo)
            {
                case DayEditFocus.Length:
                    mensesEditControl.Highlight();
                    break;
                default:
                    mensesEditControl.UnHighlight();
                    dayEditControl.SetFocusTo(focusTo);
                    break;
            }
        }

        private void ShowMenses(bool show)
        {
            if (show)
            {
                //this.Width = mensesEditControl.Location.X + mensesEditControl.Size.Width + 10;
                this.flowLayoutPanel.Controls.Add(this.mensesEditControl);
                chkMentrustions.Image = WomenCalendar.Properties.Resources.dropNot_Image;
                chkMentrustions.Text = "<<          <<";
                this.mensesEditControl.Visible = true;
                if (initialData != null && initialData.HasMenstr != true)
                { // this is first time we expand the dialog, this means user want to add new menstr. day.
                    SetFocusTo(DayEditFocus.Length);
                }
            }
            else
            {
                //this.Width = chkMentrustions.Location.X + chkMentrustions.Size.Width + 10;
                this.flowLayoutPanel.Controls.Remove(this.mensesEditControl);
                chkMentrustions.Image = WomenCalendar.Properties.Resources.drop_Image;
                chkMentrustions.Text = ">>          >>";
                this.mensesEditControl.Visible = false;
            }
        }

        private void ShowSchedules(bool show)
        {
            if (show)
            {
                this.Visible = false;
                //this.Width = mensesEditControl.Location.X + mensesEditControl.Size.Width + 10;
                this.flowLayoutPanel.Controls.Add(this.editScheduleControl);
                this.flowLayoutPanel.Controls.SetChildIndex(this.editScheduleControl, 0);
                chkSchedules.Image = WomenCalendar.Properties.Resources.calendarEdit_Image;
                chkSchedules.Text = ">>          >>";
                this.editScheduleControl.Visible = true;
                var loc = this.Location;
                this.Location = new Point(loc.X - this.editScheduleControl.Width, loc.Y);
                this.Visible = true;
            }
            else
            {
                this.Visible = false;
                //this.Width = chkMentrustions.Location.X + chkMentrustions.Size.Width + 10;
                this.flowLayoutPanel.Controls.Remove(this.editScheduleControl);
                chkSchedules.Image = WomenCalendar.Properties.Resources.calendarEdit_Image;
                chkSchedules.Text = "<<          <<";
                this.editScheduleControl.Visible = false;
                var loc = this.Location;
                this.Location = new Point(loc.X + this.editScheduleControl.Width, loc.Y);
                this.Visible = true;
            }
        }

        private bool DataChanged
        {
            get
            {
                return !initialData.Equals(CollectDayData());
            }
        }

        private void chkMentrustions_CheckedChanged(object sender, EventArgs e)
        {
            ttButton.Hide(chkMentrustions);
            ttButton.SetToolTip(chkMentrustions,
                chkMentrustions.Checked ? TEXT.Get["Cancel_these_menses"] : TEXT.Get["Set_day_as_menses_start"]);

            ShowMenses(chkMentrustions.Checked);
        }

        private void DayEditForm_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void btnPrevDay_Click(object sender, EventArgs e)
        {
            Rotate(-1);
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            Rotate(1);
        }

        private void DayEditForm_Shown(object sender, EventArgs e)
        {
            SetFocusTo(defaultFocus);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AcceptAction();
        }

        private void chkSchedules_CheckedChanged(object sender, EventArgs e)
        {
            ShowSchedules(chkSchedules.Checked);
        }
    }
}
