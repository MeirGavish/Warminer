using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOG.Core;

namespace HOG.GameLogic
{
    public class HOGEnemySpawnerComponent : HOGLogicMonoBehaviour
    {
        [SerializeField]
        private GameObject PlayerInstance;

        private List<HOGWave> wavesData;
        private int currWaveIdx;
        private int enemiesLeftThisWave;

        private bool SpawningEnemies = true;
        

        private void Awake()
        {
            currWaveIdx = 0;
            wavesData = GameLogic.WaveManager.GetWavesData();
            enemiesLeftThisWave = wavesData[currWaveIdx].NumEnemies;
            Manager.PoolManager.InitPool("Enemy", 20);
        }

        // Start is called before the first frame update
        void Start()
        {
            PlayerInstance = HOGUtils.GameObjectFindWithTagSingular("player");
            StartCoroutine(SpawnEnemies());
        }

        IEnumerator SpawnEnemies()
        {
            while (SpawningEnemies)
            {
                if (enemiesLeftThisWave <= 0)
                {
                    if (currWaveIdx < wavesData.Count)
                    {
                        // Last wave is respawned indefinitely
                        currWaveIdx++;
                    }

                    enemiesLeftThisWave = wavesData[currWaveIdx].NumEnemies;
                }

                yield return new WaitForSeconds(wavesData[currWaveIdx].SpawnInterval);

                float distanceToPlayer = Vector2.Distance(transform.position, PlayerInstance.transform.position);

                Vector2 spawnLocationOffset = HOGUtils.Vector2FromMagnitudeAngle(distanceToPlayer, Random.Range(0, 2 * Mathf.PI));
                Vector2 spawnLocation = (Vector2)PlayerInstance.transform.position + spawnLocationOffset;
                Vector2 enemyDirection = (Vector2)PlayerInstance.transform.position - spawnLocation;
                
                Quaternion enemyRotation = Quaternion.LookRotation(Vector3.forward, enemyDirection);

                var spawnedEnemy = (HOGEnemyControllerComponent)Manager.PoolManager.GetPoolable(PoolNames.EnemyPool, spawnLocation, enemyRotation);

                spawnedEnemy.StartMovement();
                InvokeEvent(HOGEventNames.OnEnemySpawned, spawnedEnemy.gameObject);

                enemiesLeftThisWave--;
            }
        }
    }
}

