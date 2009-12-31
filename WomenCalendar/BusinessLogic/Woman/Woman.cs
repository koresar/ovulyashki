using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.BZip2;

namespace WomenCalendar
{
    [XmlRoot("Woman")]
    public class Woman : ICloneable
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public MenstruationsCollection Menstruations { get; set; }

        public ConceptionsCollection Conceptions { get; set; }

        [XmlIgnore()]
        public string AssociatedFile { get; set; }

        public bool UseManualPeriodLength { get; set; }

        public bool AllwaysAskPassword { get; set; }

        [XmlIgnore()]
        private int averagePeriodLength;
        /// <summary>
        /// This is what we calculate. This is average amount of days between menstruations. Usual from 21 to 35 days.
        /// </summary>
        [XmlElement("AveragePeriodLength")]
        public int AveragePeriodLength
        {
            get { return averagePeriodLength; }
            set
            {
                int prevLength = averagePeriodLength;
                averagePeriodLength = value;
                if (prevLength != averagePeriodLength && AveragePeriodLengthChanged != null)
                {
                    AveragePeriodLengthChanged();
                }
            }
        }

        /// <summary>
        /// This is what user choose to use as period for egesting between menstruation. Usual 21 - 35 days.
        /// </summary>
        public int ManualPeriodLength { get; set; }

        public delegate void AveragePeriodLengthChangedDelegate();
        public event AveragePeriodLengthChangedDelegate AveragePeriodLengthChanged;

        /// <summary>
        /// This is the default length of woman egesting. Usual 3-5 days. It is set on the left side of the window
        /// by the user.
        /// </summary>
        public int DefaultMenstruationLength { get; set; }

        public NotesCollection Notes { get; set; }

        public BBTCollection BBT { get; set; }

        public HadSexCollection HadSexList { get; set; }

        public HealthCollection Health { get; set; }

        public CFCollection CFs { get; set; }

        public SchedulesCollection Schedules { get; set; }

        public Woman()
        {
            Notes = new NotesCollection();
            BBT = new BBTCollection();
            HadSexList = new HadSexCollection();
            Health = new HealthCollection();
            CFs = new CFCollection();
            DefaultMenstruationLength = 5;
            Menstruations = new MenstruationsCollection();
            Conceptions = new ConceptionsCollection();
            ManualPeriodLength = 28;
            averagePeriodLength = 28;
            Name = Environment.UserName;
        }

        public static Woman ReadFrom(string path)
        {
            Woman w = null;
            var fs = new FileStream(path, FileMode.Open);
            try
            {
                var s = new BZip2InputStream(fs);
                w = (Woman)(new XmlSerializer(typeof(Woman)).Deserialize(s));
                s.Close();
            }
            catch (BZip2Exception)
            { // old file type support
                fs.Seek(0, SeekOrigin.Begin);
                w = (Woman)(new XmlSerializer(typeof(Woman)).Deserialize(fs));
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

        public bool AddMenstruationDay(DateTime date)
        {
            if (Menstruations.IsMenstruationDay(date))
            {
                return false;
            }

            if (Menstruations.Add(date, DefaultMenstruationLength))
            {
                AveragePeriodLength = Menstruations.CalculateAveragePeriodLength();
                return true;
            }
            return false;
        }

        public bool IsPredictedAsMenstruationDay(DateTime date)
        {
            if (Menstruations.Count == 0)
            {
                return false;
            }

            MenstruationPeriod lastPeriod = Menstruations.Last;
            if (date <= lastPeriod.StartDay.AddDays(DefaultMenstruationLength))
            {
                return false;
            }

            int daysBetween = ((date - lastPeriod.StartDay).Days) % ManualPeriodLength;
            return daysBetween < DefaultMenstruationLength;
        }

        public bool IsPredictedAsSafeSexDay(DateTime date)
        {
            if (Menstruations.Count == 0)
            {
                return false;
            }

            MenstruationPeriod lastPeriod = Menstruations.Last;
            if (date <= lastPeriod.StartDay.AddDays(DefaultMenstruationLength))
            {
                return false;
            }

            int daysBetween = ((date - lastPeriod.StartDay).Days) % ManualPeriodLength;
            return (daysBetween >= (ManualPeriodLength - 5) && daysBetween <= (ManualPeriodLength + 4)) || (daysBetween <= 4);
        }

        public bool IsPredictedAsOvulationDay(DateTime date)
        {
            if (Menstruations.Count == 0 ||
                Menstruations.IsMenstruationDay(date) ||
                date < Menstruations.First.StartDay)
            {
                return false;
            }

            return GetClosestOvulationDay(date) == date;
        }

        public DateTime GetClosestOvulationDay(DateTime date)
        {
            if (Menstruations.Count == 0) { throw new Exception("No menstruations. The method call prohibited."); }
            MenstruationPeriod lastPeriod = Menstruations.Last;
            MenstruationPeriod firstPeriod = Menstruations.First;
            if (Menstruations.Count != 1 && date < lastPeriod.StartDay && date > firstPeriod.StartDay)
            { // the ov. date is already calculated. Let's return it.
                MenstruationPeriod resultPeriodBefore = null;
                MenstruationPeriod resultPeriodAfter = null;
                foreach (MenstruationPeriod period in Menstruations)
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

            int cycles = (date - lastPeriod.StartDay).Days / ManualPeriodLength;
            DateTime lastCycleFirstDay = lastPeriod.StartDay.AddDays((cycles + 1) * ManualPeriodLength);
            return OvulationDetector.EstimateOvulationDate(this, lastCycleFirstDay);
        }

        public bool IsPredictedAsBoyDay(DateTime date)
        {
            if (Menstruations.Count == 0 || 
                Menstruations.IsMenstruationDay(date) || 
                date < Menstruations.First.StartDay)
            {
                return false;
            }

            int days = (date - GetClosestOvulationDay(date)).Days;
            return 1 <= days && days <= 4;
        }

        public bool IsPredictedAsGirlDay(DateTime date)
        {
            if (Menstruations.Count == 0 ||
                Menstruations.IsMenstruationDay(date) ||
                date < Menstruations.First.StartDay)
            {
                return false;
            }

            int days = (GetClosestOvulationDay(date) - date).Days;
            return 1 <= days && days <= 4;
        }

        public bool IsConceptionDay(DateTime date)
        {
            return Conceptions.IsConceptionDay(date);
        }

        public bool IsPregnancyDay(DateTime date)
        {
            ConceptionPeriod period = Conceptions.GetConceptionByDate(date);
            return period != null;
        }

        public bool RemoveMenstruationDay(DateTime date)
        {
            if (!Menstruations.IsMenstruationDay(date))
            {
                return false;
            }

            MenstruationPeriod removedPeriod = Menstruations.GetPeriodByDate(date);
            if (Menstruations.Remove(date))
            {
                if (removedPeriod.HasPregnancy)
                {
                    MenstruationPeriod period = Menstruations.GetClosestPeriodBeforeDay(date);
                    if (period != null)
                    {
                        period.HasPregnancy = true;
                    }
                }
                AveragePeriodLength = Menstruations.CalculateAveragePeriodLength();
                return true;
            }
            return false;
        }

        public bool AddConceptionDay(DateTime date)
        {
            if (!Conceptions.IsPregnancyDay(date))
            {
                if (date > DateTime.Today)
                {
                    if (MessageBox.Show(TEXT.Get["Future_pregnancy_day"] + TEXT.Get["Are_you_sure_capital"],
                        TEXT.Get["What_a_situation"], MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                ConceptionPeriod concPeriod = Conceptions.GetConceptionAfterDate(date);
                if (concPeriod != null && (concPeriod.StartDay - date).Days <= ConceptionPeriod.StandardLength)
                {
                    MessageBox.Show(
                        TEXT.Get.Format("Already_pregnant_after", (concPeriod.StartDay - date).Days.ToString()), 
                        TEXT.Get["No_no_no"]);
                    return false;
                }

                MenstruationPeriod nextMenses = Menstruations.GetClosestPeriodAfterDay(date);
                if (nextMenses != null)
                {
                    if (MessageBox.Show(TEXT.Get["Have_menses_after_pregn"] + TEXT.Get["Are_you_sure_capital"],
                        TEXT.Get["What_a_situation"], MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                MenstruationPeriod prevMenses = Menstruations.GetPeriodByDate(date);
                if (prevMenses != null)
                {
                    if (MessageBox.Show(TEXT.Get["Pregn_on_menses"] + TEXT.Get["Are_you_sure_capital"],
                        TEXT.Get["What_a_situation"], MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return false;
                    }
                }
                else
                {
                    prevMenses = Menstruations.GetClosestPeriodBeforeDay(date);
                }

                if (prevMenses != null)
                {
                    prevMenses.HasPregnancy = true;
                    return Conceptions.Add(prevMenses.StartDay);
                }
                else
                {
                    return Conceptions.Add(date);
                }
            }
            return false;
        }

        public bool RemoveConceptionDay(DateTime date)
        {
            MenstruationPeriod period = Menstruations.GetClosestPeriodBeforeDay(date);
            if (period != null)
            {
                period.HasPregnancy = false;
            }
            return Conceptions.Remove(date);
        }

        public bool RemovePregnancy(DateTime date)
        {
            MenstruationPeriod period = Menstruations.GetClosestPeriodBeforeDay(date);
            if (period != null)
            {
                period.HasPregnancy = false;
            }
            return Conceptions.RemoveByDate(date);
        }

        public bool AddNote(DateTime date, string text)
        {
            Notes[date] = text;
            return true;
        }

        public bool RemoveNote(DateTime date)
        {
            return Notes.Remove(date);
        }

        public OneDayInfo GetOneDayInfo(DateTime day)
        {
            return OneDayInfo.GetByDate(this, day);
        }

        public string GenerateDayInfo(DateTime date)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(TEXT.Get["Date_double_colon"]);
            sb.Append(date.ToShortDateString());
            if (date == DateTime.Today)
            {
                sb.Append(TEXT.Get["Today_parentheses"]);
            }

            MenstruationPeriod period = Menstruations.GetPeriodByDate(date);
            if (period != null)
            {
                sb.AppendLine();
                sb.AppendLine(TEXT.Get.Format("This_is_N_menstr_day", (date - period.StartDay).Days + 1));
                sb.Append(EgestasCollection.EgestasNames[period.Egestas[date]]);
            }

            if (IsPregnancyDay(date))
            {
                sb.AppendLine();

                int week = Conceptions.GetPregnancyWeekNumber(date);
                if (week > 0)
                {
                    sb.AppendLine(TEXT.Get.Format("Preg_week_number_N", week));
                }

                if (IsConceptionDay(date))
                {
                    sb.AppendLine(TEXT.Get["Conception_day"]);
                }

                var concPeriod = Conceptions.GetConceptionByDate(date);
                DateTime conceptionDate = concPeriod.StartDay;
                DateTime dateOfBirth = conceptionDate.AddDays(ConceptionPeriod.StandardLength);
                sb.AppendLine(TEXT.Get.Format("Probable_birth_date", dateOfBirth.ToLongDateString()));
                sb.Append(TEXT.Get.Format("Zodiac_will_be", HoroscopDatePair.GetZodiacSignName(dateOfBirth)));

                string gender = string.Empty;
                if (IsPredictedAsBoyDay(concPeriod.StartDay))
                {
                    gender = TEXT.Get["Boy"];
                }
                else if (IsPredictedAsGirlDay(concPeriod.StartDay))
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
                var closestBefore = Menstruations.GetClosestPeriodBeforeDay(date);
                if (closestBefore != null && period == null)
                {
                    sb.AppendLine();
                    sb.Append(TEXT.Get.Format("Cycle_day_number_N", ((date - closestBefore.StartDay).Days + 1).ToString()));
                }

                var closestAfter = Menstruations.GetClosestPeriodAfterDay(date);
                closestBefore = Menstruations.GetClosestPeriodBeforeDay(date.AddDays(1));
                if (closestBefore != null && closestAfter != null)
                {
                    int days = (closestAfter.StartDay - closestBefore.StartDay).Days;
                    sb.AppendLine();
                    sb.Append(TEXT.Get.Format("Cycle_length_days", days.ToString(), TEXT.GetDaysString(days)));
                }

                if (IsPredictedAsMenstruationDay(date))
                {
                    sb.AppendLine();
                    sb.Append(TEXT.Get["Possible_menst"]);
                }

                if (IsPredictedAsOvulationDay(date))
                {
                    sb.AppendLine();
                    sb.Append(TEXT.Get["Estimated_ovulation_day"]);
                }

                string gender = string.Empty;
                if (IsPredictedAsBoyDay(date))
                {
                    sb.AppendLine();
                    sb.Append(TEXT.Get["Boy_conception_day"]);
                }
                else if (IsPredictedAsGirlDay(date))
                {
                    sb.AppendLine();
                    sb.Append(TEXT.Get["Boy_conception_day"]);
                }

                if (HadSexList.ContainsKey(date))
                {
                    DateTime dateOfBirth = date.AddDays(ConceptionPeriod.StandardLength);
                    sb.AppendLine();
                    sb.Append(TEXT.Get.Format("If_conceive_info", 
                        dateOfBirth.ToLongDateString(), HoroscopDatePair.GetZodiacSignName(dateOfBirth)));
                }
            }

            // got to be last
            string text;
            if (Notes.TryGetValue(date, out text))
            {
                sb.AppendLine();
                sb.Append(TEXT.Get["Note_semicolon"]);
                sb.Append(text);
            }

            return sb.ToString();
        }

        #region ICloneable Members

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
            // w.Schedules = this.Schedules.Clone(); TODO!!! CLONING NOT IMPLEMENTED!!!!!!
            return w;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (!(obj is Woman)) return false;
            Woman w = obj as Woman;

            bool equal = true;
            equal &= w.AllwaysAskPassword.Equals(this.AllwaysAskPassword);
            equal &= w.AssociatedFile.Equals(this.AssociatedFile);
            equal &= w.averagePeriodLength.Equals(this.averagePeriodLength);
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
    }
}
