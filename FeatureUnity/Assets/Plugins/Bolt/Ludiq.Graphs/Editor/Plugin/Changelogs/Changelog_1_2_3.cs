using System;
using System.Collections.Generic;

namespace Ludiq
{
    [Plugin(LudiqGraphs.ID)]
    internal class Changelog_1_2_3 : PluginChangelog
    {
        public Changelog_1_2_3(Plugin plugin) : base(plugin) { }

        public override string description => null;
        public override SemanticVersion version => "1.2.3";
        public override DateTime date => new DateTime(2018, 01, 25);

        public override IEnumerable<string> changes
        {
            get
            {
                yield return "[Refactored] Canvas code to use control IDs";
                yield return "[Fixed] Quick window dragging causing lasso";
                yield return "[Fixed] Mouse issues in canvas";
            }
        }
    }
}