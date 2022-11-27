using ReadExcelFileFramework.Models;
using ReadExcelFileFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcelFileFramework.Components.ScriptGenerators
{
    class SQLGenerator
    {
        string outputPath;
        ExcelSheet<TransactionExtended> excelSheet;

        public SQLGenerator(ExcelSheet<TransactionExtended> excelSheet)
        {
            this.excelSheet = excelSheet;
        }

        public void Run()
        {
            // insert into TRANSACTION
            // insert into TRANSACTION_TAG_CONN

        }
    }
}
/* 

SET IDENTITY_INSERT ON;
INSERT INTO TRANSACTION (ID, ACCOUNTING_DATE, TRANSACTION_ID, TYPE, ACCOUNT, ACCOUNT_NAME, SUM, CURRENCY_ID, MESSAGE, CREATE_DATE) VALUES 
(1, '2000-01-01', N'', N'', N'', N'', N'', N'', 0, 1, N'', now());

SET IDENTITY_INSERT OFF;
INSERT INTO TRANSACTION_TAG_CONN (TRANSACTION_ID, TAG_ID) VALUES (1, 1);

*/
