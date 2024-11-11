using System;
using Ludiq;

namespace Bolt
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class InspectorVariableNameAttribute : Attribute
    {
        public InspectorVariableNameAttribute(ActionDirection direction)
        {
            this.direction = direction;
        }

        public ActionDirection direction { get; private set; }
    }
}