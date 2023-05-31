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
        public HOGUpgradeManagerConfig UpgradeConfig = new(); //From cloud

        //MockData
        //Load From Save Data On Device
        //Load Config From cloud
        public HOGUpgradeManager()
        {
            HOGManager.Instance.ConfigManager.GetConfigAsync<HOGUpgradeManagerConfig>("upgrade_config",
                delegate (HOGUpgradeManagerConfig config)
                {
                    UpgradeConfig = config;
                }
            );

            if (PlayerUpgradeInventoryData == null)
            {
                HOGManager.Instance.SaveManager.Load<HOGPlayerUpgradeInventoryData>((HOGPlayerUpgradeInventoryData data) =>
                {
                    if (data != null)
                    {
                        PlayerUpgradeInventoryData = data;
                    }
                    else
                    {
                        PlayerUpgradeInventoryData = new HOGPlayerUpgradeInventoryData
                        {
                            Upgradeables = new List<HOGUpgradeableData>()
                            {   new HOGUpgradeableData
                                {
                                    upgradableTypeID = UpgradeablesTypeID.ClickPowerUpgrade,
                                    CurrentLevel = 0
                                },
                                new HOGUpgradeableData
                                {
                                    upgradableTypeID = UpgradeablesTypeID.DamageUpgrade,
                                    CurrentLevel = 0
                                },
                                new HOGUpgradeableData
                                {
                                    upgradableTypeID = UpgradeablesTypeID.FireRateUpgrade,
                                    CurrentLevel = 0
                                }
                            }
                        };

                    }
                });
            }
        }
        
        public void UpgradeItemByID(UpgradeablesTypeID typeID)
        {
            var upgradeable = GetUpgradeableByID(typeID);

            if (upgradeable == null)
            {
                HOGManager.Instance.CrashManager.LogExceptionHandling($"{nameof(UpgradeItemByID)} {typeID.ToString()} failed because upgradable was null");
                return;
            }

            var upgradeableConfig = GetHogUpgradeableConfigByID(typeID);
            HOGUpgradeableLevelData levelData = upgradeableConfig.UpgradableLevelData[upgradeable.CurrentLevel + 1];
            int amountToReduce = levelData.CurrencyAmountNeeded;
            CurrencyTypes currencyType = levelData.CurrencyType;

            if (HOGGameLogic.Instance.CurrencyManager.TryUseCurrency(currencyType, amountToReduce))
            {
                upgradeable.CurrentLevel++;
                HOGManager.Instance.EventsManager.InvokeEvent(HOGEventNames.OnUpgraded, typeID);

                HOGManager.Instance.SaveManager.Save(PlayerUpgradeInventoryData);
            }
            else
            {
                HOGDebug.LogError($"{nameof(UpgradeItemByID)} {typeID} tried to upgrade and there is not enough");
            }
        }

        public HOGUpgradeableConfig GetHogUpgradeableConfigByID(UpgradeablesTypeID typeID)
        {
            HOGUpgradeableConfig upgradeableConfig = UpgradeConfig.UpgradeableConfigs.FirstOrDefault(upgradable => upgradable.UpgradableTypeID == typeID);
            return upgradeableConfig;
        }

        public HOGUpgradeableData GetUpgradeableByID(UpgradeablesTypeID typeID)
        {
            var upgradeable = PlayerUpgradeInventoryData.Upgradeables.FirstOrDefault(x => x.upgradableTypeID == typeID);
            return upgradeable;
        }

        // TODO: Don't use, delete?
        public float GetPowerByIDAndLevel(UpgradeablesTypeID typeID, int level)
        {
            var upgradeableConfig = GetHogUpgradeableConfigByID(typeID);
            var power = upgradeableConfig.UpgradableLevelData[level].Power;
            return power;
        }

        public float GetPowerAtCurrLevelByID(UpgradeablesTypeID typeID)
        {
            return GetPowerByIDAndLevel(typeID, GetUpgradeableByID(typeID).CurrentLevel);
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
        public int CurrencyAmountNeeded;
        public CurrencyTypes CurrencyType;
        public string ArtItem;
        public float Power;   
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
        public List<HOGUpgradeableConfig> UpgradeableConfigs;
    }

    //All player saved data
    [Serializable]
    public class HOGPlayerUpgradeInventoryData : IHOGSaveData
    {
        public List<HOGUpgradeableData> Upgradeables;
    }

    [Serializable]
    public enum UpgradeablesTypeID
    {
        ClickPowerUpgrade = 0,
        DamageUpgrade,
        FireRateUpgrade
    }
}