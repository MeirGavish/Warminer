
using System;
using DG.Tweening;
using HOG.Core;
using UnityEngine;

namespace  HOG.GameLogic
{
    public class HOGShakeOnClickComponent : HOGLogicMonoBehaviour
    {
        [SerializeField] private float shakeDuration = 0.2f;
        [SerializeField] private float baseStrengthShake = 0.3f;
        [SerializeField] private int shakeVibBase = 20;
        
        public void OnMouseUpAsButton()
        {
            Shake();
        }

        private void Shake()
        {
            transform.DOShakePosition(shakeDuration, baseStrengthShake, shakeVibBase);
        }
    }
}
