﻿using UnityEditor;

namespace Bolt
{
    [CustomEditor(typeof(GlobalMessageListener), true)]
    public class GlobalMessageListenerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("This component is automatically added to relay Unity messages to Bolt.", MessageType.Info);
        }
    }
}