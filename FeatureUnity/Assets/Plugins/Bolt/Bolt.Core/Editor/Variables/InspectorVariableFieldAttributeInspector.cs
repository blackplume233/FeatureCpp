﻿using System;
using Ludiq;
using UnityEditor;
using UnityEngine;

namespace Bolt
{
    [Inspector(typeof(InspectorVariableNameAttribute))]
    public class VariableNameAttributeInspector : Inspector
    {
        public VariableNameAttributeInspector(Metadata metadata) : base(metadata)
        {
            if (metadata.definedType != typeof(string))
            {
                throw new NotSupportedException($"'{nameof(InspectorVariableNameAttribute)}' can only be used on strings.");
            }

            direction = metadata.GetAttribute<InspectorVariableNameAttribute>().direction;
        }

        public ActionDirection direction { get; set; }

        protected override float GetHeight(float width, GUIContent label)
        {
            return HeightWithLabel(metadata, width, EditorGUIUtility.singleLineHeight, label);
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            position = BeginBlock(metadata, position, label);

            var newValue = BoltGUI.VariableField(position, GUIContent.none, (string)metadata.value, direction);

            if (EndBlock(metadata))
            {
                metadata.RecordUndo();
                metadata.value = newValue;
            }
        }
    }
}