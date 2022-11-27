using ReadExcelFileFramework.Components.MergedExcelFileReaders;
using ReadExcelFileFramework.Components.OriginalExcelReaders.v2;
using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReadExcelFileFramework
{
    class Program
    {
        static void Main(string[] args)
        {

#if true
            var excelWorker = new ExcelWorker();
            excelWorker.Run(
                @"C:\Users\Aron_Szocs\Documents\Bank",
                "*.xls",
                @"C:\Users\Aron_Szocs\Documents\Bank\Merged",
                @"merged.xls");
#endif

#if true
            var reader = new MergedExcelFileReader();
            reader.Run(
                @"C:\Users\Aron_Szocs\Documents\Bank\Merged\merged.xls");
#endif

            Console.WriteLine("\nPROGRAM ENDED.");
            Console.ReadKey();
        }

        private static void TestRead()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Aron_Szocs\Documents\Bank\szamlatortenet_20141125_20150223.xls");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    //new line
                    if (j == 1)
                        Console.Write("\r\n");

                    //write the value to the console
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}
