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

        [SerializeField]
        protected int damage;

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject collidedObject = collision.gameObject;
            if (damagableTags.Contains(collidedObject.tag))
            {
                collidedObject.GetComponent<HOGHealthComponent>().DealDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}

