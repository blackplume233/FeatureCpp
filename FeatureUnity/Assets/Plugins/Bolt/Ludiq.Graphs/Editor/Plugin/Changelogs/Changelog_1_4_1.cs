using System;
using System.Collections.Generic;
using Ludiq;

namespace Bolt
{
    [Plugin(LudiqGraphs.ID)]
    internal class Changelog_1_4_1 : PluginChangelog
    {
        public Changelog_1_4_1(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.1";

        public override DateTime date => new DateTime(2019, 01, 22);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Optimized] Graph instantiation and graph instance tracking";
                yield return "[Fixed] Issue with hot control detection on newly created groups";
                yield return "[Fixed] ExitGUI exception being thrown on undo/redo";
                yield return "[Fixed] Invalid decorator error caused by deleted selection items on undo/redo";
                yield return "[Fixed] Issue where graph reference interning would return references with stale data";
                yield return "[Removed] Group label focus code because it was never reliable";
            }
        }
    }
}