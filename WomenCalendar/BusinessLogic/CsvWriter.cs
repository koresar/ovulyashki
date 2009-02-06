using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WomenCalendar
{
    public class CsvWriter : IDisposable
    {
        public char Separator { get; set; }

        private StreamWriter fileStream;
        public CsvWriter(string fileName)
        {
            Separator = ';';
            fileStream = new StreamWriter(fileName, false, Encoding.Default);
        }

        public void WriteLine(params object[] cells)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var cel in cells)
            {
                sb.Append(cel.ToString()).Append(Separator);
            }
            sb.AppendLine();
            fileStream.Write(sb.ToString());
        }

        public void Close()
        {
            fileStream.Close();
        }

        #region IDisposable Members

        public void Dispose()
        {
            fileStream.Close();
            fileStream.Dispose();
        }

        #endregion
    }
}
