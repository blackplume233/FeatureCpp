﻿using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    [Inspector(typeof(Bounds))]
    public class BoundsInspector : Inspector
    {
        public BoundsInspector(Metadata metadata) : base(metadata) { }

        protected override float GetHeight(float width, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            position = BeginBlock(metadata, position, label);

            var newValue = EditorGUI.BoundsField(position, (Bounds)metadata.value);

            if (EndBlock(metadata))
            {
                metadata.RecordUndo();
                metadata.value = newValue;
            }
        }
    }
}