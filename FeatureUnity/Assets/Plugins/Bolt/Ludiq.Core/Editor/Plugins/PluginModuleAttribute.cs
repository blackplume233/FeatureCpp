using System;

namespace Ludiq
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginModuleAttribute : Attribute
    {
        public bool required { get; set; } = true;
    }
}