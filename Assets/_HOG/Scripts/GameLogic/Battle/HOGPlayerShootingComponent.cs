using HOG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGPlayerShootingComponent : HOGLogicMonoBehaviour
    {
        [SerializeField] protected GameObject ProjectileSpawn;

        [SerializeField]  protected float StartShootingDelay = 0;

        // TODO: Should be set from weapon/config
        [SerializeField] protected float FireRate = 1;

        [SerializeField] protected HOGRangeComponent rangeComponent;

        protected GameObject Target = null;

        protected bool isShooting = true;

        private readonly Queue<GameObject> enemyQueue = new();

        void Awake()
        {
            rangeComponent.OnEnterRange = OnEnemyEnterRange;
            Manager.PoolManager.InitPool("PlayerProjectile", 10);
        }

        private void OnEnable()
        {
            AddListener(Core.HOGEventNames.OnEntityKilled, OnEnemyDestroyed);
        }

        private void OnDisable()
        {
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
            var spawnedProjectile = (HOGProjectileComponent)Manager.PoolManager.GetPoolable
            (
                PoolNames.PlayerProjectilePool,
                ProjectileSpawn.transform.position,
                ProjectileSpawn.transform.rotation
            );

            var damageUpgradeData = GameLogic.UpgradeManager.GetUpgradeableByID(UpgradeablesTypeID.DamageUpgrade);

            spawnedProjectile.Damage = (int)GameLogic.UpgradeManager.GetPowerByIDAndLevel
            (
                UpgradeablesTypeID.DamageUpgrade, 
                damageUpgradeData.CurrentLevel
            );

            spawnedProjectile.StartMovement();
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
            if (StartShootingDelay > 0)
            {
                yield return new WaitForSeconds(StartShootingDelay);
            }

            while (isShooting)
            {
                if (Target != null)
                {
                    Shoot();
                }

                yield return new WaitForSeconds(1.0f/FireRate);
            }
        }

        void OnEnemyEnterRange(GameObject spawnedEnemy)
        {
            if (!spawnedEnemy.CompareTag("Enemy"))
            {
                return;
            }

            enemyQueue.Enqueue(spawnedEnemy);

            if (Target == null)
            {
                enemyQueue.Dequeue();
                SetTarget(spawnedEnemy);
            }
        }

        void OnEnemyDestroyed(object DestroyedEntity)
        {
            GameObject DestroyedEntityGO = (GameObject)DestroyedEntity;
            if (!DestroyedEntityGO.CompareTag("Enemy"))
            {
                return;
            }

            enemyQueue.TryDequeue(out GameObject newTarget);
            SetTarget(newTarget);
        }
    }
}