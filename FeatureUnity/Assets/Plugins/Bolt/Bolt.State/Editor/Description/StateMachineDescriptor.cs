using Ludiq;

namespace Bolt
{
    [Descriptor(typeof(StateMachine))]
    public sealed class StateMachineDescriptor : MachineDescriptor<StateMachine, MachineDescription>
    {
        public StateMachineDescriptor(StateMachine target) : base(target) { }
    }
}