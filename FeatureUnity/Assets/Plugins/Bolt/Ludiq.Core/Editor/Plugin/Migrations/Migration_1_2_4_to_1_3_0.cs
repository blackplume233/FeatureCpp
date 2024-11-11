using UnityEngine;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Migration_1_2_4_to_1_3_0 : LudiqCoreMigration
    {
        public Migration_1_2_4_to_1_3_0(Plugin plugin) : base(plugin) { }

        public override SemanticVersion @from => "1.2.4";
        public override SemanticVersion to => "1.3.0";

        public override void Run()
        {
            AddDefaultTypeOption(typeof(Touch));
        }
    }
}