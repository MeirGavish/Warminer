using System;
using System.Collections.Generic;
using HOG.Core;
using UnityEngine;

namespace HOG.Test
{
    public class HOGTester : HOGMonoBehaviour
    {
        private Queue<HOGPoolable> poolsables = new();

        
        private void Start()
        {
           DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
        }

        private void OnGameStart(object obj)
        {
            //Do something
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var triangle = Manager.PoolManager.GetPoolable(PoolNames.TrianglePool);
                poolsables.Enqueue(triangle);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                var triangle = poolsables.Dequeue();
                Manager.PoolManager.ReturnPoolable(triangle);
            }

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