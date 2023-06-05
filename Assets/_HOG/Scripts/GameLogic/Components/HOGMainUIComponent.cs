
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
        public void OnUpgradeClickPowerPressed()
        {
            GameLogic.UpgradeManager.UpgradeItemByID(UpgradeablesTypeID.ClickPowerUpgrade);
        }

        public void OnUpgradeDamagePressed()
        {
            GameLogic.UpgradeManager.UpgradeItemByID(UpgradeablesTypeID.DamageUpgrade);
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
