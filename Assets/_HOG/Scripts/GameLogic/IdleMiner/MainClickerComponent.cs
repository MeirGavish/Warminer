using HOG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HOG.GameLogic
{
    public class MainClickerComponent : HOGLogicMonoBehaviour
    {
        HOGUpgradeableData clickUpgradeData;

        private void Awake()
        {
            clickUpgradeData = GameLogic.UpgradeManager.GetUpgradeableByID(UpgradeablesTypeID.ClickPowerUpgrade);
        }

        public void OnMouseUpAsButton()
        {
            // TODO: Upgrades
            HOGUpgradeableLevelData clickPowerUpgradeData = GameLogic.UpgradeManager.getUpgradeDataAtCurrLevelByType(UpgradeablesTypeID.ClickPowerUpgrade);
            GameLogic.CurrencyManager.ChangeCurrencyByAmountByType(CurrencyTypes.MetalCurrency, clickPowerUpgradeData.Power);
        }
    }
}

