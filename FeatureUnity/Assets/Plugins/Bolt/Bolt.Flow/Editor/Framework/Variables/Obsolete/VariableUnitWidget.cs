#pragma warning disable 618

using Ludiq;

namespace Bolt
{
    [Widget(typeof(VariableUnit))]
    public sealed class VariableUnitWidget : UnitWidget<VariableUnit>
    {
        public VariableUnitWidget(FlowCanvas canvas, VariableUnit unit) : base(canvas, unit) { }

        protected override NodeColorMix baseColor => NodeColorMix.TealReadable;
    }
}