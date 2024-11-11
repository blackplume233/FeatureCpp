using Ludiq;

namespace Bolt
{
    [Descriptor(typeof(SuperState))]
    public class SuperStateDescriptor : NesterStateDescriptor<SuperState>
    {
        public SuperStateDescriptor(SuperState state) : base(state) { }
    }
}