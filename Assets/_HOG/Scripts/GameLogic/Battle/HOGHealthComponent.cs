using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HOG.GameLogic
{
    public class HOGHealthComponent : HOGLogicMonoBehaviour
    {
        [field: SerializeField]
        public int Health { get; protected set; } = 100;

        // For use within the component
        public Action<GameObject> OnDeathAction { get; set; } = null;

        public void DealDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                InvokeEvent(Core.HOGEventNames.OnEntityKilled, gameObject);
                OnDeathAction?.Invoke(gameObject);
                Destroy(gameObject);
            }
        }
    }
}

