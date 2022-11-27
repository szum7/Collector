using Excel = Microsoft.Office.Interop.Excel;

namespace ReadExcelFileFramework.Components.OriginalExcelReaders.v1
{
    class ExcelCOMs
    {
        public Excel.Application xlApp { get; set; }
        public Excel.Workbook xlWorkbook { get; set; }
        public Excel._Worksheet xlWorksheet { get; set; }
        public Excel.Range xlRange { get; set; }
    }
}
