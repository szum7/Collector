using System.Collections.Generic;

namespace ReadExcelFile.NPOI.Models
{
    class TransactionExtended : Transaction
    {
        public bool IsOmitted { get; set; }
        public string GroupId { get; set; }
        public List<int> TagIds { get; set; }
    }
}
