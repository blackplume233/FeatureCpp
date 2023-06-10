using System;

namespace Typescript.Runtime
{
    public static class TypeScriptInterface
    {
        public static void RegisterProxy(TsProxy proxy)
        {
            _proxy = proxy;
        }

        public static void DisposeCurProxy()
        {
            if (_proxy != null)
            {
                _proxy.Dispose();
            }
        }

        private static TsProxy _proxy;
    }
}