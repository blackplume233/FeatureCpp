using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    public class SetupCompletePage : Page
    {
        public SetupCompletePage(Product product)
        {
            Ensure.That(nameof(product)).IsNotNull(product);

            title = "Setup Complete";
            shortTitle = "Finish";
            icon = LudiqCore.Resources.LoadIcon("Icons/Windows/SetupWizard/SetupCompletePage.png");

            this.product = product;
        }

        private readonly Product product;

        protected override void OnShow()
        {
            base.OnShow();

            foreach (var plugin in product.plugins.ResolveDependencies())
            {
                plugin.configuration.projectSetupCompleted = true;
                plugin.configuration.editorSetupCompleted = true;
                plugin.configuration.Save();
            }

            AssetDatabase.SaveAssets();

            // Run the gizmo disabler. It's an expansive operation,
            // so we don't do it on every assembly reload, but this way at least
            // we make sure that the gizmos will be properly disabled on install.
            AnnotationDisabler.DisableGizmos();
        }

        protected override void OnContentGUI()
        {
            GUILayout.BeginVertical(Styles.background, GUILayout.ExpandHeight(true));

            LudiqGUI.FlexibleSpace();
            GUILayout.Label($"{product.name} has successfully been setup.", LudiqStyles.centeredLabel);
            LudiqGUI.FlexibleSpace();

            var hasManual = !string.IsNullOrEmpty(product.manualUrl);

            var hasSupport = !string.IsNullOrEmpty(product.supportUrl);

            if (hasManual)
            {
                LudiqGUI.BeginHorizontal();
                LudiqGUI.FlexibleSpace();

                if (GUILayout.Button("Manual", Styles.button))
                {
                    Process.Start(product.manualUrl);
                }

                LudiqGUI.FlexibleSpace();
                LudiqGUI.EndHorizontal();
            }

            if (hasManual && hasSupport)
            {
                LudiqGUI.Space(10);
            }

            if (hasSupport)
            {
                LudiqGUI.BeginHorizontal();
                LudiqGUI.FlexibleSpace();

                if (GUILayout.Button("Support", Styles.button))
                {
                    Process.Start(product.supportUrl);
                }

                LudiqGUI.FlexibleSpace();
                LudiqGUI.EndHorizontal();
            }

            LudiqGUI.Space(10);

            {
                LudiqGUI.BeginHorizontal();
                LudiqGUI.FlexibleSpace();

                if (GUILayout.Button("Configuration", Styles.button))
                {
                    product.configurationPanel.Show();
                    Complete();
                }

                LudiqGUI.FlexibleSpace();
                LudiqGUI.EndHorizontal();
            }

            LudiqGUI.Space(10);

            {
                LudiqGUI.BeginHorizontal();
                LudiqGUI.FlexibleSpace();

                if (GUILayout.Button("Close", Styles.button))
                {
                    Complete();
                }

                LudiqGUI.FlexibleSpace();
                LudiqGUI.EndHorizontal();
            }

            LudiqGUI.FlexibleSpace();

            LudiqGUI.EndVertical();
        }

        public static class Styles
        {
            static Styles()
            {
                background = new GUIStyle(LudiqStyles.windowBackground);
                background.padding = new RectOffset(10, 10, 10, 10);

                button = new GUIStyle("Button");
                button.padding = new RectOffset(15, 15, 5, 5);
            }

            public static readonly GUIStyle background;
            public static readonly GUIStyle button;
        }
    }
}