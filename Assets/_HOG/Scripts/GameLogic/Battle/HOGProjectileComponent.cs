using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGProjectileComponent : HOGLogicMonoBehaviour
    {
        [SerializeField]
        protected string[] damagableTags;

        [SerializeField]
        protected int damage;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject collidedObject = collision.gameObject;
            if (true /*damagableTags.(collidedObject.tag)*/)
            {
                collidedObject.GetComponent<HOGHealthComponent>().DealDamage(damage);
            }
        }
    }
}

