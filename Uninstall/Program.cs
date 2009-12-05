using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace Uninstall
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            try
            {
                if (MessageBox.Show("Areyou sure you want to delete Ovulyashki?", "Think again",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\msiexec.exe",
                        @" /x {703F808B-60BC-4F04-B7EE-0ABB7BA5F909}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something weird happened! Try to remove Ovulyashki thru Add/Remove Programs.\n\n" + 
                    ex.Message, "WOW!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
