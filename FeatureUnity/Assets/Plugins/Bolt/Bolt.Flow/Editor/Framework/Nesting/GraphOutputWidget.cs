﻿using Ludiq;

namespace Bolt
{
    [Widget(typeof(GraphOutput))]
    public sealed class GraphOutputWidget : UnitWidget<GraphOutput>
    {
        public GraphOutputWidget(FlowCanvas canvas, GraphOutput unit) : base(canvas, unit) { }

        protected override NodeColorMix baseColor => NodeColorMix.TealReadable;
    }
}