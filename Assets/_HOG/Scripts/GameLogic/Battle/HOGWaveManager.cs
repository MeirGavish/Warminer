using System;
using System.Collections.Generic;
using System.Linq;
using HOG.Core;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGWaveManager
    {
        public HOGWaveConfig WaveConfig = new();

        //MockData
        //Load From Save Data On Device
        //Load Config From cloud
        public HOGWaveManager()
        {
            HOGManager.Instance.ConfigManager.GetConfigAsync<HOGWaveConfig>("wave_config",
                delegate (HOGWaveConfig config)
                {
                    WaveConfig = config;
                }
            );

        }

        public List<HOGWave> GetWavesData()
        {
            return WaveConfig.WavesData;
        }
    }


    [Serializable]
    public struct HOGWave
    {
        public float SpawnInterval;
        public int NumEnemies;
    }

    [Serializable]
    public class HOGWaveConfig
    {
        public List<HOGWave> WavesData;
    }
}