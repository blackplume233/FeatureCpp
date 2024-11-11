using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    public class FrameLimiterUtility
    {
        public FrameLimiterUtility(uint fpsLimit)
        {
            SetFPSLimit(fpsLimit);
        }

        public void SetFPSLimit(uint fpsLimit)
        {
            _frameTime = 1 / (double)fpsLimit;
        }

        public bool IsWithinFPSLimit()
        {
            var currentTime = EditorApplication.timeSinceStartup;
            var diff = currentTime - _lastCallTime;
            if (diff > _frameTime)
            {
                _lastCallTime = currentTime;
                return true;
            }

            return false;
        }

        private double _frameTime;
        private double _lastCallTime = 0;
    }
}
