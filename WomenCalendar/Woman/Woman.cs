﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace WomenCalendar
{
    [XmlRoot("Woman")]
    public class Woman
    {
        [XmlIgnore()]
        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [XmlIgnore()]
        private string password = string.Empty;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [XmlIgnore()]
        private MenstruationsCollection _menstruations;
        public MenstruationsCollection Menstruations
        {
            get { return _menstruations; }
            set { _menstruations = value; }
        }

        [XmlIgnore()]
        private bool isNew = true;
        [XmlIgnore()]
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        [XmlIgnore()]
        private string associatedFile;
        [XmlIgnore()]
        public string AssociatedFile
        {
            get { return associatedFile; }
            set { associatedFile = value; }
        }

        [XmlIgnore()]
        private bool useManualPeriodLength;
        [XmlElement("UseManualPeriodLength")]
        public bool UseManualPeriodLength
        {
            get { return useManualPeriodLength; }
            set { useManualPeriodLength = value; }
        }

        [XmlIgnore()]
        private bool allwaysAskPassword;
        [XmlElement("AllwaysAskPassword")]
        public bool AllwaysAskPassword
        {
            get { return allwaysAskPassword; }
            set { allwaysAskPassword = value; }
        }

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

        [XmlIgnore()]
        private int manualPeriodLength;
        /// <summary>
        /// This is what user choose to use as period for egesting between menstruation. Usual 21 - 35 days.
        /// </summary>
        [XmlElement("ManualPeriodLength")]
        public int ManualPeriodLength
        {
            get { return manualPeriodLength; }
            set { manualPeriodLength = value; }
        }

        public delegate void AveragePeriodLengthChangedDelegate();
        public event AveragePeriodLengthChangedDelegate AveragePeriodLengthChanged;

        [XmlIgnore()]
        private int defaultMenstruationLength;
        /// <summary>
        /// This is the default length of woman egesting. Usual 3-5 days. It is set on the left side of the window
        /// by the user.
        /// </summary>
        [XmlElement("DefaultMenstruationLength")]
        public int DefaultMenstruationLength
        {
            get { return defaultMenstruationLength; }
            set { defaultMenstruationLength = value; }
        }

        [XmlIgnore()]
        private NotesCollection notes;
        public NotesCollection Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        [XmlIgnore()]
        private BBTCollection bbt;
        public BBTCollection BBT
        {
            get { return bbt; }
            set { bbt = value; }
        }

        [XmlIgnore()]
        private HadSexCollection hadSex;
        public HadSexCollection HadSexList
        {
            get { return hadSex; }
            set { hadSex = value; }
        }

        [XmlIgnore()]
        private HealthCollection health;
        public HealthCollection Health
        {
            get { return health; }
            set { health = value; }
        }

        public Woman()
        {
            notes = new NotesCollection();
            bbt = new BBTCollection();
            hadSex = new HadSexCollection();
            health = new HealthCollection();
            defaultMenstruationLength = 4;
            _menstruations = new MenstruationsCollection();
            manualPeriodLength = 28;
            averagePeriodLength = 28;
        }

        public static bool SaveTo(Woman w, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            new XmlSerializer(w.GetType()).Serialize(fs, w);
            fs.Close();
            w.associatedFile = path;
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

        public bool RemoveMenstruationDay(DateTime date)
        {
            if (!Menstruations.IsMenstruationDay(date))
            {
                return false;
            }

            if (Menstruations.Remove(date))
            {
                AveragePeriodLength = Menstruations.CalculateAveragePeriodLength();
                return true;
            }
            return false;
        }

        public bool AddNote(DateTime date, string text)
        {
            Notes[date] = text;
            return true;
        }

        public bool RemoveNote(DateTime date)
        {
            return notes.Remove(date);
        }
    }
}
