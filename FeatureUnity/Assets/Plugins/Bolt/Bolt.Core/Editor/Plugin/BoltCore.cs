using Ludiq;

namespace Bolt
{
    [Plugin(ID)]
    [PluginDependency(LudiqCore.ID)]
    [PluginDependency(LudiqGraphs.ID)]
    [Product(BoltProduct.ID)]
    [PluginRuntimeAssembly(ID + ".Runtime")]
    public sealed class BoltCore : Plugin
    {
        public const string ID = "Bolt.Core";

        public BoltCore() : base()
        {
            instance = this;
        }

        public static BoltCore instance { get; private set; }

        public static BoltCoreManifest Manifest => (BoltCoreManifest)instance.manifest;
        public static BoltCoreConfiguration Configuration => (BoltCoreConfiguration)instance.configuration;
        public static BoltCoreResources Resources => (BoltCoreResources)instance.resources;
        public static BoltCorePaths Paths => (BoltCorePaths)instance.paths;
        public static BoltCoreResources.Icons Icons => Resources.icons;
    }
}