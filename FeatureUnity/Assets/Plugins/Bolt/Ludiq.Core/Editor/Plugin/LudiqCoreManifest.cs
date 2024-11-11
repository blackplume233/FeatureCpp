namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    public sealed class LudiqCoreManifest : PluginManifest
    {
        private LudiqCoreManifest(LudiqCore plugin) : base(plugin) { }

        public override string name => "Ludiq Core";
        public override string author => "";
        public override string description => "IoC framework and toolset for Unity plugin development.";
    }
}