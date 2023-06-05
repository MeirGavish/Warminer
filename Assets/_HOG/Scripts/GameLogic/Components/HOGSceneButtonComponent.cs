
using System;
using DG.Tweening;
using HOG.Core;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

namespace  HOG.GameLogic
{
    public class HOGSceneButtonComponent : HOGLogicMonoBehaviour
    {
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
