using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    public sealed class GeneratePropertyProvidersWindow : SinglePageWindow<GeneratePropertyProvidersPage>
    {
        protected override GeneratePropertyProvidersPage CreatePage()
        {
            return new GeneratePropertyProvidersPage();
        }

        protected override void ConfigureWindow()
        {
            base.ConfigureWindow();
            window.minSize = window.maxSize = new Vector2(400, 330);
        }

        static GeneratePropertyProvidersWindow()
        {
            instance = new GeneratePropertyProvidersWindow();
        }

        public static GeneratePropertyProvidersWindow instance { get; }

        [MenuItem("Tools/Bolt/Generate Custom Inspectors...", priority = LudiqProduct.ToolsMenuPriority + 303)]
        public new static void Show()
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
    }
}