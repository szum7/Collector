using System;

namespace Airports.DAL.PatternParser
{
    /// <summary>
    /// No different run support!
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    class RegexValidatorAttribute : Attribute
    {
        string _pattern;

        public RegexValidatorAttribute(string pattern)
        {
            _pattern = pattern;
        }

        public virtual string Pattern { get => _pattern; }
    }
}
