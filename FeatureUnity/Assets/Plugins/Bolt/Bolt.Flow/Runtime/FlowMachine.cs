using Ludiq;
using UnityEngine;

namespace Bolt
{
    [AddComponentMenu("Bolt/Flow Machine")]
    [RequireComponent(typeof(Variables))]
    [DisableAnnotation]
    public sealed class FlowMachine : EventMachine<FlowGraph, FlowMacro>
    {
        public override FlowGraph DefaultGraph()
        {
            return FlowGraph.WithStartUpdate();
        }

        protected override void OnEnable()
        {
            if (hasGraph)
            {
                graph.StartListening(reference);
            }

            base.OnEnable();
        }

        protected override void OnInstantiateWhileEnabled()
        {
            if (hasGraph)
            {
                graph.StartListening(reference);
            }

            base.OnInstantiateWhileEnabled();
        }

        protected override void OnUninstantiateWhileEnabled()
        {
            base.OnUninstantiateWhileEnabled();

            if (hasGraph)
            {
                graph.StopListening(reference);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (hasGraph)
            {
                graph.StopListening(reference);
            }
        }

        [ContextMenu("Show Data...")]
        protected override void ShowData()
        {
            base.ShowData();
        }
    }
}
