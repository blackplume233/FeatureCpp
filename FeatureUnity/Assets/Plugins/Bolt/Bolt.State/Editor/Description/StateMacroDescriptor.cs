using Ludiq;

namespace Bolt
{
    [Descriptor(typeof(StateMacro))]
    public sealed class StateMacroDescriptor : MacroDescriptor<StateMacro, MacroDescription>
    {
        public StateMacroDescriptor(StateMacro target) : base(target) { }
    }
}