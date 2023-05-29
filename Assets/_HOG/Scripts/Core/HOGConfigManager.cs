using System;
using Newtonsoft.Json;
using System.IO;
using Firebase.RemoteConfig;
using Firebase.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HOG.Core
{
    public class HOGConfigManager
    {
        private Action onInitComplete;
        public HOGConfigManager(Action onComplete)
        {
            onInitComplete = onComplete;
            var defaults = new Dictionary<string, object>();
            defaults.Add("upgrade_config", "{}");


            HOGDebug.Log(nameof(HOGConfigManager));
            FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
                .ContinueWithOnMainThread(OnDefaultValuesSet);
        }

        // Used instructions from https://firebase.google.com/docs/remote-config/get-started?platform=unity

        private void OnDefaultValuesSet(Task task)
        {
            HOGDebug.Log(nameof(OnDefaultValuesSet));
            FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero).ContinueWithOnMainThread(OnFetchComplete);
        }

        private void OnFetchComplete(Task task)
        {
            HOGDebug.Log(nameof(OnFetchComplete));
            FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                .ContinueWithOnMainThread(
                    task =>
                    {
                        HOGDebug.Log($"Remote data loaded and ready for use.");
                        onInitComplete.Invoke();
                    });
        }


        public void GetConfigAsync<T>(string configID, Action<T> onComplete)
        {
            var saveJson = FirebaseRemoteConfig.DefaultInstance.GetValue(configID).StringValue;
            var saveData = JsonConvert.DeserializeObject<T>(saveJson);
            onComplete.Invoke(saveData);
        }

        public void GetConfigOfflineAsync<T>(string configID, Action<T> onComplete)
        {
            var path = $"Assets/_HOG//Config/{configID}.json";

            var saveJson = File.ReadAllText(path);
            var saveData = JsonConvert.DeserializeObject<T>(saveJson);

            onComplete.Invoke(saveData);
        }
    }
}