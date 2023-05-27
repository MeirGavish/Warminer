using Firebase;
using Firebase.Crashlytics;
using System;

namespace HOG.Core
{
    public class HOGCrashManager
    {
        public HOGCrashManager()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                    Crashlytics.ReportUncaughtExceptionsAsFatal = true;
                    HOGDebug.Log("Firebase initialized");

                    // Set a flag here for indicating that your project is ready to use Firebase.
                }
                else
                {
                    HOGDebug.LogException($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }
        public void LogExceptionHandling(string message)
        {
            Crashlytics.LogException(new Exception(message));
            HOGDebug.LogException(message);
        }

        public void LogBreadcrumb(string message)
        {
            Crashlytics.Log(message);
            HOGDebug.Log(message);
        }
    }
}