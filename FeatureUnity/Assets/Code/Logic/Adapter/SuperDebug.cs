namespace Code.Logic.Adapter
{
    public static class SuperDebug
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public static void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
        
        public static void LogWarningFormat(string format, params object[] args)
        {
            UnityEngine.Debug.LogWarningFormat(format, args);
        }
        
        public static void LogErrorFormat(string format, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat(format, args);
        }
        
        public static void LogError(object message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}