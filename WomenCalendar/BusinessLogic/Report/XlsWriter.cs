using System;
using System.Collections.Generic;
using System.Text;
using CarlosAg.ExcelXmlWriter;

namespace WomenCalendar
{
    public class XlsWriter : ReportWriter
    {
        private Worksheet ws;
        private Workbook wb;
        private string fileName;

        public XlsWriter(string fileName) : base(fileName)
        {
            this.fileName = fileName;
            wb = new Workbook();
            wb.Properties.Version = "10.4219";
            WorksheetStyle dateStyle = wb.Styles.Add("dateStyle");
            dateStyle.NumberFormat = "Short Date";
            ws = wb.Worksheets.Add("Менструации");
            WorksheetStyle floatStyle = wb.Styles.Add("floatStyle");
            floatStyle.NumberFormat = "Fixed";
            WorksheetStyle Default = wb.Styles.Add("Default");
            Default.Name = "Normal";
            Default.Font.FontName = "Arial Cyr";
            Default.Alignment.Vertical = StyleVerticalAlignment.Bottom;

            foreach (string cell in OneDayInfo.Header)
            {
                ws.Table.Columns.Add(new WorksheetColumn(100) { AutoFitWidth = true });
            }
        }

        public override void WriteHeader()
        {
            WorksheetRow wr = ws.Table.Rows.Add();
            foreach (string col in OneDayInfo.Header)
            {
                wr.Cells.Add(col);
            }
        }

        public override void WriteDay(OneDayInfo day)
        {
            WorksheetRow wr = ws.Table.Rows.Add();
            wr.Cells.Add(new WorksheetCell(D(day.Date), DataType.String, "dateStyle"));
            wr.Cells.Add(new WorksheetCell(B(day.IsMentruation), DataType.String));
            wr.Cells.Add(new WorksheetCell(I(day.Egesta, day.IsMentruation), day.IsMentruation ? DataType.Number : DataType.String));
            wr.Cells.Add(new WorksheetCell(B(day.HadSex), DataType.String));
            wr.Cells.Add(new WorksheetCell(D(day.BBT), day.BBT != 0 ? DataType.Number : DataType.String, day.BBT != 0 ? "floatStyle" : "Default"));
            wr.Cells.Add(new WorksheetCell(I(day.Health), DataType.Number));
            wr.Cells.Add(new WorksheetCell(day.Note, DataType.String));
        }

        public override void Dispose()
        {
            wb.Save(fileName);
        }
    }
}
