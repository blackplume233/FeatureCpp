using Ludiq;
using UnityEditor;
using UnityEngine;

namespace Bolt
{
    public sealed class UnitOptionsWizard : Wizard
    {
        public static UnitOptionsWizard instance { get; }

        static UnitOptionsWizard()
        {
            instance = new UnitOptionsWizard();
        }

        public UnitOptionsWizard() : base()
        {
            pages.Add(new AssemblyOptionsPage());
            pages.Add(new TypeOptionsPage());
        }

        protected override void ConfigureWindow()
        {
            window.titleContent = new GUIContent("Unit Options Wizard");
            window.minSize = window.maxSize = new Vector2(500, 400);
        }

        [MenuItem("Tools/Bolt/Unit Options Wizard...", priority = BoltProduct.ToolsMenuPriority + 301)]
        private static void HookUpdateWizard()
        {
            if (instance.isOpen)
            {
                instance.window.Focus();
            }
            else
            {
                instance.ShowUtility();
                instance.window.Center();
            }
        }

        [MenuItem("Tools/Bolt/Build Unit Options", priority = BoltProduct.ToolsMenuPriority + 302)]
        private static void BuildUnitOptions()
        {
            UnitBase.Build();
        }

        [MenuItem("Tools/Bolt/Update Unit Options", priority = BoltProduct.ToolsMenuPriority + 303)]
        private static void UpdateUnitOptions()
        {
            UnitBase.Update();
        }
    }
}
