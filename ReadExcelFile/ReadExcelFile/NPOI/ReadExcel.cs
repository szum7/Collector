using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ReadExcelFile.NPOI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReadExcelFile.NPOI
{
    class ReadExcel
    {
        public List<string> Header { get; set; }
        public List<TransactionExtended> Rows { get; set; }

        public ReadExcel()
        {
            this.Header = new List<string>();
            this.Rows = new List<TransactionExtended>();
        }

        public void Run(string filePath, string fileExtension)
        {
            this.ReadFile(filePath, fileExtension);
        }

        void ReadFile(string filePath, string fileExtension)
        {
            StringBuilder sb = new StringBuilder();
            ISheet sheet;

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.Position = 0;
                if (fileExtension == ".xls")
                {
                    HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                }
                else
                {
                    XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                }

                IRow headerRow = sheet.GetRow(0); //Get Header Row
                int cellCount = headerRow.LastCellNum;
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString()))
                        continue;

                    this.Header.Add(cell.ToString());                    
                }

                //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                int rowId = 1;
                for (int i = sheet.LastRowNum; i > (sheet.FirstRowNum + 1); i--) //Read Excel File
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                        continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank))
                        continue;

                    TransactionExtended tr = new TransactionExtended();

                    tr.Id = rowId;
                    tr.AccountingDate = row.GetCell(0).DateCellValue;
                    tr.TransactionId = row.GetCell(1).ToString();
                    tr.Type = row.GetCell(2).ToString();
                    tr.Account = row.GetCell(3).ToString();
                    tr.AccountName = row.GetCell(4).ToString();
                    tr.PartnerAccount = row.GetCell(5).ToString();
                    tr.PartnerName = row.GetCell(6).ToString();
                    tr.Sum = decimal.Parse(row.GetCell(7).ToString());
                    tr.Currency = row.GetCell(7).ToString();
                    tr.Message = row.GetCell(9).ToString();

                    rowId++;
                }
            }
        }
    }
}
