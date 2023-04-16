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

            if (upgradeable != null)
            {
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
                    Debug.LogError($"UpgradeItemByID {typeID.ToString()} tried upgrade and there is not enough");
                }
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
        public int GetPowerByIDAndLevel(UpgradeablesTypeID typeID, int level)
        {
            var upgradeableConfig = GetHogUpgradeableConfigByID(typeID);
            var power = upgradeableConfig.UpgradableLevelData[level].Power;
            return power;
        }

        public int GetPowerAtCurrLevelByID(UpgradeablesTypeID typeID)
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
                        CurrencyAmountNeeded = 0,
                        CurrencyType = CurrencyTypes.CoinsCurrency,
                        Power = 1
                    },
                    new HOGUpgradeableLevelData
                    {
                        Level = 2,
                        CurrencyAmountNeeded = 30,
                        CurrencyType = CurrencyTypes.CoinsCurrency,
                        Power = 15
                    },
                    new HOGUpgradeableLevelData
                    {
                        Level = 3,
                        CurrencyAmountNeeded = 200,
                        CurrencyType = CurrencyTypes.CoinsCurrency,
                        Power = 40
                    },
                    new HOGUpgradeableLevelData
                    {
                        Level = 4,
                        CurrencyAmountNeeded = 500,
                        CurrencyType = CurrencyTypes.CoinsCurrency,
                        Power = 100
                    },
                    
                }
            },
            new HOGUpgradeableConfig
            {
                UpgradableTypeID = UpgradeablesTypeID.DamageUpgrade,
                UpgradableLevelData = new List<HOGUpgradeableLevelData>(){
                    new HOGUpgradeableLevelData
                    {
                        Level = 1,
                        CurrencyAmountNeeded = 0,
                        CurrencyType = CurrencyTypes.MetalCurrency,
                        Power = 30
                    },
                    new HOGUpgradeableLevelData
                    {
                        Level = 2,
                        CurrencyAmountNeeded = 50,
                        CurrencyType = CurrencyTypes.MetalCurrency,
                        Power = 40
                    },
                    new HOGUpgradeableLevelData
                    {
                        Level = 3,
                        CurrencyAmountNeeded = 500,
                        CurrencyType = CurrencyTypes.MetalCurrency,
                        Power = 50
                    },
                    new HOGUpgradeableLevelData
                    {
                        Level = 3,
                        CurrencyAmountNeeded = 1500,
                        CurrencyType = CurrencyTypes.MetalCurrency,
                        Power = 100
                    },
                }
            }
        };
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
        DamageUpgrade
    }
}