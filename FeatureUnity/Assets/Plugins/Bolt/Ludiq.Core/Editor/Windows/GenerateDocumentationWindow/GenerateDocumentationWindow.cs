using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    public sealed class GenerateDocumentationWindow : SinglePageWindow<GenerateDocumentationPage>
    {
        protected override GenerateDocumentationPage CreatePage()
        {
            return new GenerateDocumentationPage();
        }

        protected override void ConfigureWindow()
        {
            base.ConfigureWindow();
            window.minSize = window.maxSize = new Vector2(400, 330);
        }

        static GenerateDocumentationWindow()
        {
            instance = new GenerateDocumentationWindow();
        }

        public static GenerateDocumentationWindow instance { get; }

        [MenuItem("Tools/Bolt/Generate Documentation...", priority = LudiqProduct.ToolsMenuPriority + 304)]
        public new static void Show()
        {
            if (instance.isOpen)
            {
                instance.window.Focus();
            }
            else
            {
                instance.ShowUtility();

                if (instance.isOpen) // Could have been closed in first frame
                {
                    instance.window.Center();
                }
            }
        }
    }
}