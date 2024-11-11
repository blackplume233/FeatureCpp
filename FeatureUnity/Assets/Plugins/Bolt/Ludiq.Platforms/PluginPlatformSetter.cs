using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    public class PluginPlatformSetter : AssetPostprocessor
    {
        static PluginPlatformSetter()
        {
            buildTargetDefines = new Dictionary<string, HashSet<BuildTarget>>();
            platformData = new Dictionary<BuildTarget, Dictionary<string, Dictionary<string, string>>>();
            editorData = new Dictionary<string, Dictionary<string, string>>();

            // Figure out which build targets are valid
            validBuildTargets = Enum.GetValues(typeof(BuildTarget)).Cast<BuildTarget>().Where(target => target >= 0 && !typeof(BuildTarget).GetField(target.ToString()).IsDefined(typeof(ObsoleteAttribute), true)).ToArray();

            // Map defines

            MapPlatformDefine("RUNTIME", validBuildTargets);
            MapPlatformDefine("ALL", validBuildTargets);

            MapPlatformDefine("STANDALONE_WIN", BuildTarget.StandaloneWindows, BuildTarget.StandaloneWindows64);
            MapPlatformDefine("STANDALONE_OSX", BuildTarget.StandaloneOSX);
            MapPlatformDefine("STANDALONE_LINUX", BuildTarget.StandaloneLinux, BuildTarget.StandaloneLinux64, BuildTarget.StandaloneLinuxUniversal);

            MapPlatformDefine
            (
                "STANDALONE",
                BuildTarget.StandaloneWindows,
                BuildTarget.StandaloneWindows64,
                BuildTarget.StandaloneOSX,
                BuildTarget.StandaloneLinux,
                BuildTarget.StandaloneLinux64,
                BuildTarget.StandaloneLinuxUniversal
            );

            MapPlatformDefine("WII", BuildTarget.WiiU);
            MapPlatformDefine("IOS", BuildTarget.iOS);
            MapPlatformDefine("ANDROID", BuildTarget.Android);
            MapPlatformDefine("PS4", BuildTarget.PS4);
            MapPlatformDefine("XBOXONE", BuildTarget.XboxOne);
            MapPlatformDefine("TIZEN", BuildTarget.Tizen);
            MapPlatformDefine("TVOS", BuildTarget.tvOS);
            MapPlatformDefine("WEBGL", BuildTarget.WebGL);
            MapPlatformDefine("WSA", BuildTarget.WSAPlayer);
            MapPlatformDefine("SWITCH", BuildTarget.Switch);

            MapPlatformDefine
            (
                "AOT",
                BuildTarget.iOS,
                BuildTarget.Android,
                BuildTarget.Tizen,
                BuildTarget.PSP2,
                BuildTarget.PS4,
                BuildTarget.PSM,
                BuildTarget.XboxOne,
                BuildTarget.N3DS,
                BuildTarget.WiiU,
                BuildTarget.tvOS,
                BuildTarget.Switch,
                BuildTarget.WSAPlayer,
                BuildTarget.WebGL
            );

            MapPlatformDefine
            (
                "JIT",
                buildTargetDefines["STANDALONE"].ToArray()
            );

            MapPlatformDataDefine("TARGET_CPU_X86", BuildTarget.WSAPlayer, "CPU", "x86");
            MapPlatformDataDefine("TARGET_CPU_X86_64", BuildTarget.WSAPlayer, "CPU", "x86_64");
            MapPlatformDataDefine("TARGET_CPU_ANY", BuildTarget.WSAPlayer, "CPU", "AnyCPU");
            MapEditorDataDefine("TARGET_CPU_X86", "CPU", "x86");
            MapEditorDataDefine("TARGET_CPU_X86_64", "CPU", "x86_64");
            MapEditorDataDefine("TARGET_CPU_ANY", "CPU", "AnyCPU");

            MapEditorDataDefine("TARGET_OS_WIN", "OS", "Windows");
            MapEditorDataDefine("TARGET_OS_OSX", "OS", "OSX");
            MapEditorDataDefine("TARGET_OS_LINUX", "OS", "Linux");
            MapEditorDataDefine("TARGET_OS_ANY", "OS", "AnyOS");

            MapPlatformDataDefine("TARGET_BACKEND_IL2CPP", BuildTarget.WSAPlayer, "ScriptingBackend", "Il2Cpp");
            MapPlatformDataDefine("TARGET_BACKEND_IL2CPP", BuildTarget.Android, "ScriptingBackend", "Il2Cpp");
            MapPlatformDataDefine("TARGET_BACKEND_DOTNET", BuildTarget.WSAPlayer, "ScriptingBackend", "DotNet");
            MapPlatformDataDefine("TARGET_BACKEND_DOTNET", BuildTarget.Android, "ScriptingBackend", "DotNet");
            MapPlatformDataDefine("TARGET_BACKEND_ANY", BuildTarget.WSAPlayer, "ScriptingBackend", "AnyScriptingBackend");
            MapPlatformDataDefine("TARGET_BACKEND_ANY", BuildTarget.Android, "ScriptingBackend", "AnyScriptingBackend");

            MapPlatformDataDefine("TARGET_WSA_BACKEND_IL2CPP", BuildTarget.WSAPlayer, "ScriptingBackend", "Il2Cpp");
            MapPlatformDataDefine("TARGET_WSA_BACKEND_DOTNET", BuildTarget.WSAPlayer, "ScriptingBackend", "DotNet");
            MapPlatformDataDefine("TARGET_WSA_BACKEND_ANY", BuildTarget.WSAPlayer, "ScriptingBackend", "AnyScriptingBackend");

            MapPlatformDataDefine("TARGET_WSA_SDK_8_0", BuildTarget.WSAPlayer, "ScriptingBackend", "SDK80");
            MapPlatformDataDefine("TARGET_WSA_SDK_8_1", BuildTarget.WSAPlayer, "ScriptingBackend", "SDK81");
            MapPlatformDataDefine("TARGET_WSA_SDK_PHONE_8_1", BuildTarget.WSAPlayer, "ScriptingBackend", "PhoneSDK81");
            MapPlatformDataDefine("TARGET_WSA_SDK_UWP", BuildTarget.WSAPlayer, "ScriptingBackend", "UWP");
            MapPlatformDataDefine("TARGET_WSA_SDK_ANY", BuildTarget.WSAPlayer, "ScriptingBackend", "AnySDK");
        }

        private static readonly Dictionary<string, HashSet<BuildTarget>> buildTargetDefines;
        private static readonly Dictionary<BuildTarget, Dictionary<string, Dictionary<string, string>>> platformData;
        private static readonly Dictionary<string, Dictionary<string, string>> editorData;
        private static readonly BuildTarget[] validBuildTargets;

        private static void MapPlatformDefine(string define, params BuildTarget[] buildTargets)
        {
            buildTargetDefines.Add(define, new HashSet<BuildTarget>(buildTargets));
        }

        private static void MapPlatformDataDefine(string define, BuildTarget buildTarget, string key, string value)
        {
            if (!platformData.ContainsKey(buildTarget))
            {
                platformData.Add(buildTarget, new Dictionary<string, Dictionary<string, string>>());
            }

            var defineData = platformData[buildTarget];

            if (!defineData.ContainsKey(define))
            {
                defineData.Add(define, new Dictionary<string, string>());
            }

            defineData[define].Add(key, value);
        }

        private static void MapEditorDataDefine(string define, string key, string value)
        {
            if (!editorData.ContainsKey(define))
            {
                editorData.Add(define, new Dictionary<string, string>());
            }

            editorData[define].Add(key, value);
        }

        [MenuItem("Tools/Bolt/Internal/Assign Plugin Platforms", priority = -990000 + 5000 + 201)]
        public static void Run()
        {
            var log = new StringBuilder();

            log.AppendLine("Plugin Platforms Analysis");
            log.AppendLine("Report: ");

            var pluginImporters = PluginImporter.GetAllImporters();

            for (var i = 0; i < pluginImporters.Length; i++)
            {
                var pluginImporter = pluginImporters[i];

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
                var definesPath = Path.ChangeExtension(assemblyPath, "defines");

                EditorUtility.DisplayProgressBar("Plugin Platform Analysis...", assemblyPath, (float)i / pluginImporters.Length);

                log.AppendLine();
                log.AppendLine($"<b>{pluginImporter.assetPath}</b>: ");

                // Skip plugin if it has no companion defines file
                if (!File.Exists(definesPath))
                {
                    log.AppendLine($"   <color=#FF0000>No defines file, skipping.</color>");
                    continue;
                }

                try
                {
                    var defines = File.ReadAllText(definesPath).Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(d => d.Trim()).ToArray();

                    log.AppendLine("Defines:");

                    foreach (var define in defines)
                    {
                        log.AppendLine($"   {define}");
                    }

                    pluginImporter.SetCompatibleWithAnyPlatform(false);

                    log.AppendLine("Platforms:");

                    var editorCompatible = IsEditorCompatible(defines);
                    pluginImporter.SetCompatibleWithEditor(editorCompatible);

                    if (editorCompatible)
                    {
                        log.AppendLine($"   Editor");

                        foreach (var data in editorData.Where(kvp => defines.Contains(kvp.Key)).Select(kvp => kvp.Value))
                        {
                            foreach (var item in data)
                            {
                                pluginImporter.SetEditorData(item.Key, item.Value);
                                log.AppendLine($"      {item.Key}: {item.Value}");
                            }
                        }
                    }

                    foreach (var buildTarget in validBuildTargets)
                    {
                        var targetCompatible = IsBuildTargetCompatible(buildTarget, defines);
                        pluginImporter.SetCompatibleWithPlatform(buildTarget, targetCompatible);

                        if (targetCompatible)
                        {
                            log.AppendLine($"   {buildTarget}");

                            if (platformData.ContainsKey(buildTarget))
                            {
                                foreach (var data in platformData[buildTarget].Where(kvp => defines.Contains(kvp.Key)).Select(kvp => kvp.Value))
                                {
                                    foreach (var item in data)
                                    {
                                        pluginImporter.SetPlatformData(buildTarget, item.Key, item.Value);
                                        log.AppendLine($"      {item.Key}: {item.Value}");
                                    }
                                }
                            }
                        }
                    }

                    pluginImporter.SaveAndReimport();
                }
                catch (Exception ex)
                {
                    Debug.LogWarning(ex);
                    log.AppendLine("Analysis failed. See warnings above.");
                }
            }

            EditorUtility.ClearProgressBar();

            Debug.Log(log.ToString());
        }

        private static bool IsBuildTargetCompatible(BuildTarget candidate, IEnumerable<string> defines)
        {
            var validTargets = new HashSet<BuildTarget>();

            // Whitelist
            foreach (var define in defines)
            {
                if (define.StartsWith("TARGET_INCLUDE_"))
                {
                    var key = define.Substring("TARGET_INCLUDE_".Length);

                    if (buildTargetDefines.ContainsKey(key))
                    {
                        validTargets.UnionWith(buildTargetDefines[key]);
                    }
                }
            }

            // Blacklist
            foreach (var define in defines)
            {
                if (define.StartsWith("TARGET_EXCLUDE_"))
                {
                    var key = define.Substring("TARGET_EXCLUDE_".Length);

                    if (buildTargetDefines.ContainsKey(key))
                    {
                        validTargets.ExceptWith(buildTargetDefines[key]);
                    }
                }
            }

            return validTargets.Contains(candidate);
        }

        private static bool IsEditorCompatible(IEnumerable<string> defines)
        {
            return (defines.Contains("TARGET_INCLUDE_EDITOR") || defines.Contains("TARGET_INCLUDE_ALL")) && !defines.Contains("TARGET_EXCLUDE_EDITOR");
        }
    }
}