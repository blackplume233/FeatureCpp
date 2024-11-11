using Ludiq;

namespace Bolt
{
    [Plugin(BoltCore.ID)]
    public sealed class BoltCoreConfiguration : PluginConfiguration
    {
        private BoltCoreConfiguration(BoltCore plugin) : base(plugin) { }

        public override string header => "Core";

        /// <summary>
        /// Whether inactive graph nodes should be dimmed.
        /// </summary>
        [EditorPref]
        public bool dimInactiveNodes { get; set; } = true;

        /// <summary>
        /// Whether incompatible graph nodes should be dimmed.
        /// </summary>
        [EditorPref]
        public bool dimIncompatibleNodes { get; set; } = true;

        /// <summary>
        /// Whether the header help panel should be shown in the  variables window.
        /// </summary>
        [EditorPref]
        public bool showVariablesHelp { get; set; } = true;

        /// <summary>
        /// Whether the scene variables object should be created automatically.
        /// </summary>
        [EditorPref]
        public bool createSceneVariables { get; set; } = true;
    }
}