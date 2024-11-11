using System;

namespace Ludiq
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class NullMeansSelfAttribute : Attribute
    {
        public NullMeansSelfAttribute() { }
    }
}