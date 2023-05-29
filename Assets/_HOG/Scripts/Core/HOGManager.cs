using Firebase.Extensions;
using System;

namespace HOG.Core
{
    public class HOGManager : IHOGBaseManager
    {
        public static HOGManager Instance;

        public HOGEventsManager EventsManager;
        public HOGFactoryManager FactoryManager;
        public HOGPoolManager PoolManager;
        public HOGSaveManager SaveManager;
        public HOGConfigManager ConfigManager;
        public HOGCrashManager CrashManager;

        private Action onInitCompleteAction;

        public HOGManager()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = this;
        }

        public void LoadManager(Action onComplete)
        {
            onInitCompleteAction = onComplete;
            InitFirebase(InitManagers);
        }

        private void InitManagers()
        {
            CrashManager = new HOGCrashManager();
            EventsManager = new HOGEventsManager();
            FactoryManager = new HOGFactoryManager();
            PoolManager = new HOGPoolManager();
            SaveManager = new HOGSaveManager();
            ConfigManager = new HOGConfigManager(delegate
            {
                // So we don't miss the onComplete chain
                onInitCompleteAction.Invoke();
            });
        }

        private void InitFirebase(Action OnComplete)
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                    HOGDebug.Log("Firebase initialized");

                    OnComplete.Invoke();
                }
                else
                {
                    HOGDebug.LogException($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }
    }



    public interface IHOGBaseManager
    {
        public void LoadManager(Action onComplete);
    }
}