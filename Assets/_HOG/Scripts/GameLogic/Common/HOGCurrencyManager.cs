using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOG.Core;

namespace HOG.GameLogic
{
    public class HOGCurrencyManager
    {
        public HOGPlayerCurrencyData PlayerCurrencyData;

        public HOGCurrencyManager()
        {
            HOGManager.Instance.SaveManager.Load<HOGPlayerCurrencyData>
            (
                (HOGPlayerCurrencyData data) => { PlayerCurrencyData = data ?? new(); }
            );
        }

        public bool TryGetCurrencyByType(CurrencyTypes type, out int currencyOut) 
            => PlayerCurrencyData.CurrencyByType.TryGetValue(type, out currencyOut);

        public void GetOrInitCurrencyByType(CurrencyTypes type, out int currencyOut)
        {
            if (TryGetCurrencyByType(type, out currencyOut))
            {
                return;
            }

            currencyOut = PlayerCurrencyData.CurrencyByType[type] = 0;
        }

        public void SetCurrencyByType(CurrencyTypes type, int amount = 0)
        {
            HOGManager.Instance.EventsManager.InvokeEvent(HOGEventNames.OnCurrencyChanged, (type, amount));
            PlayerCurrencyData.CurrencyByType[type] = amount;

            HOGManager.Instance.SaveManager.Save(PlayerCurrencyData);
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
            var hasType = TryGetCurrencyByType(currencyType, out int currency);
            var hasEnough = false;

            if (hasType)
            {
                hasEnough = amountToReduce <= currency;
            }

            if (hasEnough)
            {
                ChangeCurrencyByAmountByType(currencyType, -amountToReduce);
            }

            return hasEnough;
        }
    }

    public class HOGPlayerCurrencyData : IHOGSaveData
    {
        public Dictionary<CurrencyTypes, int> CurrencyByType = new();
    }

    public enum CurrencyTypes
    {
        MetalCurrency,
        CoinsCurrency
    }
}

