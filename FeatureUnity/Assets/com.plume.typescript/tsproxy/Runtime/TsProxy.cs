using System;

namespace Typescript.Runtime
{
    public abstract class TsProxy : IDisposable
    {
        public void Dispose()
        {
            OnDispose();
        }

        protected abstract void OnDispose();
    }
}