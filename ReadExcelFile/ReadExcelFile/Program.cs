using ReadExcelFile.NPOI;
using ReadExcelFileFramework.Components.OriginalExcelReaders.v2;
using System;
using System.Linq;
using System.Text;

namespace ReadExcelFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var npoiRead = new ReadExcel();
            npoiRead.Run(@"C:\Users\Aron_Szocs\Documents\Bank\szamlatortenet_20141125_20150223.xls", ".xls");
            //var npoiWrite = new ExportExcel();
            //npoiWrite.Run();

#if false
            MergeSpreadSheets mss = new MergeSpreadSheets("BankCopy", "*.xlsx");
#endif

#if false
            var excelWorker = new ExcelWorker();
            excelWorker.Run(
                @"C:\Users\Aron_Szocs\Documents\Bank",
                "*.xls",
                @"C:\Users\Aron_Szocs\Documents\Bank\Merged",
                @"merged.xls");
#endif
        }
    }
}
