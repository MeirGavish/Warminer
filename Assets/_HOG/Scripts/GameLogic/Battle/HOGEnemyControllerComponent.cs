
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGEnemyControllerComponent : HOGLogicMonoBehaviour
    {

        [SerializeField]
        private HOGMovementComponent MovementComponent;

        [SerializeField]
        private int Health = 100;

        void Start()
        {
            MovementComponent = GetComponent<HOGMovementComponent>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "player")
            {
                MovementComponent.StopMovement();
            }
            //else if (collision.gameObject.tag == "PlayerProjectile")
            //{
            //    collision.gameObject.GetComponent<HOGProjectileComponent>();
            //    ga
            //}
        }
    }
}

