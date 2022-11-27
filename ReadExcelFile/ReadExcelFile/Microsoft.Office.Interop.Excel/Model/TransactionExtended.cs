using ReadExcelFileFramework.Attributes;
using System.Collections.Generic;

namespace ReadExcelFileFramework.Models
{
    class TransactionExtended : Transaction
    {
        [Column(11)] public bool IsOmitted { get; set; }
        [Column(12)] public string GroupId { get; set; }
        [Column(13)] public List<int> TagIds { get; set; }
    }
}
