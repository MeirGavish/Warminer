using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGHealthComponent : HOGLogicMonoBehaviour
    {
        // TODO: Make sure fields are consistently capitalized...
        [SerializeField]
        private int health = 100;
        
        public void DealDamage(int damage)
        {
            health -= damage;
            if (health < 0)
            {
                InvokeEvent(Core.HOGEventNames.OnEntityKilled, gameObject);
                Destroy(gameObject);
            }
        }
    }
}

