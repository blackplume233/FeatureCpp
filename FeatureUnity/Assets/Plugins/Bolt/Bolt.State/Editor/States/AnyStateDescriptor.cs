using Ludiq;

namespace Bolt
{
    [Descriptor(typeof(AnyState))]
    public class AnyStateDescriptor : StateDescriptor<AnyState>
    {
        public AnyStateDescriptor(AnyState state) : base(state) { }
    }
}