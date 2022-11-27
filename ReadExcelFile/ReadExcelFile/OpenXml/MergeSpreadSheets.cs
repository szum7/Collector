using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Text;
using ReadExcelFile.TableLike;

namespace ReadExcelFile
{
    class MergeSpreadSheets
    {
        public string FolderPath { get; set; }
        private List<string> FilePaths { get; set; }
        public List<SheetCell> Content { get; set; }
        public string ExtensionPattern { get; set; }

        public MergeSpreadSheets(string folderPath, string extensionPattern)
        {
            this.FolderPath = folderPath;
            this.ExtensionPattern = extensionPattern;
            this.Content = new List<SheetCell>();
            this.Program();

            // TESTS
            //ExcelFileReader.ReadFile();
        }

        public void Program()
        {
            // Order files on date
            this.FilePaths = Directory.GetFiles(this.FolderPath, this.ExtensionPattern).ToList();
            this.FilePaths.Sort();

            // Read lines in current file
            int currentFileIndex = 0;
            while (currentFileIndex < this.FilePaths.Count)
            {
                #region Read file
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FilePaths[currentFileIndex], true))
                {
                    //create the object for workbook part  
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    Sheets thesheetcollection = workbookPart.Workbook.GetFirstChild<Sheets>();
                    //StringBuilder excelResult = new StringBuilder();

                    //using for each loop to get the sheet from the sheetcollection  
                    foreach (Sheet thesheet in thesheetcollection)
                    {
                        //statement to get the worksheet object by using the sheet id  
                        Worksheet theWorksheet = ((WorksheetPart)workbookPart.GetPartById(thesheet.Id)).Worksheet;

                        SheetData thesheetdata = (SheetData)theWorksheet.GetFirstChild<SheetData>();
                        foreach (Row thecurrentrow in thesheetdata)
                        {
                            foreach (Cell thecurrentcell in thecurrentrow)
                            {
                                //statement to take the integer value  
                                string currentcellvalue = string.Empty;
                                if (thecurrentcell.DataType != null)
                                {
                                    if (thecurrentcell.DataType == CellValues.SharedString)
                                    {
                                        int id;
                                        if (Int32.TryParse(thecurrentcell.InnerText, out id))
                                        {
                                            SharedStringItem item = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                                            if (item.Text != null)
                                            {
                                                //code to take the string value  
                                                //excelResult.Append(item.Text.Text + " ");
                                                this.Content.Add(new SheetCell()
                                                {
                                                    //CellReference = thecurrentcell.CellReference,
                                                    //Value = item.Text.Text
                                                });
                                            }
                                            else if (item.InnerText != null)
                                            {
                                                currentcellvalue = item.InnerText;
                                            }
                                            else if (item.InnerXml != null)
                                            {
                                                currentcellvalue = item.InnerXml;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //excelResult.Append(Convert.ToInt16(thecurrentcell.InnerText) + " ");
                                }
                            }
                            //excelResult.AppendLine();
                        }
                        // FINISHED READING
                    }
                }
                #endregion
                currentFileIndex++;
            }

            // If finished open next file
                // Get the last line in the new file, read from next line
                    // If no same line, write from the start + Log missing lines
                    // If all lines are older then current line, get the next file

            // If no more files, write data, save, exit
        }
    }
}