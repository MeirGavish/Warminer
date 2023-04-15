using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGPlayerTargetingComponent : HOGLogicMonoBehaviour
    {
        private Queue<GameObject> enemyQueue = new();
        private HOGShootingComponent shootingComp;

        private void Awake()
        {
            AddListener(Core.HOGEventNames.OnEnemySpawned, OnEnemySpawned);
            AddListener(Core.HOGEventNames.OnEntityKilled, OnEnemyDestroyed);
        }

        private void Start()
        {
            shootingComp = GetComponent<HOGShootingComponent>();
        }

        void OnEnemySpawned(object spawnedEnemy)
        {
            GameObject spawnedEnemyGO = (GameObject)spawnedEnemy;
            enemyQueue.Enqueue(spawnedEnemyGO);

            if (shootingComp.Target == null)
            {
                enemyQueue.Dequeue();
                shootingComp.Target = spawnedEnemyGO;
            }
        }

        void OnEnemyDestroyed(object DestroyedEntity)
        {
            GameObject DestroyedEntityGO = (GameObject)DestroyedEntity; 
            if (DestroyedEntityGO.tag == "Enemy")
            {
                shootingComp.Target = enemyQueue.Dequeue();
            }
        }

        private void OnDisable()
        {
            RemoveListener(Core.HOGEventNames.OnEnemySpawned, OnEnemySpawned);
            RemoveListener(Core.HOGEventNames.OnEntityKilled, OnEnemyDestroyed);
        }
    }
}

