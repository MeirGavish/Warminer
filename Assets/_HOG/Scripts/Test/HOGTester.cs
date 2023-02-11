using System;
using HOG.Core;
using UnityEngine;

namespace HOG.Test
{
    public class HOGTester : HOGMonoBehaviour
    {
        private void Start()
        {
            
            AddListener(HOGEventNames.OnGameStart, OnGameStart);
        }

        private void OnDestroy()
        {
            RemoveListener(HOGEventNames.OnGameStart, OnGameStart);
        }

        private void OnGameStart(object obj)
        {
            //Do something
        }
    }

    public class HOGGameUI : MonoBehaviour
    {
        //Will be called from UI Button
        public void OnStartPressed()
        {
            HOGGameLogic.TryStartGame();
        }
    }

    //Will be real system
    public static class HOGGameLogic
    {
        public static bool isGameRunning = false;
        public static void TryStartGame()
        {
            if (isGameRunning)
            {
                return;
            }

            isGameRunning = true;
            
            HOGManager.Instance.EventsManager.InvokeEvent(HOGEventNames.OnGameStart, null);
        }
    }
}