using System;

namespace Ludiq
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InitializeAfterPluginsAttribute : Attribute { }
}