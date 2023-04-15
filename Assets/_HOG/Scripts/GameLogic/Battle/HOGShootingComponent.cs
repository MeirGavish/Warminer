using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGShootingComponent : HOGLogicMonoBehaviour
    {
        [SerializeField]
        protected GameObject ProjectileSpawn;

        [SerializeField]
        protected GameObject ProjectileObject;

        [SerializeField]
        protected float StartShootingDelay = 1;

        // TODO: Should be set from weapon/config
        [SerializeField]
        protected float ShootingInterval = 1;

        private GameObject _target = null;
        public GameObject Target 
        { 
            get => _target;

            // TODO: Is this okay to do as a property setter?
            set 
            {
                _target = value;
                if (_target == null)
                {
                    return;
                }

                Vector2 playerToTarget = _target.transform.position - transform.position;

                // TODO: Tween
                transform.rotation = Quaternion.LookRotation(Vector3.forward, playerToTarget);
            }
        }

        protected bool isShooting = true;

        // TODO: Should be used as listener actions
        protected void StartShooting()
        {
            isShooting = true;
        }

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
            // TODO: Use pooling
            Instantiate(ProjectileObject, 
                        ProjectileSpawn.transform.position,
                        ProjectileSpawn.transform.rotation);
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
    }

}
