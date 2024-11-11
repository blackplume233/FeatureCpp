using System;
using System.Collections.Generic;
using Ludiq;

namespace Bolt
{
    [Plugin(BoltCore.ID)]
    internal class Changelog_1_4_15 : PluginChangelog
    {
        public Changelog_1_4_15(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.15";

        public override DateTime date => new DateTime(2021, 03, 02);

        public override IEnumerable<string> changes
        {
            get
            {
                /* Fixing shipping generated folders by accident */
                yield return "[Fixed] Bug where upgrading from older versions nuked application variables";
            }
        }
    }
}