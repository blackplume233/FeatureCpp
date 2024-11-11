using UnityEditor;

namespace Unity.Bolt.Analytics
{
    class StateMacroSavedEvent : UnityEditor.AssetModificationProcessor
    {
        static string[] OnWillSaveAssets(string[] paths)
        {
            foreach (string path in paths)
            {
                var assetType = AssetDatabase.GetMainAssetTypeAtPath(path);
                if (assetType == typeof(global::Bolt.StateMacro))
                {
                    UsageAnalytics.CollectAndSend();
                    break;
                }
            }
            return paths;
        }
    }
}
