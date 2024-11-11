using Ludiq;

namespace Bolt
{
    [Editor(typeof(SuperState))]
    public sealed class SuperStateEditor : NesterStateEditor
    {
        public SuperStateEditor(Metadata metadata) : base(metadata) { }
    }
}