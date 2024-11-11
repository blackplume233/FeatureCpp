using Ludiq;
using UnityEditor;
using UnityEngine;

namespace Bolt
{
    [Widget(typeof(FlowStateTransition))]
    public sealed class FlowStateTransitionWidget : NesterStateTransitionWidget<FlowStateTransition>, IDragAndDropHandler
    {
        public FlowStateTransitionWidget(StateCanvas canvas, FlowStateTransition transition) : base(canvas, transition) { }

        #region Drag & Drop

        public DragAndDropVisualMode dragAndDropVisualMode => DragAndDropVisualMode.Generic;

        public bool AcceptsDragAndDrop()
        {
            return DragAndDropUtility.Is<FlowMacro>();
        }

        public void PerformDragAndDrop()
        {
            UndoUtility.RecordEditedObject("Drag & Drop Macro");
            transition.nest.source = GraphSource.Macro;
            transition.nest.macro = DragAndDropUtility.Get<FlowMacro>();
            transition.nest.embed = null;
            GUI.changed = true;
        }

        public void UpdateDragAndDrop()
        {

        }

        public void DrawDragAndDropPreview()
        {
            GraphGUI.DrawDragAndDropPreviewLabel(new Vector2(edgePosition.x, outerPosition.yMax), "Replace with: " + DragAndDropUtility.Get<FlowMacro>().name, typeof(FlowMacro).Icon());
        }

        public void ExitDragAndDrop()
        {

        }

        #endregion
    }
}