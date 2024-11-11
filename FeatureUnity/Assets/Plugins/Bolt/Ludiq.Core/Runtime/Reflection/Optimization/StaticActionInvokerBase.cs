using System.Reflection;

namespace Ludiq
{
    public abstract class StaticActionInvokerBase : StaticInvokerBase
    {
        protected StaticActionInvokerBase(MethodInfo methodInfo) : base(methodInfo) { }
    }
}