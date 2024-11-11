using System.IO;
using Ludiq;

namespace Bolt
{
    [Plugin(BoltCore.ID)]
    public class BoltCorePaths : PluginPaths
    {
        public BoltCorePaths(Plugin plugin) : base(plugin) { }

        public string variableResources => Path.Combine(persistentGenerated, "Variables/Resources");
    }
}