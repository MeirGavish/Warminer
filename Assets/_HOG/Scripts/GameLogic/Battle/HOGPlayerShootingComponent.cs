using HOG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGPlayerShootingComponent : HOGLogicMonoBehaviour
    {
        [SerializeField] protected GameObject ProjectileSpawn;

        [SerializeField] protected GameObject ProjectileObject;

        [SerializeField]  protected float StartShootingDelay = 1;

        // TODO: Should be set from weapon/config
        [SerializeField] protected float ShootingInterval = 1;

        [SerializeField]  protected float range = 1;

        protected GameObject Target = null;

        protected bool isShooting = true;

        private readonly Queue<GameObject> enemyQueue = new();

        protected HOGUpgradeableData damageUpgradeData;
        protected HOGProjectileComponent projectileComponent;

        void Awake()
        {
            AddListener(Core.HOGEventNames.OnEnemySpawned, OnEnemySpawned);
            AddListener(Core.HOGEventNames.OnEntityKilled, OnEnemyDestroyed);

            damageUpgradeData = GameLogic.UpgradeManager.GetUpgradeableByID(UpgradeablesTypeID.DamageUpgrade);

            int projectileDamage = GameLogic.UpgradeManager.GetPowerByIDAndLevel(UpgradeablesTypeID.DamageUpgrade, damageUpgradeData.CurrentLevel);
            projectileComponent = ProjectileObject.GetComponent<HOGProjectileComponent>();
            projectileComponent.Damage = projectileDamage;

            Manager.PoolManager.InitPool("PlayerProjectile", 30);
        }

        private void OnDisable()
        {
            RemoveListener(Core.HOGEventNames.OnEnemySpawned, OnEnemySpawned);
            RemoveListener(Core.HOGEventNames.OnEntityKilled, OnEnemyDestroyed);
        }

        // Note that after calling StopShooting(), setting isShooting to true will not make it shoot.
        // TODO: It is mainly so there isn't a while(true) in the shooting coroutine... Maybe fix?
        // Also should be used as listener actions?

        protected void StopShooting()
        {
            isShooting = false;
        }

        private void Start()
        {
            StartCoroutine(ShootingCoroutine());
        }

        void Shoot()
        {
            HOGPoolable spawnedProjectile = Manager.PoolManager.GetPoolable(PoolNames.PlayerProjectilePool);
            // TODO: Use pooling
            spawnedProjectile.transform.position = ProjectileSpawn.transform.position;
            spawnedProjectile.transform.rotation = ProjectileSpawn.transform.rotation;
        }

        protected void SetTarget(GameObject newTarget)
        {
            Target = newTarget;
            if (newTarget != null)
            {
                Vector2 playerToTarget = Target.transform.position - transform.position;

                // TODO: Tween
                transform.rotation = Quaternion.LookRotation(Vector3.forward, playerToTarget);
            }
        }

        IEnumerator ShootingCoroutine()
        {
            yield return new WaitForSeconds(StartShootingDelay);

            while (isShooting)
            {
                if (Target != null)
                {
                    Shoot();
                }

                yield return new WaitForSeconds(ShootingInterval);
            }
        }

        void OnEnemySpawned(object spawnedEnemy)
        {
            GameObject spawnedEnemyGO = (GameObject)spawnedEnemy;
            enemyQueue.Enqueue(spawnedEnemyGO);

            if (Target == null)
            {
                enemyQueue.Dequeue();
                SetTarget(spawnedEnemyGO);
            }
        }

        void OnEnemyDestroyed(object DestroyedEntity)
        {
            GameObject DestroyedEntityGO = (GameObject)DestroyedEntity;
            if (DestroyedEntityGO.CompareTag("Enemy"))
            {
                GameObject newTarget;
                enemyQueue.TryDequeue(out newTarget);
                SetTarget(newTarget);
            }
        }
    }
}