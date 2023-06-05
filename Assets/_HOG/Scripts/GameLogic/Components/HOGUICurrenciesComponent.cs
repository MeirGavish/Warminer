using HOG.Core;
using System;
using TMPro;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGUICurrenciesComponent : HOGLogicMonoBehaviour
    {

        [Tooltip("The currency type of the text is dictated by CurrencyTypesofTexts.\n" +
                 "Lengths must be the same")]
        [SerializeField] private TMP_Text[] CurrencyTexts;
        [Tooltip("The types dictate which currency each of the texts in CurrencyTexts is.\n" +
                 "Lengths must be the same")]
        [SerializeField] private CurrencyTypes[] CurrencyTypesOfTexts;

        private void OnEnable()
        {
            if (CurrencyTypesOfTexts.Length != CurrencyTexts.Length)
            {
                HOGDebug.LogError("Mismatching currency types and texts length!");
                return;
            }

            for (int i = 0; i < CurrencyTypesOfTexts.Length; i++)
            {
                CurrencyTypes currencyType = CurrencyTypesOfTexts[i];
                int currencyVal = 0;
                GameLogic.CurrencyManager.TryGetCurrencyByType(currencyType, out currencyVal);
                UpdateCurrencyText(CurrencyTexts[i], currencyVal);
            }

            AddListener(HOGEventNames.OnCurrencyChanged, OnCurrencyChanged);
        }

        private void OnDisable()
        {
            RemoveListener(HOGEventNames.OnCurrencyChanged, OnCurrencyChanged);
        }

        private void OnCurrencyChanged(object obj)
        {
            var currencyEventData = ((CurrencyTypes, int))obj;
            CurrencyTypes currencyType = currencyEventData.Item1;
            int currencyVal = currencyEventData.Item2;

            UpdateCurrencyText(CurrencyTexts[Array.IndexOf(CurrencyTypesOfTexts, currencyType)], currencyVal);
        }

        private void UpdateCurrencyText(TMP_Text currencyText, int currencyVal)
        {
            currencyText.text = $"<sprite=0> {currencyVal.ToString("N0")}";
        }
    }
}