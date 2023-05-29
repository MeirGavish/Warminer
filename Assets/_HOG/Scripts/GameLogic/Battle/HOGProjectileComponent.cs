using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using HOG.Core;

namespace HOG.GameLogic
{
    public class HOGProjectileComponent : HOGPoolable
    {
        [SerializeField]
        protected string[] damagableTags;

        // TODO: Fix
        [field: SerializeField]
        public int Damage { get; set; }

        // TODO: Change to layer collision system
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var projectileComponent = gameObject.GetComponent<HOGProjectileComponent>();
            if (damagableTags.Contains(collision.gameObject.tag))
            {
                var healthComponent = collision.gameObject.GetComponent<HOGHealthComponent>();
                healthComponent.DealDamage(projectileComponent.Damage);
                Manager.PoolManager.ReturnPoolable(this);
            }
        }
    }
}

