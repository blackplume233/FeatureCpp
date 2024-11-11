using System;

namespace Ludiq
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PluginRuntimeAssemblyAttribute : Attribute
    {
        public PluginRuntimeAssemblyAttribute(string name)
        {
            assemblyName = name;
        }

        public LooseAssemblyName assemblyName { get; }
    }
}