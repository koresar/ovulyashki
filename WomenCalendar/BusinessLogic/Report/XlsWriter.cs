using System;
using System.Collections.Generic;
using System.Text;
using CarlosAg.ExcelXmlWriter;

namespace WomenCalendar
{
    /// <summary>
    /// The report to write the .xls file.
    /// </summary>
    public class XlsWriter : ReportWriter
    {
        private Worksheet ws;
        private Workbook wb;
        private string fileName;

        /// <summary>
        /// Create the instance of .xls writer.
        /// </summary>
        /// <param name="fileName">The name of the file to write to.</param>
        public XlsWriter(string fileName)
        {
            this.fileName = fileName;
            this.wb = new Workbook();
            this.wb.Properties.Version = "10.4219";
            WorksheetStyle dateStyle = this.wb.Styles.Add("dateStyle");
            dateStyle.NumberFormat = "Short Date";
            this.ws = this.wb.Worksheets.Add(TEXT.Get["Menses"]);
            WorksheetStyle floatStyle = this.wb.Styles.Add("floatStyle");
            floatStyle.NumberFormat = "Fixed";
            WorksheetStyle default_ = this.wb.Styles.Add("Default");
            default_.Name = "Normal";
            default_.Font.FontName = "Arial";
            default_.Alignment.Vertical = StyleVerticalAlignment.Bottom;

            foreach (string cell in OneDayInfo.Header)
            {
                this.ws.Table.Columns.Add(new WorksheetColumn(100) { AutoFitWidth = true });
            }
        }

        /// <summary>
        /// Write the header of the report.
        /// </summary>
        public override void WriteHeader()
        {
            WorksheetRow wr = this.ws.Table.Rows.Add();
            for (int i = 0; i < OneDayInfo.Header.Count; i++)
            {
                wr.Cells.Add(OneDayInfo.Header[i]);
            }
        }

        /// <summary>
        /// Write an item of the report.
        /// </summary>
        /// <param name="day">One day info.</param>
        public override void WriteDay(OneDayInfo day)
        {
            WorksheetRow wr = this.ws.Table.Rows.Add();
            wr.Cells.Add(new WorksheetCell(this.D(day.Date), DataType.String, "dateStyle"));
            wr.Cells.Add(new WorksheetCell(this.B(day.IsMentruation), DataType.String));
            wr.Cells.Add(new WorksheetCell(this.I(day.Egesta, day.IsMentruation), day.IsMentruation ? DataType.Number : DataType.String));
            wr.Cells.Add(new WorksheetCell(this.B(day.IsOvulation), DataType.String));
            wr.Cells.Add(new WorksheetCell(this.B(day.HadSex), DataType.String));
            wr.Cells.Add(new WorksheetCell(this.D(day.BBT), day.BBT != 0 ? DataType.Number : DataType.String, day.BBT != 0 ? "floatStyle" : "Default"));
            wr.Cells.Add(new WorksheetCell(this.I(day.Health), DataType.Number));
            wr.Cells.Add(new WorksheetCell(this.CF(day.CF), DataType.String));
            wr.Cells.Add(new WorksheetCell(day.Note, DataType.String));
        }

        /// <summary>
        /// Save the file to disk.
        /// </summary>
        public override void Dispose()
        {
            this.wb.Save(this.fileName);
        }
    }
}
