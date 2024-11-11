using System.Collections.Generic;
using Ludiq;

namespace Bolt
{
    [Plugin(ID)]
    [PluginDependency(LudiqCore.ID)]
    [PluginDependency(LudiqGraphs.ID)]
    [PluginDependency(BoltCore.ID)]
    [Product(BoltProduct.ID)]
    [PluginRuntimeAssembly(ID + ".Runtime")]
    public sealed class BoltFlow : Plugin
    {
        public override IEnumerable<Page> SetupWizardPages()
        {
            yield return new AssemblyOptionsPage();
            yield return new TypeOptionsPage();
        }

        public BoltFlow()
        {
            instance = this;
        }

        public static BoltFlow instance { get; private set; }

        public const string ID = "Bolt.Flow";

        public static BoltFlowManifest Manifest => (BoltFlowManifest)instance.manifest;

        public static BoltFlowConfiguration Configuration => (BoltFlowConfiguration)instance.configuration;

        public static BoltFlowResources Resources => (BoltFlowResources)instance.resources;

        public static BoltFlowResources.Icons Icons => Resources.icons;

        public static BoltFlowPaths Paths => (BoltFlowPaths)instance.paths;

        public override IEnumerable<string> tips
        {
            get
            {
                yield return "Did you know you can dance?";
                yield return "Lorem ipsum dolor sit amet";
            }
        }
    }
}
