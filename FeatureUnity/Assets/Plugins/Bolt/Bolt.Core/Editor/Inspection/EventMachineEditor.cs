using Ludiq;

namespace Bolt
{
    [Editor(typeof(IEventMachine))]
    public class EventMachineEditor : MachineEditor
    {
        public EventMachineEditor(Metadata metadata) : base(metadata) { }
    }
}
