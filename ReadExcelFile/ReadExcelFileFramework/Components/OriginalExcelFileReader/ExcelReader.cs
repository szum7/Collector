using Microsoft.Office.Interop.Excel;
using ReadExcelFileFramework.Models;
using ReadExcelFileFramework.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReadExcelFileFramework.Components.OriginalExcelReaders.v1
{
    class ExcelReader
    {
        public ExcelSheet<Transaction> ExcelSheet { get; set; }

        string folderPath;
        string extensionPattern;
        List<string> filePaths;
        ConsoleWatch watch;
        ExcelCOMs excelCOMs;

        public ExcelReader(ConsoleWatch watch, ExcelCOMs excelCOMs, string folderPath, string extensionPattern)
        {
            this.watch = watch;
            this.watch.ProgramId = this.GetType().Name;
            this.excelCOMs = excelCOMs;
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

        void ReadHeaderRow()
        {
            for (int i = 1; i < 11; i++)
            {
                this.ExcelSheet.Header.Add(this.excelCOMs.xlRange.Cells[1, i].Value2);
            }
            this.watch.Diff("Read header.");
        }

        Transaction ReadDocument(Transaction lastRow, string path)
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            this.excelCOMs.xlWorkbook = this.excelCOMs.xlApp.Workbooks.Open(path);
            this.excelCOMs.xlWorksheet = this.excelCOMs.xlWorkbook.Sheets[1];
            this.excelCOMs.xlRange = this.excelCOMs.xlWorksheet.UsedRange;

            this.watch.Diff("OPENED an excel file.");

            // Read one header
            if (this.ExcelSheet.Header.Count == 0)
            {
                this.ReadHeaderRow();
            }

            int rowCount = this.excelCOMs.xlRange.Rows.Count;
            int i = rowCount;
            int rowId = 1;

            // Find first not empty row
            while (i > 1 && (this.excelCOMs.xlRange.Cells[i, 1] == null || this.excelCOMs.xlRange.Cells[i, 1].Value2 == null)) i--;

            // Find the first new date
            DateTime? currentRowDate = null;
            while (i > 1 && lastRow != null && (!currentRowDate.HasValue || currentRowDate < lastRow.AccountingDate))
            {
                currentRowDate = new DateTime(1900, 1, 1);
                currentRowDate = currentRowDate.Value.AddDays(double.Parse(this.excelCOMs.xlRange.Cells[i, 1].Value2.ToString()) - 2);
                i--;
            }

            while (i > 1)
            {
                if (this.excelCOMs.xlRange.Cells[i, 1] != null && this.excelCOMs.xlRange.Cells[i, 1].Value2 != null)
                {
                    currentRowDate = new DateTime(1900, 1, 1);
                    currentRowDate = currentRowDate.Value.AddDays(double.Parse(this.excelCOMs.xlRange.Cells[i, 1].Value2.ToString()) - 2);

                    Transaction tr = new Transaction();

                    tr.Id = rowId;
                    tr.AccountingDate = currentRowDate.Value;
                    tr.TransactionId = this.SetProperty<string>(this.excelCOMs.xlRange.Cells[i, 2]);
                    tr.Type = this.SetProperty<string>(this.excelCOMs.xlRange.Cells[i, 3]);
                    tr.Account = this.SetProperty<string>(this.excelCOMs.xlRange.Cells[i, 4]);
                    tr.AccountName = this.SetProperty<string>(this.excelCOMs.xlRange.Cells[i, 5]);
                    tr.PartnerAccount = this.SetProperty<string>(this.excelCOMs.xlRange.Cells[i, 6]);
                    tr.PartnerName = this.SetProperty<string>(this.excelCOMs.xlRange.Cells[i, 7]);
                    tr.Sum = this.SetProperty<decimal>(this.excelCOMs.xlRange.Cells[i, 8]);
                    tr.Currency = this.SetProperty<string>(this.excelCOMs.xlRange.Cells[i, 9]);
                    tr.Message = this.SetProperty<string>(this.excelCOMs.xlRange.Cells[i, 10]);

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

            this.watch.Diff("READ properties.");

            return lastRow;
        }

        T SetProperty<T>(Range range)
        {
            if (range == null || range.Value2 == null)
                return default(T);

            if (typeof(T) == typeof(string))
            {
                return range.Value2.ToString();
            }
            else
            {
                return (T)Convert.ChangeType(range.Value2.ToString(), typeof(T));
            }
        }

        public bool IsExcelSheetEmpty()
        {
            return this.ExcelSheet.Transactions == null || this.ExcelSheet.Transactions.Count == 0;
        }
    }
}
