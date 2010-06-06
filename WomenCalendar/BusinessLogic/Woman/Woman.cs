using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.BZip2;
using WomenCalendar.Forms;

namespace WomenCalendar
{
    /// <summary>
    /// The application main class. Contains all the info about a woman and application business logic. 
    /// Used widely everywhere. Application can operate with only one woman - SDI.
    /// It is also the root of XML serializable woman data file.
    /// </summary>
    [XmlRoot("Woman")]
    public class Woman : ICloneable
    {
        [XmlIgnore()]
        private int averagePeriodLength;

        /// <summary>
        /// The only constructor. Initializes all data with default values.
        /// </summary>
        public Woman()
        {
            this.Notes = new NotesCollection();
            this.BBT = new BBTCollection();
            this.HadSexList = new HadSexCollection();
            this.Health = new HealthCollection();
            this.CFs = new CFCollection();
            this.DefaultMenstruationLength = 5;
            this.Menstruations = new MenstruationsCollection();
            this.Conceptions = new ConceptionsCollection();
            this.Schedules = new SchedulesCollection();
            this.ManualPeriodLength = 28;
            this.averagePeriodLength = 28;
            this.Name = Environment.UserName;
            this.Password = string.Empty;
            this.AssociatedFile = string.Empty;
            this.OvDetector = new OvulationDetector(this);
        }

        /// <summary>
        /// Fired when user changes period between menstructions.
        /// </summary>
        public event Action AveragePeriodLengthChanged;

        /// <summary>
        /// The woman name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Her password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The major data we are operating with - menstructions.
        /// </summary>
        public MenstruationsCollection Menstruations { get; set; }

        /// <summary>
        /// The scheduled actvities.
        /// </summary>
        public SchedulesCollection Schedules { get; set; }

        /// <summary>
        /// The pregnancy data.
        /// </summary>
        public ConceptionsCollection Conceptions { get; set; }

        /// <summary>
        /// The woman file name which was opened.
        /// </summary>
        [XmlIgnore()]
        public string AssociatedFile { get; set; }

        /// <summary>
        /// Indicates if woman want to set period length herself.
        /// </summary>
        public bool UseManualPeriodLength { get; set; }

        /// <summary>
        /// Should we protect the data with password?
        /// </summary>
        public bool AllwaysAskPassword { get; set; }

        /// <summary>
        /// This is what we calculate. This is average amount of days between menstruations. Usual from 21 to 35 days.
        /// </summary>
        [XmlElement("AveragePeriodLength")]
        public int AveragePeriodLength
        {
            get
            {
                return this.averagePeriodLength;
            }

            set
            {
                int prevLength = this.averagePeriodLength;
                this.averagePeriodLength = value;
                if (prevLength != this.averagePeriodLength && this.AveragePeriodLengthChanged != null)
                {
                    this.AveragePeriodLengthChanged();
                }
            }
        }

        /// <summary>
        /// This is what user choose to use as period between menstruation. Usual 21 - 35 days.
        /// </summary>
        public int ManualPeriodLength { get; set; }

        /// <summary>
        /// This is the default length of woman egesting. Usual 5 days.
        /// </summary>
        public int DefaultMenstruationLength { get; set; }

        /// <summary>
        /// Eery day notes.
        /// </summary>
        public NotesCollection Notes { get; set; }

        /// <summary>
        /// The daily Basal Body Temperatures.
        /// </summary>
        public BBTCollection BBT { get; set; }

        /// <summary>
        /// Indicates if there was sex that day.
        /// </summary>
        public HadSexCollection HadSexList { get; set; }

        /// <summary>
        /// The woman daily wellbeing.
        /// </summary>
        public HealthCollection Health { get; set; }

        /// <summary>
        /// The Cervical Fluid type for each day.
        /// </summary>
        public CFCollection CFs { get; set; }

        /// <summary>
        /// The business logic object which predicts the day of the ovulation using different data and methods.
        /// </summary>
        [XmlIgnore]
        public OvulationDetector OvDetector { get; private set; }

        /// <summary>
        /// Read the woman from a file and creates the woman object.
        /// </summary>
        /// <param name="path">Path to the woman file.</param>
        /// <returns>Newly loaded woman object.</returns>
        public static Woman ReadFrom(string path)
        {
            try
            {
                Woman w = null;
                var fs = new FileStream(path, FileMode.Open);
                try
                {
                    var s = new BZip2InputStream(fs);
                    w = (Woman)new XmlSerializer(typeof(Woman)).Deserialize(s);
                    s.Close();
                }
                catch (BZip2Exception)
                { // old file type support
                    fs.Seek(0, SeekOrigin.Begin);
                    w = (Woman)new XmlSerializer(typeof(Woman)).Deserialize(fs);
                }
                finally
                {
                    fs.Close();
                }

                if (w == null)
                {
                    return null;
                }

                w.AssociatedFile = path;
                return w;
            }
            catch (Exception ex)
            {
                ErrorForm.Show(ex);
                return null;
            }
        }

        /// <summary>
        /// Add one more menstruation to the collection with default egestion length.
        /// </summary>
        /// <param name="date">The bleeding start day.</param>
        /// <returns>True if was added.</returns>
        public bool AddMenstruationDay(DateTime date)
        {
            if (this.Menstruations.IsMenstruationDay(date))
            {
                return false;
            }

            if (this.Menstruations.Add(date, this.DefaultMenstruationLength))
            {
                this.AveragePeriodLength = this.Menstruations.CalculateAveragePeriodLength();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if ше is more probable to bleed that day.
        /// </summary>
        /// <param name="date">Day to check.</param>
        /// <returns>True if predicted as bleeding day; otherwise - false.</returns>
        public bool IsPredictedAsMenstruationDay(DateTime date)
        {
            if (this.Menstruations.Count == 0)
            {
                return false;
            }

            MenstruationPeriod lastPeriod = this.Menstruations.Last;
            if (date <= lastPeriod.StartDay.AddDays(this.DefaultMenstruationLength))
            {
                return false;
            }

            int daysBetween = (date - lastPeriod.StartDay).Days % this.ManualPeriodLength;
            return daysBetween < this.DefaultMenstruationLength;
        }

        /// <summary>
        /// Check if the day is predicted as no-conceive day.
        /// </summary>
        /// <param name="date">Day to check.</param>
        /// <returns>True if it is safe to have sex that day.</returns>
        public bool IsPredictedAsSafeSexDay(DateTime date)
        {
            if (this.Menstruations.Count == 0)
            {
                return false;
            }

            MenstruationPeriod lastPeriod = this.Menstruations.Last;
            if (date <= lastPeriod.StartDay.AddDays(this.DefaultMenstruationLength))
            {
                return false;
            }

            int daysBetween = (date - lastPeriod.StartDay).Days % this.ManualPeriodLength;
            return (daysBetween >= (this.ManualPeriodLength - 5) && daysBetween <= (this.ManualPeriodLength + 4)) || (daysBetween <= 4);
        }

        /// <summary>
        /// Check if the day is the ovulation day.
        /// </summary>
        /// <param name="date">Day to check.</param>
        /// <returns>True if ovulation is gonna be that day (probably).</returns>
        public bool IsPredictedAsOvulationDay(DateTime date)
        {
            if (this.Menstruations.Count == 0 ||
                this.Menstruations.IsMenstruationDay(date) ||
                date < this.Menstruations.First.StartDay)
            {
                return false;
            }

            return this.GetClosestOvulationDay(date) == date;
        }

        /// <summary>
        /// Finds the nearest (before or after) ovulation date. This function is necessary to find boy/girl
        /// conception days.
        /// </summary>
        /// <param name="date">Day to search near.</param>
        /// <returns>The ovulation date vlosest to the givem date.</returns>
        public DateTime GetClosestOvulationDay(DateTime date)
        {
            if (this.Menstruations.Count == 0)
            {
                throw new InvalidOperationException("No menstruations. The method call prohibited.");
            }

            MenstruationPeriod lastPeriod = this.Menstruations.Last;
            MenstruationPeriod firstPeriod = this.Menstruations.First;
            if (this.Menstruations.Count != 1 && date < lastPeriod.StartDay && date > firstPeriod.StartDay)
            { // the ov. date is already calculated. Let's return it.
                MenstruationPeriod resultPeriodBefore = null;
                MenstruationPeriod resultPeriodAfter = null;
                foreach (MenstruationPeriod period in this.Menstruations)
                {
                    if (period.StartDay < date && (resultPeriodBefore == null || period.StartDay > resultPeriodBefore.StartDay))
                    {
                        resultPeriodBefore = period;
                    }

                    if (period.StartDay >= date && (resultPeriodAfter == null || period.StartDay < resultPeriodAfter.StartDay))
                    {
                        resultPeriodAfter = period;
                    }
                }

                return resultPeriodBefore.GetOvulationDate(this);
            }

            // We are trying to get ovulation of the day without any menstruation near.
            int cycles = (date - lastPeriod.StartDay).Days / this.ManualPeriodLength;
            DateTime lastCycleFirstDay = lastPeriod.StartDay.AddDays((cycles + 1) * this.ManualPeriodLength);
            return this.OvDetector.EstimateOvulationDate(lastCycleFirstDay);
        }

        /// <summary>
        /// Check if the date is good to conceive a boy.
        /// </summary>
        /// <param name="date">Day to evaluate.</param>
        /// <returns>True if it is more likely to conceive a boy that day.</returns>
        public bool IsPredictedAsBoyDay(DateTime date)
        {
            if (this.Menstruations.Count == 0 ||
                this.Menstruations.IsMenstruationDay(date) ||
                date < this.Menstruations.First.StartDay)
            {
                return false;
            }

            int days = (date - this.GetClosestOvulationDay(date)).Days;
            return 1 <= days && days <= 4;
        }

        /// <summary>
        /// Check if the date is good to conceive a girl.
        /// </summary>
        /// <param name="date">Day to evaluate.</param>
        /// <returns>True if it is more likely to conceive a girl that day.</returns>
        public bool IsPredictedAsGirlDay(DateTime date)
        {
            if (this.Menstruations.Count == 0 ||
                this.Menstruations.IsMenstruationDay(date) ||
                date < this.Menstruations.First.StartDay)
            {
                return false;
            }

            int days = (this.GetClosestOvulationDay(date) - date).Days;
            return 1 <= days && days <= 4;
        }

        /// <summary>
        /// Check if it is a start day of pregnancy.
        /// </summary>
        /// <param name="date">Date to check.</param>
        /// <returns>True if the pregnancy start here.</returns>
        public bool IsConceptionDay(DateTime date)
        {
            return this.Conceptions.IsConceptionDay(date);
        }

        /// <summary>
        /// Check if the baby is carried by woman that day.
        /// </summary>
        /// <param name="date">Date to check.</param>
        /// <returns>True if the baby is in the woman's stomak at the day.</returns>
        public bool IsPregnancyDay(DateTime date)
        {
            ConceptionPeriod period = this.Conceptions.GetConceptionByDate(date);
            return period != null;
        }

        /// <summary>
        /// Remove the menstruation by any of it egestion day.
        /// </summary>
        /// <param name="date">Day of egestion.</param>
        /// <returns>True if successfully removed the menstruation cycle.</returns>
        public bool RemoveMenstruationDay(DateTime date)
        {
            if (!this.Menstruations.IsMenstruationDay(date))
            {
                return false;
            }

            MenstruationPeriod removedPeriod = this.Menstruations.GetPeriodByDate(date);
            if (this.Menstruations.Remove(date))
            {
                if (removedPeriod.HasPregnancy)
                {
                    MenstruationPeriod period = this.Menstruations.GetClosestPeriodBeforeDay(date);
                    if (period != null)
                    {
                        period.HasPregnancy = true;
                    }
                }

                this.AveragePeriodLength = this.Menstruations.CalculateAveragePeriodLength();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try to add pregnancy. Asks user any questions he has to be asked.
        /// </summary>
        /// <param name="date">The clicked day.</param>
        /// <returns>True if added. Otherwise false.</returns>
        public bool AddConceptionDay(DateTime date)
        {
            if (!this.Conceptions.IsPregnancyDay(date))
            {
                if (date > DateTime.Today)
                {
                    if (MessageBox.Show(
                        TEXT.Get["Future_pregnancy_day"] + TEXT.Get["Are_you_sure_capital"],
                        TEXT.Get["What_a_situation"], 
                        MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                ConceptionPeriod concPeriod = this.Conceptions.GetConceptionAfterDate(date);
                if (concPeriod != null && (concPeriod.StartDay - date).Days <= ConceptionPeriod.StandardLength)
                {
                    MessageBox.Show(
                        TEXT.Get.Format("Already_pregnant_after", (concPeriod.StartDay - date).Days.ToString()), 
                        TEXT.Get["No_no_no"]);
                    return false;
                }

                MenstruationPeriod nextMenses = this.Menstruations.GetClosestPeriodAfterDay(date);
                if (nextMenses != null)
                {
                    if (MessageBox.Show(
                        TEXT.Get["Have_menses_after_pregn"] + TEXT.Get["Are_you_sure_capital"],
                        TEXT.Get["What_a_situation"], 
                        MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                MenstruationPeriod prevMenses = this.Menstruations.GetPeriodByDate(date);
                if (prevMenses != null)
                {
                    if (MessageBox.Show(
                        TEXT.Get["Pregn_on_menses"] + TEXT.Get["Are_you_sure_capital"],
                        TEXT.Get["What_a_situation"], 
                        MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return false;
                    }
                }
                else
                {
                    prevMenses = this.Menstruations.GetClosestPeriodBeforeDay(date);
                }

                if (prevMenses != null && Math.Abs((date - prevMenses.LastDay).Days) <= this.ManualPeriodLength)
                { // The pregnancy must start from last cycle start.
                  // Also we must be sure is was not so long time ago since last cycle.
                    prevMenses.HasPregnancy = true;
                    return this.Conceptions.Add(prevMenses.StartDay);
                }
                else
                {
                    return this.Conceptions.Add(date);
                }
            }

            return false;
        }

        /// <summary>
        /// Remove a pregnancy by its any date.
        /// </summary>
        /// <param name="date">Date of pregnancy period.</param>
        /// <returns>True if remover; otherwise false.</returns>
        public bool RemovePregnancy(DateTime date)
        {
            MenstruationPeriod period = this.Menstruations.GetClosestPeriodBeforeDay(date);
            if (period != null)
            {
                period.HasPregnancy = false;
            }

            return this.Conceptions.RemoveByDate(date);
        }

        /// <summary>
        /// Create the information data about one day.
        /// </summary>
        /// <param name="day">Day to obtain data for.</param>
        /// <returns>A day information structure.</returns>
        public OneDayInfo GetOneDayInfo(DateTime day)
        {
            return OneDayInfo.GetByDate(this, day);
        }

        /// <summary>
        /// Create the string which is shown to user as the full day description. May have lots of text.
        /// </summary>
        /// <param name="date">The day to describe.</param>
        /// <returns>The day descruption.</returns>
        public string GenerateDayInfo(DateTime date)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(TEXT.Get["Date_double_colon"]);
            sb.Append(date.ToShortDateString());
            if (date == DateTime.Today)
            {
                sb.Append(TEXT.Get["Today_parentheses"]);
            }

            MenstruationPeriod period = this.Menstruations.GetPeriodByDate(date);
            if (period != null)
            {
                sb.AppendLine();
                sb.AppendLine(TEXT.Get.Format("This_is_N_menstr_day", (date - period.StartDay).Days + 1));
                sb.Append(EgestasCollection.EgestasNames[period.Egestas[date]]);
            }

            if (this.IsPregnancyDay(date))
            {
                sb.AppendLine();

                int week = this.Conceptions.GetPregnancyWeekNumber(date);
                if (week > 0)
                {
                    sb.AppendLine(TEXT.Get.Format("Preg_week_number_N", week));
                }

                if (this.IsConceptionDay(date))
                {
                    sb.AppendLine(TEXT.Get["Conception_day"]);
                }

                var concPeriod = this.Conceptions.GetConceptionByDate(date);
                DateTime conceptionDate = concPeriod.StartDay;
                DateTime dateOfBirth = conceptionDate.AddDays(ConceptionPeriod.StandardLength);
                sb.AppendLine(TEXT.Get.Format("Probable_birth_date", dateOfBirth.ToLongDateString()));
                sb.Append(TEXT.Get.Format("Zodiac_will_be", HoroscopDatePair.GetZodiacSignName(dateOfBirth)));

                string gender = string.Empty;
                if (this.IsPredictedAsBoyDay(concPeriod.StartDay))
                {
                    gender = TEXT.Get["Boy"];
                }
                else if (this.IsPredictedAsGirlDay(concPeriod.StartDay))
                {
                    gender = TEXT.Get["Girl"];
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    sb.AppendLine();
                    sb.Append(TEXT.Get.Format("Child_gender_will_be", gender));
                }
            }
            else
            {
                var closestBefore = this.Menstruations.GetClosestPeriodBeforeDay(date);
                if (closestBefore != null && period == null)
                {
                    sb.AppendLine();
                    sb.Append(TEXT.Get.Format("Cycle_day_number_N", ((date - closestBefore.StartDay).Days + 1).ToString()));
                }

                var closestAfter = this.Menstruations.GetClosestPeriodAfterDay(date);
                closestBefore = this.Menstruations.GetClosestPeriodBeforeDay(date.AddDays(1));
                if (closestBefore != null && closestAfter != null)
                {
                    int days = (closestAfter.StartDay - closestBefore.StartDay).Days;
                    sb.AppendLine();
                    sb.Append(TEXT.Get.Format("Cycle_length_days", days.ToString(), TEXT.GetDaysString(days)));
                }

                if (this.IsPredictedAsMenstruationDay(date))
                {
                    sb.AppendLine();
                    sb.Append(TEXT.Get["Possible_menst"]);
                }

                if (this.IsPredictedAsOvulationDay(date))
                {
                    sb.AppendLine();
                    sb.Append(TEXT.Get["Estimated_ovulation_day"]);
                }

                string gender = string.Empty;
                if (this.IsPredictedAsBoyDay(date))
                {
                    sb.AppendLine();
                    sb.Append(TEXT.Get["Boy_conception_day"]);
                }
                else if (this.IsPredictedAsGirlDay(date))
                {
                    sb.AppendLine();
                    sb.Append(TEXT.Get["Girl_conception_day"]);
                }

                if (this.HadSexList.ContainsKey(date))
                {
                    DateTime dateOfBirth = date.AddDays(ConceptionPeriod.StandardLength);
                    sb.AppendLine();
                    sb.Append(TEXT.Get.Format(
                        "If_conceive_info", 
                        dateOfBirth.ToLongDateString(), 
                        HoroscopDatePair.GetZodiacSignName(dateOfBirth)));
                }
            }

            // got to be last
            string text;
            if (this.Notes.TryGetValue(date, out text))
            {
                sb.AppendLine();
                sb.Append(TEXT.Get["Note_semicolon"]);
                sb.Append(text);
            }

            return sb.ToString();
        }

        #region ICloneable Members

        /// <summary>
        /// Create the full copy of the woman object.
        /// </summary>
        /// <returns>Fully copied woman.</returns>
        public object Clone()
        {
            var w = new Woman();
            w.AllwaysAskPassword = this.AllwaysAskPassword;
            w.AssociatedFile = this.AssociatedFile;
            w.averagePeriodLength = this.averagePeriodLength;
            w.BBT = this.BBT.Clone() as BBTCollection;
            w.CFs = this.CFs.Clone() as CFCollection;
            w.Conceptions = this.Conceptions.Clone() as ConceptionsCollection;
            w.DefaultMenstruationLength = this.DefaultMenstruationLength;
            w.HadSexList = this.HadSexList.Clone() as HadSexCollection;
            w.Health = this.Health.Clone() as HealthCollection;
            w.ManualPeriodLength = this.ManualPeriodLength;
            w.Menstruations = this.Menstruations.Clone() as MenstruationsCollection;
            w.Name = this.Name;
            w.Notes = this.Notes.Clone() as NotesCollection;
            w.Password = this.Password;
            w.UseManualPeriodLength = this.UseManualPeriodLength;
            return w;
        }

        #endregion

        /// <summary>
        /// Check if two women data is the same. Used to determine if any changes were done to woman data.
        /// </summary>
        /// <param name="obj">Object to compare with.</param>
        /// <returns>True if women data are same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Woman))
            {
                return false;
            }

            Woman w = obj as Woman;

            bool equal = true;
            equal &= w.AllwaysAskPassword.Equals(this.AllwaysAskPassword);
            equal &= w.AveragePeriodLength.Equals(this.AveragePeriodLength);
            equal &= w.BBT.Equals(this.BBT);
            equal &= w.CFs.Equals(this.CFs);
            equal &= w.Conceptions.Equals(this.Conceptions);
            equal &= w.DefaultMenstruationLength.Equals(this.DefaultMenstruationLength);
            equal &= w.HadSexList.Equals(this.HadSexList);
            equal &= w.Health.Equals(this.Health);
            equal &= w.ManualPeriodLength.Equals(this.ManualPeriodLength);
            equal &= w.Menstruations.Equals(this.Menstruations);
            equal &= w.Name.Equals(this.Name);
            equal &= w.Notes.Equals(this.Notes);
            equal &= w.Password.Equals(this.Password);
            equal &= w.UseManualPeriodLength.Equals(this.UseManualPeriodLength);

            return equal;
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <returns>Returns zero.</returns>
        public override int GetHashCode()
        { // the function is useless, but created to remove compilation warning message.
            return 0;
        }
    }
}
