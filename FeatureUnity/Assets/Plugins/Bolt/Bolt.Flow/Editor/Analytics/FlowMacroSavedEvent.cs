using UnityEditor;

namespace Unity.Bolt.Analytics
{
    class FlowMacroSavedEvent : UnityEditor.AssetModificationProcessor
    {
        static string[] OnWillSaveAssets(string[] paths)
        {
            foreach (string path in paths)
            {
                var assetType = AssetDatabase.GetMainAssetTypeAtPath(path);
                if (assetType == typeof(global::Bolt.FlowMacro))
                {
                    UsageAnalytics.CollectAndSend();
                    break;
                }
            }
            return paths;
        }
    }
}
