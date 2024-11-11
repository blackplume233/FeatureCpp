using Ludiq;

namespace Bolt
{
    /// <summary>
    /// Returns the remainder of the division of two objects.
    /// </summary>
    [UnitCategory("Math/Generic")]
    [UnitTitle("Modulo")]
    public sealed class GenericModulo : Modulo<object>
    {
        public override object Operation(object a, object b)
        {
            return OperatorUtility.Modulo(a, b);
        }
    }
}