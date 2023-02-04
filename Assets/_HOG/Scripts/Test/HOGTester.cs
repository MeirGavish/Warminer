using System;
using HOG.Core;
using UnityEngine;

namespace HOG.Test
{
    public class HOGTester : HOGMonoBehaviour
    {
        private void Start()
        {
            
            AddListener("game_start_event", OnGameStart);
        }

        private void OnDestroy()
        {
            RemoveListener("game_start_event", OnGameStart);
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
            
            HOGManager.Instance.EventsManager.InvokeEvent("game_start_event", null);
        }
    }
}