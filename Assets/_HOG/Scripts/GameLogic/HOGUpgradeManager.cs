using System;
using System.Collections.Generic;
using System.Linq;
using HOG.Core;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGUpgradeManager
    {
        public HOGPlayerUpgradeInventoryData PlayerUpgradeInventoryData; //Player Saved Data
        public HOGUpgradeManagerConfig UpgradeConfig = new HOGUpgradeManagerConfig(); //From cloud

        //MockData
        //Load From Save Data On Device (Future)
        //Load Config From Load
        public HOGUpgradeManager()
        {
            PlayerUpgradeInventoryData = new HOGPlayerUpgradeInventoryData
            {
                Upgradeables = new List<HOGUpgradeableData>(){new HOGUpgradeableData
                    {
                        upgradableTypeID = UpgradeablesTypeID.ClickPowerUpgrade,
                        CurrentLevel = 0
                    }
                }
            };
            
            
        }
        
        public void UpgradeItemByID(UpgradeablesTypeID typeID)
        {
            var upgradeable = GetUpgradeableByID(typeID);

            if (upgradeable != null)
            {
                var upgradeableConfig = GetHogUpgradeableConfigByID(typeID);
                HOGUpgradeableLevelData levelData = upgradeableConfig.UpgradableLevelData[upgradeable.CurrentLevel + 1];
                int amountToReduce = levelData.CoinsNeeded;
                CurrencyTypes coinsType = levelData.CurrencyTag;

                if (HOGGameLogic.Instance.CurrencyManager.TryUseScore(coinsType, amountToReduce))
                {
                    upgradeable.CurrentLevel++;
                    HOGManager.Instance.EventsManager.InvokeEvent(HOGEventNames.OnUpgraded, typeID);
                }
                else
                {
                    Debug.LogError($"UpgradeItemByID {typeID.ToString()} tried upgrade and there is no enough");
                }
            }
        }

        public HOGUpgradeableConfig GetHogUpgradeableConfigByID(UpgradeablesTypeID typeID)
        {
            HOGUpgradeableConfig upgradeableConfig = UpgradeConfig.UpgradeableConfigs.FirstOrDefault(upgradable => upgradable.UpgradableTypeID == typeID);
            return upgradeableConfig;
        }

        public int GetPowerByIDAndLevel(UpgradeablesTypeID typeID, int level)
        {
            var upgradeableConfig = GetHogUpgradeableConfigByID(typeID);
            var power = upgradeableConfig.UpgradableLevelData[level].Power;
            return power;
        }
        
        public HOGUpgradeableData GetUpgradeableByID(UpgradeablesTypeID typeID)
        {
            var upgradeable = PlayerUpgradeInventoryData.Upgradeables.FirstOrDefault(x => x.upgradableTypeID == typeID);
            return upgradeable;
        }
    }
    
    
    //Per Player Owned Item
    [Serializable]
    public class HOGUpgradeableData
    {
        public UpgradeablesTypeID upgradableTypeID;
        public int CurrentLevel;
    }

    //Per Level in Item config
    [Serializable]
    public struct HOGUpgradeableLevelData
    {
        public int Level;
        public int CoinsNeeded;
        public CurrencyTypes CurrencyTag;
        public string ArtItem;
        public int Power;   
    }

    //Per Item Config
    [Serializable]
    public class HOGUpgradeableConfig
    {
        public UpgradeablesTypeID UpgradableTypeID;
        public List<HOGUpgradeableLevelData> UpgradableLevelData;
    }

    //All config for upgradeable
    [Serializable]
    public class HOGUpgradeManagerConfig
    {
        public List<HOGUpgradeableConfig> UpgradeableConfigs = new List<HOGUpgradeableConfig>(){new HOGUpgradeableConfig
            {
                UpgradableTypeID = UpgradeablesTypeID.ClickPowerUpgrade,
                UpgradableLevelData = new List<HOGUpgradeableLevelData>(){
                    new HOGUpgradeableLevelData
                    {
                        Level = 1,
                        CoinsNeeded = 0,
                        CurrencyTag = CurrencyTypes.MetalCurrency,
                        Power = 1
                    },
                    new HOGUpgradeableLevelData
                    {
                        Level = 2,
                        CoinsNeeded = 50,
                        CurrencyTag = CurrencyTypes.MetalCurrency,
                        Power = 15
                    },
                    new HOGUpgradeableLevelData
                    {
                        Level = 3,
                        CoinsNeeded = 1500,
                        CurrencyTag = CurrencyTypes.MetalCurrency,
                        Power = 40
                    },
                    new HOGUpgradeableLevelData
                    {
                        Level = 4,
                        CoinsNeeded = 8000,
                        CurrencyTag = CurrencyTypes.MetalCurrency,
                        Power = 100
                    },
                    
                }
            }
        };
    }

    //All player saved data
    [Serializable]
    public class HOGPlayerUpgradeInventoryData
    {
        public List<HOGUpgradeableData> Upgradeables;
    }

    [Serializable]
    public enum UpgradeablesTypeID
    {
        ClickPowerUpgrade = 0
    }
}