using ReadExcelFileFramework.Attributes;
using System;

namespace ReadExcelFileFramework.Models
{
    class Transaction
    {
        public int Id { get; set; }
        public int Number { get { return this.Id; } set { this.Id = value; } }

        [Column(1)] public DateTime AccountingDate { get; set; }
        [Column(2)] public string TransactionId { get; set; }
        [Column(3)] public string Type { get; set; }
        [Column(4)] public string Account { get; set; }
        [Column(5)] public string AccountName { get; set; }
        [Column(6)] public string PartnerAccount { get; set; }
        [Column(7)] public string PartnerName { get; set; }
        [Column(8)] public decimal Sum { get; set; }
        [Column(9)] public string Currency { get; set; }
        [Column(10)] public string Message { get; set; }

        public string ContentId
        {
            get
            {
                return $"{AccountingDate.ToString()}{TransactionId}{Type}{Account}{AccountName}{PartnerAccount}{PartnerName}{Sum.ToString()}{Currency}{Message}";
            }
        }

        public bool IsTheSame(Transaction other)
        {
            if (other == null)
                return false;
            return this.ContentId == other.ContentId;
        }
    }
}
