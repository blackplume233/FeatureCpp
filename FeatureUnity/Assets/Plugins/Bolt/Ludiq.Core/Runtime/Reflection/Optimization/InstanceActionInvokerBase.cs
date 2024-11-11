using System.Reflection;

namespace Ludiq
{
    public abstract class InstanceActionInvokerBase<TTarget> : InstanceInvokerBase<TTarget>
    {
        protected InstanceActionInvokerBase(MethodInfo methodInfo) : base(methodInfo) { }
    }
}