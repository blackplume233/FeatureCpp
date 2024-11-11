using System;

namespace Bolt
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class SpecialUnitAttribute : Attribute
    { }
}