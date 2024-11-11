using System;

namespace Bolt
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class PortLabelHiddenAttribute : Attribute
    { }
}