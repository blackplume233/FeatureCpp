using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    [Plugin(ID)]
    [PluginDependency(LudiqCore.ID)]
    [Product(LudiqProduct.ID)]
    [PluginRuntimeAssembly(ID + ".Runtime")]
    public sealed class LudiqGraphs : Plugin
    {
        public const string ID = "Ludiq.Graphs";

        public LudiqGraphs() : base()
        {
            instance = this;
        }

        public static LudiqGraphs instance { get; private set; }

        public static LudiqGraphsManifest Manifest => (LudiqGraphsManifest)instance.manifest;
        public static LudiqGraphsConfiguration Configuration => (LudiqGraphsConfiguration)instance.configuration;
        public static LudiqGraphsResources Resources => (LudiqGraphsResources)instance.resources;
        public static LudiqGraphsResources.Icons Icons => Resources.icons;

        public static class Styles
        {
            static Styles()
            {
                nodeLabel = new GUIStyle(EditorStyles.label);

                if (EditorGUIUtility.isProSkin)
                {
                    nodeLabel.normal.textColor = ColorUtility.Gray(0.92f);
                }
            }

            public static readonly GUIStyle nodeLabel;
        }
    }
}