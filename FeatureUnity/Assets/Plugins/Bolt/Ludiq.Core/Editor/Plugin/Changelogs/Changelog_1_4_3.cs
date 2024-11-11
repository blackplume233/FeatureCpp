using System;
using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_4_3 : PluginChangelog
    {
        public Changelog_1_4_3(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.3";

        public override DateTime date => new DateTime(2019, 04, 29);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Fixed] Issue with Unity 2019 compatibility due to internal annotations API change";
                yield return "[Fixed] Standalone JIT support detection";
            }
        }
    }

    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_4_3f2 : PluginChangelog
    {
        public Changelog_1_4_3f2(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.3f2";

        public override DateTime date => new DateTime(2019, 05, 02);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Fixed] Temporarily disabled JIT on Standalone + Mono platforms as they throw PlatformNotSupportedException";
            }
        }
    }
}