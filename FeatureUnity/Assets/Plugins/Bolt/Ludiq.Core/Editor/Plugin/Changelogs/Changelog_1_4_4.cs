using System;
using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_4_4 : PluginChangelog
    {
        public Changelog_1_4_4(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.4";

        public override DateTime date => new DateTime(2019, 06, 11);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Fixed] Documentation generator MSBuild path on Visual Studio 2019";
                yield return "[Fixed] Force locked assembly reloads during play mode to avoid data corruption";
            }
        }
    }
}