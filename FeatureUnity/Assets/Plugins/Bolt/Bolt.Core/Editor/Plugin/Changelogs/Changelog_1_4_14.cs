using System;
using System.Collections.Generic;
using Ludiq;

namespace Bolt
{
    [Plugin(BoltCore.ID)]
    internal class Changelog_1_4_14 : PluginChangelog
    {
        public Changelog_1_4_14(Plugin plugin) : base(plugin) { }

        public override SemanticVersion version => "1.4.14";

        public override DateTime date => new DateTime(2021, 02, 25);

        public override IEnumerable<string> changes
        {
            get
            {
                /* Backport: BOLT-1347 */
                yield return "[Fixed] ThreadAbortException error when entering play mode while using the fuzzy search window";
                /* Backport: BOLT-725 */
                yield return "[Fixed] Output of Cooldown completed is treated as unentered";
                /* Backport: BOLT-1222 */
                yield return "[Fixed] Unity Message Listener blocks proper trickling of UGUI events in hierarchies";
                /* Backport: BOLT-1223 */
                yield return "[Fixed] NullReferenceException when graph window is opened on a new project";
                /* Backport: BOLT-1224 */
                yield return "[Fixed] Bolt deserialization error and nodes missing after pressing Undo when Update Coroutine with Wait node is present in graph";
                /* Backport: BOLT-1225 */
                yield return "[Fixed] Select on Enum doesn't work with the RuntimePlatform enum";
                /* Backport: BOLT-1226 */
                yield return "[Fixed] [Bolt] Bolt 1 InvalidImplementationException is thrown in the Console Window when entering Play Mode";
                /* Backport: BOLT-1227 */
                yield return "[Fixed] Groups move when showing the scale cursor randomly";
                /* Backport: BOLT-1228 */
                yield return "[Fixed] [Bolt] Bolt Graph Variables do not update in Play Mode when enabling a GameObject which was disabled before entering Play Mode";
                /* Backport: BOLT-1229 */
                yield return "[Fixed] Node Inspector Disappear when scaling Graph window";
                /* Backport: BOLT-1231 */
                yield return "[Fixed] Backup fails to complete";
                /* Backport: BOLT-1235 */
                yield return "[Fixed] High CPU and GPU usage when using the Bolt 1 or 2 visual graph";
            }
        }
    }
}