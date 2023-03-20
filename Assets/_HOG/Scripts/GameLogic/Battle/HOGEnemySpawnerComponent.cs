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

        // TODO: Visible GameObject?
        [SerializeField]
        private float SpawnDistance;

        [SerializeField]
        private GameObject EnemyGameObject;

        [SerializeField]
        private GameObject PlayerInstance;

        private bool SpawningEnemies;

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
            yield return new WaitForSeconds(WaitBeforeSpawnStart);

            while (SpawningEnemies)
            {
                yield return new WaitForSeconds(SpawnInterval);

                Vector2 spawnLocation = Random.onUnitSphere * SpawnDistance;
                Vector2 enemyDirection = (Vector2)PlayerInstance.transform.position - spawnLocation;
                Quaternion enemyRotation = Quaternion.LookRotation(enemyDirection, Vector3.forward);

                // TODO: Use pooling
                Instantiate(EnemyGameObject, spawnLocation, enemyRotation);

                InvokeEvent(HOGEventNames.OnEnemySpawned, EnemyGameObject);
            }
        }
    }
}

