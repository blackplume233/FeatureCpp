using Ludiq;

namespace Bolt
{
    [Plugin(ID)]
    [PluginDependency(LudiqCore.ID)]
    [PluginDependency(LudiqGraphs.ID)]
    [PluginDependency(BoltCore.ID)]
    [Product(BoltProduct.ID)]
    [PluginRuntimeAssembly(ID + ".Runtime")]
    public sealed class BoltState : Plugin
    {
        public const string ID = "Bolt.State";

        public BoltState() : base()
        {
            instance = this;
        }

        public static BoltState instance { get; private set; }

        public static BoltStateManifest Manifest => (BoltStateManifest)instance.manifest;
        public static BoltStateConfiguration Configuration => (BoltStateConfiguration)instance.configuration;
        public static BoltStateResources Resources => (BoltStateResources)instance.resources;
        public static BoltStateResources.Icons Icons => Resources.icons;
    }
}