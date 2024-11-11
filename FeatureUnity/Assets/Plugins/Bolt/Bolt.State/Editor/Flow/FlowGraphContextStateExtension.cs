using Ludiq;

namespace Bolt
{
    [GraphContextExtension(typeof(FlowGraphContext))]
    public sealed class FlowGraphContextStateExtension : GraphContextExtension<FlowGraphContext>
    {
        public FlowGraphContextStateExtension(FlowGraphContext context) : base(context) { }

        public override bool AcceptsDragAndDrop()
        {
            return DragAndDropUtility.Is<StateMacro>();
        }

        public override void PerformDragAndDrop()
        {
            var statemacro = DragAndDropUtility.Get<StateMacro>();
            var stateUnit = new StateUnit(statemacro);
            context.canvas.AddUnit(stateUnit, DragAndDropUtility.position);
        }

        public override void DrawDragAndDropPreview()
        {
            GraphGUI.DrawDragAndDropPreviewLabel(DragAndDropUtility.offsetedPosition, DragAndDropUtility.Get<StateMacro>().name, typeof(StateMacro).Icon());
        }
    }
}