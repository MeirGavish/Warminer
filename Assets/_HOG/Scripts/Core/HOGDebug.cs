using UnityEditor;
using UnityEngine;

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
    }
}