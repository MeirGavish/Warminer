using Debug = UnityEngine.Debug;
using System;

namespace HOG.Core
{
    public class HOGDebug
    {
        [System.Diagnostics.Conditional("LOGS_ENABLE")]
        public static void Log(object message)
        {
            Debug.Log(message);
        }

        [System.Diagnostics.Conditional("LOGS_ENABLE")]
        public static void LogError(object message)
        {
            Debug.LogError(new Exception(message.ToString()));
        }

        // TODO: expand Context? LogWarning?

        [System.Diagnostics.Conditional("LOGS_ENABLE")]
        public static void LogException(object message)
        {
            Debug.LogException(new Exception(message.ToString()));
        }
    }
}