using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace WomenCalendar
{
    public class CsvWriter : ReportWriter
    {
        public char Separator { get; set; }

        private StreamWriter fileStream;
        public CsvWriter(string fileName) : base(fileName)
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

        public override void Dispose()
        {
            fileStream.Close();
            fileStream.Dispose();
        }

        #endregion

        public override void WriteHeader()
        {
            WriteLine(OneDayInfo.Header.Select(str => TEXT.Get[str]).ToArray());
        }

        public override void WriteDay(OneDayInfo day)
        {
            WriteLine(
                D(day.Date), 
                B(day.IsMentruation),
                I(day.Egesta, day.IsMentruation),
                B(day.IsOvulation),
                B(day.HadSex), 
                D(day.BBT),
                I(day.Health),
                CF(day.CF),
                day.Note.Replace('\n', ' ')
                );
        }

    }
}
