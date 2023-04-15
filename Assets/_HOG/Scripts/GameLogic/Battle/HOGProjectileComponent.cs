using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace HOG.GameLogic
{
    public class HOGProjectileComponent : HOGLogicMonoBehaviour
    {
        [SerializeField]
        protected string[] damagableTags;

        [field: SerializeField]
        public int Damage { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var projectileComponent = gameObject.GetComponent<HOGProjectileComponent>();
            if (damagableTags.Contains(collision.gameObject.tag))
            {
                var healthComponent = collision.gameObject.GetComponent<HOGHealthComponent>();
                Destroy(gameObject);
                healthComponent.DealDamage(projectileComponent.Damage);
            }
        }
    }
}

