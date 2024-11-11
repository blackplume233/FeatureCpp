using System;

namespace Ludiq
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class InspectorToggleLeftAttribute : Attribute
    {
        public InspectorToggleLeftAttribute() { }
    }
}