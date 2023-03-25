
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGEnemyControllerComponent : HOGLogicMonoBehaviour
    {

        [SerializeField]
        private HOGMovementComponent MovementComponent;

        void Start()
        {
            MovementComponent = GetComponent<HOGMovementComponent>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "player")
            {
                MovementComponent.StopMovement();
            }
        }
    }
}

