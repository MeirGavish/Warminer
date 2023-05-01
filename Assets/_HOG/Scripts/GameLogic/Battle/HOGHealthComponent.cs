using UnityEngine;
using System;
using UnityEngine.UI;

namespace HOG.GameLogic
{
    public class HOGHealthComponent : HOGLogicMonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] Image healthBar;

        private int _health = 100;

        private int Health
        {
            get => _health;
            set
            {
                _health = value;
                if (healthBar != null)
                {
                    healthBar.fillAmount = (float)_health / _maxHealth;
                }
            }
        }

        // For use within the component
        [field: SerializeField ]public Action<GameObject> OnDeathAction { get; set; } = null;

        public void Init(int maxHealth)
        {
            _maxHealth = maxHealth;
        }

        public void Reset()
        {
            Health = _maxHealth;
        }

        public void DealDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                InvokeEvent(Core.HOGEventNames.OnEntityKilled, gameObject);
                OnDeathAction?.Invoke(gameObject);
            }
        }
    }
}

