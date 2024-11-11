using System;
using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_4_7 : PluginChangelog
    {
        public Changelog_1_4_7(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.7";

        public override DateTime date => new DateTime(2019, 09, 26);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Fixed] Memory leaks caused by static caches";
            }
        }
    }
}