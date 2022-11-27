using System.Collections.Generic;

namespace ReadExcelFile.CellLike
{
    class SheetCell
    {
        public string CellReference { get; set; }
        public string Value { get; set; }
        public uint Row
        {
            get
            {
                return uint.Parse(this.CellReference.Substring(1, this.CellReference.Length - 1));
            }
        }
        public string Column
        {
            get
            {
                return this.CellReference[0].ToString();
            }
        }
    }
}

/// <summary>
/// SheetTable {
///     Rows: [
///         {
///             Row : 1,
///             Cells: [
///                 { Column: "A", Value: "Some content..." },
///                 { Column: "B", Value: "Some content..." },
///                 { Column: "C", Value: "Some content..." },
///             ]
///         }, {
///             Row : 2,
///             Cells: [
///                 { Column: "A", Value: "Some content..." },
///                 { Column: "B", Value: "Some content..." },
///                 { Column: "C", Value: "Some content..." },
///             ]
///         }
///     ]
/// }
/// </summary>
namespace ReadExcelFile.TableLike
{
    class SheetTable
    {
        public List<SheetRow> Rows { get; set; }
    }

    class SheetRow
    {
        public uint Row { get; set; }
        public List<SheetCell> Cells { get; set; }
    }

    class SheetCell
    {
        public string Column { get; set; }
        public string Value { get; set; }
    }
}