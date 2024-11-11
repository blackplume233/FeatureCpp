using System;
using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_0_3 : PluginChangelog
    {
        public Changelog_1_0_3(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.0.3";
        public override DateTime date => new DateTime(2017, 10, 03);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Fixed] Constant fields crash";
                yield return "[Fixed] Unused references";
                yield return "[Fixed] Void type bug";
                yield return "[Optimized] Plugin container access";
                yield return "[Optimized] Inspector draw speed";
            }
        }
    }
}