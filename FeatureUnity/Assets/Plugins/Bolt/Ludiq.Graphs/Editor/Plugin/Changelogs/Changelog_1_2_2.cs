using System;
using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(LudiqGraphs.ID)]
    internal class Changelog_1_2_2 : PluginChangelog
    {
        public Changelog_1_2_2(Plugin plugin) : base(plugin) { }

        public override string description => null;
        public override SemanticVersion version => "1.2.2";
        public override DateTime date => new DateTime(2017, 12, 04);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Added] Droplets animation helpers";
                yield return "[Fixed] Shift-locking axis detection";
                yield return "[Fixed] Context click issues on OSX";
                yield return "[Fixed] Graph group label not always focusing ";
            }
        }
    }
}