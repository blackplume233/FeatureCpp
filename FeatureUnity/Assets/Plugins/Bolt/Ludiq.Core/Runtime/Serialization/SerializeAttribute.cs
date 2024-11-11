using System;

namespace Ludiq
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SerializeAttribute : Attribute
    {
        public SerializeAttribute() { }
    }
}