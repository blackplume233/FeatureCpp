using Ludiq;

namespace Bolt
{
    [Descriptor(typeof(FlowState))]
    public class FlowStateDescriptor : NesterStateDescriptor<FlowState>
    {
        public FlowStateDescriptor(FlowState state) : base(state) { }
    }
}