using System;
using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_4_12 : PluginChangelog
    {
        public Changelog_1_4_12(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.12";

        public override DateTime date => new DateTime(2020, 03, 14);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Added] Unity 2020.1 compatibility";
                yield return "[Fixed] AOT stub generation for type names starting with an underscore then a number";
            }
        }
    }
}