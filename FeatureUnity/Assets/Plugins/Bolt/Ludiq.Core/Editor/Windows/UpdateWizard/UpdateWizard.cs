using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    [InitializeAfterPlugins]
    public sealed class UpdateWizard : Wizard
    {
        public UpdateWizard(Product product)
        {
            Ensure.That(nameof(product)).IsNotNull(product);

            this.product = product;
            plugins = product.plugins.ToList();
        }

        public UpdateWizard(IEnumerable<Plugin> plugins)
        {
            Ensure.That(nameof(plugins)).IsNotNull(plugins);

            this.plugins = plugins.ToList();
        }

        private void Initialize()
        {
            PluginContainer.UpdateVersionMismatch();

            canNavigateBackward = false;

            pages.Clear();

            pages.Add(new UpdateStartPage(product, plugins));

            if (plugins.Any(plugin => plugin.manifest.savedVersion > plugin.manifest.currentVersion))
            {
                pages.Add(new UpdateConsolidatePage(plugins));
            }
            else
            {
                pages.Add(new UpdateBackupPage());
            }

            pages.Add(new UpdateMigrationPage(plugins));
            pages.Add(new UpdateUserActionsPage(plugins));
            pages.Add(new UpdateCompletePage(plugins));
            pages.Add(new ChangelogsPage(plugins));
        }

        private readonly Product product;
        private readonly List<Plugin> plugins;

        protected override void ConfigureWindow()
        {
            window.titleContent = new GUIContent($"{product?.name ?? "Plugin"} Update Wizard");
            window.minSize = window.maxSize = new Vector2(500, 400);
        }

        public override void OnShow()
        {
            // Initializing the pages on show in case the developer
            // manually changes versions in the project settings for testing
            Initialize();

            // Run the gizmo disabler. It's an expansive operation,
            // so we don't do it on every assembly reload, but this way at least
            // we make sure that the gizmos will be properly disabled at every update.
            AnnotationDisabler.DisableGizmos();

            base.OnShow();
        }

        static UpdateWizard()
        {
            global = new UpdateWizard(PluginContainer.plugins);
        }

        public static UpdateWizard global { get; }

        public static void DrawPluginVersionTable(IEnumerable<Plugin> plugins)
        {
            var savedColumnHeader = new GUIContent("Saved");
            var installedColumnHeader = new GUIContent("Installed");

            var pluginsColumnWidth = 0f;
            var savedColumnWidth = Styles.columnHeader.CalcSize(savedColumnHeader).x;
            var installedColumnWidth = Styles.columnHeader.CalcSize(installedColumnHeader).x;
            var stateColumnWidth = 0f;

            foreach (var plugin in plugins)
            {
                pluginsColumnWidth = Mathf.Max(pluginsColumnWidth, Styles.pluginName.CalcSize(new GUIContent(plugin.manifest.name)).x);
                savedColumnWidth = Mathf.Max(savedColumnWidth, Styles.version.CalcSize(new GUIContent(plugin.manifest.savedVersion.ToString())).x);
                installedColumnWidth = Mathf.Max(installedColumnWidth, Styles.version.CalcSize(new GUIContent(plugin.manifest.currentVersion.ToString())).x);
                stateColumnWidth = Mathf.Max(stateColumnWidth, Styles.state.CalcSize(VersionStateContent(plugin)).x);
            }

            LudiqGUI.BeginVertical();

            // Header row
            LudiqGUI.BeginHorizontal();
            LudiqGUI.FlexibleSpace();
            GUILayout.Label(GUIContent.none, Styles.columnHeader, GUILayout.Width(pluginsColumnWidth));
            LudiqGUI.Space(Styles.columnSpacing);
            GUILayout.Label(savedColumnHeader, Styles.columnHeader, GUILayout.Width(savedColumnWidth));
            LudiqGUI.Space(Styles.columnSpacing);
            GUILayout.Label(installedColumnHeader, Styles.columnHeader, GUILayout.Width(installedColumnWidth));
            LudiqGUI.Space(Styles.columnSpacing);
            GUILayout.Label(GUIContent.none, Styles.state, GUILayout.Width(stateColumnWidth));
            LudiqGUI.FlexibleSpace();
            LudiqGUI.EndHorizontal();

            // Plugin rows
            foreach (var plugin in plugins)
            {
                LudiqGUI.Space(Styles.rowSpacing);

                LudiqGUI.BeginHorizontal();
                LudiqGUI.FlexibleSpace();
                GUILayout.Label(new GUIContent(plugin.manifest.name), Styles.pluginName, GUILayout.Width(pluginsColumnWidth));
                LudiqGUI.Space(Styles.columnSpacing);
                GUILayout.Label(new GUIContent(plugin.manifest.savedVersion.ToString()), Styles.version, GUILayout.Width(savedColumnWidth));
                LudiqGUI.Space(Styles.columnSpacing);
                GUILayout.Label(new GUIContent(plugin.manifest.currentVersion.ToString()), Styles.version, GUILayout.Width(installedColumnWidth));
                LudiqGUI.Space(Styles.columnSpacing);
                GUILayout.Label(VersionStateContent(plugin), Styles.state, GUILayout.Width(stateColumnWidth));
                LudiqGUI.FlexibleSpace();
                LudiqGUI.EndHorizontal();
            }

            LudiqGUI.EndVertical();
        }

        private static GUIContent VersionStateContent(Plugin plugin)
        {
            if (plugin.manifest.savedVersion < plugin.manifest.currentVersion)
            {
                return new GUIContent("New version", LudiqCore.Icons.upgrade?[IconSize.Small]);
            }
            else if (plugin.manifest.savedVersion == plugin.manifest.currentVersion)
            {
                return new GUIContent("Up to date", LudiqCore.Icons.upToDate?[IconSize.Small]);
            }
            else if (plugin.manifest.savedVersion > plugin.manifest.currentVersion)
            {
                return new GUIContent("Downgrade", LudiqCore.Icons.downgrade?[IconSize.Small]);
            }

            throw new NotSupportedException("Impossible plugin version state.");
        }

        public static class Styles
        {
            static Styles()
            {
                pluginName = new GUIStyle(EditorStyles.label);
                pluginName.alignment = TextAnchor.MiddleRight;

                version = new GUIStyle(EditorStyles.label);
                version.alignment = TextAnchor.MiddleCenter;

                columnHeader = new GUIStyle(EditorStyles.label);
                columnHeader.alignment = TextAnchor.LowerCenter;
                columnHeader.fontStyle = FontStyle.Bold;

                state = new GUIStyle();
                state.fixedWidth = IconSize.Small;
                state.fixedHeight = IconSize.Small;
                state.imagePosition = ImagePosition.ImageOnly;
                state.alignment = TextAnchor.MiddleCenter;
            }

            public static readonly GUIStyle pluginName;
            public static readonly GUIStyle columnHeader;
            public static readonly GUIStyle version;
            public static readonly GUIStyle state;
            public static readonly float columnSpacing = 10;
            public static readonly float rowSpacing = 10;
        }
    }
}