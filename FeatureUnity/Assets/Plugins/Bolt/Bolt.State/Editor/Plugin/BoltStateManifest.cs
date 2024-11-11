using Ludiq;

namespace Bolt
{
    [Plugin(BoltState.ID)]
    public sealed class BoltStateManifest : PluginManifest
    {
        private BoltStateManifest(BoltState plugin) : base(plugin) { }

        public override string name => "Bolt State";
        public override string author => "";
        public override string description => "State-machine based visual scripting.";
    }
}