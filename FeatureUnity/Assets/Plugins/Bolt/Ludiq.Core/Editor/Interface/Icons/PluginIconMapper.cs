using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using UnityEditor;
using UnityEngine;
using YamlDotNet.Core.Events;
using YamlDotNet.RepresentationModel;
using UnityObject = UnityEngine.Object;

namespace Ludiq
{
    public class PluginIconMapper : AssetPostprocessor
    {
        [MenuItem("Tools/Bolt/Internal/Assign Plugin Iconmaps", priority = LudiqProduct.DeveloperToolsMenuPriority + 202)]
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

                var plugin = PluginContainer.plugins.FirstOrDefault(p => p.runtimeAssembly == assembly);

                // Skip if the assembly is not mapped to a Ludiq plugin
                if (plugin == null)
                {
                    continue;
                }

                var mappableTypes = assembly.GetTypesSafely().Where(t => typeof(UnityObject).IsAssignableFrom(t));

                // Skip assemblies without any mappable types
                if (!mappableTypes.Any())
                {
                    continue;
                }

                // Create a dictionary of type-to-icon mapping
                // The key is the fully qualified type name.
                // The value is the Unity-assigned GUID of the icon file.
                var iconsByTypes = new Dictionary<string, string>();

                foreach (var type in mappableTypes)
                {
                    var iconAssetPath = PathUtility.FromProject(Path.Combine(plugin.paths.iconMap, type.CSharpFileName(true, false) + ".png"));

                    var typeIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(iconAssetPath);

                    // Skip if type icon loading fails
                    if (typeIcon == null)
                    {
                        continue;
                    }

                    iconsByTypes.Add(type.ToString(), AssetDatabase.AssetPathToGUID(iconAssetPath));
                }

                // Modify the matching meta file's YAML
                var metaFilePath = assemblyPath + ".meta";

                try
                {
                    var yaml = new YamlStream();

                    new FileInfo(metaFilePath).IsReadOnly = false;

                    using (var input = new StreamReader(metaFilePath))
                    {
                        yaml.Load(input);

                        // Dig down the PluginImporter.iconMap node and clear it
                        var rootNode = (YamlMappingNode)yaml.Documents[0].RootNode;
                        var pluginImporterNode = (YamlMappingNode)rootNode.Children["PluginImporter"];
                        var iconMapNode = (YamlMappingNode)pluginImporterNode.Children["iconMap"];

                        iconMapNode.Children.Clear();

                        AddIconMap(iconMapNode, iconsByTypes);

                        iconMapNode.Style = MappingStyle.Block;
                    }

                    // The only way I found to get Unity to release its freaking file lock
                    File.Delete(metaFilePath);

                    using (var output = new StreamWriter(metaFilePath))
                    {
                        yaml.Save(output, false);
                    }

                    Debug.Log($"Added icon mapping.\n{metaFilePath}");
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Failed to traverse plugin meta file '{Path.GetFileName(metaFilePath)}' for icon mapping: \n{ex}");

                    if (iconsByTypes.Count > 0)
                    {
                        // Unity broke Yaml specification in 5.5: (... ugh)
                        // https://fogbugz.unity3d.com/default.asp?909364_td7ssft6c2cgh3i2
                        // YamlDotNet won't be able to load the meta files.
                        // Therefore, we'll have to add the iconmap manually in the meta files.
                        // To facilitate that process, we'll output the proper required Yaml
                        // to the console for each file.

                        var iconMapNode = new YamlMappingNode();
                        AddIconMap(iconMapNode, iconsByTypes);
                        iconMapNode.Style = MappingStyle.Block;
                        var rootNode = new YamlMappingNode { { "iconMap", iconMapNode } };
                        var yaml = new YamlStream(new YamlDocument(rootNode));
                        var output = new StringWriter();
                        yaml.Save(output, false);
                        output.Dispose();
                        var @string = output.ToString();
                        var indented = "";
                        foreach (var line in @string.Split('\n'))
                        {
                            indented += "  " + line + "\n";
                        }
                        Debug.Log($"Manual iconMap node for '{Path.GetFileName(metaFilePath)}':\n\n{indented}\n\n");
                    }
                }
            }

            AssetDatabase.Refresh();

            AnnotationDisabler.DisableGizmos();
        }

        private static void AddIconMap(YamlMappingNode iconMapNode, Dictionary<string, string> iconsByTypes)
        {
            // Add our own mappings.
            // https://forum.unity3d.com/threads/custom-asset-icons.118656/#post-2443602
            foreach (var typeIconMap in iconsByTypes)
            {
                var typeFullName = typeIconMap.Key;
                var iconAssetGuid = typeIconMap.Value;

                var iconNode = new YamlMappingNode
                {
                    { "fileID", "2800000" },
                    { "guid", iconAssetGuid },
                    { "type", "3" }
                };

                iconMapNode.Add(typeFullName, iconNode);
            }
        }
    }
}