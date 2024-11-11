using System;

namespace Ludiq
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class AotIncompatibleAttribute : Attribute
    {
        public AotIncompatibleAttribute() : base() { }
    }
}
