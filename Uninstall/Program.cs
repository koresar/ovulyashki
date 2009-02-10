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
                if (MessageBox.Show("Уверена, что хочешь удалить Овуляшки?", "Подумай еще раз",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\msiexec.exe", 
                        @" /x {334556E0-10B1-4308-A487-DA137AF1BCF1}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то ужасное произошло! Попробуй удалить Овуляшки через Установку/Удаление программ.\n\n" + 
                    ex.Message, "ОЙ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
