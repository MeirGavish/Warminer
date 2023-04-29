using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HOG.GameLogic
{
    public class HOGHealthComponent : HOGLogicMonoBehaviour
    {
        [SerializeField] private int _initHealth = 100;

        private int _health = 100;

        // For use within the component
        public Action<GameObject> OnDeathAction { get; set; } = null;

        public void Init(int initHealth)
        {
            _initHealth = initHealth;
        }

        public void Reset()
        {
            _health = _initHealth;
        }

        public void DealDamage(int damage)
        {
            _health -= damage;
            if (_health < 0)
            {
                InvokeEvent(Core.HOGEventNames.OnEntityKilled, gameObject);
                OnDeathAction?.Invoke(gameObject);
            }
        }
    }
}

