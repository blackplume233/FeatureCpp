using Ludiq;
using UnityEngine;

namespace Bolt
{
    [CreateAssetMenu(menuName = "Bolt/Flow Macro", fileName = "New Flow Macro")]
    public sealed class FlowMacro : Macro<FlowGraph>
    {
        [ContextMenu("Show Data...")]
        protected override void ShowData()
        {
            base.ShowData();
        }

        public override FlowGraph DefaultGraph()
        {
            return FlowGraph.WithInputOutput();
        }
    }
}