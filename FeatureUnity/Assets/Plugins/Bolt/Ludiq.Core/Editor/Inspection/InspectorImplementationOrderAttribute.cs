using System;

namespace Ludiq
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class InspectorImplementationOrderAttribute : Attribute
    {
        public InspectorImplementationOrderAttribute(int order)
        {
            this.order = order;
        }

        public int order { get; }
    }
}