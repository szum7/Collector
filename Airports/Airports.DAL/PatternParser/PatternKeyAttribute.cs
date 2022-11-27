using System;
using System.Collections.Generic;
using System.Text;

namespace Airports.DAL.PatternParser
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    class PatternKeyAttribute : Attribute
    {
        string _programId;

        public PatternKeyAttribute(string programId)
        {
            _programId = programId;
        }

        public virtual string ProgramId { get => _programId; }
    }
}
