using UnityEngine;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Migration_1_2_2_to_1_2_3 : LudiqCoreMigration
    {
        public Migration_1_2_2_to_1_2_3(Plugin plugin) : base(plugin) { }

        public override SemanticVersion from => "1.2.2";

        public override SemanticVersion to => "1.2.3";

        public override void Run()
        {
            AddDefaultTypeOption(typeof(Screen));
        }
    }
}
