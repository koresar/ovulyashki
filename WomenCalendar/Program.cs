﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using WomenCalendar.Properties;
using System.ComponentModel;

namespace WomenCalendar
{
    static class Program
    {
        public static MainForm ApplicationForm;

        public static Woman CurrentWoman = new Woman();

        public static ComponentResourceManager IconResource;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationForm = new MainForm();
            IconResource = new ComponentResourceManager(typeof(MainForm));
            Application.Run(ApplicationForm);
        }

        public static class MonthAppearance
        {
            public static Brush HeaderBrush = Brushes.White;
        }

        public static class DayCellAppearance
        {
            public static Pen FocusEdgePen = new Pen(Brushes.Red, 2);
            public static Pen EdgePen = Pens.Black;
        }
    }
}
