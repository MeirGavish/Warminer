
using System;
using DG.Tweening;
using HOG.Core;
using TMPro;
using UnityEngine;

namespace  HOG.GameLogic
{
    public class HOGMainUIComponent : HOGLogicMonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        
        private void OnEnable()
        {
            var score = 0;
            GameLogic.CurrencyManager.TryGetCurrencyByType(CurrencyTypes.MetalCurrency, out score);
            scoreText.text = score.ToString("N0");
            
            AddListener(HOGEventNames.OnCurrencyChanged, OnCurrencyChanged);
        }

        private void OnDisable()
        {
            RemoveListener(HOGEventNames.OnCurrencyChanged, OnCurrencyChanged);
        }

        private void OnCurrencyChanged(object obj)
        {
            var scoreEventData = ((CurrencyTypes, int)) obj;

            if (scoreEventData.Item1 == CurrencyTypes.MetalCurrency)
            {
                scoreText.text = scoreEventData.Item2.ToString("N0");
            }
        }
        
        public void OnUpgradePressed()
        {
            GameLogic.UpgradeManager.UpgradeItemByID(UpgradeablesTypeID.ClickPowerUpgrade);
        }
    }
}
