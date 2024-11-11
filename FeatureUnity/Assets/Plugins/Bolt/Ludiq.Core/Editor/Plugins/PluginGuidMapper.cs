using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using YamlDotNet.RepresentationModel;
using UnityObject = UnityEngine.Object;

namespace Ludiq
{
    public static class PluginGuidMapper
    {
        [MenuItem("Tools/Bolt/Internal/Assign Plugin GUIDs", priority = LudiqProduct.DeveloperToolsMenuPriority + 203)]
        public static void Run()
        {
            var pluginImporters = PluginImporter.GetAllImporters();

            foreach (var pluginImporter in pluginImporters)
            {
                // Skip native plugins
                if (pluginImporter.isNativePlugin)
                {
                    continue;
                }

                // Skip built-in Unity plugins, which are referenced by full path
                if (!pluginImporter.assetPath.StartsWith("Assets"))
                {
                    continue;
                }

                var assemblyPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, pluginImporter.assetPath);

                var assembly = Codebase.assemblies.FirstOrDefault(a => !string.IsNullOrEmpty(a.Location) && new Uri(assemblyPath) == new Uri(a.Location));

                // Skip if plugin importer has no matching assembly
                if (assembly == null)
                {
                    continue;
                }

                // Modify the matching meta file's YAML
                var metaFilePath = assemblyPath + ".meta";
                var definesPath = Path.ChangeExtension(assemblyPath, "defines");

                if (!File.Exists(definesPath))
                {
                    continue;
                }

                var defines = File.ReadAllText(definesPath).Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(d => d.Trim()).ToArray();

                string guid = null;

                foreach (var define in defines)
                {
                    if (define.StartsWith("GUID_"))
                    {
                        guid = define.TrimStart("GUID_");
                        break;
                    }
                }

                if (guid == null)
                {
                    continue;
                }

                try
                {
                    var yaml = new YamlStream();

                    using (var input = new StreamReader(metaFilePath))
                    {
                        yaml.Load(input);

                        // Dig down the PluginImporter.iconMap node and clear it
                        var rootNode = (YamlMappingNode)yaml.Documents[0].RootNode;
                        var guidNode = (YamlScalarNode)rootNode.Children["guid"];

                        guidNode.Value = guid;
                    }

                    // The only way I found to get Unity to release its freaking file lock
                    File.Delete(metaFilePath);

                    using (var output = new StreamWriter(metaFilePath, false))
                    {
                        yaml.Save(output, false);
                    }

                    Debug.Log($"Set GUID to : {guid}\n{metaFilePath}");
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Failed to traverse plugin meta file '{Path.GetFileName(metaFilePath)}' for GUID mapping: \n{ex}");

                    // Unity broke Yaml specification in 5.5: (... ugh)
                    // Fixed only in 2017.2
                    // https://fogbugz.unity3d.com/default.asp?909364_td7ssft6c2cgh3i2
                    // YamlDotNet won't be able to load the meta files.
                    // Therefore, we'll have to add the guid manually in the meta files.
                    // To facilitate that process, we'll output the proper required GUID.

                    Debug.Log($"Manual GUID for '{Path.GetFileName(metaFilePath)}':\n{guid}\n");
                }
            }

            AssetDatabase.Refresh();
        }
    }
}