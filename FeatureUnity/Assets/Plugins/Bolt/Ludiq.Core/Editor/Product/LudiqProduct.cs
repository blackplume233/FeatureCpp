using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    [Product(ID)]
    public sealed class LudiqProduct : Product
    {
        public LudiqProduct() { }

        public override string configurationPanelLabel => "";

        public override string name => "Ludiq Framework";
        public override string description => "";
        public override string authorLabel => "";
        public override string author => "";
        public override string copyrightHolder => "Unity";
        public override string supportUrl => "";
        public const string ID = "Ludiq";

        public const int ToolsMenuPriority = -990000;
        public const int DeveloperToolsMenuPriority = ToolsMenuPriority + 5000;

        public static LudiqProduct instance => (LudiqProduct)ProductContainer.GetProduct(ID);

        public override void Initialize()
        {
            base.Initialize();
        }

        [SettingsProvider]
        private static SettingsProvider BoltExSettingsProvider()
        {
            var provider = new SettingsProvider("Preferences/BoltEx", SettingsScope.User)
            {
                label = "BoltEx",
                guiHandler = (searchContext) =>
                {
                    if (EditorApplication.isCompiling)
                    {
                        LudiqGUI.CenterLoader();
                        return;
                    }

                    instance.configurationPanel.PreferenceItem();
                }
            };

            return provider;
        }
    }
}