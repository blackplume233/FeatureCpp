using System;

namespace Bolt
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class UnitHeaderInspectableAttribute : Attribute
    {
        public UnitHeaderInspectableAttribute() { }

        public UnitHeaderInspectableAttribute(string label)
        {
            this.label = label;
        }

        public string label { get; }
    }
}