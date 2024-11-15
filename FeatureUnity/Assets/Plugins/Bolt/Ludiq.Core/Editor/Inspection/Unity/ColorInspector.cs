﻿using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    [Inspector(typeof(Color))]
    public class ColorInspector : Inspector
    {
        public ColorInspector(Metadata metadata) : base(metadata) { }

        protected override float GetHeight(float width, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            position = BeginBlock(metadata, position, label);

            var newValue = EditorGUI.ColorField(position, (Color)metadata.value);

            if (EndBlock(metadata))
            {
                metadata.RecordUndo();
                metadata.value = newValue;
            }
        }
    }
}