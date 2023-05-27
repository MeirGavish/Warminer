using Debug = UnityEngine.Debug;
using System;

namespace HOG.Core
{
    public class HOGDebug
    {
        [System.Diagnostics.Conditional("LOGS_ENABLE")]
        public static void Log(object message)
        {
            // TODO: Context?
            Debug.Log(message);
        }

        [System.Diagnostics.Conditional("LOGS_ENABLE")]
        public static void LogException(object message)
        {
            // TODO: Context?
            Debug.LogException(new Exception(message.ToString()));
        }
    }
}