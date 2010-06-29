using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WomenCalendar
{
    /// <summary>
    /// Represent the look of day cells. The colors of cells, etc.
    /// </summary>
    public class DayCellAppearance
    {
        private static Pen focusEdgePen = new Pen(Brushes.Red, 2);
        private static Pen todayEdgePen = new Pen(Brushes.Blue, 2);
        private static Pen edgePen = Pens.Black;
        private static Brush dayNumberBrush = Brushes.RoyalBlue;
        private static Brush pregnancyWeekNumberBrush = Brushes.Green;
        private static Brush menstruationPredictionBrush = Brushes.Red;
        private static Brush hadSexBrush = Brushes.Red;

        [XmlIgnore]
        [ColorInfo(Type = "CustomColors")]
        private Color backConceptionDay = Color.DeepSkyBlue;
        [XmlIgnore]
        [ColorInfo(Type = "CustomColors")]
        private Color backPregnancyDay = Color.LightCyan;
        [XmlIgnore]
        [ColorInfo(Type = "CustomColors")]
        private Color backMenstruationDay = Color.LightPink;
        [XmlIgnore]
        [ColorInfo(Type = "CustomColors")]
        private Color backPredictedMenstruationDay = ControlPaint.LightLight(Color.LightPink);
        [XmlIgnore]
        [ColorInfo(Type = "CustomColors")]
        private Color backOvulationDay = Color.Gold;
        [XmlIgnore]
        [ColorInfo(Type = "CustomColors")]
        private Color backSafeSex = Color.LightGreen;
        [XmlIgnore]
        [ColorInfo(Type = "CustomColors")]
        private Color backEmpty = Color.White;
        [XmlIgnore]
        [ColorInfo(Type = "CustomColors")]
        private Color backHadSex = Color.White;
        [XmlIgnore]
        [ColorInfo(Type = "CustomColors")]
        private Color backHaveNote = Color.White;
        [XmlIgnore]
        [ColorInfo(Type = "CustomColors")]
        private Color backBoyDay = Color.White;
        [XmlIgnore]
        [ColorInfo(Type = "CustomColors")]
        private Color backGirlDay = Color.White;

        /// <summary>
        /// The pen of focused cell.
        /// </summary>
        public static Pen FocusEdgePen
        {
            get { return focusEdgePen; }
        }

        /// <summary>
        /// The pen of today cell.
        /// </summary>
        public static Pen TodayEdgePen
        {
            get { return todayEdgePen; }
        }

        /// <summary>
        /// Pen of all cells.
        /// </summary>
        public static Pen EdgePen
        {
            get { return edgePen; }
        }

        /// <summary>
        /// The brush of month day number.
        /// </summary>
        public static Brush DayNumberBrush
        {
            get { return dayNumberBrush; }
        }

        /// <summary>
        /// Brush of pregnancy week number.
        /// </summary>
        public static Brush PregnancyWeekNumberBrush
        {
            get { return pregnancyWeekNumberBrush; }
        }

        /// <summary>
        /// The predicted/forecasted menstruation symbol '?' brush.
        /// </summary>
        public static Brush MenstruationPredictionBrush
        {
            get { return menstruationPredictionBrush; }
        }

        /// <summary>
        /// The Had Sex symbol 'S' brush.
        /// </summary>
        public static Brush HadSexBrush
        {
            get { return hadSexBrush; }
        }

        /// <summary>
        /// The customizable color of conception cell.
        /// </summary>
        [XmlIgnore]
        public Color BackConceptionDay
        {
            get { return this.backConceptionDay; }
            set { this.backConceptionDay = value; }
        }

        /// <summary>
        /// The customizable color of pregnancy cell.
        /// </summary>
        [XmlIgnore]
        public Color BackPregnancyDay
        {
            get { return this.backPregnancyDay; }
            set { this.backPregnancyDay = value; }
        }

        /// <summary>
        /// The customizable color of menstruation cell.
        /// </summary>
        [XmlIgnore]
        public Color BackMenstruationDay
        {
            get { return this.backMenstruationDay; }
            set { this.backMenstruationDay = value; }
        }

        /// <summary>
        /// The customizable color of forecasted menstruation cell.
        /// </summary>
        [XmlIgnore]
        public Color BackPredictedMenstruationDay
        {
            get { return this.backPredictedMenstruationDay; }
            set { this.backPredictedMenstruationDay = value; }
        }

        /// <summary>
        /// The customizable color of ovulation day cell.
        /// </summary>
        [XmlIgnore]
        public Color BackOvulationDay
        {
            get { return this.backOvulationDay; }
            set { this.backOvulationDay = value; }
        }

        /// <summary>
        /// The customizable color of safe sex day cell.
        /// </summary>
        [XmlIgnore]
        public Color BackSafeSex
        {
            get { return this.backSafeSex; }
            set { this.backSafeSex = value; }
        }

        /// <summary>
        /// The customizable color of information less cell.
        /// </summary>
        [XmlIgnore]
        public Color BackEmpty
        {
            get { return this.backEmpty; }
            set { this.backEmpty = value; }
        }

        /// <summary>
        /// The customizable color of day of the sex cell.
        /// </summary>
        [XmlIgnore]
        public Color BackHadSex
        {
            get { return this.backHadSex; }
            set { this.backHadSex = value; }
        }

        /// <summary>
        /// The customizable color of day with notes cell.
        /// </summary>
        [XmlIgnore]
        public Color BackHaveNote
        {
            get { return this.backHaveNote; }
            set { this.backHaveNote = value; }
        }

        /// <summary>
        /// The customizable color of best day to conceive a boy cell.
        /// </summary>
        [XmlIgnore]
        public Color BackBoyDay
        {
            get { return this.backBoyDay; }
            set { this.backBoyDay = value; }
        }

        /// <summary>
        /// The customizable color of best day to conceive a girl cell.
        /// </summary>
        [XmlIgnore]
        public Color BackGirlDay
        {
            get { return this.backGirlDay; }
            set { this.backGirlDay = value; }
        }

        /// <summary>
        /// The serializable color of conception cell.
        /// </summary>
        [XmlElement("BackConceptionDay")]
        public string ConceptionDay
        {
            get { return ColorTranslator.ToHtml(this.BackConceptionDay); }
            set { this.BackConceptionDay = ColorTranslator.FromHtml(value); }
        }

        /// <summary>
        /// The serializable color of pregnancy cell.
        /// </summary>
        [XmlElement("BackPregnancyDay")]
        public string PregnancyDay
        {
            get { return ColorTranslator.ToHtml(this.BackPregnancyDay); }
            set { this.BackPregnancyDay = ColorTranslator.FromHtml(value); }
        }

        /// <summary>
        /// The serializable color of menstruation cell.
        /// </summary>
        [XmlElement("BackMenstruationDay")]
        public string MenstruationDay
        {
            get { return ColorTranslator.ToHtml(this.BackMenstruationDay); }
            set { this.BackMenstruationDay = ColorTranslator.FromHtml(value); }
        }

        /// <summary>
        /// The serializable color of forecasted menstruation cell.
        /// </summary>
        [XmlElement("BackPredictedMenstruationDay")]
        public string PredictedMenstruationDay
        {
            get { return ColorTranslator.ToHtml(this.BackPredictedMenstruationDay); }
            set { this.BackPredictedMenstruationDay = ColorTranslator.FromHtml(value); }
        }

        /// <summary>
        /// The serializable color of ovulation day cell.
        /// </summary>
        [XmlElement("BackOvulationDay")]
        public string OvulationDay
        {
            get { return ColorTranslator.ToHtml(this.BackOvulationDay); }
            set { this.BackOvulationDay = ColorTranslator.FromHtml(value); }
        }

        /// <summary>
        /// The serializable color of safe sex day cell.
        /// </summary>
        [XmlElement("BackSafeSex")]
        public string SafeSex
        {
            get { return ColorTranslator.ToHtml(this.BackSafeSex); }
            set { this.BackSafeSex = ColorTranslator.FromHtml(value); }
        }

        /// <summary>
        /// The serializable color of information less cell.
        /// </summary>
        [XmlElement("BackEmpty")]
        public string Empty
        {
            get { return ColorTranslator.ToHtml(this.BackEmpty); }
            set { this.BackEmpty = ColorTranslator.FromHtml(value); }
        }

        /// <summary>
        /// The serializable color of day of the sex  cell.
        /// </summary>
        [XmlElement("BackHadSex")]
        public string HadSex
        {
            get { return ColorTranslator.ToHtml(this.BackHadSex); }
            set { this.BackHadSex = ColorTranslator.FromHtml(value); }
        }

        /// <summary>
        /// The serializable color of day with notes cell.
        /// </summary>
        [XmlElement("BackHaveNote")]
        public string HaveNote
        {
            get { return ColorTranslator.ToHtml(this.BackHaveNote); }
            set { this.BackHaveNote = ColorTranslator.FromHtml(value); }
        }

        /// <summary>
        /// The serializable color of best day to conceive a boy cell.
        /// </summary>
        [XmlElement("BackBoyDay")]
        public string BoyDay
        {
            get { return ColorTranslator.ToHtml(this.BackBoyDay); }
            set { this.BackBoyDay = ColorTranslator.FromHtml(value); }
        }

        /// <summary>
        /// The serializable color of best day to conceive a girl cell.
        /// </summary>
        [XmlElement("BackGirlDay")]
        public string GirlDay
        {
            get { return ColorTranslator.ToHtml(this.BackGirlDay); }
            set { this.BackGirlDay = ColorTranslator.FromHtml(value); }
        }

        /// <summary>
        /// The the color value by its name.
        /// </summary>
        /// <param name="colorID">Color string identificator.</param>
        /// <param name="color">The actual color structure.</param>
        /// <returns>True if was set; otherwise false.</returns>
        public bool SetColor(string colorID, Color color)
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                var attribute = prop.GetCustomAttributes(typeof(XmlElementAttribute), false).FirstOrDefault() as XmlElementAttribute;
                if (attribute != null && attribute.ElementName == colorID)
                {
                    prop.SetValue(this, ColorTranslator.ToHtml(color), null);
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Метод возвращает целочисленный массив, в котором содержаться предопределенные цвета. 
        /// Целочисленный массив специально преобразован для записи в поле ColorDialog.CustomColors 
        /// </summary>
        /// <returns>int[] - в котором содержаться предопределенные цвета </returns>
        public int[] GetAllCurrentColorsAsArgb()
        {
            int[] mm = new int[this.SizeColor()];
            int i = 0;
            foreach (var prop in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var attribute = prop.GetCustomAttributes(typeof(ColorInfoAttribute), false).FirstOrDefault() as ColorInfoAttribute;
                if (attribute != null && attribute.Type == "CustomColors")
                {
                    Color col = (Color)prop.GetValue(this);
                    mm[i] = (col.B << 16) + (col.G << 8) + col.R;
                    i++;
                }
            }

            return mm;
        }
        
        /// <summary>
        /// Метод подсчитывает количество переменных цвета.
        /// </summary>
        /// <returns>количество переменных цвета</returns>
        private int SizeColor()
        {
            int i = 0;
            foreach (var prop in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var attribute = prop.GetCustomAttributes(typeof(ColorInfoAttribute), false).FirstOrDefault() as ColorInfoAttribute;

                if (attribute != null && attribute.Type == "CustomColors")
                {
                    i++;
                }
            }

            return i;
        }

        /// <summary>
        /// Тип атрибут, нужен для обозначения переменных используемых как предопределенные цвета.
        /// </summary>
        private sealed class ColorInfoAttribute : System.Attribute
        {
            private string collorType;

            /// <summary>
            /// Тип атрибута
            /// </summary>
            public string Type
            {
                get { return this.collorType; }
                set { this.collorType = value; }
            }
        }
    }
}
