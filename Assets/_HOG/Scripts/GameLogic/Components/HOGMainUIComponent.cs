
using System;
using DG.Tweening;
using HOG.Core;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

namespace  HOG.GameLogic
{
    public class HOGMainUIComponent : HOGLogicMonoBehaviour
    {
        // TODO: Do I really want to do it like this...?
        [Tooltip("The order of the currency types dictates which currency each text is.\n" +
                 "Length Must be the same")]
        [SerializeField] private TMP_Text[] CurrencyTexts;
        [SerializeField] private CurrencyTypes[] CurrencyTypesOfTexts;

        private void OnEnable()
        {
            if (CurrencyTypesOfTexts.Length != CurrencyTexts.Length)
            {
                Debug.LogError("Mismatching currency types and texts length!");
                return;
            }

            for (int i = 0; i < CurrencyTypesOfTexts.Length; i++)
            {
                CurrencyTypes currencyType = CurrencyTypesOfTexts[i];
                int currencyVal = 0;
                GameLogic.CurrencyManager.TryGetCurrencyByType(currencyType, out currencyVal);
                CurrencyTexts[i].text = currencyVal.ToString("N0");
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

            CurrencyTexts[Array.IndexOf(CurrencyTypesOfTexts, currencyType)].text = currencyVal.ToString("N0");
        }
        
        public void OnUpgradePressed()
        {
            GameLogic.UpgradeManager.UpgradeItemByID(UpgradeablesTypeID.ClickPowerUpgrade);
        }

        public void LoadBattle()
        {
            SceneManager.LoadScene("BattleTD");
        }

        public void LoadIdleMiner()
        {
            SceneManager.LoadScene("IdleMiner");
        }
    }
}
