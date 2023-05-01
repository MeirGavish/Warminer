using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOG.Core;

namespace HOG.GameLogic
{
    public class HOGEnemySpawnerComponent : HOGLogicMonoBehaviour
    {
        [SerializeField]
        private float SpawnInterval;

        [SerializeField]
        private float WaitBeforeSpawnStart;

        [SerializeField]
        private GameObject EnemyGameObject;

        [SerializeField]
        private GameObject PlayerInstance;

        private bool SpawningEnemies = true;

        private void Awake()
        {
            Manager.PoolManager.InitPool("Enemy", 40);
        }

        // Start is called before the first frame update
        void Start()
        {
            PlayerInstance = HOGUtils.GameObjectFindWithTagSingular("player");
            StartCoroutine(SpawnEnemies());
        }

        IEnumerator SpawnEnemies()
        {
            // TODO: Use timing system...
            yield return new WaitForSeconds(WaitBeforeSpawnStart);

            while (SpawningEnemies)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, PlayerInstance.transform.position);

                Vector2 spawnLocationOffset = HOGUtils.Vector2FromMagnitudeAngle(distanceToPlayer, Random.Range(0, 2 * Mathf.PI));
                Vector2 spawnLocation = (Vector2)PlayerInstance.transform.position + spawnLocationOffset;
                Vector2 enemyDirection = (Vector2)PlayerInstance.transform.position - spawnLocation;
                
                Quaternion enemyRotation = Quaternion.LookRotation(Vector3.forward, enemyDirection);

                HOGPoolable spawnedEnemyPoolable = Manager.PoolManager.GetPoolable(PoolNames.EnemyPool);
                spawnedEnemyPoolable.transform.position = spawnLocation;
                spawnedEnemyPoolable.transform.rotation = enemyRotation;

                InvokeEvent(HOGEventNames.OnEnemySpawned, spawnedEnemyPoolable.gameObject);

                yield return new WaitForSeconds(SpawnInterval);
            }
        }
    }
}

