using System;

namespace ReadExcelFileFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    class ColumnAttribute: Attribute
    {
        int _columnNumber;

        public ColumnAttribute(int columnNumber)
        {
            _columnNumber = columnNumber;
        }

        public virtual int ColumnNumber { get => _columnNumber; }
    }
}
