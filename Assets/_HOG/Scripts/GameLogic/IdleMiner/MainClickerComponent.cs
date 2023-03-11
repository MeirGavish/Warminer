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
        public int metalPerClick = 1;

        private void Awake()
        {
            clickUpgradeData = GameLogic.UpgradeManager.GetUpgradeableByID(UpgradeablesTypeID.ClickPowerUpgrade);
        }

        public void OnMouseUpAsButton()
        {
            GameLogic.CurrencyManager.ChangeCurrencyByAmountByType(CurrencyTypes.Metal, metalPerClick);
        }
    }
}

