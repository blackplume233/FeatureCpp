using System.Linq;
using Ludiq;
using UnityEditor;
using UnityEngine;

namespace Bolt
{
    [Widget(typeof(SuperUnit))]
    public class SuperUnitWidget : NestrerUnitWidget<SuperUnit>, IDragAndDropHandler
    {
        public SuperUnitWidget(FlowCanvas canvas, SuperUnit unit) : base(canvas, unit) { }

        protected override NodeColorMix baseColor
        {
            get
            {
                // TODO: Move to descriptor for optimization
                using (var recursion = Recursion.New(1))
                {
                    if (unit.nest.graph?.GetUnitsRecursive(recursion).OfType<IEventUnit>().Any() ?? false)
                    {
                        return NodeColor.Green;
                    }
                }

                return base.baseColor;
            }
        }

        public DragAndDropVisualMode dragAndDropVisualMode => DragAndDropVisualMode.Generic;

        public bool AcceptsDragAndDrop()
        {
            return DragAndDropUtility.Is<FlowMacro>();
        }

        public void PerformDragAndDrop()
        {
            UndoUtility.RecordEditedObject("Drag & Drop Macro");
            unit.nest.source = GraphSource.Macro;
            unit.nest.macro = DragAndDropUtility.Get<FlowMacro>();
            unit.nest.embed = null;
            unit.Define();
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
    }
}