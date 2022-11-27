using System;
using System.Collections.Generic;
using System.Text;

namespace Airports.DAL.PatternParser
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    class PatternPropertyAttribute : Attribute
    {
        string _regexGroupName;
        string _programId;

        public PatternPropertyAttribute(string programId, string regexGroupName)
        {
            _programId = programId;
            _regexGroupName = regexGroupName;
        }

        public virtual string RegexGroupName { get => _regexGroupName; }
        public virtual string ProgramId { get => _programId; }
    }
}
