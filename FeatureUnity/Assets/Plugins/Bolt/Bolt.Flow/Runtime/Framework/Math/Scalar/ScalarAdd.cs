namespace Bolt
{
    /// <summary>
    /// Returns the sum of two scalars.
    /// </summary>
    [UnitCategory("Math/Scalar")]
    [UnitTitle("Add")]
    public sealed class ScalarAdd : Add<float>
    {
        protected override float defaultB => 1;

        public override float Operation(float a, float b)
        {
            return a + b;
        }
    }
}