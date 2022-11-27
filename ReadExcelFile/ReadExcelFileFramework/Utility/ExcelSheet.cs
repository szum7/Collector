using ReadExcelFileFramework.Models;
using System.Collections.Generic;

namespace ReadExcelFileFramework.Utilities
{
    class ExcelSheet<T> where T : Transaction, new()
    {
        public string Name { get; set; }
        public List<string> Header { get; set; }
        public List<T> Transactions { get; set; }

        public ExcelSheet()
        {
            this.Header = new List<string>();
            this.Transactions = new List<T>();
        }

        public void AddNewRow()
        {
            this.Transactions.Add(new T());
        }

        public void SetLastRow(T value)
        {
            if (this.Transactions.Count == 0)
                return;
            this.Transactions[this.Transactions.Count - 1] = value;
        }

        public T GetLastRow()
        {
            if (this.Transactions.Count == 0)
                return null;
            return this.Transactions[this.Transactions.Count - 1];
        }
    }
}
