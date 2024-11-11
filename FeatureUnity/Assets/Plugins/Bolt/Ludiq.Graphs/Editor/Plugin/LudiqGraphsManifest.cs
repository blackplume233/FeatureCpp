namespace Ludiq
{
    [Plugin(LudiqGraphs.ID)]
    public sealed class LudiqGraphsManifest : PluginManifest
    {
        private LudiqGraphsManifest(LudiqGraphs plugin) : base(plugin) { }

        public override string name => "Ludiq Graphs";
        public override string author => "";
        public override string description => "Toolset for Unity graph editing.";
    }
}