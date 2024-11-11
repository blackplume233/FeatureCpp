using Ludiq;
using UnityEditor;
using UnityEngine;

namespace Bolt
{
    [Widget(typeof(StateUnit))]
    public class StateUnitWidget : NestrerUnitWidget<StateUnit>, IDragAndDropHandler
    {
        public StateUnitWidget(FlowCanvas canvas, StateUnit unit) : base(canvas, unit) { }

        public DragAndDropVisualMode dragAndDropVisualMode => DragAndDropVisualMode.Generic;

        public bool AcceptsDragAndDrop()
        {
            return DragAndDropUtility.Is<StateMacro>();
        }

        public void PerformDragAndDrop()
        {
            UndoUtility.RecordEditedObject("Drag & Drop Macro");
            unit.nest.source = GraphSource.Macro;
            unit.nest.macro = DragAndDropUtility.Get<StateMacro>();
            unit.nest.embed = null;
            unit.Define();
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
    }
}