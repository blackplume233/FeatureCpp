using System;

namespace Ludiq
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class InspectViaImplementationsAttribute : Attribute
    {
        public InspectViaImplementationsAttribute() { }
    }
}