using Microsoft.Office.Interop.Excel;
using ReadExcelFileFramework.Models;
using ReadExcelFileFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReadExcelFileFramework.Components.MergedExcelFileReaders
{
    class MergedExcelFileReader
    {
        public ExcelSheet<TransactionExtended> ExcelSheet { get; set; }

        ConsoleWatch watch;

        public MergedExcelFileReader()
        {
            this.ExcelSheet = new ExcelSheet<TransactionExtended>();
            this.watch = new ConsoleWatch(this.GetType().Name);
        }

        public void Run(string filePath)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            this.watch.Diff("OPENED the merged excel file.");

            int rowCount = xlRange.Rows.Count;
            int i = rowCount;
            int rowId = 1;

            while (i > 1)
            {
                if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                {
                    DateTime currentRowDate = new DateTime(1900, 1, 1);
                    currentRowDate = currentRowDate.AddDays(double.Parse(xlRange.Cells[i, 1].Value2.ToString()) - 2);

                    TransactionExtended tr = new TransactionExtended();

                    tr.Id = rowId;
                    tr.AccountingDate = currentRowDate;
                    tr.TransactionId = this.SetProperty<string>(xlRange.Cells[i, 2]);
                    tr.Type = this.SetProperty<string>(xlRange.Cells[i, 3]);
                    tr.Account = this.SetProperty<string>(xlRange.Cells[i, 4]);
                    tr.AccountName = this.SetProperty<string>(xlRange.Cells[i, 5]);
                    tr.PartnerAccount = this.SetProperty<string>(xlRange.Cells[i, 6]);
                    tr.PartnerName = this.SetProperty<string>(xlRange.Cells[i, 7]);
                    tr.Sum = this.SetProperty<decimal>(xlRange.Cells[i, 8]);
                    tr.Currency = this.SetProperty<string>(xlRange.Cells[i, 9]);
                    tr.Message = this.SetProperty<string>(xlRange.Cells[i, 10]);

                    tr.IsOmitted = this.GetBoolValue(xlRange.Cells[i, 11]);
                    tr.GroupId = this.SetProperty<string>(xlRange.Cells[i, 12]);
                    tr.TagIds = this.GetIntList(xlRange.Cells[i, 13]);

                    this.ExcelSheet.AddNewRow();
                    this.ExcelSheet.SetLastRow(tr);
                    rowId++;
                }
                i--;
            }


            #region Clean up COM objects
            // Cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.watch.Diff("GC cleanup init.");

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
            #endregion

            this.watch.Diff("FINISHED, properties are in memory!");
            Console.WriteLine();
        }

        List<int> GetIntList(Range range)
        {
            if (range == null || range.Value2 == null)
                return null;

            var ret = new List<int>();
            var arr = range.Value2.ToString().Split(',');
            foreach (var item in arr)
            {
                ret.Add(int.Parse(item));
            }
            return ret;
        }

        bool GetBoolValue(Range range)
        {
            if (range == null || range.Value2 == null)
                return false;

            return range.Value2.ToString() == "1" || range.Value2.ToString() == "'1";
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
    }
}
