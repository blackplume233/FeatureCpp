using Ludiq;

namespace Bolt
{
    [Editor(typeof(SuperUnit))]
    public sealed class SuperUnitEditor : NesterUnitEditor
    {
        public SuperUnitEditor(Metadata metadata) : base(metadata) { }
    }
}