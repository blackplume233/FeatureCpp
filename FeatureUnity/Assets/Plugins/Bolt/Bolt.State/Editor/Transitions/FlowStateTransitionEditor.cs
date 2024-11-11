using Ludiq;

namespace Bolt
{
    [Editor(typeof(FlowStateTransition))]
    public sealed class FlowStateTransitionEditor : NesterStateTransitionEditor
    {
        public FlowStateTransitionEditor(Metadata metadata) : base(metadata) { }
    }
}