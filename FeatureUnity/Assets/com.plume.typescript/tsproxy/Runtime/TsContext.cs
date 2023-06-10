using System;

namespace Typescript.Runtime
{
    public class JsContext
    {
        #region BindToJs

        public Action OnStart;
        public Action OnUpdate;
        public Action OnDispose;

        #endregion
    }
}