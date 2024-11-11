using Ludiq;
using UnityEngine;

namespace Bolt
{
    [CreateAssetMenu(menuName = "Bolt/State Macro", fileName = "New State Macro")]
    public sealed class StateMacro : Macro<StateGraph>
    {
        [ContextMenu("Show Data...")]
        protected override void ShowData()
        {
            base.ShowData();
        }

        public override StateGraph DefaultGraph()
        {
            return StateGraph.WithStart();
        }
    }
}