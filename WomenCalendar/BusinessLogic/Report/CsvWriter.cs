using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WomenCalendar
{
    /// <summary>
    /// Implements report to .csv file.
    /// </summary>
    public class CsvWriter : ReportWriter
    {
        private StreamWriter fileStream;

        /// <summary>
        /// Creates report writer to the given file.
        /// </summary>
        /// <param name="fileName">File to write ro.</param>
        public CsvWriter(string fileName)
        {
            this.Separator = ';';
            this.fileStream = new StreamWriter(fileName, false, Encoding.Default);
        }

        /// <summary>
        /// The separator to use for fields split.
        /// </summary>
        private char Separator { get; set; }

        #region IDisposable Members

        /// <summary>
        /// Closes the opened file.
        /// </summary>
        public override void Dispose()
        {
            this.fileStream.Close();
            this.fileStream.Dispose();
        }

        #endregion

        /// <summary>
        /// Write the report header.
        /// </summary>
        public override void WriteHeader()
        {
            this.WriteLine(OneDayInfo.Header.Select(str => TEXT.Get[str]).ToArray());
        }

        /// <summary>
        /// Write the report item (row).
        /// </summary>
        /// <param name="day">One day info.</param>
        public override void WriteDay(OneDayInfo day)
        {
            this.WriteLine(
                this.D(day.Date),
                this.B(day.IsMentruation),
                this.I(day.Egesta, day.IsMentruation),
                this.B(day.IsOvulation),
                this.B(day.HadSex),
                this.D(day.BBT),
                this.I(day.Health),
                this.CF(day.CF),
                day.Note.Replace('\n', ' '));
        }

        private void WriteLine(params object[] cells)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var cel in cells)
            {
                sb.Append(cel.ToString()).Append(this.Separator);
            }

            sb.AppendLine();
            this.fileStream.Write(sb.ToString());
        }
    }
}
