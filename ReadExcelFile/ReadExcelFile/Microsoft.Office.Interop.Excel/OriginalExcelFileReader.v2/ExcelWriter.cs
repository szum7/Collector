using ReadExcelFileFramework.Models;
using ReadExcelFileFramework.Utilities;
using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReadExcelFileFramework.Components.OriginalExcelReaders.v2
{
    class ExcelWriter
    {
        ConsoleWatch watch;

        public ExcelWriter(ConsoleWatch watch)
        {
            this.watch = watch;
            this.watch.ProgramId = this.GetType().Name;
        }

        public void Run(ExcelSheet<Transaction> excelSheet, string path, string fileName)
        {
            #region Create COM objects
            Excel.Workbook xlWorkbook;
            Excel.Worksheet xlWorksheet;

            Excel.Application xlApp = new Excel.Application();
            this.watch.Diff("Excel opened.");
            #endregion

            #region Program
            if (xlApp == null)
            {
                Console.WriteLine("Excel is not properly installed!");
                return;
            }
            object misValue = System.Reflection.Missing.Value;

            xlWorkbook = xlApp.Workbooks.Add(misValue);
            xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);
            this.watch.Diff("New excel file opened.");

            // Write header
            for (int i = 1; i <= excelSheet.Header.Count; i++)
            {
                xlWorksheet.Cells[1, i] = excelSheet.Header[i - 1];
            }
            this.watch.Diff("Header written.");

            // Write content
            for (int i = 2; i <= excelSheet.Transactions.Count + 1; i++)
            {
                var row = excelSheet.Transactions[i - 2];
                xlWorksheet.Cells[i, 1] = row.AccountingDate;
                xlWorksheet.Cells[i, 2] = row.TransactionId;
                xlWorksheet.Cells[i, 3] = row.Type;
                xlWorksheet.Cells[i, 4] = this.GetFormattedValue(row.Account);
                xlWorksheet.Cells[i, 5] = row.AccountName;
                xlWorksheet.Cells[i, 6] = this.GetFormattedValue(row.PartnerAccount);
                xlWorksheet.Cells[i, 7] = row.PartnerName;
                xlWorksheet.Cells[i, 8] = row.Sum.ToString("#.##");
                xlWorksheet.Cells[i, 9] = row.Currency;
                xlWorksheet.Cells[i, 10] = this.GetFormattedValue(row.Message);
            }
            this.watch.Diff("Records written.");

            xlWorkbook.SaveAs(
                $@"{path}\{fileName}",
                Excel.XlFileFormat.xlWorkbookNormal,
                misValue,
                misValue,
                misValue,
                misValue,
                Excel.XlSaveAsAccessMode.xlExclusive,
                misValue,
                misValue,
                misValue,
                misValue,
                misValue);
            this.watch.Diff("New file saved.");
            #endregion

            #region Clean up COM objects
            // Cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            this.watch.Diff("GC cleanup init.");

            // Rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            // Release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlWorksheet);
            this.watch.Diff("Marshal.ReleaseComObject(xlWorksheet)");

            // Close and release
            xlWorkbook.Close();
            this.watch.Diff("xlWorkbook.Close()");
            Marshal.ReleaseComObject(xlWorkbook);
            this.watch.Diff("Marshal.ReleaseComObject(xlWorkbook)");

            // Quit and release
            xlApp.Quit();
            this.watch.Diff("xlApp.Quit()");
            Marshal.ReleaseComObject(xlApp);
            this.watch.Diff("Marshal.ReleaseComObject(xlApp)");
            Console.WriteLine();
            #endregion
        }

        string GetFormattedValue(string value)
        {
            if (value == "" || value == null)
                return null;
            return $"'{value}";
        }
    }
}
