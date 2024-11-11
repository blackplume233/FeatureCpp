using System;

namespace Bolt
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class UnitSurtitleAttribute : Attribute
    {
        public UnitSurtitleAttribute(string surtitle)
        {
            this.surtitle = surtitle;
        }

        public string surtitle { get; private set; }
    }
}