using Ludiq;

namespace Bolt
{
    /// <summary>
    /// Returns the sum of two objects.
    /// </summary>
    [UnitCategory("Math/Generic")]
    [UnitTitle("Add")]
    public sealed class GenericAdd : Add<object>
    {
        public override object Operation(object a, object b)
        {
            return OperatorUtility.Add(a, b);
        }
    }
}