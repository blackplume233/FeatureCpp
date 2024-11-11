using System;
using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_4_6 : PluginChangelog
    {
        public Changelog_1_4_6(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.6";

        public override DateTime date => new DateTime(2019, 08, 20);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Removed] Documentation and inspector generation from setup wizard to avoid alarming harmless errors";
                yield return "[Added] Support for invoking extension methods as static methods from their declaring class";
                yield return "[Fixed] Missing legacy input module in Unity 2019";
                yield return "[Fixed] Null reference exception in documentation generator page";
                yield return "[Fixed] Crash when invoking methods with VarArgs calling convention";
                yield return "[Fixed] Version control issues with automatic checkout of generated files";
                yield return "[Fixed] Inspector generation not working for types outside Ludiq assemblies";
            }
        }
    }

    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_4_6f2 : PluginChangelog
    {
        public Changelog_1_4_6f2(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.6f2";

        public override DateTime date => new DateTime(2019, 08, 22);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Fixed] Error with VersionControl API change in Unity 2019";
            }
        }
    }

    [Plugin(LudiqCore.ID)]
    internal class Changelog_1_4_6f3 : PluginChangelog
    {
        public Changelog_1_4_6f3(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.6f3";

        public override DateTime date => new DateTime(2019, 08, 27);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Fixed] Typo in legacy input module name";
            }
        }
    }
}