using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Ludiq
{
    [Inspector(typeof(LayerMask))]
    public class LayerMaskInspector : Inspector
    {
        public LayerMaskInspector(Metadata metadata) : base(metadata) { }

        protected override float GetHeight(float width, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            position = BeginBlock(metadata, position, label);

            var newValue = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(EditorGUI.MaskField
            (
                position,
                InternalEditorUtility.LayerMaskToConcatenatedLayersMask((LayerMask)metadata.value),
                InternalEditorUtility.layers
            ));

            if (EndBlock(metadata))
            {
                metadata.RecordUndo();
                metadata.value = newValue;
            }
        }
    }
}