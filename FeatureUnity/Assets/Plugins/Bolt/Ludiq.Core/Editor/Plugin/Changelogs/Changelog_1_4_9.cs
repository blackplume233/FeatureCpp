using System;
using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_4_9 : PluginChangelog
    {
        public Changelog_1_4_9(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.9";

        public override DateTime date => new DateTime(2019, 11, 04);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Fixed] Graphs failing to load when they included a newly created macro (the legendary 'undo bug')";
            }
        }
    }
}