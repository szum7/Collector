using ReadExcelFileFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReadExcelFileFramework.Components.OriginalExcelReaders.v1
{
    class ExcelWorker
    {
        ExcelWriter writer;
        ExcelReader reader;
        ConsoleWatch watch;
        ExcelCOMs excelCOMs;

        public ExcelWorker()
        {
            this.excelCOMs = new ExcelCOMs();
        }

        public void Run(string inputPath, string inputPattern, string outputPath, string outputFileName)
        {
            this.StartTimer();
            this.Init();
            this.ReadExcelFiles(inputPath, inputPattern);
            this.CreateMergedExcelFile(outputPath, outputFileName);
            this.Kill();

            this.watch.ProgramId = this.GetType().Name;
            this.watch.Timestamp("ExcelWorker finished.");
            this.watch.StopAll();
        }

        void Init()
        {
            this.excelCOMs.xlApp = new Excel.Application();
            this.watch.Diff("Excel application opened.");
        }

        void StartTimer()
        {
            this.watch = new ConsoleWatch(this.GetType().Name);
            this.watch.StartAll();
        }

        void ReadExcelFiles(string inputPath, string inputPattern)
        {
            this.reader = new ExcelReader(watch, excelCOMs, inputPath, inputPattern);
            this.reader.Run();
        }

        void CreateMergedExcelFile(string outputPath, string outputFileName)
        {
            if (this.reader.IsExcelSheetEmpty())
            {
                Console.WriteLine("[ALERT] Excel record list is empty! The reading may have been unsuccessful.");
                return;
            }

            this.writer = new ExcelWriter(this.watch, this.excelCOMs);
            this.writer.Run(this.reader.ExcelSheet, outputPath, outputFileName);
        }

        void Kill()
        {
            // Cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            this.watch.Diff("GC cleanup init.");

            // Rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            // Release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(this.excelCOMs.xlRange);
            this.watch.Diff("Marshal.ReleaseComObject(xlRange)");
            Marshal.ReleaseComObject(this.excelCOMs.xlWorksheet);
            this.watch.Diff("Marshal.ReleaseComObject(xlWorksheet)");

            // Close and release
            this.excelCOMs.xlWorkbook.Close();
            this.watch.Diff("xlWorkbook.Close()");
            Marshal.ReleaseComObject(this.excelCOMs.xlWorkbook);
            this.watch.Diff("Marshal.ReleaseComObject(xlWorkbook)");

            // Quit and release
            this.excelCOMs.xlApp.Quit();
            this.watch.Diff("xlApp.Quit()");
            Marshal.ReleaseComObject(this.excelCOMs.xlApp);
            this.watch.Diff("Marshal.ReleaseComObject(xlApp)");
        }
    }
}
