using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace WomenCalendar
{
    [XmlRoot("Woman")]
    public class Woman
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public MenstruationsCollection Menstruations { get; set; }

        public ConceptionsCollection Conceptions { get; set; }

        [XmlIgnore()]
        public bool IsNew { get; set; }

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

        public Woman()
        {
            Notes = new NotesCollection();
            BBT = new BBTCollection();
            HadSexList = new HadSexCollection();
            Health = new HealthCollection();
            DefaultMenstruationLength = 4;
            Menstruations = new MenstruationsCollection();
            Conceptions = new ConceptionsCollection();
            ManualPeriodLength = 28;
            averagePeriodLength = 28;
        }

        public static bool SaveTo(Woman w, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            new XmlSerializer(w.GetType()).Serialize(fs, w);
            fs.Close();
            w.AssociatedFile = path;
            return true;
        }

        public static Woman ReadFrom(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            Woman w = (Woman)(new XmlSerializer(typeof(Woman)).Deserialize(fs));
            fs.Close();
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
            if (Menstruations.Count == 0)
            {
                return false;
            }

            MenstruationPeriod lastPeriod = Menstruations.Last;
            MenstruationPeriod firstPeriod = Menstruations.First;
            if (Menstruations.Count != 1 && date < lastPeriod.StartDay && date > firstPeriod.StartDay)
            {
                DateTime ovDay = Menstruations.GetClosestOvulationDay(date);
                return ovDay == date;
            }

            int daysBetween = ((date - lastPeriod.StartDay).Days) % ManualPeriodLength;
            return daysBetween == ManualPeriodLength / 2;
        }

        public bool IsPredictedAsBoyDay(DateTime date)
        {
            if (Menstruations.Count == 0)
            {
                return false;
            }

            MenstruationPeriod lastPeriod = Menstruations.Last;
            MenstruationPeriod firstPeriod = Menstruations.First;
            if (date > lastPeriod.StartDay)
            {
                int daysBetween = ((date - lastPeriod.StartDay).Days) % ManualPeriodLength;
                if (daysBetween > ManualPeriodLength / 2 && daysBetween < ManualPeriodLength / 2 + 5)
                {
                    return true;
                }
            }
            else if (date > firstPeriod.StartDay && date < lastPeriod.StartDay)
            {
                DateTime ovDate = Menstruations.GetClosestOvulationDay(date);
                if (date > ovDate && (date - ovDate).Days < 5)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsPredictedAsGirlDay(DateTime date)
        {
            if (Menstruations.Count == 0 || Menstruations.IsMenstruationDay(date))
            {
                return false;
            }

            MenstruationPeriod lastPeriod = Menstruations.Last;
            MenstruationPeriod firstPeriod = Menstruations.First;
            if (date > lastPeriod.StartDay)
            {
                int daysBetween = ((date - lastPeriod.StartDay).Days) % ManualPeriodLength;
                if ((daysBetween < ManualPeriodLength / 2) && (daysBetween > ManualPeriodLength / 2 - 5))
                {
                    return true;
                }
            }
            else if (date > firstPeriod.StartDay && date < lastPeriod.StartDay)
            {
                DateTime ovDate = Menstruations.GetClosestOvulationDay(date);
                if (date < ovDate && (ovDate - date).Days < 5)
                {
                    return true;
                }
            }

            return false;
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
                    if (MessageBox.Show("Этот день еще не наступил! Врядли ты забеременеешь именно в этот день.\nТЫ УВЕРЕНА В ТОМ ЧТО ДЕЛАЕШЬ?",
                        "Ухты, какая необычная ситуация!", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                ConceptionPeriod concPeriod = Conceptions.GetConceptionAfterDate(date);
                if (concPeriod != null && (concPeriod.StartDay - date).Days <= ConceptionPeriod.StandardLength)
                {
                    MessageBox.Show("У вас уже есть беременность через " +
                        (concPeriod.StartDay - date).Days.ToString() + " дней после этого.", "Не-не-не!");
                    return false;
                }

                MenstruationPeriod mensPeriod = Menstruations.GetClosestPeriodBeforeDay(date);
                if (mensPeriod != null)
                {
                    mensPeriod.HasPregnancy = true;
                }
                return Conceptions.Add(date);
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

            sb.Append("Дата: ");
            sb.Append(date.ToShortDateString());
            if (date == DateTime.Today)
            {
                sb.Append(" (сегодня)");
            }

            MenstruationPeriod period = Menstruations.GetPeriodByDate(date);
            if (period != null)
            {
                sb.AppendLine();
                sb.Append((date - period.StartDay).Days + 1);
                sb.AppendLine("-й день менструашек");
                sb.Append(DayCellPopupControl.EgestasNames[period.Egestas[date]]);
            }

            if (IsPregnancyDay(date))
            {
                sb.AppendLine();

                int week = Conceptions.GetPregnancyWeekNumber(date);
                if (week > 0)
                {
                    sb.Append(week);
                    sb.AppendLine("-я неделя беременности");
                }

                if (IsConceptionDay(date))
                {
                    sb.AppendLine("Это день зачатия! Ура!");
                }

                var concPeriod = Conceptions.GetConceptionByDate(date);
                DateTime conceptionDate = concPeriod.StartDay;
                DateTime dateOfBirth = conceptionDate.AddDays(ConceptionPeriod.StandardLength);
                sb.AppendLine("Ребёнок родится примерно ");
                sb.AppendLine(dateOfBirth.ToLongDateString());
                sb.Append("Знак зодиака будет ");
                sb.Append(HoroscopDatePair.GetZodiacSignName(dateOfBirth));

                string gender = string.Empty;
                if (IsPredictedAsBoyDay(concPeriod.StartDay))
                {
                    gender = "мальчик";
                }
                else if (IsPredictedAsGirlDay(concPeriod.StartDay))
                {
                    gender = "девочка";
                }
                if (!string.IsNullOrEmpty(gender))
                {
                    sb.AppendLine();
                    sb.Append("и предположительно это будет " + gender);
                }
            }
            else
            {
                if (IsPredictedAsMenstruationDay(date))
                {
                    sb.AppendLine();
                    sb.Append("Вероятны менструашки");
                }

                if (IsPredictedAsOvulationDay(date))
                {
                    sb.AppendLine();
                    sb.Append("Это примерный день овуляции");
                }

                string gender = string.Empty;
                if (IsPredictedAsBoyDay(date))
                {
                    gender = "мальчика";
                }
                else if (IsPredictedAsGirlDay(date))
                {
                    gender = "девочки";
                }
                if (!string.IsNullOrEmpty(gender))
                {
                    sb.AppendLine();
                    sb.Append("Более вероятно зачание " + gender);
                }

                if (HadSexList.ContainsKey(date))
                {
                    DateTime dateOfBirth = date.AddDays(ConceptionPeriod.StandardLength);
                    sb.AppendLine();
                    sb.Append("Если ты в этот день зачала ребёнка,\nто он родится примерно ");
                    sb.AppendLine(dateOfBirth.ToLongDateString());
                    sb.Append("Знак зодиака будет ");
                    sb.Append(HoroscopDatePair.GetZodiacSignName(dateOfBirth));
                }
            }

            // got to be last
            string text;
            if (Notes.TryGetValue(date, out text))
            {
                sb.AppendLine();
                sb.Append("Заметка: ");
                sb.Append(text);
            }

            return sb.ToString();
        }
    }
}
