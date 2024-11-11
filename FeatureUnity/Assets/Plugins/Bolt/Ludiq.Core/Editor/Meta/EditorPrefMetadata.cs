using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    public sealed class EditorPrefMetadata : PluginConfigurationItemMetadata
    {
        public EditorPrefMetadata(PluginConfiguration configuration, MemberInfo member, Metadata parent) : base(configuration, member, parent) { }

        public string namespacedKey => $"{configuration.plugin.id}.{key}";

        public override bool exists => EditorPrefs.HasKey(namespacedKey);

        public override void Load()
        {
            try
            {
                value = new SerializationData(EditorPrefs.GetString(namespacedKey)).Deserialize();
            }
            catch (Exception)
            {
                Debug.LogWarning($"Failed to deserialize editor pref '{configuration.plugin.id}.{key}', reverting to default.");
                value = defaultValue;
                Save();
            }

            if (!definedType.IsAssignableFrom(valueType))
            {
                Debug.LogWarning($"Failed to deserialize editor pref '{configuration.plugin.id}.{key}' as '{definedType.CSharpName()}', reverting to default.");
                value = defaultValue;
                Save();
            }
        }

        public override void Save()
        {
            EditorPrefs.SetString(namespacedKey, value.Serialize().json);
        }
    }
}