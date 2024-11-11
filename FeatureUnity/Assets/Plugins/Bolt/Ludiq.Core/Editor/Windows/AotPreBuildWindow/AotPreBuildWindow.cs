using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Ludiq
{
    public class AotPreBuildWindow : SinglePageWindow<AotPreBuildPage>
    {
        protected override AotPreBuildPage CreatePage()
        {
            return new AotPreBuildPage();
        }

        protected override void ConfigureWindow()
        {
            window.titleContent = new GUIContent("AOT Pre-Build");
            window.minSize = window.maxSize = new Vector2(400, 400);
        }

        static AotPreBuildWindow()
        {
            instance = new AotPreBuildWindow();
        }

        public static AotPreBuildWindow instance { get; }

        [MenuItem("Tools/Bolt/AOT Pre-Build...", priority = LudiqProduct.ToolsMenuPriority + 301)]
        private static void Open()
        {
            instance.ShowUtility();
            instance.window.Center();
        }

        [MenuItem("Tools/Bolt/Clear AOT Pre-Build", priority = LudiqProduct.ToolsMenuPriority + 302)]
        private static void Clear()
        {
            AotPreBuilder.DeleteLinker();
            AotPreBuilder.DeleteStubScript();
            AssetDatabase.Refresh();
            Debug.Log("AOT pre-build data has been cleared.\n");
        }
    }
}
