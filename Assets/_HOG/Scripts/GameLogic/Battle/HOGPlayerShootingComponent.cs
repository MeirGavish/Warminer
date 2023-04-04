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
            AddListener(Core.HOGEventNames.OnEntityKilled, OnEnemyDestroyed);
        }

        void OnEnemySpawned(object spawnedEnemy)
        {
            GameObject spawnedEnemyGO = (GameObject)spawnedEnemy;
            enemyQueue.Enqueue(spawnedEnemyGO);

            if (target == null)
            {
                enemyQueue.Dequeue();
                SetTarget(spawnedEnemyGO);
            }
        }

        void OnEnemyDestroyed(object DestroyedEntity)
        {
            GameObject DestroyedEntityGO = (GameObject)DestroyedEntity; 
            if (DestroyedEntityGO.tag == "Enemy")
            {
                SetTarget(enemyQueue.Dequeue());
            }
        }
    }
}

