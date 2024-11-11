using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(ID)]
    [Product(LudiqProduct.ID)]
    [PluginRuntimeAssembly(ID + ".Runtime")]
    public class LudiqCore : Plugin
    {
        public LudiqCore() : base()
        {
            instance = this;
        }

        public static LudiqCore instance { get; private set; }

        public override IEnumerable<Page> SetupWizardPages()
        {
            yield return new NamingSchemePage();

            // Disabling for now as they have too high a risk for failure
            // (especially GenerateDocumentation) and scare off new users.
            // The operations remain available in the menu.
            // yield return new GenerateDocumentationPage();
            // yield return new GeneratePropertyProvidersPage();
        }

        public const string ID = "Ludiq.Core";

        public static LudiqCoreManifest Manifest => (LudiqCoreManifest)instance.manifest;
        public static LudiqCorePaths Paths => (LudiqCorePaths)instance.paths;
        public static LudiqCoreConfiguration Configuration => (LudiqCoreConfiguration)instance.configuration;
        //public static LudiqCoreReporter Reporter => (LudiqCoreReporter)instance.reporter;
        public static LudiqCoreResources Resources => (LudiqCoreResources)instance.resources;
        public static LudiqCoreResources.Icons Icons => Resources.icons;
    }
}