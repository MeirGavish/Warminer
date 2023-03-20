
using System;
using DG.Tweening;
using HOG.Core;
using TMPro;
using UnityEngine;

namespace  HOG.GameLogic
{
    public class HOGMainUIComponent : HOGLogicMonoBehaviour
    {
        [Tooltip("Match text to currency type by order of CurrencyType enum")]
        [SerializeField] private TMP_Text[] CurrencyTexts = new TMP_Text[Enum.GetValues(typeof(CurrencyTypes)).Length];
        
        private void OnEnable()
        {
            int score = 0;
            GameLogic.CurrencyManager.TryGetCurrencyByType(CurrencyTypes.MetalCurrency, out score);
            
            foreach(CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
            {
                int currencyVal = 0;
                GameLogic.CurrencyManager.TryGetCurrencyByType(currencyType, out currencyVal);
                CurrencyTexts[(int)currencyType].text = currencyVal.ToString("N0");
            }
            
            AddListener(HOGEventNames.OnCurrencyChanged, OnCurrencyChanged);
        }

        private void OnDisable()
        {
            RemoveListener(HOGEventNames.OnCurrencyChanged, OnCurrencyChanged);
        }

        private void OnCurrencyChanged(object obj)
        {
            var currencyEventData = ((CurrencyTypes, int)) obj;
            CurrencyTypes currencyType  = currencyEventData.Item1;
            int currencyVal = currencyEventData.Item2;

            CurrencyTexts[(int)currencyType].text = currencyVal.ToString("N0");
        }
        
        public void OnUpgradePressed()
        {
            GameLogic.UpgradeManager.UpgradeItemByID(UpgradeablesTypeID.ClickPowerUpgrade);
        }
    }
}
