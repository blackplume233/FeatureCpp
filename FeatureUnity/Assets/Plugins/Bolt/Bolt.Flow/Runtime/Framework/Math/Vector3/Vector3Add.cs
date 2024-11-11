using UnityEngine;

namespace Bolt
{
    /// <summary>
    /// Returns the sum of two 3D vectors.
    /// </summary>
    [UnitCategory("Math/Vector 3")]
    [UnitTitle("Add")]
    public sealed class Vector3Add : Add<Vector3>
    {
        protected override Vector3 defaultB => Vector3.zero;

        public override Vector3 Operation(Vector3 a, Vector3 b)
        {
            return a + b;
        }
    }
}