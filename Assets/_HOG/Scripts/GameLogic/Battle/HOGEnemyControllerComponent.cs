
using UnityEngine;
using HOG.Core;

namespace HOG.GameLogic
{
    public class HOGEnemyControllerComponent : HOGLogicMonoBehaviour
    {
        private HOGMovementComponent movementComponent;
        private HOGHealthComponent healthComponent;

        // TODO: From config?
        [SerializeField] private int coinValue = 3;

        void Awake()
        {
            movementComponent = GetComponent<HOGMovementComponent>();
            healthComponent = GetComponent<HOGHealthComponent>();
            healthComponent.OnDeathAction = OnDeathAction;

            Manager.PoolManager.InitPool("TextToast", 15);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("player"))
            {
                movementComponent.StopMovement();
            }
        }
        
        private void OnDeathAction(GameObject thisEnemyUnused)
        {
            GameLogic.CurrencyManager.ChangeCurrencyByAmountByType(CurrencyTypes.CoinsCurrency, coinValue);

            var scoreText = (HOGTweenScoreComponent)Manager.PoolManager.GetPoolable(PoolNames.ScoreToast);
            scoreText.transform.position = transform.position + Vector3.forward * 5;
            scoreText.Init(coinValue);
        }
    }
}

