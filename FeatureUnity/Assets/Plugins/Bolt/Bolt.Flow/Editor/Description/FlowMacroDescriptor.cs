using Ludiq;

namespace Bolt
{
    [Descriptor(typeof(FlowMacro))]
    public sealed class FlowMacroDescriptor : MacroDescriptor<FlowMacro, MacroDescription>
    {
        public FlowMacroDescriptor(FlowMacro target) : base(target) { }
    }
}