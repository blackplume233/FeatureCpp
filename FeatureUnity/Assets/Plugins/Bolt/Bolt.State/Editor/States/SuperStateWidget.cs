using Ludiq;
using UnityEditor;
using UnityEngine;

namespace Bolt
{
    [Widget(typeof(SuperState))]
    public sealed class SuperStateWidget : NesterStateWidget<SuperState>, IDragAndDropHandler
    {
        public SuperStateWidget(StateCanvas canvas, SuperState state) : base(canvas, state) { }

        #region Drag & Drop

        public DragAndDropVisualMode dragAndDropVisualMode => DragAndDropVisualMode.Generic;

        public bool AcceptsDragAndDrop()
        {
            return DragAndDropUtility.Is<StateMacro>();
        }

        public void PerformDragAndDrop()
        {
            UndoUtility.RecordEditedObject("Drag & Drop Macro");
            state.nest.source = GraphSource.Macro;
            state.nest.macro = DragAndDropUtility.Get<StateMacro>();
            state.nest.embed = null;
            GUI.changed = true;
        }

        public void UpdateDragAndDrop()
        {

        }

        public void DrawDragAndDropPreview()
        {
            GraphGUI.DrawDragAndDropPreviewLabel(new Vector2(edgePosition.x, outerPosition.yMax), "Replace with: " + DragAndDropUtility.Get<StateMacro>().name, typeof(StateMacro).Icon());
        }

        public void ExitDragAndDrop()
        {

        }

        #endregion
    }
}