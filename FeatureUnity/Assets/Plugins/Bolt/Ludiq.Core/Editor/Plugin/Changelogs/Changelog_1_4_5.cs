using System;
using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_4_5 : PluginChangelog
    {
        public Changelog_1_4_5(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.5";

        public override DateTime date => new DateTime(2019, 07, 15);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Fixed] GUI errors with the new editor skin in Unity 2019.3 Alpha";
                yield return "[Improved] Warning message when assemblies are set to reload during play mode";
                yield return "[Added] tools menu option to clear AOT stubs in case they prevent compilation";
                yield return "[Fixed] Temporarily disabled JIT reflection optimization due to instability in recent Unity versions";
            }
        }
    }
}