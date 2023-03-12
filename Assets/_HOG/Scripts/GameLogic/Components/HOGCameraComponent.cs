
using System;
using DG.Tweening;
using HOG.Core;
using UnityEngine;

namespace  HOG.GameLogic
{
    public class HOGCameraComponent : HOGLogicMonoBehaviour
    {
        [SerializeField] private float shakeDuration = 0.1f;
        [SerializeField] private float baseStrengthShake = 1f;
        [SerializeField] private int shakeVibBase = 1;
        
        private void OnEnable()
        {
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
                ShakeCamera();
            }
        }

        private void ShakeCamera()
        {
            transform.DOShakePosition(shakeDuration, baseStrengthShake, shakeVibBase);
        }
    }
}
