using System;
using Puerts;
using Puerts.TSLoader;
using Typescript.Runtime;
//using Puerts.TSLoader;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Typescript.Editor
{
    public class EditorTsProxy : Runtime.TsProxy, IDisposable
    {
        public EditorTsProxy()
        {
        }

        public void Init()
        {
            try
            {
                var loader = new TSLoader();
                _jsEnv = new JsEnv(loader, 8080);
                var initFonction = _jsEnv.ExecuteModule<Action<JsContext>>("main.mjs","default");
                _JsContext = new JsContext();
                initFonction?.Invoke(_JsContext);
                EditorApplication.update += Update;
                
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        

        private void Update()
        {
            _jsEnv?.Tick();
            _JsContext?.OnUpdate?.Invoke();
        }


        #region Interface

        protected override void OnDispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            EditorApplication.update -= Update;
            _JsContext = null;
            
            _jsEnv?.Dispose();
        }

        #endregion
        private bool _disposed = false;
        private JsEnv _jsEnv;
        private JsContext _JsContext = null;
    }
}