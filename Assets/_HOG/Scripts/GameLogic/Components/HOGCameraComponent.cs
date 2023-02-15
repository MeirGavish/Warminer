
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
            AddListener(HOGEventNames.OnScoreSet, OnScoreSet);
        }

        private void OnDisable()
        {
            RemoveListener(HOGEventNames.OnScoreSet, OnScoreSet);
        }

        private void OnScoreSet(object obj)
        {
            var scoreEventData = ((ScoreTags, int)) obj;

            if (scoreEventData.Item1 == ScoreTags.MainScore)
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
