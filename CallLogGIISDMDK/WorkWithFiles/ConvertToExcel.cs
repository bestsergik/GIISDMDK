using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using AmRoMessageDialog;

namespace CallLogGIISDMDK.WorkWithFiles
{
    class ConvertToExcel
    {
        public ConvertToExcel(string path)
        {
            System.Data.DataTable table = CreateDataTableFromXml(StaticData.CorrectPathToXml);
            ExportDataTableToExcel(table, path);
        }
        private void Button_Click()
        {
            string path = @"1.xml";
            string Xlfile = @"123";
            System.Data.DataTable table = CreateDataTableFromXml(path);
            ExportDataTableToExcel(table, Xlfile);
        }
        public System.Data.DataTable CreateDataTableFromXml(string path)
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(path);
                Dt.Load(ds.CreateDataReader());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't create DataTable");
            }
            return Dt;
        }
        private void ExportDataTableToExcel(System.Data.DataTable table, string Xlfile)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook book = excel.Application.Workbooks.Add(Type.Missing);
            excel.Visible = false;
            excel.DisplayAlerts = false;
            Worksheet excelWorkSheet = (Worksheet)book.ActiveSheet;
            excelWorkSheet.Name = table.TableName;
            Excel.Range formatRange = excelWorkSheet.UsedRange;
            for (int i = 1; i < table.Columns.Count + 1; i++) // Creating Header Column In Excel
            {
                excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
            }
            for (int j = 0; j < table.Rows.Count; j++) // Exporting Rows in Excel
            {
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    Excel.Range cell = (Microsoft.Office.Interop.Excel.Range)formatRange.Cells[j + 2, k + 1];
                    Excel.Borders border = cell.Borders;
                    border.LineStyle = Excel.XlLineStyle.xlContinuous;
                    border.Weight = 2d;
                }
            }

            formatRange = excelWorkSheet.get_Range("a1");
            formatRange.EntireRow.Font.Bold = true;
            excelWorkSheet.Cells[1, 5] = "Bold";
            book.SaveAs(Xlfile);
            book.Close(true);
            excel.Quit();
            Marshal.ReleaseComObject(book);
            Marshal.ReleaseComObject(book);
            Marshal.ReleaseComObject(excel);
            CompleteExport();
        }
        private void CompleteExport()
        {
            var messageBox = new AmRoMessageBox
            {
                Background = "#284F4F",
                TextColor = "#ffffff",
                IconColor = "#3399ff",
                RippleEffectColor = "#000000",
                ClickEffectColor = "#1F2023",
                ShowMessageWithEffect = true,
                EffectArea = this,
            };
            messageBox.Show("Экспорт выполнен успешно!");
        }
    }
}

