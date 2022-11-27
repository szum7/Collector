using Microsoft.Office.Interop.Excel;
using ReadExcelFileFramework.Models;
using ReadExcelFileFramework.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReadExcelFileFramework.Components.OriginalExcelReaders.v2
{
    class ExcelReader
    {
        public ExcelSheet<Transaction> ExcelSheet { get; set; }

        string folderPath;
        string extensionPattern;
        List<string> filePaths;
        ConsoleWatch watch;

        public ExcelReader(ConsoleWatch watch, string folderPath, string extensionPattern)
        {
            this.watch = watch;
            this.watch.ProgramId = this.GetType().Name;
            this.folderPath = folderPath;
            this.extensionPattern = extensionPattern;

            this.filePaths = new List<string>();
            this.ExcelSheet = new ExcelSheet<Transaction>();
        }

        public void Run()
        {
            this.watch.Timestamp("ExcelReader started.");

            this.InitFilePaths();
            this.watch.Diff("Paths read.");

            Transaction lastRow = null;
            foreach (string path in this.filePaths)
            {
                lastRow = this.ReadDocument(lastRow, path);
            }
            this.watch.Diff("All documents read.");

            this.watch.Timestamp($"Row count is: {this.ExcelSheet.Transactions.Count}");
            this.watch.Timestamp("ExcelReader finished.");
            Console.WriteLine();
        }

        void InitFilePaths()
        {
            this.filePaths = Directory.GetFiles(this.folderPath, this.extensionPattern).ToList();
            this.filePaths.Sort();
        }

        void ReadHeaderRow(Excel.Range xlRange)
        {
            for (int i = 1; i < 11; i++)
            {
                Excel.Range myRange = (Excel.Range)(xlRange.Cells[1, i]);
                this.ExcelSheet.Header.Add(myRange.Value2.ToString());
                //this.ExcelSheet.Header.Add(xlRange.Cells[1, i].Value2);
            }
            this.watch.Diff("Read header.");
        }

        Transaction ReadDocument(Transaction lastRow, string path)
        {
            #region Create COM objects
            Excel.Application xlApp = new Excel.Application();
            this.watch.Diff("Excel opened.");
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            this.watch.Diff("Excel file opened.");
            Excel._Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            #endregion

            #region Program
            // Read one header
            if (this.ExcelSheet.Header.Count == 0)
            {
                this.ReadHeaderRow(xlRange);
            }

            int rowCount = xlRange.Rows.Count;
            int i = rowCount;
            int rowId = 1;

            // Find first not empty row
            while (i > 1 && (xlRange.Cells[i, 1] == null || ((Excel.Range)xlRange.Cells[i, 1]).Value2 == null)) i--;

            // Find the first new date
            DateTime? currentRowDate = null;
            while (i > 1 && lastRow != null && (!currentRowDate.HasValue || currentRowDate < lastRow.AccountingDate))
            {
                currentRowDate = new DateTime(1900, 1, 1);
                currentRowDate = currentRowDate.Value.AddDays(double.Parse(((Excel.Range)xlRange.Cells[i, 1]).Value2.ToString()) - 2);
                i--;
            }

            while (i > 1)
            {
                if (xlRange.Cells[i, 1] != null && ((Excel.Range)xlRange.Cells[i, 1]).Value2 != null)
                {
                    currentRowDate = new DateTime(1900, 1, 1);
                    currentRowDate = currentRowDate.Value.AddDays(double.Parse(((Excel.Range)xlRange.Cells[i, 1]).ToString()) - 2);

                    Transaction tr = new Transaction();

                    tr.Id = rowId;
                    tr.AccountingDate = currentRowDate.Value;
                    tr.TransactionId = this.SetProperty<string>((Excel.Range)xlRange.Cells[i, 2]);
                    tr.Type = this.SetProperty<string>((Excel.Range)xlRange.Cells[i, 3]);
                    tr.Account = this.SetProperty<string>((Excel.Range)xlRange.Cells[i, 4]);
                    tr.AccountName = this.SetProperty<string>((Excel.Range)xlRange.Cells[i, 5]);
                    tr.PartnerAccount = this.SetProperty<string>((Excel.Range)xlRange.Cells[i, 6]);
                    tr.PartnerName = this.SetProperty<string>((Excel.Range)xlRange.Cells[i, 7]);
                    tr.Sum = this.SetProperty<decimal>((Excel.Range)xlRange.Cells[i, 8]);
                    tr.Currency = this.SetProperty<string>((Excel.Range)xlRange.Cells[i, 9]);
                    tr.Message = this.SetProperty<string>((Excel.Range)xlRange.Cells[i, 10]);

                    // Add row if not already added
                    if (lastRow == null || currentRowDate != lastRow.AccountingDate || lastRow.ContentId != tr.ContentId)
                    {
                        this.ExcelSheet.AddNewRow();
                        this.ExcelSheet.SetLastRow(tr);
                        rowId++;
                    }

                    lastRow = tr;
                }
                i--;
            }
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
            Marshal.ReleaseComObject(xlRange);
            this.watch.Diff("Marshal.ReleaseComObject(xlRange)");
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

            return lastRow;
        }

        T SetProperty<T>(Excel.Range range)
        {
            if (range == null || (Excel.Range)range.Value2 == null)
                return default(T);

            return (T)Convert.ChangeType(range.Value2.ToString(), typeof(T));           
        }

        public bool IsExcelSheetEmpty()
        {
            return this.ExcelSheet.Transactions == null || this.ExcelSheet.Transactions.Count == 0;
        }
    }
}
