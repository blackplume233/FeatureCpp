using System;
using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_4_11 : PluginChangelog
    {
        public Changelog_1_4_11(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.11";

        public override DateTime date => new DateTime(2020, 01, 25);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Fixed] Codebase failing to initialize when assembly metadata is corrupted (often due to obfuscation)";
                yield return "[Fixed] False positive detection of out parameter modifier when parameter was marked with [Out] attribute";
            }
        }
    }
}