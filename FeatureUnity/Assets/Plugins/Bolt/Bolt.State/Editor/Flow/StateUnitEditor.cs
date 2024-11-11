using Ludiq;

namespace Bolt
{
    [Editor(typeof(StateUnit))]
    public sealed class StateUnitEditor : NesterUnitEditor
    {
        public StateUnitEditor(Metadata metadata) : base(metadata) { }
    }
}