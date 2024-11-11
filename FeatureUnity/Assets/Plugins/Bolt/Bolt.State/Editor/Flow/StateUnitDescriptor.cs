using Ludiq;

namespace Bolt
{
    [Descriptor(typeof(StateUnit))]
    public class StateUnitDescriptor : NesterUnitDescriptor<StateUnit>
    {
        public StateUnitDescriptor(StateUnit unit) : base(unit) { }
    }
}