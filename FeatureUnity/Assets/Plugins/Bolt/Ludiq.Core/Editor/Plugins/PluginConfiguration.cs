using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    [PluginModule(required = true)]
    public class PluginConfiguration : IPluginModule, IEnumerable<PluginConfigurationItemMetadata>
    {
        protected PluginConfiguration(Plugin plugin)
        {
            this.plugin = plugin;
        }

        public virtual void Initialize()
        {
            savedVersion = plugin.manifest.version;
            Load();
        }

        public virtual void LateInitialize() { }

        public Plugin plugin { get; }

        public virtual string header => plugin.manifest.name;



        #region Lifecycle

        private void Load()
        {
            LoadEditorPrefs();
            LoadProjectSettings();
        }

        public void Reset()
        {
            foreach (var item in allItems)
            {
                item.Reset();
            }
        }

        public void Save()
        {
            foreach (var item in allItems)
            {
                item.Save();
            }
        }

        #endregion



        #region All Items

        private IEnumerable<PluginConfigurationItemMetadata> allItems => LinqUtility.Concat<PluginConfigurationItemMetadata>(editorPrefs, projectSettings);

        public IEnumerator<PluginConfigurationItemMetadata> GetEnumerator()
        {
            return allItems.OrderBy(i => i.member.MetadataToken).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public PluginConfigurationItemMetadata GetMetadata(string memberName)
        {
            return allItems.First(metadata => metadata.member.Name == memberName);
        }

        #endregion



        #region Editor Prefs

        internal List<EditorPrefMetadata> editorPrefs;

        private void LoadEditorPrefs()
        {
            editorPrefs = new List<EditorPrefMetadata>();

            var metadata = Metadata.Root();

            foreach (var memberInfo in GetType().GetMembers().Where(f => f.HasAttribute<EditorPrefAttribute>()).OrderBy(m => m.MetadataToken))
            {
                editorPrefs.Add(metadata.EditorPref(this, memberInfo));
            }
        }

        #endregion



        #region Project Settings

        internal List<ProjectSettingMetadata> projectSettings;

        private string projectSettingsStoragePath => plugin.paths.projectSettings;

        internal DictionaryAsset projectSettingsAsset { get; private set; }

        private void LoadProjectSettings()
        {
            AssetUtility.TryLoad(projectSettingsStoragePath, out DictionaryAsset _projectSettingsAsset);

            projectSettingsAsset = _projectSettingsAsset;

            projectSettings = new List<ProjectSettingMetadata>();

            var metadata = Metadata.Root();

            foreach (var memberInfo in GetType().GetMembers().Where(f => f.HasAttribute<ProjectSettingAttribute>()).OrderBy(m => m.MetadataToken))
            {
                projectSettings.Add(metadata.ProjectSetting(this, memberInfo));
            }
        }

        public void SaveProjectSettingsAsset()
        {
            EditorUtility.SetDirty(projectSettingsAsset);
        }

        #endregion



        #region Items

        /// <summary>
        /// Whether the plugin was properly setup.
        /// </summary>
        [ProjectSetting(visibleCondition = nameof(developerMode), resettable = false)]
        public bool projectSetupCompleted { get; internal set; }

        /// <summary>
        /// Whether the plugin was properly setup.
        /// </summary>
        [EditorPref(visibleCondition = nameof(developerMode), resettable = false)]
        public bool editorSetupCompleted { get; internal set; }

        /// <summary>
        /// The last version to which the plugin successfully upgraded.
        /// </summary>
        [ProjectSetting(visibleCondition = nameof(developerMode), resettable = false)]
        public SemanticVersion savedVersion { get; internal set; }

        protected bool developerMode => LudiqCore.Configuration.developerMode;

        #endregion



        #region Menu

        [MenuItem("Tools/Bolt/Internal/Delete All Project Settings", priority = LudiqProduct.DeveloperToolsMenuPriority + 401)]
        public static void DeleteAllProjectSettings()
        {
            foreach (var plugin in PluginContainer.plugins)
            {
                AssetDatabase.DeleteAsset(PathUtility.FromProject(plugin.configuration.projectSettingsStoragePath));
            }
        }

        [MenuItem("Tools/Bolt/Internal/Delete All Editor Prefs", priority = LudiqProduct.DeveloperToolsMenuPriority + 402)]
        public static void DeleteAllEditorPrefs()
        {
            foreach (var plugin in PluginContainer.plugins)
            {
                foreach (var editorPref in plugin.configuration.editorPrefs)
                {
                    EditorPrefs.DeleteKey(editorPref.namespacedKey);
                }
            }
        }

        [MenuItem("Tools/Bolt/Internal/Delete All Player Prefs", priority = LudiqProduct.DeveloperToolsMenuPriority + 403)]
        public static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        #endregion
    }
}
