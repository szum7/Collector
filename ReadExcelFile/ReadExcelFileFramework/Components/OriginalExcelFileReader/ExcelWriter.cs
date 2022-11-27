using ReadExcelFileFramework.Models;
using ReadExcelFileFramework.Utilities;
using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReadExcelFileFramework.Components.OriginalExcelReaders.v1
{
    class ExcelWriter
    {
        ExcelCOMs excelCOMs;
        ConsoleWatch watch;

        public ExcelWriter(ConsoleWatch watch, ExcelCOMs excelCOMs)
        {
            this.watch = watch;
            this.watch.ProgramId = this.GetType().Name;
            this.excelCOMs = excelCOMs;
        }

        public void Run(ExcelSheet<Transaction> excelSheet, string path, string fileName)
        {
            if (this.excelCOMs.xlApp == null)
            {
                Console.WriteLine("Excel is not properly installed!");
                return;
            }

            object misValue = System.Reflection.Missing.Value;

            this.excelCOMs.xlWorkbook = this.excelCOMs.xlApp.Workbooks.Add(misValue);
            this.excelCOMs.xlWorksheet = (Excel.Worksheet)this.excelCOMs.xlWorkbook.Worksheets.get_Item(1);
            this.watch.Diff("New excel file opened.");

            // Write header
            for (int i = 1; i <= excelSheet.Header.Count; i++)
            {
                this.excelCOMs.xlWorksheet.Cells[1, i] = excelSheet.Header[i - 1];
            }
            this.watch.Diff("Header written.");

            // Write content
            for (int i = 2; i <= excelSheet.Transactions.Count + 1; i++)
            {
                var row = excelSheet.Transactions[i - 2];
                this.excelCOMs.xlWorksheet.Cells[i, 1] = row.AccountingDate;
                this.excelCOMs.xlWorksheet.Cells[i, 2] = row.TransactionId;
                this.excelCOMs.xlWorksheet.Cells[i, 3] = row.Type;
                this.excelCOMs.xlWorksheet.Cells[i, 4] = this.GetFormattedValue(row.Account);
                this.excelCOMs.xlWorksheet.Cells[i, 5] = row.AccountName;
                this.excelCOMs.xlWorksheet.Cells[i, 6] = this.GetFormattedValue(row.PartnerAccount);
                this.excelCOMs.xlWorksheet.Cells[i, 7] = row.PartnerName;
                this.excelCOMs.xlWorksheet.Cells[i, 8] = row.Sum.ToString("#.##");
                this.excelCOMs.xlWorksheet.Cells[i, 9] = row.Currency;
                this.excelCOMs.xlWorksheet.Cells[i, 10] = this.GetFormattedValue(row.Message);
            }
            this.watch.Diff("Records written.");

            this.excelCOMs.xlWorkbook.SaveAs(
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
            this.watch.Diff("New file saved. ExcelWriter finished.");
        }

        string GetFormattedValue(string value)
        {
            if (value == "" || value == null)
                return null;
            return $"'{value}";
        }
    }
}
