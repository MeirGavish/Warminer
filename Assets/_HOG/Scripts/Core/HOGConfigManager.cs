using System;
using UnityEngine;
using Unity.Plastic.Newtonsoft.Json;
using System.IO;

namespace HOG.Core
{
    public class HOGConfigManager
    {
        public void GetConfigAsync<T>(string configID, Action<T> onComplete)
        {
            var path = $"Assets/_HOG//Config/{configID}.json";

            var saveJson = File.ReadAllText(path);
            var saveData = JsonConvert.DeserializeObject<T>(saveJson);

            onComplete.Invoke(saveData);
        }
    }
}