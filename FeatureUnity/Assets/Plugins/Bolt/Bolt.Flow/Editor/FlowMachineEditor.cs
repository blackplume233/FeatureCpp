using Ludiq;

namespace Bolt
{
    [Editor(typeof(FlowMachine))]
    public class FlowMachineEditor : MachineEditor
    {
        public FlowMachineEditor(Metadata metadata) : base(metadata) { }
    }
}