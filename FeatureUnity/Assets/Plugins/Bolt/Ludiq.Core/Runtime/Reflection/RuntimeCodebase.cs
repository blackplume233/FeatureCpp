using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Ludiq.AssemblyQualifiedNameParser;
using UnityEngine;
using Exception = System.Exception;

namespace Ludiq
{
    public static class RuntimeCodebase
    {
        private static readonly object @lock = new object();

        private static readonly List<Type> _types = new List<Type>();

        public static IEnumerable<Type> types => _types;

        private static readonly List<Assembly> _assemblies = new List<Assembly>();

        public static IEnumerable<Assembly> assemblies => _assemblies;

        private static readonly Dictionary<string, Type> typeSerializations = new Dictionary<string, Type>();

        private static Dictionary<string, string> _renamedTypes = null;

        private static readonly Dictionary<Type, Dictionary<string, string>> _renamedMembers = new Dictionary<Type, Dictionary<string, string>>();

        static RuntimeCodebase()
        {
            lock (@lock)
            {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    _assemblies.Add(assembly);

                    foreach (var assemblyType in assembly.GetTypesSafely())
                    {
                        _types.Add(assemblyType);
                    }
                }
            }
        }

        #region Serialization

        public static void PrewarmTypeDeserialization(Type type)
        {
            Ensure.That(nameof(type)).IsNotNull(type);

            var serialization = SerializeType(type);

            if (typeSerializations.ContainsKey(serialization))
            {
                // Some are duplicates, but almost always compiler generated stuff.
                // Safe to ignore, and anyway what would we even do to deserialize them properly?
            }
            else
            {
                typeSerializations.Add(serialization, type);
            }
        }

        public static string SerializeType(Type type)
        {
            Ensure.That(nameof(type)).IsNotNull(type);

            return type?.FullName;
        }

        public static bool TryDeserializeType(string typeName, out Type type)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                type = null;
                return false;
            }

            lock (@lock)
            {
                if (!TryCachedTypeLookup(typeName, out type))
                {
                    if (!TrySystemTypeLookup(typeName, out type))
                    {
                        if (!TryRenamedTypeLookup(typeName, out type))
                        {
                            return false;
                        }
                    }

                    typeSerializations.Add(typeName, type);
                }

                return true;
            }
        }

        public static Type DeserializeType(string typeName)
        {
            if (!TryDeserializeType(typeName, out var type))
            {
                throw new SerializationException($"Unable to find type: '{typeName ?? "(null)"}'.");
            }

            return type;
        }

        private static bool TryCachedTypeLookup(string typeName, out Type type)
        {
            return typeSerializations.TryGetValue(typeName, out type);
        }

        private static bool TrySystemTypeLookup(string typeName, out Type type)
        {
            foreach (var assembly in _assemblies)
            {
                type = assembly.GetType(typeName);

                if (type != null)
                {
                    return true;
                }
            }

            type = null;
            return false;
        }

        private static bool TryRenamedTypeLookup(string previousTypeName, out Type type)
        {
            string newTypeName;

            // Try for an exact match in our renamed types dictionary. 
            // That should work for every non-generic type.
            // If we can't get an exact match, we'll try parsing the previous type name,
            // replacing all the renamed types we can find, then reconstructing it.
            if (!renamedTypes.TryGetValue(previousTypeName, out newTypeName))
            {
                var parsedTypeName = new ParsedAssemblyQualifiedName(previousTypeName);

                foreach (var renamedType in renamedTypes)
                {
                    parsedTypeName.Replace(renamedType.Key, renamedType.Value);
                }

                newTypeName = parsedTypeName.ToString();
            }

            // Run the system lookup
            if (TrySystemTypeLookup(newTypeName, out type))
            {
                return true;
            }

            type = null;
            return false;
        }

        #endregion

        #region Renaming

        // Can't use AttributeUtility here, because the caching system will
        // try to load all attributes of the type for efficiency, which is
        // not allowed on the serialization thread because some of Unity's
        // attribute constructors use Unity API methods (ugh!).

        public static Dictionary<string, string> renamedTypes
        {
            get
            {
                if (_renamedTypes == null)
                {
                    // Fetch only on demand because attribute lookups are expensive
                    _renamedTypes = FetchRenamedTypes();
                }

                return _renamedTypes;
            }
        }

        public static Dictionary<string, string> RenamedMembers(Type type)
        {
            Dictionary<string, string> renamedMembers;

            if (!_renamedMembers.TryGetValue(type, out renamedMembers))
            {
                renamedMembers = FetchRenamedMembers(type);
                _renamedMembers.Add(type, renamedMembers);
            }

            return renamedMembers;
        }

        private static Dictionary<string, string> FetchRenamedMembers(Type type)
        {
            Ensure.That(nameof(type)).IsNotNull(type);

            var renamedMembers = new Dictionary<string, string>();

            var members = type.GetExtendedMembers(Member.SupportedBindingFlags);

            foreach (var member in members)
            {
                IEnumerable<RenamedFromAttribute> renamedFromAttributes;

                try
                {
                    renamedFromAttributes = Attribute.GetCustomAttributes(member, typeof(RenamedFromAttribute), false).Cast<RenamedFromAttribute>();
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Failed to fetch RenamedFrom attributes for member '{member}':\n{ex}");
                    continue;
                }

                var newMemberName = member.Name;

                foreach (var renamedFromAttribute in renamedFromAttributes)
                {
                    var previousMemberName = renamedFromAttribute.previousName;

                    if (renamedMembers.ContainsKey(previousMemberName))
                    {
                        Debug.LogWarning($"Multiple members on '{type}' indicate having been renamed from '{previousMemberName}'.\nIgnoring renamed attributes for '{member}'.");

                        continue;
                    }

                    renamedMembers.Add(previousMemberName, newMemberName);
                }
            }

            return renamedMembers;
        }

        private static Dictionary<string, string> FetchRenamedTypes()
        {
            var renamedTypes = new Dictionary<string, string>();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypesSafely())
                {
                    IEnumerable<RenamedFromAttribute> renamedFromAttributes;

                    try
                    {
                        renamedFromAttributes = Attribute.GetCustomAttributes(type, typeof(RenamedFromAttribute), false).Cast<RenamedFromAttribute>();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"Failed to fetch RenamedFrom attributes for type '{type}':\n{ex}");
                        continue;
                    }


                    var newTypeName = type.FullName;

                    foreach (var renamedFromAttribute in renamedFromAttributes)
                    {
                        var previousTypeName = renamedFromAttribute.previousName;

                        if (renamedTypes.ContainsKey(previousTypeName))
                        {
                            Debug.LogWarning($"Multiple types indicate having been renamed from '{previousTypeName}'.\nIgnoring renamed attributes for '{type}'.");

                            continue;
                        }

                        renamedTypes.Add(previousTypeName, newTypeName);
                    }
                }
            }

            return renamedTypes;
        }

        #endregion
    }
}
