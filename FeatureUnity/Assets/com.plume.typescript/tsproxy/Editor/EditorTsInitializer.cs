using System.Threading.Tasks;
using Typescript.Runtime;
using UnityEditor;
using UnityEngine;

namespace Typescript.Editor
{
    public static class EditorTsInitializer
    {
        private const string PUERTS_MENU_PREFIX = "PuerTS";

        [MenuItem(PUERTS_MENU_PREFIX + "/ReStart", false, 1)]
        public static void Reload()
        {
            Initialize();
        }

        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            EditorApplication.delayCall += DelayInit;
        }

        public static void DelayInit()
        {
            EditorApplication.delayCall -= DelayInit;
            TypeScriptInterface.DisposeCurProxy();
            AsyncInit();
        }
        public static  void AsyncInit()
        {
            var proxy = new EditorTsProxy();
            proxy.Init();
            TypeScriptInterface.RegisterProxy(proxy);
        }
    }
}