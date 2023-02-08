using System;
using System.Collections.Generic;
using System.Linq;

namespace HOG.GameLogic
{
    public class HOGUpgradeManager
    {
        public HOGPlayerUpgradeData PlayerUpgradeData; //Player Saved Data
        public HOGUpgradeManagerConfig UpgradeConfig; //From cloud

        public void UpgradeItemByID(UpgradeablesID id)
        {
            var upgradeable = GetUpgradeableByID(id);

            if (upgradeable != null)
            {
                var upgradeableConfig = GetHogUpgradeableConfigByID(id);
                HOGUpgradeableLevelData levelData = upgradeableConfig.UpgradableLevelData[upgradeable.CurrentLevel + 1];
                int amountToReduce = levelData.CoinsNeeded;
                ScoreTags coinsType = levelData.CurrencyTag;
                //TODO: Score Reduce
                
                upgradeable.CurrentLevel++;
            }
        }

        private HOGUpgradeableConfig GetHogUpgradeableConfigByID(UpgradeablesID id)
        {
            HOGUpgradeableConfig upgradeableConfig = UpgradeConfig.UpgradeableConfigs.FirstOrDefault(x => x.UpgradableID == id);
            return upgradeableConfig;
        }

        private HOGUpgradeableData GetUpgradeableByID(UpgradeablesID id)
        {
            var upgradeable = PlayerUpgradeData.Upgradeables.FirstOrDefault(x => x.UpgradableID == id);
            return upgradeable;
        }
    }
    
    

    //Per Player Owned Item
    [Serializable]
    public class HOGUpgradeableData
    {
        public UpgradeablesID UpgradableID;
        public int CurrentLevel;
    }

    //Per Level in Item config
    [Serializable]
    public struct HOGUpgradeableLevelData
    {
        public int Level;
        public int CoinsNeeded;
        public ScoreTags CurrencyTag;
        public string ArtItem;
        public int Power;
    }

    //Per Item Config
    [Serializable]
    public class HOGUpgradeableConfig
    {
        public UpgradeablesID UpgradableID;
        public List<HOGUpgradeableLevelData> UpgradableLevelData;
    }

    //All config for upgradeable
    [Serializable]
    public class HOGUpgradeManagerConfig
    {
        public List<HOGUpgradeableConfig> UpgradeableConfigs;
    }

    //All player saved data
    [Serializable]
    public class HOGPlayerUpgradeData
    {
        public List<HOGUpgradeableData> Upgradeables;
    }

    [Serializable]
    public enum UpgradeablesID
    {
        Upgradable1
    }
}