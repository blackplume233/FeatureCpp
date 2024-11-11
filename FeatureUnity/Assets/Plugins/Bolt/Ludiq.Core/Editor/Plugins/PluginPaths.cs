#pragma warning disable 162

using System;
using System.IO;
using UnityEngine;

namespace Ludiq
{
    [PluginModule(required = true)]
    public class PluginPaths : IPluginModule
    {
        protected PluginPaths(Plugin plugin)
        {
            this.plugin = plugin;
        }

        public virtual void Initialize()
        {
            if (isFirstPass)
            {
                Debug.LogWarning($"Plugin '{plugin.id}' is in a special folder that makes it compile first.\nThis might cause issues with generated assets.");
            }
        }

        public virtual void LateInitialize() { }

        public Plugin plugin { get; }

        private string _package;

        public string package
        {
            get
            {
                if (_package == null)
                {
                    _package = PathUtility.GetRootPath(rootFileName, Path.Combine(Paths.assets, $"Ludiq/{plugin.id}"), true);
                }

                return _package;
            }
        }

        protected string rootFileName => plugin.id + ".root";

        public bool isFirstPass => package.Contains("/Plugins/") || package.Contains("/Standard Assets/") || package.Contains("/Pro Standard Assets/");

        public string resourcesFolder => Path.Combine(package, "Editor/Resources");

        public string resourcesBundle => Path.Combine(package, "Editor/Resources.assetbundle");

        public string projectSettings => Path.Combine(persistentGenerated, "ProjectSettings.asset");

        public string persistentGenerated => Path.Combine(package, "Generated");

        public string transientGenerated => Path.Combine(package, "Generated");

        public string iconMap => Path.Combine(package, "IconMap");
    }
}
