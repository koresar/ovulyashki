using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WomenCalendar
{
    public static class MonthAppearance
    {
        public static Brush MonthHeaderBrush = new LinearGradientBrush(new Point(0, 0), new Point(120, 0),
            Color.FromArgb(248, 153, 250), Color.White) { WrapMode = WrapMode.TileFlipXY };
        public static Brush WeekDayHeaderBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, 16),
            Color.RoyalBlue, ControlPaint.LightLight(Color.RoyalBlue)) { WrapMode = WrapMode.TileFlipXY, };
        public static Brush MonthNameBrush = Brushes.Purple;
        public static Brush WeekDayTextBrush = Brushes.White;
        public static Brush WeekDayHolidayTextBrush = Brushes.Salmon;
        public static Pen MonthEdgePen = new Pen(Brushes.Gray, 6);
        public static Pen WeekDayEdgePen = new Pen(Brushes.White, 1);
        public static Pen TodayEdgePen = new Pen(Brushes.Blue, 6);
    }

    public class DayCellAppearance
    {
        public static Pen FocusEdgePen = new Pen(Brushes.Red, 2);
        public static Pen TodayEdgePen = new Pen(Brushes.Blue, 2);
        public static Pen EdgePen = Pens.Black;
        public static Brush DayNumberBrush = Brushes.RoyalBlue;
        public static Brush PregnancyWeekNumberBrush = Brushes.Green;
        public static Brush MenstruationPredictionBrush = Brushes.Red;
        public static Brush HadSexBrush = Brushes.Red;

        // Back colors
        [XmlIgnore]
        public Color BackConceptionDay = Color.DeepSkyBlue;
        [XmlIgnore]
        public Color BackPregnancyDay = Color.LightCyan;
        [XmlIgnore]
        public Color BackMenstruationDay = Color.LightPink;
        [XmlIgnore]
        public Color BackPredictedMenstruationDay = ControlPaint.LightLight(Color.LightPink);
        [XmlIgnore]
        public Color BackOvulationDay = Color.Gold;
        [XmlIgnore]
        public Color BackSafeSex = Color.LightGreen;
        [XmlIgnore]
        public Color BackEmpty = Color.White;
        [XmlIgnore]
        public Color BackHadSex = Color.White;
        [XmlIgnore]
        public Color BackHaveNote = Color.White;
        [XmlIgnore]
        public Color BackBoyDay = Color.White;
        [XmlIgnore]
        public Color BackGirlDay = Color.White;

        [XmlElement("BackConceptionDay")]
        public string ConceptionDay
        {
            get { return ColorTranslator.ToHtml(BackConceptionDay); }
            set { BackConceptionDay = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("BackPregnancyDay")]
        public string PregnancyDay
        {
            get { return ColorTranslator.ToHtml(BackPregnancyDay); }
            set { BackPregnancyDay = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("BackMenstruationDay")]
        public string MenstruationDay
        {
            get { return ColorTranslator.ToHtml(BackMenstruationDay); }
            set { BackMenstruationDay = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("BackPredictedMenstruationDay")]
        public string PredictedMenstruationDay
        {
            get { return ColorTranslator.ToHtml(BackPredictedMenstruationDay); }
            set { BackPredictedMenstruationDay = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("BackOvulationDay")]
        public string OvulationDay
        {
            get { return ColorTranslator.ToHtml(BackOvulationDay); }
            set { BackOvulationDay = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("BackSafeSex")]
        public string SafeSex
        {
            get { return ColorTranslator.ToHtml(BackSafeSex); }
            set { BackSafeSex = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("BackEmpty")]
        public string Empty
        {
            get { return ColorTranslator.ToHtml(BackEmpty); }
            set { BackEmpty = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("BackHadSex")]
        public string HadSex
        {
            get { return ColorTranslator.ToHtml(BackHadSex); }
            set { BackHadSex = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("BackHaveNote")]
        public string HaveNote
        {
            get { return ColorTranslator.ToHtml(BackHaveNote); }
            set { BackHaveNote = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("BackBoyDay")]
        public string BoyDay
        {
            get { return ColorTranslator.ToHtml(BackBoyDay); }
            set { BackBoyDay = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("BackGirlDay")]
        public string GirlDay
        {
            get { return ColorTranslator.ToHtml(BackGirlDay); }
            set { BackGirlDay = ColorTranslator.FromHtml(value); }
        }


        public bool SetColor(string colorID, Color color)
        {
            foreach (var prop in this.GetType().GetFields())
            {
                if (prop.Name == colorID)
                {
                    prop.SetValue(this, color);
                    return true;
                }
            }
            return false;
        }
    }
}
