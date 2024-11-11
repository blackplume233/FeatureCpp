using UnityEngine;

namespace Bolt
{
    /// <summary>
    /// Returns the positive version of a scalar.
    /// </summary>
    [UnitCategory("Math/Scalar")]
    [UnitTitle("Absolute")]
    public sealed class ScalarAbsolute : Absolute<float>
    {
        protected override float Operation(float input)
        {
            return Mathf.Abs(input);
        }
    }
}