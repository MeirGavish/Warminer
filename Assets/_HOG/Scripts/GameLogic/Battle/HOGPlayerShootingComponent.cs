using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGPlayerShootingComponent : HOGShootingComponent
    {
        private Queue<GameObject> enemyQueue = new();

        private void Awake()
        {
            AddListener(Core.HOGEventNames.OnEnemySpawned, OnEnemySpawned);
        }

        

        void OnEnemySpawned(object spawnedEnemy)
        {
            GameObject spawnedEnemyGO = (GameObject)spawnedEnemy;
            enemyQueue.Enqueue(spawnedEnemyGO);

            if (target == null)
            {
                SetTarget(spawnedEnemyGO);
            }
        }


        void OnEnemyDestroyed(object notUsed)
        {
            GameObject nextTarget = null;
            if (!enemyQueue.TryDequeue(out nextTarget))
            {
                target = null;
                return;
            }

            SetTarget(nextTarget);
        }
    }
}

