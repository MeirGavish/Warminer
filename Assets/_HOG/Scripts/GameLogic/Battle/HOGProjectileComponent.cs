using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using HOG.Core;

namespace HOG.GameLogic
{
    public class HOGProjectileComponent : HOGPoolable
    {
        [SerializeField] protected string[] damagableTags;

        [SerializeField] protected HOGMovementComponent movementComponent;

        public int Damage;

        // TODO: Change to layer collision system
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (damagableTags.Contains(collision.gameObject.tag))
            {
                var healthComponent = collision.gameObject.GetComponent<HOGHealthComponent>();
                healthComponent.DealDamage(Damage);
                Manager.PoolManager.ReturnPoolable(this);
            }
        }

        public void StartMovement()
        {
            movementComponent.StartMovement();
        }

        public void StopMovement()
        {
            movementComponent.StopMovement();
        }
    }
}

