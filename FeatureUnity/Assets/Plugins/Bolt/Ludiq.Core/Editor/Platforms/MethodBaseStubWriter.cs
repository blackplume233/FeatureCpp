using System.Reflection;

namespace Ludiq
{
    public abstract class MethodBaseStubWriter<TMethodBase> : MemberInfoStubWriter<TMethodBase> where TMethodBase : MethodBase
    {
        protected MethodBaseStubWriter(TMethodBase methodBase) : base(methodBase) { }
    }
}