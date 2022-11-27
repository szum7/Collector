using ReadExcelFileFramework.Utilities;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcelFileFramework.Components.OriginalExcelReaders.v2
{
    class ExcelWorker
    {
        ExcelWriter writer;
        ExcelReader reader;
        ConsoleWatch watch;

        public ExcelWorker()
        {
        }

        public void Run(string inputPath, string inputPattern, string outputPath, string outputFileName)
        {
            this.StartTimer();
            this.ReadExcelFiles(inputPath, inputPattern);
            this.CreateMergedExcelFile(outputPath, outputFileName);

            this.watch.ProgramId = this.GetType().Name;
            this.watch.Timestamp("ExcelWorker finished.");
            Console.WriteLine();
            this.watch.StopAll();
        }

        void StartTimer()
        {
            this.watch = new ConsoleWatch(this.GetType().Name);
            this.watch.StartAll();
        }

        void ReadExcelFiles(string inputPath, string inputPattern)
        {
            this.reader = new ExcelReader(watch, inputPath, inputPattern);
            this.reader.Run();
        }

        void CreateMergedExcelFile(string outputPath, string outputFileName)
        {
            if (this.reader.IsExcelSheetEmpty())
            {
                Console.WriteLine("[ALERT] Excel record list is empty! The reading may have been unsuccessful.");
                return;
            }

            this.writer = new ExcelWriter(this.watch);
            this.writer.Run(this.reader.ExcelSheet, outputPath, outputFileName);
        }
    }
}
