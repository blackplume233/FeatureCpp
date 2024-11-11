using System;

namespace Bolt
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class UnitOrderAttribute : Attribute
    {
        public UnitOrderAttribute(int order)
        {
            this.order = order;
        }

        public int order { get; private set; }
    }
}