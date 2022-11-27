using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace Airports.DAL.PatternParser
{
    class PatternReader
    {
        class PropertyMap
        {
            /// <summary>
            /// Instance key names. Can be a single or multiple (list of) properties.
            /// </summary>
            public List<string> Keys { get; set; }

            /// <summary>
            /// Dictionary for unique instances
            /// </summary>
            public Dictionary<string/*:key*/, object/*:instance*/> Instances { get; set; }

            public PropertyMap()
            {
                Instances = new Dictionary<string, object>();
            }
        }

        /// <summary>
        /// Uses JsonKey and JsonPlacement property attributes.
        /// </summary>
        /// <features>
        /// Current version:
        /// - remove JsonPlacement property: regex already has the sequence information
        /// - create a good struct for regex pattern param
        /// - fix object duplicate problem
        /// TODO: 
        /// - map delegates (with parameters) to properties
        /// - work with list, not just file
        /// - null checks
        /// - logical error in params checks
        /// </features>
        /// <param name="filePath"></param>
        /// <param name="types"></param>
        /// <param name="programId"></param>
        /// <param name="pattern"></param>
        /// <param name="delegates"></param>
        /// <param name="regexFailDelegate"></param>
        /// <returns>Lists are empty collections, if no elements were found.</returns>
        public static Dictionary<string, List<object>> Read(IEnumerable<string> collection, List<Type> types, string programId, Regex pattern, Dictionary<string, PatternPropertyAction> delegates = null, Action<string> regexFailDelegate = null)
        {
            if (types == null || types.Count == 0 || 
                String.IsNullOrWhiteSpace(programId) || 
                pattern == null)
                return null;            

            // # Filter class properties on has PatternProperty attr and programId
            List<PropertyInfo> properties = types
                .SelectMany(t => t.GetProperties())
                .Where(delegate (PropertyInfo p)
                {
                    if (!p.CustomAttributes.Any(c => c.AttributeType == typeof(PatternPropertyAttribute)))
                        return false;

                    var attrList = p.GetCustomAttributes(typeof(PatternPropertyAttribute), false);
                    var jsonAttrList = attrList.Cast<PatternPropertyAttribute>();

                    return jsonAttrList.Any(x => x.ProgramId == programId);
                })
                .ToList();

            // # Init the return value
            var result = new Dictionary<string, List<object>>();
            // # Init property map (for better performance contra memory space)
            var propertyMaps = new Dictionary<string, PropertyMap>();
            foreach (Type type in types)
            {
                result.Add(type.FullName, new List<object>());

                // Instances: Airports.Model.City => [LondonUnitedKingdom => City, IpswichUnitedKingdom => City, LondonUSA => City]
                // Keys: Airports.Model.City => [Name, CountryName]
                propertyMaps.Add(type.FullName, new PropertyMap()
                {
                    Keys = properties
                    .Where(p => p.DeclaringType.FullName == type.FullName && p.GetCustomAttribute<PatternKeyAttribute>() != null)
                    .Select(s => $"{s.DeclaringType.Name}.{s.Name}")
                    .ToList()
                });
            }

            // # Associate full property name with regex group name in a dictionary. 
            // # E.g.: Airports.Model.Airport.Id => Id
            Dictionary<string, string> groupNames = properties
                .ToDictionary(
                k => $"{k.DeclaringType.Name}.{k.Name}",
                v => v.GetCustomAttributes(typeof(PatternPropertyAttribute), false)
                    .Cast<PatternPropertyAttribute>()
                    .Single(f => f.ProgramId == programId)
                    .RegexGroupName);

            string keyJoinChar = "/*$/*/*/";

            foreach (string line in collection)
            {
                Match match = pattern.Match(line);
                if (match.Success)
                {
                    var alreadyHandledTypes = new List<string/*:Type.FullName*/>();

                    // # Create instances
                    var currentInstanceLine = new Dictionary<string/*:Type.FullName*/, object>();
                    foreach (Type type in types)
                    {
                        object instance = null;

                        // # Check if instance is already present
                        List<string> keys = propertyMaps[type.FullName].Keys;

                        if (keys.Count > 0)
                        {
                            // # Get key values and join them into a single key string
                            var keyString = String.Join(keyJoinChar, keys.Select(k => match.Groups[groupNames[k]].Value.ToString()));

                            if (propertyMaps[type.FullName].Instances.TryGetValue(keyString, out instance))
                            {
                                alreadyHandledTypes.Add(type.FullName);
                            }
                            else
                            {
                                // # First time creating the keyed instance
                                instance = Activator.CreateInstance(type);
                                propertyMaps[type.FullName].Instances.Add(keyString, instance);

                                // # Add to result collection
                                result[type.FullName].Add(instance);
                            }
                        }
                        else
                        {
                            // # Every line is a new instance (no key(s) specified)
                            instance = Activator.CreateInstance(type);

                            // # Add to result collection
                            result[type.FullName].Add(instance);
                        }

                        // # Add to current collection
                        currentInstanceLine.Add(type.FullName, instance);
                    }

                    // # Set the value of instances' properties
                    foreach (PropertyInfo property in properties)
                    {
                        if (alreadyHandledTypes.Contains(property.DeclaringType.FullName))
                            continue;

                        string groupName = groupNames[$"{property.DeclaringType.Name}.{property.Name}"];
                        string value = match.Groups[groupName].Value;
                        object instance = currentInstanceLine[property.DeclaringType.FullName]; // NOTE: should always ContainsKey=true

                        // # Convert types
                        if (property.PropertyType == typeof(double) ||
                            property.PropertyType == typeof(float) ||
                            property.PropertyType == typeof(decimal))
                        {
                            char commaChar = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                            if (commaChar == '.')
                                value = value.Replace(',', '.');
                            else if (commaChar == ',')
                                value = value.Replace('.', ',');
                        }
                        property.SetValue(instance, Convert.ChangeType(value, property.PropertyType));
                    }

                    // # Set relations and delegate-defined values
                    foreach (Type type in types)
                    {
                        if (alreadyHandledTypes.Contains(type.FullName))
                            continue;

                        foreach (PropertyInfo property in type.GetProperties())
                        {
                            // # Set instance relations
                            object instance = currentInstanceLine[property.DeclaringType.FullName];
                            object instanceValue;
                            if (currentInstanceLine.TryGetValue(property.PropertyType.FullName, out instanceValue))
                            {
                                property.SetValue(instance, instanceValue);
                            }

                            // # Set properties on delegate
                            // # If has delegate defined for the current property
                            PatternPropertyAction jsonReadDelegate;
                            if (delegates.TryGetValue($"{property.DeclaringType.FullName}.{property.Name}", out jsonReadDelegate))
                            {
                                object paramValue = null;
                                if (!String.IsNullOrWhiteSpace(jsonReadDelegate.PropertyNameParam))
                                {
                                    PropertyInfo paramType = types
                                        .SelectMany(t => t.GetProperties())
                                        .FirstOrDefault(x => jsonReadDelegate.PropertyNameParam == $"{x.ReflectedType.FullName}.{x.Name}");

                                    if (paramType != null)
                                    {
                                        paramValue = paramType
                                            .ReflectedType
                                            .GetProperty(paramType.Name)
                                            .GetValue(currentInstanceLine[paramType.ReflectedType.FullName]);
                                    }
                                }
                                property.SetValue(instance, jsonReadDelegate.Delegate(paramValue));
                            }
                        }
                    }
                }
                else
                {
                    regexFailDelegate(line);
                }
            }
            return result;
        }
    }
}
