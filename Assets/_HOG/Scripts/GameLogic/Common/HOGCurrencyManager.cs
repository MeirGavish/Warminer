using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOG.Core;

namespace HOG.GameLogic
{
    public class HOGCurrencyManager
    {
        public HOGPlayerCurrencyData PlayerCurrencyData;

        public bool TryGetCurrencyByType(CurrencyTypes type, out int currencyOut) 
            => PlayerCurrencyData.CurrencyByType.TryGetValue(type, out currencyOut);

        public void SetCurrencyByType(CurrencyTypes type, int amount = 0)
        {
            HOGManager.Instance.EventsManager.InvokeEvent(HOGEventNames.CurrencyChanged, (type, amount));
        }
        public void ChangeCurrencyByAmountByType(CurrencyTypes type, int amount = 0)
        {
            if (PlayerCurrencyData.CurrencyByType.ContainsKey(type))
            {
                SetCurrencyByType(type, PlayerCurrencyData.CurrencyByType[type] + amount);
            }
            else
            {
                SetCurrencyByType(type, amount);
            }
        }

        public bool TryUseCurrency(CurrencyTypes currencyType, int amountToReduce)
        {
            var currency = 0;
            var hasType = TryGetCurrencyByType(currencyType, out currency);
            var hasEnough = false;

            if (hasType)
            {
                hasEnough = amountToReduce >= currency;
            }

            if (hasEnough)
            {
                ChangeCurrencyByAmountByType(currencyType, -amountToReduce);
            }

            return hasEnough;
        }
    }

    public class HOGPlayerCurrencyData
    {
        public Dictionary<CurrencyTypes, int> CurrencyByType = new();
    }

    public enum CurrencyTypes
    {
        Metal,
        Coins
    }
}

