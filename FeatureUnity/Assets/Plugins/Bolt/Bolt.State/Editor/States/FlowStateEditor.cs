using Ludiq;

namespace Bolt
{
    [Editor(typeof(FlowState))]
    public sealed class FlowStateEditor : NesterStateEditor
    {
        public FlowStateEditor(Metadata metadata) : base(metadata) { }
    }
}