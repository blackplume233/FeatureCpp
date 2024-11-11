using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    public class AotPreBuildPage : Page
    {
        public AotPreBuildPage()
        {
            title = $"AOT Pre-Build";
            icon = LudiqCore.Resources.LoadIcon("Icons/Windows/AotPreBuildWindow/AotPreBuildPage.png");
        }

        protected override void OnContentGUI()
        {
            GUILayout.BeginVertical(Styles.background, GUILayout.ExpandHeight(true));

            var label = "Every Unity build target except Standalone requires 'ahead of time' (AOT) compilation. ";
            label += "Before building for these platforms, you should always run this step to create AOT stubs for reflection and generics. ";
            label += "Otherwise, there may be runtime errors in your builds.";

            var label2 = "This pre-build step will scan all the assets and scenes in the project to generate the required AOT stubs. ";
            label2 += "Make sure you save the current scene before starting.";

            LudiqGUI.FlexibleSpace();
            GUILayout.Label(label, LudiqStyles.centeredLabel);
            LudiqGUI.FlexibleSpace();
            GUILayout.Label(label2, LudiqStyles.centeredLabel);
            LudiqGUI.FlexibleSpace();

            LudiqGUI.BeginHorizontal();
            LudiqGUI.FlexibleSpace();

            if (GUILayout.Button("Pre-Build", Styles.nextButton))
            {
                try
                {
                    AotPreBuilder.GenerateLinker();
                    AotPreBuilder.GenerateStubScript();
                    EditorUtility.DisplayDialog("AOT Pre-Build", $"AOT pre-build has completed.\n\nThe generated linker file is located at:\n'{PathUtility.FromProject(AotPreBuilder.aotStubsPath)}'\n\nThe generated stubs script is located at:\n'{PathUtility.FromProject(AotPreBuilder.linkerPath)}'", "OK");
                    Complete();
                }
                catch (Exception ex)
                {
                    EditorUtility.DisplayDialog("AOT Pre-Build Error", $"AOT pre-build has failed: \n{ex.Message}", "OK");
                    Debug.LogException(ex);
                    AotPreBuilder.DeleteLinker();
                    AotPreBuilder.DeleteStubScript();
                }
            }

            LudiqGUI.FlexibleSpace();
            LudiqGUI.EndHorizontal();

            LudiqGUI.FlexibleSpace();
            EditorGUILayout.HelpBox("AOT Pre-build is in Beta. Please report any compile-time or runtime error you may encounter.", MessageType.Warning);
            LudiqGUI.FlexibleSpace();

            LudiqGUI.EndVertical();
        }

        public static class Styles
        {
            static Styles()
            {
                background = new GUIStyle(LudiqStyles.windowBackground);
                background.padding = new RectOffset(30, 30, 10, 16);

                nextButton = new GUIStyle("Button");
                nextButton.padding = new RectOffset(20, 20, 10, 10);
            }

            public static readonly GUIStyle background;
            public static readonly GUIStyle nextButton;
        }
    }
}