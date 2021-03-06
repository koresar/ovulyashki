﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Uninstall
{
    /// <summary>
    /// Uninstall main executalbw.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Input arguments.</param>
        public static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                MessageBox.Show(
                    "Something weird happened! No parameter for Uninstall. \n Try to remove Ovulyashki thru Add/Remove Programs.", 
                    "WOW!", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\msiexec.exe", @" /x " + args[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Something weird happened! Try to remove Ovulyashki thru Add/Remove Programs.\n\n" + 
                    ex.Message, 
                    "WOW!", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
    }
}
