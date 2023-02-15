
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
            GameLogic.ScoreManager.TryGetScoreByTag(ScoreTags.MainScore, ref score);
            scoreText.text = score.ToString("N0");
            
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
                scoreText.text = scoreEventData.Item2.ToString("N0");
            }
        }
        
        public void OnUpgradePressed()
        {
            GameLogic.UpgradeManager.UpgradeItemByID(UpgradeablesTypeID.ClickPowerUpgrade);
        }
    }
}
