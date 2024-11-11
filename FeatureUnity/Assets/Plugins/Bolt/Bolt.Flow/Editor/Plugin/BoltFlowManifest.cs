using Ludiq;

namespace Bolt
{
    [Plugin(BoltFlow.ID)]
    public sealed class BoltFlowManifest : PluginManifest
    {
        private BoltFlowManifest(BoltFlow plugin) : base(plugin) { }

        public override string name => "Bolt Flow";
        public override string author => "";
        public override string description => "Flow-graph based visual scripting.";

        public override void Initialize()
        {
            base.Initialize();

            // Variables moved to Bolt.Core assembly in v.1.3

            ScriptReferenceResolver.AddReplacement
            (
                new ScriptReference(BoltFlow.instance, "Bolt", "Variables"),
                new ScriptReference(BoltCore.instance, typeof(Variables))
            );

            ScriptReferenceResolver.AddReplacement
            (
                new ScriptReference(BoltFlow.instance, "Bolt", "SceneVariables"),
                new ScriptReference(BoltCore.instance, typeof(SceneVariables))
            );

            ScriptReferenceResolver.AddReplacement
            (
                new ScriptReference(BoltFlow.instance, "Bolt", "VariablesAsset"),
                new ScriptReference(BoltCore.instance, typeof(VariablesAsset))
            );
        }
    }
}