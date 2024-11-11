using System.Collections.Generic;
using Ludiq;

namespace Bolt
{
    [GraphContext(typeof(FlowGraph))]
    public class FlowGraphContext : GraphContext<FlowGraph, FlowCanvas>
    {
        public FlowGraphContext(GraphReference reference) : base(reference) { }

        public override string windowTitle => "Flow Graph";

        protected override IEnumerable<ISidebarPanelContent> SidebarPanels()
        {
            yield return new GraphInspectorPanel(this);
            yield return new VariablesPanel(this);
        }
    }
}
