using System;
using System.Collections.Generic;
using System.Text;

namespace Airports.DAL.PatternParser
{
    /// <summary>
    /// ...
    /// </summary>
    /// <features>
    /// Current version:
    /// x Supports only 1 parameter, as one of the property of the current instance.
    ///     Example: Current instance is type Airports.Model.Airport. InstancePropertyNameParam = "Name"
    /// - Support a property across all instances in the current json line.
    ///     Example: Current instance is type Airports.Model.Airport. PropertyNameParam = "Airports.Model.Country"
    /// TODO: 
    /// - Support a list of parameters. Replace string PropertyNameParam, with string[] Params. 
    /// </features>
    public struct PatternPropertyAction
    {
        public Func<object, object> Delegate { get; set; }
        public string PropertyNameParam { get; set; }
    }
}
