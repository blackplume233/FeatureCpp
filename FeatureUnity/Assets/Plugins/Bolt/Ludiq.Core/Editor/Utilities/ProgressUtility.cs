using UnityEditor;

namespace Ludiq
{
    public static class ProgressUtility
    {
        public static void DisplayProgressBar(string title, string info, float progress)
        {
            if (UnityThread.allowsAPI)
            {
                EditorUtility.DisplayProgressBar(title, info, progress);
            }
            else
            {
                BackgroundWorker.ReportProgress(title, progress);
            }
        }

        [MenuItem("Tools/Bolt/Internal/Force Clear Progress Bar", priority = LudiqProduct.DeveloperToolsMenuPriority + 601)]
        public static void ClearProgressBar()
        {
            if (UnityThread.allowsAPI)
            {
                EditorUtility.ClearProgressBar();
            }

            BackgroundWorker.ClearProgress();
        }
    }
}