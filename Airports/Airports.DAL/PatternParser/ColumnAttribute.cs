using System;

namespace Airports.DAL.PatternParser
{
    /// <summary>
    /// No different run support!
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    class ColumnAttribute : Attribute
    {
        string _columnName;

        public ColumnAttribute(string columnName)
        {
            _columnName = columnName;
        }

        public virtual string ColumnName { get => _columnName; }
    }
}
